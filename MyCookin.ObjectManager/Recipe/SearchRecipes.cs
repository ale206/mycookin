using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Data;
using System.Net;
using System.IO;
using System.Text;
using MyCookin.Common;
using MyCookin.ObjectManager.IngredientManager;
using MyCookin.DAL.Recipe.ds_RecipeTableAdapters;

namespace MyCookin.ObjectManager.RecipeManager
{
    public class SearchRecipes
    {
        #region PrivateFileds

        private string _SearchQuery;
        private bool _Vegan;
        private bool _Vegetarian;
        private bool _GlutenFree;
        private int _quickRecipe;
        private double _lightRecipe;
        private bool _Mix;
        private int _ItemToDispaly;
        private int _RowOffSet;
        private List<Recipe> _RecipesFound;
        private bool _FrigoMixOK;
        private bool _IsInError;
        private string _ErrorCode;
        string[] _ingrArrayList;
        int _IDLanguage;

        #endregion

        #region PublicProperties

        public string SearchQuery
        {
            get { return _SearchQuery; }
        }

        public bool Vegan
        {
            get { return _Vegan; }
        }

        public bool Vegetarian
        {
            get { return _Vegetarian; }
        }

        public bool GlutenFree
        {
            get { return _GlutenFree; }
        }

        public bool FrigoMix
        {
            get { return _Mix; }
        }

        public int ItemToDispaly
        {
            get { return _ItemToDispaly; }
            set { _ItemToDispaly = value; }
        }

        public int RowOffSet
        {
            get { return _RowOffSet; }
            set { _RowOffSet = value; }
        }

        //public List<Recipe> RecipesFound
        //{
        //    get { return _RecipesFound; }
        //}

        public bool FrigoMixOK
        {
            get { return _FrigoMixOK; }
        }

        public bool IsInError
        {
            get { return _IsInError; }
        }

        public string ErrorCode
        {
            get { return _ErrorCode; }
        }

        #endregion

        #region Costructors

        public SearchRecipes(string query, bool vegan, bool vegetarian, bool glutenFree, int quickRecipe, double lightRecipe, bool frigoMix, int numItem, int rowOffSet, int IDLanguage)
        {
            _SearchQuery = query;
            _Vegan = vegan;
            _Vegetarian = vegetarian;
            _GlutenFree = glutenFree;
            _quickRecipe = quickRecipe;
            _lightRecipe = lightRecipe;
            _Mix = frigoMix;
            _ItemToDispaly = numItem;
            _RowOffSet = rowOffSet;
            _IDLanguage = IDLanguage;

            if (_Mix)
            {
                _FrigoMixOK = true;
                char ingredientSeparator = ' ';
                int countComma = _SearchQuery.Count(c => c == ',');
                int countSemicolon = _SearchQuery.Count(c => c == ';');


                if (countComma > 1 || countSemicolon > 1)
                {
                    if (countComma > countSemicolon)
                    {
                        ingredientSeparator = ',';
                    }
                    else
                    {
                        ingredientSeparator = ';';
                    }

                    _ingrArrayList = _SearchQuery.Split(ingredientSeparator);
                }
                else
                {
                    _FrigoMixOK = false;
                }
            }
        }

        #endregion

        #region Methods

        public DataTable Find()
        {
            DataTable dtRecipes = null;
            GetRecipesDAL RecipeDAL = new GetRecipesDAL();
            
            if (!_Mix)
            {
                
                dtRecipes = RecipeDAL.USP_SearchRecipe(_SearchQuery,_IDLanguage, _Vegan, _Vegetarian, _GlutenFree, _lightRecipe, _quickRecipe, _RowOffSet, _ItemToDispaly);
                if (dtRecipes.Rows.Count == 0)
                {
                    try
                    {
                        _SearchQuery = GetSuggestion(_SearchQuery);

                        dtRecipes = RecipeDAL.USP_SearchRecipe(_SearchQuery, _IDLanguage, _Vegan, _Vegetarian, _GlutenFree, _lightRecipe, _quickRecipe, _RowOffSet, _ItemToDispaly);
                    }
                    catch
                    {
                    }
                }
            }
            else if (_Mix && _FrigoMixOK)
            {
                string _CompleteIngredientList = ""; ;
                string _suggestedName = "";

                foreach (string _ingrName in _ingrArrayList)
                {
                    //IngredientLanguage IngrLang = new IngredientLanguage(_ingrName, _IDLanguage, true);
                    //if (IngrLang.IDIngredient != null || IngrLang.IDIngredient != new Guid())
                    //{
                    //    _IDIngredientList += IngrLang.IDIngredient.ToString() + ",";
                    //}
                    //else
                    //{
                    //    //non va bene, bisogna fare una ricerca più generica e farsi tornare tutti gli id
                    //    // degli ingredienti che contengono la parola inserita 2 grandi balle!!!!
                    //    IngrLang = new IngredientLanguage(GetSuggestion(_ingrName), _IDLanguage, true);

                    //    if (IngrLang.IDIngredient != null || IngrLang.IDIngredient != new Guid())
                    //    {
                    //        _IDIngredientList += IngrLang.IDIngredient.ToString() + ",";
                    //    }
                    //}
                    _CompleteIngredientList += _ingrName.Trim() + ',';
                    if (MyConvert.ToBoolean(AppConfig.GetValue("UseGoogleSuggestionsForFreeFridge", AppDomain.CurrentDomain), false))
                    {
                        _suggestedName = GetSuggestion(_ingrName.Trim());
                    }

                    if(!String.IsNullOrEmpty(_suggestedName))
                    {
                        _CompleteIngredientList += _suggestedName.Trim() +',';
                    }

                }

                _CompleteIngredientList = _CompleteIngredientList.Substring(0, _CompleteIngredientList.Length - 1);

                try
                {
                    dtRecipes = RecipeDAL.USP_SearchFreeFridgeRecipe(_CompleteIngredientList, _Vegan, _Vegetarian, _GlutenFree, _lightRecipe, _quickRecipe, _RowOffSet, _ItemToDispaly);
                }
                catch
                {
                }
            }
            else
            {
                dtRecipes = RecipeDAL.USP_SearchRecipe(_SearchQuery, _IDLanguage, _Vegan, _Vegetarian, _GlutenFree, _lightRecipe, _quickRecipe, _RowOffSet, _ItemToDispaly);
                if (dtRecipes.Rows.Count == 0)
                {
                    try
                    {
                        _SearchQuery = GetSuggestion(_SearchQuery);

                        RecipeDAL.USP_SearchRecipe(_SearchQuery, _IDLanguage, _Vegan, _Vegetarian, _GlutenFree, _lightRecipe, _quickRecipe, _RowOffSet, _ItemToDispaly);
                    }
                    catch
                    {
                    }
                }
            }
            
            return dtRecipes;
        }

        private static string GetSuggestion(string words)
        {
            string _return = "";
            if (MyConvert.ToBoolean(AppConfig.GetValue("TurnOnUseGoogleSuggestions", AppDomain.CurrentDomain), false))
            {
                try
                {
                    string _returnValue = "";
                    string _uri = "";
                    try
                    {
                        _uri = AppConfig.GetValue("GoogleSuggestURI", AppDomain.CurrentDomain) + words;
                    }
                    catch
                    {
                        _uri = "http://www.google.com/complete/search?output=toolbar&q=" + words;
                    }
                    HttpWebRequest _request = (HttpWebRequest)WebRequest.Create(_uri);
                    _request.Timeout = 1000;
                    HttpWebResponse _responce = (HttpWebResponse)_request.GetResponse();
                    using (StreamReader _sr = new StreamReader(_responce.GetResponseStream()))
                    {
                        _returnValue = _sr.ReadToEnd();
                    }

                    XDocument _doc = XDocument.Parse(_returnValue);
                    XAttribute _attr = _doc.Root.Element("CompleteSuggestion").Element("suggestion").Attribute("data");
                    _return = _attr.Value;
                }
                catch
                {

                }
            }
            return _return;
        }

        #endregion
    }
}
