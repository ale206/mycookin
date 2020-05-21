using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Web;
using MyCookin.ErrorAndMessage;
using MyCookin.ObjectManager.UserManager;
using MyCookin.ObjectManager.MediaManager;
using MyCookin.DAL.Recipe.ds_RecipeTableAdapters;
using MyCookin.Common;
using MyCookin.ObjectManager;
using MyCookin.ObjectManager.IngredientManager;

namespace MyCookin.ObjectManager.RecipeManager
{
    /// <summary>
    /// Manage the recipe difficult level Enum Class container
    /// </summary>
    public class RecipeInfo
    {
        /// <summary>
        /// List possible Recipe difficult level
        /// </summary>
        public enum Difficulties : int
        {
            None = 0,
            Easy = 1,
            Medium = 2,
            Difficult = 3
        }
        /// <summary>
        /// Get a language value for the RecipeDifficulties.Difficulties Enumeration
        /// </summary>
        /// <param name="value">A value of RecipeDifficulties.Difficulties enum</param>
        /// <param name="IDLanguage">int IDLanguage</param>
        /// <returns>string language value for the RecipeDifficulties.Difficulties</returns>
        public static string RecipeDifficultiesLang(Difficulties value, int IDLanguage)
        {
            string _return = "";
            switch (value)
            {
                case Difficulties.None:
                    _return = RetrieveMessage.RetrieveDBMessage(IDLanguage, "RC-EN-0000");
                    break;
                case Difficulties.Easy:
                    _return= RetrieveMessage.RetrieveDBMessage(IDLanguage, "RC-EN-0001");
                    break;
                case Difficulties.Medium:
                    _return = RetrieveMessage.RetrieveDBMessage(IDLanguage, "RC-EN-0002");
                    break;
                case Difficulties.Difficult:
                    _return = RetrieveMessage.RetrieveDBMessage(IDLanguage, "RC-EN-0003");
                    break;
                default:
                    _return= "";
                    break;
            }

            return _return;
            
        }

        public static DataTable GetAllRecipeDifficultiesLang(int IDLanguage)
        {
            DataTable _return = new DataTable();

            _return.Columns.Add("value",typeof(int));
            _return.Columns.Add("viewText",typeof(string));

            foreach (int value in Enum.GetValues(typeof(Difficulties)))
            {
                if (value > 0)
                {
                    _return.Rows.Add(value, RecipeDifficultiesLang((Difficulties)value, IDLanguage));
                }
            }
            
            return _return;
        }

        /// <summary>
        /// Ingredient Relevance into a recipe
        /// </summary>
        public enum IngredientRelevances : int
        {
            OptionalIngredient = 1,
            //AromaticIngredient = 2,
            ImportantIngredient = 3,
            KeyIngredient = 4
        }

        /// <summary>
        /// Get Ingredient Relevance Language
        /// </summary>
        /// <param name="value">A ingrediente relevance value</param>
        /// <param name="IDLanguage">ID of selected language</param>
        /// <returns></returns>
        public static string IngredientRelevancesLang(IngredientRelevances value, int IDLanguage)
        {
            string _return = "";
            switch (value)
            {
                case IngredientRelevances.OptionalIngredient:
                    _return = RetrieveMessage.RetrieveDBMessage(IDLanguage, "RC-EN-0004");
                    break;
                //case IngredientRelevances.AromaticIngredient:
                //    _return = RetrieveMessage.RetrieveDBMessage(IDLanguage, "RC-EN-0005");
                //    break;
                case IngredientRelevances.ImportantIngredient:
                    _return = RetrieveMessage.RetrieveDBMessage(IDLanguage, "RC-EN-0006");
                    break;
                case IngredientRelevances.KeyIngredient:
                    _return = RetrieveMessage.RetrieveDBMessage(IDLanguage, "RC-EN-0007");
                    break;
                default:
                    _return = "";
                    break;
            }

            return _return;
        }

        public static DataTable GetAllIngredientRelevancesLang(int IDLanguage)
        {
            DataTable _return = new DataTable();

            _return.Columns.Add("value", typeof(int));
            _return.Columns.Add("viewText", typeof(string));

            foreach (int value in Enum.GetValues(typeof(IngredientRelevances)))
            {
                if (value > 0)
                {
                    _return.Rows.Add(value, IngredientRelevancesLang((IngredientRelevances)value, IDLanguage));
                }
            }

            return _return;
        }
    }

    /// <summary>
    /// Rapresente a Recipe without the language information
    /// </summary>
    public class Recipe
    {

        #region PrivateFileds

        protected Guid _IDRecipe;
        protected Recipe _IDRecipeFather;
        protected MyUser _IDOwner;
        protected int _NumberOfPerson;
        protected int _PreparationTimeMinute;
        protected int? _CookingTimeMinute;
        protected RecipeInfo.Difficulties _RecipeDifficulties;
        protected Photo _IDRecipeImage;
        protected Video _IDRecipeVideo;
        protected int? _IDCity;
        protected DateTime? _CreationDate;
        protected DateTime? _LastUpdate;
        protected MyUser _UpdatedByUser;
        protected int _RecipeConsulted;
        protected double _RecipeAvgRating;
        protected bool _isStarterRecipe;
        protected DateTime? _DeletedOn;
        protected bool _BaseRecipe;
        protected bool _RecipeEnabled;
        protected bool _Checked;
        protected int? _RecipeCompletePerc;
        protected RecipeIngrediets[] _RecipeIngredients;
        protected double? _RecipePortionKcal;
        protected double? _RecipePortionProteins;
        protected double? _RecipePortionFats;
        protected double? _RecipePortionCarbohydrates;
        protected double? _RecipePortionAlcohol;
        protected double? _RecipePortionQta;
        protected bool? _Vegetarian;
        protected bool? _Vegan;
        protected bool? _GlutenFree;
        protected bool? _HotSpicy;
        protected bool? _Draft;
        protected int? _RecipeRated;
        
        #endregion

        #region PublicProperties

        public Guid IDRecipe
        {
            get { return _IDRecipe; }
        }

        public Recipe RecipeFather
        {
            get { return _IDRecipeFather; }
            set { _IDRecipeFather = value; }
        }
        public MyUser Owner
        {
            get { return _IDOwner; }
            set { _IDOwner = value; }
        }
        public int NumberOfPerson
        {
            get { return _NumberOfPerson; }
            set { _NumberOfPerson = value; }
        }
        public int PreparationTimeMinute
        {
            get { return _PreparationTimeMinute; }
            set { _PreparationTimeMinute = value; }
        }
        public int? CookingTimeMinute
        {
            get { return _CookingTimeMinute ; }
            set { _CookingTimeMinute = value; }
        }
        public RecipeInfo.Difficulties RecipeDifficulties
        {
            get { return _RecipeDifficulties; }
            set { _RecipeDifficulties = value; }
        }
        public Photo RecipeImage
        {
            get { return _IDRecipeImage; }
            set { _IDRecipeImage = value; }
        }
        public Video RecipeVideo
        {
            get { return _IDRecipeVideo; }
            set { _IDRecipeVideo = value; }
        }
        public int? IDCity
        {
            get { return _IDCity; }
            set { _IDCity = value; }
        }
        public DateTime? CreationDate
        {
            get { return _CreationDate; }
            set { _CreationDate = value; }
        }
        public DateTime? LastUpdate
        {
            get { return _LastUpdate; }
            set { _LastUpdate = value; }
        }
        public MyUser UpdatedByUser
        {
            get { return _UpdatedByUser; }
            set { _UpdatedByUser = value; }
        }
        public int RecipeConsulted
        {
            get { return _RecipeConsulted; }
            set { _RecipeConsulted = value; }
        }
        public double RecipeAvgRating
        {
            get { return _RecipeAvgRating; }
            set { _RecipeAvgRating = value; }
        }
        public bool isStarterRecipe
        {
            get { return _isStarterRecipe; }
            set { _isStarterRecipe = value; }
        }
        public DateTime? DeletedOn
        {
            get { return _DeletedOn; }
            set { _DeletedOn = value; }
        }
        public bool BaseRecipe
        {
            get { return _BaseRecipe; }
            set { _BaseRecipe = value; }
        }
        public bool RecipeEnabled
        {
            get { return _RecipeEnabled; }
            set { _RecipeEnabled = value; }
        }
        public bool Checked
        {
            get { return _Checked; }
            set { _Checked = value; }
        }
        public int? RecipeCompletePerc
        {
            get { return _RecipeCompletePerc; }
            set { _RecipeCompletePerc = value; }
        }
        public RecipeIngrediets[] RecipeIngredients
        {
            get { return _RecipeIngredients; }
            set { _RecipeIngredients = value; }
        }
        public double? RecipePortionKcal
        {
            get { return _RecipePortionKcal; }
            set { _RecipePortionKcal = value; }
        }
        public double? RecipePortionProteins
        {
            get { return _RecipePortionProteins; }
            set { _RecipePortionProteins = value; }
        }
        public double? RecipePortionFats
        {
            get { return _RecipePortionFats; }
            set { _RecipePortionFats = value; }
        }
        public double? RecipePortionAlcohol
        {
            get { return _RecipePortionAlcohol; }
            set { _RecipePortionAlcohol = value; }
        }
        public double? RecipePortionCarbohydrates
        {
            get { return _RecipePortionCarbohydrates; }
            set { _RecipePortionCarbohydrates = value; }
        }
        public double? RecipePortionQta
        {
            get { return _RecipePortionQta; }
            set { _RecipePortionQta = value; }
        }
        public bool? Vegetarian
        {
            get { return _Vegetarian; }
            set { _Vegetarian = value; }
        }
        public bool? Vegan
        {
            get { return _Vegan; }
            set { _Vegan = value; }
        }
        public bool? GlutenFree
        {
            get { return _GlutenFree; }
            set { _GlutenFree = value; }
        }
        public bool? HotSpicy
        {
            get { return _HotSpicy; }
            set { _HotSpicy = value; }
        }
        public bool? Draft
        {
            get { return _Draft; }
            set { _Draft = value; }
        }
        public int? RecipeRated
        {
            get { return _RecipeRated; }
            set { _RecipeRated = value; }
        }

        #endregion

        #region Constructors

        protected Recipe()
        {
        }

        /// <summary>
        /// Istance of recipe without language info
        /// </summary>
        /// <param name="IDRecipe">Guid for recipe selected from database</param>
        public Recipe(Guid IDRecipe)
        {
            _IDRecipe = IDRecipe;
           // QueryRecipeInfo();
        }

        #endregion

        #region Methods

       public void QueryRecipeInfo()
        {
            GetRecipesDAL RecipeDAL = new GetRecipesDAL();
            DataTable RecipeInfo = new DataTable();

            RecipeInfo = RecipeDAL.GetRecipeByIDRecipe(_IDRecipe);

            if (RecipeInfo.Rows.Count > 0)
            {
                _IDRecipeFather = RecipeInfo.Rows[0].Field<Guid?>("IDRecipeFather");
                _IDOwner = RecipeInfo.Rows[0].Field<Guid?>("IDOwner");
                _NumberOfPerson = RecipeInfo.Rows[0].Field<int>("NumberOfPerson");
                _PreparationTimeMinute = RecipeInfo.Rows[0].Field<int>("PreparationTimeMinute");
                _CookingTimeMinute = RecipeInfo.Rows[0].Field<int?>("CookingTimeMinute");
                _RecipeDifficulties = (RecipeInfo.Difficulties)(RecipeInfo.Rows[0].Field<int?>("RecipeDifficulties") == null ? 0 : RecipeInfo.Rows[0].Field<int?>("RecipeDifficulties"));
                _IDRecipeImage = RecipeInfo.Rows[0].Field<Guid?>("IDRecipeImage");
                _IDRecipeVideo = RecipeInfo.Rows[0].Field<Guid?>("IDRecipeVideo");
                _IDCity = RecipeInfo.Rows[0].Field<int?>("IDCity");
                _CreationDate = RecipeInfo.Rows[0].Field<DateTime?>("CreationDate");
                _LastUpdate = RecipeInfo.Rows[0].Field<DateTime?>("LastUpdate");
                _UpdatedByUser = RecipeInfo.Rows[0].Field<Guid?>("UpdatedByUser");
                _RecipeConsulted = RecipeInfo.Rows[0].Field<int>("RecipeConsulted");
                _RecipeAvgRating = RecipeInfo.Rows[0].Field<double>("RecipeAvgRating");
                _isStarterRecipe = RecipeInfo.Rows[0].Field<bool>("isStarterRecipe");
                _DeletedOn = RecipeInfo.Rows[0].Field<DateTime?>("DeletedOn");
                _BaseRecipe = RecipeInfo.Rows[0].Field<bool>("BaseRecipe");
                _RecipeEnabled = RecipeInfo.Rows[0].Field<bool>("RecipeEnabled");
                _Checked = RecipeInfo.Rows[0].Field<bool>("Checked");
                _RecipeCompletePerc = RecipeInfo.Rows[0].Field<int?>("RecipeCompletePerc");
                _RecipePortionKcal = RecipeInfo.Rows[0].Field<double?>("RecipePortionKcal");
                _RecipePortionProteins = RecipeInfo.Rows[0].Field<double?>("RecipePortionProteins");
                _RecipePortionFats = RecipeInfo.Rows[0].Field<double?>("RecipePortionFats");
                _RecipePortionAlcohol = RecipeInfo.Rows[0].Field<double?>("RecipePortionAlcohol");
                _RecipePortionCarbohydrates = RecipeInfo.Rows[0].Field<double?>("RecipePortionCarbohydrates");
                _RecipePortionQta = RecipeInfo.Rows[0].Field<double?>("RecipePortionQta");
                _Vegetarian = RecipeInfo.Rows[0].Field<bool?>("Vegetarian");
                _Vegan = RecipeInfo.Rows[0].Field<bool?>("Vegan");
                _GlutenFree = RecipeInfo.Rows[0].Field<bool?>("GlutenFree");
                _HotSpicy = RecipeInfo.Rows[0].Field<bool?>("HotSpicy");
                _Draft = RecipeInfo.Rows[0].Field<bool?>("Draft");
                _RecipeRated = RecipeInfo.Rows[0].Field<int?>("RecipeRated");

            }
            else
            {
                _IDRecipe = new Guid();
            }

            
        }

       public static int GetNumRecipeNotChecked()
       {
           int NumRecipe = 0;
           GetRecipesDAL recipeDAL = new GetRecipesDAL();
           try
           {
               NumRecipe = (int)recipeDAL.GetNumRecipeNotChecked();
           }
           catch
           {
           }
           return NumRecipe;
       }

       public static int NumberOfRecipesInsertedByUser(Guid IDUser)
       {
           GetRecipesDAL dalRecipe = new GetRecipesDAL();
           return MyConvert.ToInt32(dalRecipe.GetNumRecipesInsertedByUser(IDUser).ToString(), 0);
       }

        public void GetRecipeIngredients()
        {
            GetRecipesIngredientsDAL recipeIngrDAL = new GetRecipesIngredientsDAL();
            DataTable dtRecipeIngr = recipeIngrDAL.GetIngredientsRecipeByIDRecipe(IDRecipe);

            RecipeIngrediets[] recipeIngrList = null;

            if (dtRecipeIngr.Rows.Count > 0)
            {
                recipeIngrList = new RecipeIngrediets[dtRecipeIngr.Rows.Count];
                int i = 0;
                int? actualRecipeIngrGroup=null;
                foreach (DataRow dr in dtRecipeIngr.Rows)
                {
                    recipeIngrList[i] = new RecipeIngrediets(dr.Field<Guid>("IDRecipeIngredient"));

                    if (actualRecipeIngrGroup != recipeIngrList[i].RecipeIngredientGroupNumber)
                    {
                        recipeIngrList[i].RecipeIngredientGroupNumberChange = true;
                        actualRecipeIngrGroup = recipeIngrList[i].RecipeIngredientGroupNumber;
                    }
                    else
                    {
                        recipeIngrList[i].RecipeIngredientGroupNumberChange = false;
                        actualRecipeIngrGroup = recipeIngrList[i].RecipeIngredientGroupNumber;
                    }
                    i++;
                }
            }

            _RecipeIngredients = recipeIngrList;
        }

        /// <summary>
        /// Return the Name of the recipe in the language specified by the IDLanguage
        /// </summary>
        /// <param name="IDLanguage">int ID of the language</param>
        /// <returns>string Recipe name</returns>
        public string GetRecipeName(int IDLanguage)
        {
            string RecipeNameLang = "";
            GetRecipesLanguagesDAL RecipeLanguageDAL = new GetRecipesLanguagesDAL();
            DataTable RecipeLanguageInfo = new DataTable();

            RecipeLanguageInfo = RecipeLanguageDAL.USP_GetRecipeByIDRecipeIDLanguage(_IDRecipe, IDLanguage);

            if (RecipeLanguageInfo.Rows.Count > 0)
            {
                RecipeNameLang = RecipeLanguageInfo.Rows[0].Field<string>("RecipeName");
            }
            return RecipeNameLang;
        }

        public string ToString(int IDLanguage)
        {
            return GetRecipeName(IDLanguage);
        }

        public ManageUSPReturnValue AddIngredient(RecipeIngredientsLanguage newRecipeIngredient)
        {
            ManageUSPReturnValue addResult = null;
            ManageUSPReturnValue addRecipeIngr = newRecipeIngredient.Add(_IDRecipe);

            if (!addRecipeIngr.IsError)
            {
                ManageUSPReturnValue addRecipeIngrLang = newRecipeIngredient.Save();
                if (!addRecipeIngrLang.IsError)
                {
                    addResult = new ManageUSPReturnValue("RC-IN-0002", "", false);
                }
                else
                {
                    addResult = new ManageUSPReturnValue("RC-ER-0002", "", true);
                    newRecipeIngredient.Delete();
                }
            }
            else
            {
                addResult = new ManageUSPReturnValue("RC-ER-0002", "", true);
                newRecipeIngredient.Delete();
            }
            return addResult;
            
        }
        /// <summary>
        /// Calculate recipe Average rating from all vote in talbe
        /// </summary>
        /// <returns>double</returns>
        public double AvgRecipeRating()
        {
            return RecipeVote.GetRecipeAvgVote(_IDRecipe);
        }

        public static DataTable GetRecipeWithIngredient(int NumberOfResults, Guid IDIngredient, bool Checked, string IngredientName)
        {
            string _ingredientForQuery = "";
            try
            {
                if (IngredientName.IndexOf(" ") > -1)
                {
                    _ingredientForQuery = IngredientName.Split(' ')[0];
                    if (_ingredientForQuery.Length < 4 && IngredientName.Length > 4)
                    {
                        _ingredientForQuery = IngredientName.Substring(0, 4);
                    }
                    else if (_ingredientForQuery.Length < 4 && IngredientName.Length < 4)
                    {
                        _ingredientForQuery = IngredientName;
                    }
                }
                else
                {
                    _ingredientForQuery = IngredientName.Substring(0, IngredientName.Length - 2);
                }
            }
            catch
            {
                _ingredientForQuery = IngredientName;
            }
            GetRecipesDAL RecipeDAL = new GetRecipesDAL();
            return RecipeDAL.GetRecipesWithIngredient(NumberOfResults, IDIngredient, Checked, _ingredientForQuery);
        }

        #endregion

        #region Operators

        public static implicit operator Recipe(Guid guid)
        {
            Recipe recipe = new Recipe(guid);
            return recipe;
        }

        public static implicit operator Guid(Recipe recipe)
        {
            Guid guid = new Guid();
            if (recipe == null)
            {
                return guid;
            }
            else
            {
                return recipe.IDRecipe;
            }
        }

        public static bool operator ==(Recipe recipe1, Recipe recipe2)
        {
            if ((Object)recipe1 == null)
            {
                recipe1 = new Recipe(new Guid());
            }
            if ((Object)recipe2 == null)
            {
                recipe2 = new Recipe(new Guid());
            }
            if ((Object)recipe1 == null || (Object)recipe2 == null)
            {
                return (Object)recipe1 == (Object)recipe2;
            }
            else
            {
                return recipe1.IDRecipe == recipe2.IDRecipe;
            }
        }

        public static bool operator !=(Recipe recipe1, Recipe recipe2)
        {
            return !(recipe1==recipe2);
        }

        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Recipe return false.
            Recipe recipe = obj as Recipe;
            if ((System.Object)recipe == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (IDRecipe == recipe.IDRecipe);
        }

        public bool Equals(Recipe recipe)
        {
            // If parameter is null return false:
            if ((object)recipe == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (IDRecipe == recipe.IDRecipe);
        }

        public override int GetHashCode()
        {
            return IDRecipe.GetHashCode();
        } 

        #endregion

    }

    public class RecipeLanguage : Recipe
    {
        #region PrivateFileds

        private Guid _IDRecipeLanguage;
        private int _IDLanguage;
        private string _RecipeName;
        private bool _RecipeLanguageAutoTranslate;
        private string _RecipeHistory;
        private DateTime? _RecipeHistoryDate;
        private string _RecipeNote;
        private string _RecipeSuggestion;
        private bool _RecipeDisabled;
        private RecipeStep[] _RecipeSteps;
        private RecipePropertyValue[] _RecipePropValue;
        private int? _GeoIDRegion;
        private string _RecipeLanguageTags;
        private bool? _OriginalVersion;
        private Guid? _TranslatedBy;

        #endregion

        #region PublicProperties

        public Guid IDRecipeLanguage
        {
            get { return _IDRecipeLanguage; }
        }

        public int IDLanguage
        {
            get { return _IDLanguage; }
            set { _IDLanguage = value; }
        }
        public string RecipeName
        {
            get { return _RecipeName; }
            set { _RecipeName = value; }
        }
        public bool RecipeLanguageAutoTranslate
        {
            get { return _RecipeLanguageAutoTranslate; }
            set { _RecipeLanguageAutoTranslate = value; }
        }
        public string RecipeHistory
        {
            get { return _RecipeHistory; }
            set { _RecipeHistory = value; }
        }
        public DateTime? RecipeHistoryDate
        {
            get { return _RecipeHistoryDate; }
            set { _RecipeHistoryDate = value; }
        }
        public string RecipeNote
        {
            get { return _RecipeNote; }
            set { _RecipeNote = value; }
        }
        public string RecipeSuggestion
        {
            get { return _RecipeSuggestion; }
            set { _RecipeSuggestion = value; }
        }
        public bool RecipeDisabled
        {
            get { return _RecipeDisabled; }
            set { _RecipeDisabled = value; }
        }
        public RecipeStep[] RecipeSteps
        {
            get { return _RecipeSteps; }
            set { _RecipeSteps = value; }
        }
        public RecipePropertyValue[] PropertyValue
        {
            get { return _RecipePropValue; }
            set { _RecipePropValue = value; }
        }
        public int? GeoIDRegion
        {
            get { return _GeoIDRegion; }
            set { _GeoIDRegion = value; }
        }
        public string RecipeLanguageTags
        {
            get { return _RecipeLanguageTags; }
            set { _RecipeLanguageTags = value; }
        }
        public bool? OriginalVersion
        {
            get { return _OriginalVersion; }
            set { _OriginalVersion = value; }
        }
        public Guid? TranslatedBy
        {
            get { return _TranslatedBy; }
            set { _TranslatedBy = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new emty Recipe with language properties
        /// </summary>
        RecipeLanguage()
        {

        }

        /// <summary>
        /// Retrieve Recipe with language properties from database
        /// </summary>
        /// <param name="IDRecipe">The recipe Guid</param>
        /// <param name="IDLanguage">The language ID</param>
        public RecipeLanguage(Guid IDRecipe, int IDLanguage)
        {
            _IDRecipe = IDRecipe;
            _IDLanguage = IDLanguage;
            //QueryRecipeLanguageInfo();
        }

        /// <summary>
        /// Retrieve Recipe with language properties from database
        /// </summary>
        /// <param name="IDRecipe">The recipe Guid</param>
        /// <param name="IDLanguage">The language ID</param>
        public RecipeLanguage(Guid IDRecipe, Guid IDRecipeLanguage, int IDLanguage)
        {
            _IDRecipe = IDRecipe;
            _IDRecipeLanguage = IDRecipeLanguage;
            _IDLanguage = IDLanguage;
            //QueryRecipeLanguageInfo();
        }

        public RecipeLanguage(Recipe baseRecipe, int IDLanguage)
        {
            if (baseRecipe != null)
            {
                _IDRecipe = baseRecipe.IDRecipe;
                _IDRecipeFather = baseRecipe.RecipeFather;
                _IDOwner = baseRecipe.Owner;
                _NumberOfPerson = baseRecipe.NumberOfPerson;
                _PreparationTimeMinute = baseRecipe.PreparationTimeMinute;
                _CookingTimeMinute = baseRecipe.CookingTimeMinute;
                _RecipeDifficulties = baseRecipe.RecipeDifficulties;
                _IDRecipeImage = baseRecipe.RecipeImage;
                _IDRecipeVideo = baseRecipe.RecipeVideo;
                _IDCity = baseRecipe.IDCity;
                _CreationDate = baseRecipe.CreationDate;
                _LastUpdate = baseRecipe.LastUpdate;
                _UpdatedByUser = baseRecipe.UpdatedByUser;
                _RecipeConsulted = baseRecipe.RecipeConsulted;
                _RecipeAvgRating = baseRecipe.RecipeAvgRating;
                _isStarterRecipe = baseRecipe.isStarterRecipe;
                _BaseRecipe = baseRecipe.BaseRecipe;
                _RecipeEnabled = baseRecipe.RecipeEnabled;
                _DeletedOn = baseRecipe.DeletedOn;
                _Checked = baseRecipe.Checked;
                _RecipeIngredients = baseRecipe.RecipeIngredients;
                _RecipeCompletePerc = baseRecipe.RecipeCompletePerc;
                _IDLanguage = IDLanguage;
                _RecipePortionKcal = baseRecipe.RecipePortionKcal;
                _RecipePortionProteins = baseRecipe.RecipePortionProteins;
                _RecipePortionFats = baseRecipe.RecipePortionKcal;
                _RecipePortionCarbohydrates = baseRecipe.RecipePortionCarbohydrates;
                _RecipePortionAlcohol = baseRecipe.RecipePortionAlcohol;
                _RecipePortionQta = baseRecipe.RecipePortionQta;
                _Vegetarian = baseRecipe.Vegetarian;
                _Vegan = baseRecipe.Vegan;
                _GlutenFree = baseRecipe.GlutenFree;
                _HotSpicy = baseRecipe.HotSpicy;
            }
           // QueryRecipeLanguageInfo();
        }

        #endregion

        #region Methods

        public void QueryBaseRecipeInfo()
        {
            if (_IDRecipe != null && _IDRecipe != new Guid())
            {
                Recipe baseRecipe = new Recipe(_IDRecipe);
                baseRecipe.QueryRecipeInfo();

                _IDRecipeFather = baseRecipe.RecipeFather;
                _IDOwner = baseRecipe.Owner;
                _NumberOfPerson = baseRecipe.NumberOfPerson;
                _PreparationTimeMinute = baseRecipe.PreparationTimeMinute;
                _CookingTimeMinute = baseRecipe.CookingTimeMinute;
                _RecipeDifficulties = baseRecipe.RecipeDifficulties;
                _IDRecipeImage = baseRecipe.RecipeImage;
                _IDRecipeVideo = baseRecipe.RecipeVideo;
                _IDCity = baseRecipe.IDCity;
                _CreationDate = baseRecipe.CreationDate;
                _LastUpdate = baseRecipe.LastUpdate;
                _UpdatedByUser = baseRecipe.UpdatedByUser;
                _RecipeConsulted = baseRecipe.RecipeConsulted;
                _RecipeAvgRating = baseRecipe.RecipeAvgRating;
                _isStarterRecipe = baseRecipe.isStarterRecipe;
                _BaseRecipe = baseRecipe.BaseRecipe;
                _RecipeEnabled = baseRecipe.RecipeEnabled;
                _DeletedOn = baseRecipe.DeletedOn;
                _Checked = baseRecipe.Checked;
                _RecipeIngredients = baseRecipe.RecipeIngredients;
                _RecipeCompletePerc = baseRecipe.RecipeCompletePerc;
                _RecipePortionKcal = baseRecipe.RecipePortionKcal;
                _RecipePortionProteins = baseRecipe.RecipePortionProteins;
                _RecipePortionFats = baseRecipe.RecipePortionKcal;
                _RecipePortionCarbohydrates = baseRecipe.RecipePortionCarbohydrates;
                _RecipePortionAlcohol = baseRecipe.RecipePortionAlcohol;
                _RecipePortionQta = baseRecipe.RecipePortionQta;
                _Vegetarian = baseRecipe.Vegetarian;
                _Vegan = baseRecipe.Vegan;
                _GlutenFree = baseRecipe.GlutenFree;
                _HotSpicy = baseRecipe.HotSpicy;
                _Draft = baseRecipe.Draft;
                _RecipeRated = baseRecipe.RecipeRated;
            }
        }

        public void QueryRecipeLanguageInfo()
        {
            
            GetRecipesLanguagesDAL RecipeLanguageDAL = new GetRecipesLanguagesDAL();
            DataTable RecipeLanguageInfo = new DataTable();

            RecipeLanguageInfo = RecipeLanguageDAL.USP_GetRecipeByIDRecipeIDLanguage(_IDRecipe, _IDLanguage);

            if (RecipeLanguageInfo.Rows.Count > 0)
            {
                _IDRecipeLanguage = RecipeLanguageInfo.Rows[0].Field<Guid>("IDRecipeLanguage");
                _IDLanguage = RecipeLanguageInfo.Rows[0].Field<int>("IDLanguage");
                _RecipeName = RecipeLanguageInfo.Rows[0].Field<string>("RecipeName");
                _RecipeLanguageAutoTranslate = RecipeLanguageInfo.Rows[0].Field<bool>("RecipeLanguageAutoTranslate");
                _RecipeHistory = RecipeLanguageInfo.Rows[0].Field<string>("RecipeHistory");
                _RecipeHistoryDate = RecipeLanguageInfo.Rows[0].Field<DateTime?>("RecipeHistoryDate");
                _RecipeNote = RecipeLanguageInfo.Rows[0].Field<string>("RecipeNote");
                _RecipeSuggestion = RecipeLanguageInfo.Rows[0].Field<string>("RecipeSuggestion");
                _RecipeDisabled = RecipeLanguageInfo.Rows[0].Field<bool>("RecipeDisabled");
                _GeoIDRegion = RecipeLanguageInfo.Rows[0].Field<int?>("GeoIDRegion");
                _RecipeLanguageTags = RecipeLanguageInfo.Rows[0].Field<string>("RecipeLanguageTags");
                _OriginalVersion = RecipeLanguageInfo.Rows[0].Field<bool?>("OriginalVersion");
                _TranslatedBy = RecipeLanguageInfo.Rows[0].Field<Guid?>("TranslatedBy");
            }
        }

        public override string ToString()
        {
            return _RecipeName;
        }

        public static DataTable GetRecipeByName(string recipeName, int idLanguage)
        {
            GetRecipesLanguagesDAL RecipeLanguageDAL = new GetRecipesLanguagesDAL();

            recipeName = recipeName.Trim();

            return RecipeLanguageDAL.USP_GetNoFatherRecipeByRecipeNameContains(recipeName, idLanguage);
        }

        public static DataTable GetRecipeSiteMap(int idLanguage)
        {
            GETSiteMapRecipeDAL dalRecipeSiteMap = new GETSiteMapRecipeDAL();

            return dalRecipeSiteMap.USP_SiteMapRecipe(idLanguage);
        }

        public static DataTable GetRecipeByOwner(Guid IDOwner)
        {
            GetRecipesLanguagesDAL RecipeLanguageDAL = new GetRecipesLanguagesDAL();
            return RecipeLanguageDAL.GetRecipeByIDOwner(IDOwner);
        }

        public static DataTable GetTopRecipes(int IDLanguage)
        {
            GetRecipesLanguagesDAL RecipeLanguageDAL = new GetRecipesLanguagesDAL();
            return RecipeLanguageDAL.USP_GetHomeTopRecipes(IDLanguage);
        }

        public static DataTable GetRecipeToTranslate(int idLanguageFrom, int idLanguageTo, int RecipeToTranslate)
        {
            GetRecipesLanguagesDAL RecipeLanguageDAL = new GetRecipesLanguagesDAL();
            return RecipeLanguageDAL.USP_GetRecipeToTranslate(idLanguageFrom, idLanguageTo, RecipeToTranslate);
        }

        /// <summary>
        /// Use GetRecipeSteps() and not this!
        /// </summary>
        /// <param name="IDRecipe"></param>
        /// <returns></returns>
        [Obsolete("Use GetRecipeSteps()",false)]
        public string GetRecipeStep()
        {
            GetRecipesStepsDAL dalRecipeSteps = new GetRecipesStepsDAL();

            return dalRecipeSteps.GetStepsByIDRecipe(_IDRecipe).Rows[0][4].ToString();
        }

        public void GetRecipeSteps()
        {
            GetRecipesStepsDAL recipeStepsDAL = new GetRecipesStepsDAL();
            DataTable dtRecipeSteps = recipeStepsDAL.GetStepsByIDRecipeLang(_IDRecipeLanguage);

            RecipeStep[] recipeStepList = null;

            if (dtRecipeSteps.Rows.Count > 0)
            {
                recipeStepList = new RecipeStep[dtRecipeSteps.Rows.Count];
                int i = 0;

                foreach (DataRow dr in dtRecipeSteps.Rows)
                {
                    recipeStepList[i] = new RecipeStep(dr.Field<Guid>("IDRecipeStep"));
                    i++;
                }
            }

            _RecipeSteps = recipeStepList;
        }

        public void GetRecipePropertiesValue()
        {
            GetRecipesPropertiesValuesDAL recipePropValueDAL = new GetRecipesPropertiesValuesDAL();
            DataTable dtRecipePropValue = recipePropValueDAL.GetPropValueByIDRecipe(_IDRecipe);

            RecipePropertyValue[] recipePropertiesValue = null;

            if (dtRecipePropValue.Rows.Count > 0)
            {
                recipePropertiesValue = new RecipePropertyValue[dtRecipePropValue.Rows.Count];
                int i = 0;

                foreach (DataRow dr in dtRecipePropValue.Rows)
                {
                    recipePropertiesValue[i] = new RecipePropertyValue(dr.Field<Guid>("IDRecipePropertyValue"), _IDLanguage);
                    i++;
                }

            }

            _RecipePropValue = recipePropertiesValue;

        }

        public ManageUSPReturnValue ClearRecipePropertiesVales()
        {
            ManageRecipesDAL manageDAL = new ManageRecipesDAL();
            DataTable _dtResult = manageDAL.USP_ClearRecipePropertyValue(_IDRecipe);
            ManageUSPReturnValue _result = new ManageUSPReturnValue(_dtResult);

            return _result;
        }

        public static List<RecipeLanguage> GetAllRecipeLangNotChecked(int IDLanguage)
        {
            GetRecipesLanguagesDAL dalRecipeLang = new GetRecipesLanguagesDAL();

            DataTable dtRecipeLang = dalRecipeLang.GetAllRecipeLangNotChecked(IDLanguage);

            List<RecipeLanguage> AllRecipeLangNotChecked = new List<RecipeLanguage>();

            if (dtRecipeLang.Rows.Count > 0)
            {
                for (int i = 0; i < dtRecipeLang.Rows.Count; i++)
                {
                    AllRecipeLangNotChecked.Add(new RecipeLanguage()
                    {
                        _IDRecipe = dtRecipeLang.Rows[i].Field<Guid>("IDRecipe"),
                        _RecipeName = dtRecipeLang.Rows[i].Field<string>("RecipeName"),
                    });
                }
            }

            return AllRecipeLangNotChecked;
        }

        public static List<RecipeLanguage> GetRecipesByType(int RecipeType, int OffsetRows, int FetchRows, bool Vegan, bool Vegetarian, 
                                                                bool GlutenFree, int LightThreshold, int QuickThreshold, int IDLanguage)
        {
            GetRecipesListsDAL dalRecipeLang = new GetRecipesListsDAL();

            DataTable dtRecipeLang = dalRecipeLang.USP_GetRecipesByType(RecipeType, OffsetRows, FetchRows, Vegan, Vegetarian, 
                                                                            GlutenFree, LightThreshold, QuickThreshold, IDLanguage);

            List<RecipeLanguage> RecipesByType = new List<RecipeLanguage>();

            if (dtRecipeLang.Rows.Count > 0)
            {
                for (int i = 0; i < dtRecipeLang.Rows.Count; i++)
                {
                    RecipesByType.Add(new RecipeLanguage()
                    {
                        _IDRecipe = dtRecipeLang.Rows[i].Field<Guid>("IDRecipe"),
                        _RecipeName = dtRecipeLang.Rows[i].Field<string>("RecipeName"),
                        _IDRecipeImage = dtRecipeLang.Rows[i].Field<Guid>("IDRecipeImage"),
                        _IDOwner = dtRecipeLang.Rows[i].Field<Guid>("IDOwner"),
                    });
                }
            }

            return RecipesByType;
        }

        public List<RecipeLanguage> FindRecipeFatherByRecipeNameContains()
        {
            GetRecipesLanguagesDAL dalRecipeLang = new GetRecipesLanguagesDAL();

            DataTable dtRecipeLang = dalRecipeLang.USP_GetRecipeFatherByRecipeNameContain(_RecipeName, _IDRecipe, _IDLanguage);

            List<RecipeLanguage> PossibleRecipeFather = new List<RecipeLanguage>();

            if (dtRecipeLang.Rows.Count > 0)
            {
                for (int i = 0; i < dtRecipeLang.Rows.Count; i++)
                {
                    PossibleRecipeFather.Add(new RecipeLanguage()
                    {
                        _IDRecipe = dtRecipeLang.Rows[i].Field<Guid>("IDRecipe"),
                        _RecipeName = dtRecipeLang.Rows[i].Field<string>("RecipeName"),
                    });
                }
            }

            return PossibleRecipeFather;
        }

        public List<Recipe> GetSimilarRecipes(string RecipeName, bool? Vegan, bool? Vegetarian, bool? GlutenFree, int IDLanguage)
        {
            GetRecipesListsDAL dalSimilarRecipes = new GetRecipesListsDAL();

            DataTable dtSimilarRecipes = dalSimilarRecipes.USP_GetSimilarRecipes(_IDRecipe, RecipeName, Vegan, Vegetarian, GlutenFree, IDLanguage);

            List<Recipe> SimilarRecipes = new List<Recipe>();

            if (dtSimilarRecipes.Rows.Count > 0)
            {
                for (int i = 0; i < dtSimilarRecipes.Rows.Count; i++)
                {
                    SimilarRecipes.Add(new RecipeLanguage()
                    {
                        _IDRecipe = dtSimilarRecipes.Rows[i].Field<Guid>("IDRecipe"),
                        _RecipeName = dtSimilarRecipes.Rows[i].Field<string>("RecipeName"),
                        _IDRecipeImage = dtSimilarRecipes.Rows[i].Field<Guid>("IDRecipeImage"),
                        _IDOwner = dtSimilarRecipes.Rows[i].Field<Guid>("IDOwner")
                    });
                }
            }

            return SimilarRecipes;
        }

        public ManageUSPReturnValue Save()
        {
            ManageRecipesDAL _manageDAL = new ManageRecipesDAL();
            DataTable _dtResult = _manageDAL.USP_ManageRecipeWithLanguageInfo(_IDRecipe, _IDRecipeFather, _IDOwner, _NumberOfPerson,
                    _PreparationTimeMinute, _CookingTimeMinute, (int)_RecipeDifficulties, _IDRecipeImage, _IDRecipeVideo, _IDCity,
                    _CreationDate, _LastUpdate, _UpdatedByUser, _RecipeConsulted, _RecipeAvgRating, _isStarterRecipe, _DeletedOn,
                    _BaseRecipe, _RecipeEnabled, _Checked, _RecipeCompletePerc, _RecipePortionKcal, _RecipePortionProteins,
                    _RecipePortionFats, _RecipePortionCarbohydrates,_RecipePortionAlcohol,_RecipePortionQta,_Vegetarian,_Vegan,_GlutenFree,_HotSpicy, _IDRecipeLanguage, _IDLanguage, _RecipeName,
                    _RecipeLanguageAutoTranslate, _RecipeHistory, _RecipeHistoryDate, _RecipeNote, _RecipeSuggestion,
                    _RecipeDisabled, _GeoIDRegion, _RecipeLanguageTags,_OriginalVersion,_TranslatedBy, false,_Draft,_RecipeRated);

            ManageUSPReturnValue _result = new ManageUSPReturnValue(_dtResult);

            return _result;
        }

        public ManageUSPReturnValue SaveLanguageInfo()
        {
            ManageRecipesDAL _manageDAL = new ManageRecipesDAL();
            DataTable _dtResult = _manageDAL.USP_ManageRecipeLanguage(_IDRecipe, _IDRecipeLanguage, _IDLanguage, _RecipeName,
                    _RecipeLanguageAutoTranslate, _RecipeHistory, _RecipeHistoryDate, _RecipeNote, _RecipeSuggestion,
                    _RecipeDisabled, _GeoIDRegion, _RecipeLanguageTags, _OriginalVersion, _TranslatedBy, false);

            ManageUSPReturnValue _result = new ManageUSPReturnValue(_dtResult);

            return _result;
        }

        public ManageUSPReturnValue Delete()
        {
            _RecipeEnabled = false;
            ManageRecipesDAL _manageDAL = new ManageRecipesDAL();
            DataTable _dtResult = _manageDAL.USP_ManageRecipeWithLanguageInfo(_IDRecipe, _IDRecipeFather, _IDOwner, _NumberOfPerson,
                    _PreparationTimeMinute, _CookingTimeMinute, (int)_RecipeDifficulties, _IDRecipeImage, _IDRecipeVideo, _IDCity,
                    _CreationDate, _LastUpdate, _UpdatedByUser, _RecipeConsulted, _RecipeAvgRating, _isStarterRecipe, DateTime.UtcNow,
                    _BaseRecipe, _RecipeEnabled, _Checked, _RecipeCompletePerc, _RecipePortionKcal, _RecipePortionProteins,
                    _RecipePortionFats, _RecipePortionCarbohydrates,_RecipePortionAlcohol, _RecipePortionQta, _Vegetarian, _Vegan, _GlutenFree,_HotSpicy, _IDRecipeLanguage, _IDLanguage, _RecipeName,
                    _RecipeLanguageAutoTranslate, _RecipeHistory, _RecipeHistoryDate, _RecipeNote, _RecipeSuggestion,
                    _RecipeDisabled, _GeoIDRegion, _RecipeLanguageTags, _OriginalVersion, _TranslatedBy, false,_Draft,_RecipeRated);

            ManageUSPReturnValue _result = new ManageUSPReturnValue(_dtResult);

            return _result;
        }

        public int PercentageComplete()
        {
            double _return = 0;

            if (_NumberOfPerson!=0) _return+=2;
            if (_PreparationTimeMinute != 0) _return+=3;
            if (_CookingTimeMinute != null && _CookingTimeMinute != 0) _return+=2;
            if (_IDRecipeImage != null) _return+=5;
            if (_IDCity != null) _return++;
            if (_RecipeIngredients != null) _return+=5;
            if (_RecipePortionKcal != null) _return++;
            if (_RecipePortionProteins != null) _return++;
            if (_RecipePortionFats != null) _return++;
            if (_RecipePortionCarbohydrates != null) _return++;
            if (_RecipeName != null) _return++;
            if (_RecipeHistory != null && _RecipeHistory != "") _return+=3;
            if (_RecipeNote != null && _RecipeNote != "") _return+=2;
            if (_RecipeSuggestion != null && _RecipeSuggestion != "") _return+=5;
            if (_RecipeSteps != null) _return++;
            if (_RecipeSteps != null && _RecipeSteps.Length>1) _return+=10;
            if (_RecipePropValue != null && _RecipePropValue.Length>4) _return+=5;
            if (_RecipeLanguageTags != null) _return++;

            return Convert.ToInt32(_return/50*100);
        }

        #endregion

        #region Operators

       

        #endregion
    }

     public class RecipeIngrediets
    {
        #region PrivateFields

        protected Guid _IDRecipeIngredient;
        protected Guid _IDRecipe;
        protected Ingredient _IDIngredient;
        protected bool _IsPrincipalIngredient;
        protected string _QuantityNotStd;
        protected QuantityNotStdType _QuantityNotStdType;
        protected double? _Quantity;
        protected IngredientQuantityType _QuantityType;
        protected bool _QuantityNotSpecified;
        protected int _RecipeIngredientGroupNumber;
        protected bool _RecipeIngredientGroupNumberChange;
        protected Guid? _IDRecipeIngredientAlternative;
        protected RecipeIngrediets[] _RecipeIngredientAlternatives;
        protected RecipeInfo.IngredientRelevances _IngredientRelevance;

        #endregion

        #region PublicProperties

        public Guid IDRecipeIngredient
        {
            get { return _IDRecipeIngredient; }
        }
        public Recipe Recipe
        {
            get { return _IDRecipe; }
            set { _IDRecipe = value; }
        }
        public Ingredient Ingredient
        {
            get { return _IDIngredient; }
            set { _IDIngredient = value; }
        }
        public bool IsPrincipalIngredient
        {
            get { return _IsPrincipalIngredient; }
            set { _IsPrincipalIngredient = value; }
        }
        public string QuantityNotStd
        {
            get { return _QuantityNotStd; }
            set { _QuantityNotStd = value; }
        }
        public QuantityNotStdType QuantityNotStdType
        {
            get { return _QuantityNotStdType == null ? new QuantityNotStdType(0) : _QuantityNotStdType; }
            set { _QuantityNotStdType = value; }
        }
        public double? Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        public IngredientQuantityType QuantityType
        {
            get { return _QuantityType == null ? new IngredientQuantityType(0) : _QuantityType; }
            set { _QuantityType = value; }
        }
        public bool QuantityNotSpecified
        {
            get { return _QuantityNotSpecified; }
            set { _QuantityNotSpecified = value; }
        }

        public int RecipeIngredientGroupNumber
        {
            get { return _RecipeIngredientGroupNumber; }
            set { _RecipeIngredientGroupNumber = value; }
        }

        public bool RecipeIngredientGroupNumberChange
        {
            get { return _RecipeIngredientGroupNumberChange; }
            set { _RecipeIngredientGroupNumberChange = value; }
        }
        public Guid? IDRecipeIngredientAlternative
        {
            get { return _IDRecipeIngredientAlternative; }
            set { _IDRecipeIngredientAlternative = value; }
        }
        public RecipeIngrediets[] RecipeIngredientAlternatives
        {
            get { return _RecipeIngredientAlternatives; }
        }
        public RecipeInfo.IngredientRelevances IngredientRelevance
        {
            get { return _IngredientRelevance; }
            set { _IngredientRelevance = value; }
        }

        #endregion

        #region Constructors

        public RecipeIngrediets()
        {
        }

        /// <summary>
        /// Create istance of RecipeIngrediets with existing DB info
        /// </summary>
        /// <param name="IDRecipeIngredient">ID of RecipesIngredient table</param>
        public RecipeIngrediets(Guid IDRecipeIngredient)
        {
            _IDRecipeIngredient = IDRecipeIngredient;
            _RecipeIngredientGroupNumberChange = false;

            GetRecipesIngredientsDAL recipeIngrDAL = new GetRecipesIngredientsDAL();
            DataTable dtRecipeIngr = recipeIngrDAL.GetIngredientRecipeByID(_IDRecipeIngredient);

            if (dtRecipeIngr.Rows.Count > 0)
            {
                _IDRecipe = dtRecipeIngr.Rows[0].Field<Guid>("IDRecipe");
                _IDIngredient = dtRecipeIngr.Rows[0].Field<Guid>("IDIngredient");
                _IsPrincipalIngredient = dtRecipeIngr.Rows[0].Field<bool>("IsPrincipalIngredient");
                _QuantityNotStd = dtRecipeIngr.Rows[0].Field<string>("QuantityNotStd");
                _QuantityNotStdType = dtRecipeIngr.Rows[0].Field<int?>("IDQuantityNotStd");
                _Quantity = dtRecipeIngr.Rows[0].Field<double?>("Quantity");
                _QuantityType = dtRecipeIngr.Rows[0].Field<int?>("IDQuantityType");
                _QuantityNotSpecified = dtRecipeIngr.Rows[0].Field<bool>("QuantityNotSpecified");
                _RecipeIngredientGroupNumber = dtRecipeIngr.Rows[0].Field<int>("RecipeIngredientGroupNumber");
                _IDRecipeIngredientAlternative = dtRecipeIngr.Rows[0].Field<Guid?>("IDRecipeIngredientAlternative");
                try
                {
                    _IngredientRelevance = (RecipeInfo.IngredientRelevances)Enum.Parse(typeof(RecipeInfo.IngredientRelevances), dtRecipeIngr.Rows[0].Field<int?>("IngredientRelevance").ToString());
                }
                catch
                {
                    _IngredientRelevance = RecipeInfo.IngredientRelevances.ImportantIngredient;
                }
                if (_QuantityNotStdType == null)
                {
                    _QuantityNotStdType = new QuantityNotStdType(0);
                }
            }

        }

        public RecipeIngrediets(Guid IDRecipeIngredient, bool noFull)
        {
            _IDRecipeIngredient = IDRecipeIngredient;
        }

        /// <summary>
        /// Create istance of RecipeIngrediets for Manage an ingredient in existing recipe
        /// </summary>
        /// <param name="IDRecipeIngredient">ID Recipe Ingredient</param>
        /// <param name="IDRecipe">ID of recipe</param>
        /// <param name="IDIngredient">ID of ingredient</param>
        /// <param name="IsPrincipalIngredient">Specify if this is a principal (or fundamental) ingredient</param>
        /// <param name="QuantityNotStd">A strange, not standard, quantity</param>
        /// <param name="QuantityNotStdType">The type of quantity above</param>
        /// <param name="Quantity">A standard numeric quantity</param>
        /// <param name="QuantityType">The standard quantity type</param>
        /// <param name="QuantityNotSpecified">specify if the quantity is not specified</param>
        /// <param name="RecipeIngredientGroupNumeber">specify the group number</param>
        /// <param name="IngredientRelevance">Relevance of this ingredient in the recipe</param>
        public RecipeIngrediets(Guid IDRecipeIngredient, Guid IDRecipe, Guid IDIngredient, bool IsPrincipalIngredient,
                                   string QuantityNotStd, QuantityNotStdType QuantityNotStdType, double? Quantity,
                                   IngredientQuantityType QuantityType, bool QuantityNotSpecified, int RecipeIngredientGroupNumeber, RecipeInfo.IngredientRelevances IngredientRelevance)
        {
            _IDRecipeIngredient = IDRecipeIngredient;
            _IDRecipe = IDRecipe;
            _IDIngredient = IDIngredient;
            _IsPrincipalIngredient = IsPrincipalIngredient;
            _QuantityNotStd = QuantityNotStd;
            _QuantityNotStdType = QuantityNotStdType;
            _Quantity = Quantity;
            _QuantityType = QuantityType;
            _QuantityNotSpecified = QuantityNotSpecified;
            _RecipeIngredientGroupNumber = RecipeIngredientGroupNumeber;
            _IngredientRelevance = IngredientRelevance;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get the alternative ingredient for this ingredient
        /// </summary>
        public void GetAlternativeIngredients()
        {
            GetRecipesIngredientsDAL recipeAltIngrDAL = new GetRecipesIngredientsDAL();
            DataTable dtRecipeAltIngr = recipeAltIngrDAL.GetAlternativeIngredientsByIDRecipeIngredientAlternative(_IDRecipeIngredient);


            if (dtRecipeAltIngr.Rows.Count > 0)
            {
                _RecipeIngredientAlternatives = new RecipeIngrediets[dtRecipeAltIngr.Rows.Count];

                for (int i = 0; i < dtRecipeAltIngr.Rows.Count;i++ )
                {
                    RecipeIngrediets _recIngrAlt = new RecipeIngrediets();
                    _recIngrAlt._IDRecipe = dtRecipeAltIngr.Rows[i].Field<Guid>("IDRecipe");
                    _recIngrAlt._IDIngredient = dtRecipeAltIngr.Rows[i].Field<Guid>("IDIngredient");
                    _recIngrAlt._IsPrincipalIngredient = dtRecipeAltIngr.Rows[i].Field<bool>("IsPrincipalIngredient");
                    _recIngrAlt._QuantityNotStd = dtRecipeAltIngr.Rows[i].Field<string>("QuantityNotStd");
                    _recIngrAlt._QuantityNotStdType = dtRecipeAltIngr.Rows[i].Field<int?>("IDQuantityNotStd");
                    _recIngrAlt._Quantity = dtRecipeAltIngr.Rows[i].Field<double?>("Quantity");
                    _recIngrAlt._QuantityType = dtRecipeAltIngr.Rows[i].Field<int?>("IDQuantityType");
                    _recIngrAlt._QuantityNotSpecified = dtRecipeAltIngr.Rows[i].Field<bool>("QuantityNotSpecified");
                    _recIngrAlt._RecipeIngredientGroupNumber = dtRecipeAltIngr.Rows[i].Field<int>("RecipeIngredientGroupNumber");

                    _RecipeIngredientAlternatives[i] = _recIngrAlt;
                }
            }
        }

         /// <summary>
         /// Delete an ingredient from a Recipe
         /// </summary>
        public ManageUSPReturnValue Delete()
        {
            ManageRecipesDAL delRecipeIngredient = new ManageRecipesDAL();
            ManageUSPReturnValue delRecipeIngredientResult = 
                new ManageUSPReturnValue(delRecipeIngredient.USP_ManageRecipeIngredient(_IDRecipeIngredient, null, null, null, 
                                                "", null, null, null, null, null,null,null, true));

            return delRecipeIngredientResult;

        }

         /// <summary>
         /// Add ingredient to recipe
         /// </summary>
        public ManageUSPReturnValue Add(Guid IDRecipe)
        {
            ManageRecipesDAL AddRecipeIngredient = new ManageRecipesDAL();
            ManageUSPReturnValue AddRecipeIngredientResult =
                new ManageUSPReturnValue(AddRecipeIngredient.USP_ManageRecipeIngredient(_IDRecipeIngredient, IDRecipe, _IDIngredient, _IsPrincipalIngredient,
                                                _QuantityNotStd, _QuantityNotStdType.IDQuantityNotStdNullable, _Quantity, _QuantityType.IDIngredientQuantityType,
                                                _QuantityNotSpecified, Convert.ToByte(_RecipeIngredientGroupNumber), _IDRecipeIngredientAlternative,(int)_IngredientRelevance, false));
            if (!AddRecipeIngredientResult.IsError)
            {
                _IDRecipeIngredient = new Guid(AddRecipeIngredientResult.USPReturnValue);
            }

            return AddRecipeIngredientResult;
        }

        #endregion
    }

     public class RecipeIngredientsLanguage : RecipeIngrediets
     {
         #region PrivateFileds

         private Guid _IDRecipeIngredientLanguage;
         //private Guid _IDRecipeIngredient;
         private int _IDLanguage;
         private string _RecipeIngredientNote;
         private string _RecipeIngredientGroupName;
         private bool _isAutoTranslate;

         #endregion

         #region PublicProperties

         public Guid IDRecipeIngredientLanguage
         {
             get { return _IDRecipeIngredientLanguage; }
         }
         //public Guid IDRecipeIngredient
         //{
         //    get { return _IDRecipeIngredient; }
         //    set { _IDRecipeIngredient = value; }
         //}
         public int IDLanguage
         {
             get { return _IDLanguage; }
             set { _IDLanguage = value; }
         }
         public string RecipeIngredientNote
         {
             get { return _RecipeIngredientNote; }
             set { _RecipeIngredientNote = value; }
         }
         public string RecipeIngredientGroupName
         {
             get { return _RecipeIngredientGroupName; }
             set { _RecipeIngredientGroupName = value; }
         }
         public bool isAutoTranslate
         {
             get { return _isAutoTranslate; }
             set { _isAutoTranslate = value; }
         }

         #endregion

         #region Costructors

         public RecipeIngredientsLanguage()
         {
         }

         public RecipeIngredientsLanguage(Guid IDRecipeIngredient, int IDLanguage)
         {
             _IDRecipeIngredient = IDRecipeIngredient;
             _IDLanguage = IDLanguage;

             GetRecipesIngredientsLanguagesDAL recipeIngrLangDAL = new GetRecipesIngredientsLanguagesDAL();
             DataTable dtRecipeIngrLang = recipeIngrLangDAL.GetRecipeIngredientLanguageByIDRecIngrIDLang(_IDRecipeIngredient, _IDLanguage);

             if (dtRecipeIngrLang.Rows.Count > 0)
             {
                 _IDRecipeIngredientLanguage = dtRecipeIngrLang.Rows[0].Field<Guid>("IDRecipeIngredientLanguage");
                 _RecipeIngredientNote = dtRecipeIngrLang.Rows[0].Field<string>("RecipeIngredientNote");
                 _RecipeIngredientGroupName = dtRecipeIngrLang.Rows[0].Field<string>("RecipeIngredientGroupName");
                 _isAutoTranslate = dtRecipeIngrLang.Rows[0].Field<bool>("isAutoTranslate");

             }

         }

         public RecipeIngredientsLanguage(Guid IDRecipeIngredientLanguage, Guid IDRecipeIngredient, int IDLanguage,
                                        string RecipeIngredientNote, string RecipeIngredientGroupName, bool isAutoTranslate)
         {
             _IDRecipeIngredientLanguage = IDRecipeIngredientLanguage;
             _IDRecipeIngredient = IDRecipeIngredient;
             _IDLanguage = IDLanguage;
             _RecipeIngredientNote = RecipeIngredientNote;
             _RecipeIngredientGroupName = RecipeIngredientGroupName;
             _isAutoTranslate = isAutoTranslate;
         }

         public RecipeIngredientsLanguage(Guid IDRecipeIngredientLanguage, Guid IDRecipeIngredient, int IDLanguage,
                                        string RecipeIngredientNote, string RecipeIngredientGroupName, bool isAutoTranslate,
                                        Guid IDRecipe, Guid IDIngredient, bool IsPrincipalIngredient,
                                        string QuantityNotStd, QuantityNotStdType QuantityNotStdType, double? Quantity,
                                        IngredientQuantityType QuantityType, bool QuantityNotSpecified, int RecipeIngredientGroupNumber,
                                        Guid? IDRecipeIngredientAlternative, RecipeInfo.IngredientRelevances IngredientRelevance)
         {
             _IDRecipeIngredientLanguage = IDRecipeIngredientLanguage;
             _IDRecipeIngredient = IDRecipeIngredient;
             _IDLanguage = IDLanguage;
             _RecipeIngredientNote = RecipeIngredientNote;
             _RecipeIngredientGroupName = RecipeIngredientGroupName;
             _isAutoTranslate = isAutoTranslate;
             base._IDRecipe = IDRecipe;
             base._IDIngredient = new Ingredient(IDIngredient);
             base._IsPrincipalIngredient = IsPrincipalIngredient;
             base._QuantityNotStd = QuantityNotStd;
             base._QuantityNotStdType = QuantityNotStdType;
             base._Quantity = Quantity;
             base._QuantityType = QuantityType;
             base._QuantityNotSpecified = QuantityNotSpecified;
             base._RecipeIngredientGroupNumber = RecipeIngredientGroupNumber;
             base._IDRecipeIngredient = _IDRecipeIngredient;
             base._IDRecipeIngredientAlternative = IDRecipeIngredientAlternative;
             base._IngredientRelevance = IngredientRelevance;
         }

         #endregion

         #region Methods

         public void QueryBaseRecipesIngredientsInfo()
         {
             if (_IDRecipeIngredient != null && _IDRecipeIngredient != new Guid())
             {
                RecipeIngrediets _baseRecipeIngr = new RecipeIngrediets(_IDRecipeIngredient);

                base.Recipe = _baseRecipeIngr.Recipe;
                base.Ingredient = _baseRecipeIngr.Ingredient;
                base.IsPrincipalIngredient = _baseRecipeIngr.IsPrincipalIngredient;
                base.QuantityNotStd =_baseRecipeIngr.QuantityNotStd;
                base.QuantityNotStdType = _baseRecipeIngr.QuantityNotStdType;
                base.Quantity = _baseRecipeIngr.Quantity;
                base.QuantityType = _baseRecipeIngr.QuantityType;
                base.QuantityNotSpecified = _baseRecipeIngr.QuantityNotSpecified;
                base.RecipeIngredientGroupNumber = _baseRecipeIngr.RecipeIngredientGroupNumber;
                base.IngredientRelevance = _baseRecipeIngr.IngredientRelevance;

             }
         }

         /// <summary>
         /// Save ingredient language info into Recipe
         /// </summary>
         /// <returns></returns>
         public ManageUSPReturnValue Save()
         {
              ManageRecipesDAL addRecipeIngredientLang = new ManageRecipesDAL();
              ManageUSPReturnValue addRecipeIngredientResultLang = 
                 new ManageUSPReturnValue(addRecipeIngredientLang.USP_ManageRecipeIngredientLanguage(_IDRecipeIngredientLanguage,
                                              _IDRecipeIngredient, _IDLanguage, _RecipeIngredientNote, _RecipeIngredientGroupName, _isAutoTranslate, false));
              return addRecipeIngredientResultLang;
         }

         #endregion

         public static DataTable GetRecipeIngredientNotesToTranslate(int IDLanguageFrom, int IDLanguageTo, int NumRow)
         {
             GetRecipesIngredientsLanguagesDAL dalIngredientsLanguages = new GetRecipesIngredientsLanguagesDAL();
             return dalIngredientsLanguages.USP_GetRecipeIngredientNotesToTranslate(IDLanguageFrom, IDLanguageTo, NumRow);
         }
     }

     public class RecipeStep
     {

         #region PrivateFields

         private Guid _IDRecipeStep;
         private Guid _IDRecipeLanguage;
         private string _StepGroup;
         private int _StepNumber;
         private string _RecipeStep;
         private int? _StepTimeMinute;
         private Photo _StepImage;

         #endregion

        #region PublicProperties

         public Guid IDRecipeStep
         {
             get { return _IDRecipeStep; }
         }
         public Guid IDRecipeLanguage
         {
             get { return _IDRecipeLanguage; }
             set { _IDRecipeLanguage = value; }
         }
         public string StepGroup
         {
             get { return _StepGroup; }
             set { _StepGroup = value; }
         }
         public int StepNumber
         {
             get { return _StepNumber; }
             set { _StepNumber = value; }
         }
         public string Step
         {
             get { return _RecipeStep; }
             set { _RecipeStep = value; }
         }
         public int? StepTimeMinute
         {
             get { return _StepTimeMinute; }
             set { _StepTimeMinute = value; }
         }
         public Photo IDRecipeStepImage
         {
             get { return _StepImage; }
             set { _StepImage = value; }
         }

        #endregion

         #region Costructors

         public RecipeStep(Guid IDRecipeStep)
         {
             _IDRecipeStep = IDRecipeStep;

             GetRecipesStepsDAL recipeStepDAL = new GetRecipesStepsDAL();
             DataTable dtRecipeStep = recipeStepDAL.GetStepsByIDStep(_IDRecipeStep);

             if (dtRecipeStep.Rows.Count > 0)
             {
                 _IDRecipeStep = dtRecipeStep.Rows[0].Field<Guid>("IDRecipeStep");
                 _IDRecipeLanguage = dtRecipeStep.Rows[0].Field<Guid>("IDRecipeLanguage");
                 _StepGroup = dtRecipeStep.Rows[0].Field<string>("StepGroup");
                 _StepNumber = dtRecipeStep.Rows[0].Field<int>("StepNumber");
                 _RecipeStep = dtRecipeStep.Rows[0].Field<string>("RecipeStep");
                 _StepTimeMinute = dtRecipeStep.Rows[0].Field<int?>("StepTimeMinute");
                 _StepImage = dtRecipeStep.Rows[0].Field<Guid?>("IDRecipeStepImage");

             }
         }

         public RecipeStep(Guid IDRecipeLang, int StepNum, string Step, Photo Image)
         {
             _IDRecipeStep = Guid.NewGuid();
             _IDRecipeLanguage = IDRecipeLang;
             _StepNumber = StepNum;
             _RecipeStep = Step;
             _StepImage = Image;
         }

         #endregion

         #region Methods

         public static DataTable GetRecipeStepsToTranslate(int idLanguageFrom, int idLanguageTo, int StepsToTranslate)
         {
             GetRecipesStepsDAL dalRecipesSteps = new GetRecipesStepsDAL();
             return dalRecipesSteps.USP_GetRecipeStepsToTranslate(idLanguageFrom, idLanguageTo, StepsToTranslate);
         }

         public ManageUSPReturnValue Save()
         {
             ManageRecipesDAL _manageDAL = new ManageRecipesDAL();
             DataTable _dtResult = _manageDAL.USP_ManageRecipeStep(_IDRecipeStep, _IDRecipeLanguage, _StepGroup, 
                                                                        _StepNumber,_RecipeStep, _StepTimeMinute, _StepImage, false);
             ManageUSPReturnValue _result = new ManageUSPReturnValue(_dtResult);

             return _result;
         }

         public ManageUSPReturnValue Delete()
         {
             ManageRecipesDAL _manageDAL = new ManageRecipesDAL();
             DataTable _dtResult = _manageDAL.USP_ManageRecipeStep(_IDRecipeStep, null, null, null,null, null, null, true);
             ManageUSPReturnValue _result = new ManageUSPReturnValue(_dtResult);

             return _result;
         }

         #endregion
     }

     public class RecipeVote
     {
         #region PrivateFileds

         private Guid _IDRecipeVote;
	     private Guid _IDRecipe;
	     private Guid _IDUser;
	     private DateTime _RecipeVoteDate;
	     private double _RecipeVote;

         #endregion

         #region PublicProperties

         public Guid IDRecipeVote
         {
             get { return _IDRecipeVote; }
         }

         public Guid IDRecipe
         {
             get { return _IDRecipe; }
             set { _IDRecipe = value; }
         }

         public Guid IDUser
         {
             get { return _IDUser; }
             set { _IDUser = value; }
         }

         public DateTime RecipeVoteDate
         {
             get { return _RecipeVoteDate; }
             set { _RecipeVoteDate = value; }
         }

         public double RecipeVoteValue
         {
             get { return _RecipeVote; }
             set { _RecipeVote = value; }
         }

         #endregion

         #region Costructors

         internal RecipeVote()
         {
         }

         public RecipeVote(Guid IDRecipe, Guid IDUser)
         {
             _IDRecipe = IDRecipe;
             _IDUser = IDUser;

             GetRecipesVotesDAL recipeVoteDAL = new GetRecipesVotesDAL();
             DataTable dtRecipeVote = recipeVoteDAL.GetUserVoteForRecipe(_IDRecipe, _IDUser);
             if (dtRecipeVote.Rows.Count > 0)
             {
                 _IDRecipeVote = dtRecipeVote.Rows[0].Field<Guid>("IDRecipeVote");
                 _RecipeVote = dtRecipeVote.Rows[0].Field<double>("RecipeVote");
                 _RecipeVoteDate = dtRecipeVote.Rows[0].Field<DateTime>("RecipeVoteDate");
             }
             else
             {
                 _RecipeVote = 0;
             }
         }

         public RecipeVote(Guid IDRecipe, Guid IDUser, double RecipeVote)
         {
             _IDRecipeVote = Guid.NewGuid();
             _IDRecipe = IDRecipe;
             _IDUser = IDUser;
             _RecipeVoteDate = DateTime.UtcNow;
             _RecipeVote = RecipeVote;
         }

         #endregion

         #region Methods

         public static double GetRecipeAvgVote(Guid IDRecipe)
         {
             try
             {
                 GetRecipesVotesDAL recipeDAL = new GetRecipesVotesDAL();
                 DataTable dtRecipe = recipeDAL.GetAvgRecipeVote(IDRecipe);
                 if (dtRecipe.Rows.Count > 0)
                 {
                     return dtRecipe.Rows[0].Field<double>("RecipeVote");
                 }
                 else
                 {
                     return 0;
                 }
             }
             catch
             {
                 return 0;
             }
         }

         public ManageUSPReturnValue Save()
         {
             ManageRecipesDAL recipeVoteDAL = new ManageRecipesDAL();
             DataTable dtRecipeVote = recipeVoteDAL.USP_ManageRecipeVote(_IDRecipeVote, _IDRecipe, _IDUser, 
                                                            _RecipeVoteDate, _RecipeVote, false);
             ManageUSPReturnValue _result = new ManageUSPReturnValue(dtRecipeVote);
             return _result;
         }

         public ManageUSPReturnValue Delete()
         {
             ManageRecipesDAL recipeVoteDAL = new ManageRecipesDAL();
             DataTable dtRecipeVote = recipeVoteDAL.USP_ManageRecipeVote(_IDRecipeVote, _IDRecipe, _IDUser,
                                                            _RecipeVoteDate, _RecipeVote, true);
             ManageUSPReturnValue _result = new ManageUSPReturnValue(dtRecipeVote);
             return _result;

         }

         #endregion
     }

     public class RecipeBook
     {
         #region PrivateFields
         
         private Guid _IDRecipeBookRecipe;
         private Guid _IDUser;
         private Guid _IDRecipe;
         private DateTime _RecipeAddedOn;
         private int _RecipeOrder;

         #endregion

         #region PublicProperties

         public Guid IDRecipeBookRecipe
         {
             get { return _IDRecipeBookRecipe; }
         }

         public Guid IDUser
         {
             get { return _IDUser; }
             set { _IDUser = value; }
         }

         public Guid IDRecipe
         {
             get { return _IDRecipe; }
             set { _IDRecipe = value; }
         }

         public DateTime RecipeAddedOn
         {
             get { return _RecipeAddedOn; }
         }

         public int RecipeOrder
         {
             get { return _RecipeOrder; }
             set { _RecipeOrder = value; }
         }

         #endregion

         public RecipeBook(Guid IDUser, Guid IDRecipe, int RecipeOrder)
         {
             _IDRecipeBookRecipe = Guid.NewGuid();
             _IDUser = IDUser;
             _IDRecipe = IDRecipe;
             _RecipeAddedOn = DateTime.UtcNow;
             _RecipeOrder = RecipeOrder;
         }

         public static DataTable GetRecipes(Guid IDUser, int RecipeType, int ShowFilter, string RecipeNameFilter, int RowOffSet, int FetchRows)
         {
             DataTable _dtRecipesInBook = null;
             try
             {
                 GetRecipesBooksRecipesDAL _dalRecipeBook = new GetRecipesBooksRecipesDAL();
                 _dtRecipesInBook = _dalRecipeBook.GetRecipesInRecipeBook(IDUser, RecipeType, ShowFilter, RecipeNameFilter,false,false,false,10000,1000,false,1, RowOffSet, FetchRows);
             }
             catch
             {
             }
             return _dtRecipesInBook;
         }

         public static DataTable GetRecipes(Guid IDUser, int RecipeType, int ShowFilter, string RecipeNameFilter, bool Vegan, 
                                                bool Vegetarian, bool GlutenFree, double LightThreshold, int QuickThreshold, 
                                                bool ShowDraft, int IDLanguage, int RowOffSet, int FetchRows)
         {
             DataTable _dtRecipesInBook = null;
             try
             {
                 GetRecipesBooksRecipesDAL _dalRecipeBook = new GetRecipesBooksRecipesDAL();
                 _dtRecipesInBook = _dalRecipeBook.GetRecipesInRecipeBook(IDUser, RecipeType, ShowFilter, RecipeNameFilter, 
                                                                            Vegan, Vegetarian, GlutenFree, LightThreshold, QuickThreshold, 
                                                                            ShowDraft, IDLanguage, RowOffSet, FetchRows);
             }
             catch
             {
             }
             return _dtRecipesInBook;
         }
         
        public static int NumberOfRecipesInsertedByUser(Guid IDUser)
        {
            GetRecipesBooksRecipesDAL dalRecipe = new GetRecipesBooksRecipesDAL();
            return MyConvert.ToInt32(dalRecipe.GetNumRecipesInRecipeBookByUser(IDUser).ToString(),0);
        }
        
         public static bool CheckRecipeIsInBook(Guid IDUser, Guid IDRecipe)
         {
             DataTable _dtRecipesInBook = null;
             try
             {
                 GetRecipesBooksRecipesDAL _dalRecipeBook = new GetRecipesBooksRecipesDAL();
                 _dtRecipesInBook = _dalRecipeBook.CheckRecipeIsInBook(IDUser, IDRecipe);
                 if (_dtRecipesInBook.Rows.Count > 0)
                 {
                     return true;
                 }
                 else
                 {
                     return false;
                 }
             }
             catch
             {
             }
             return false;
         }

         public ManageUSPReturnValue SaveRecipe()
         {
             ManageUSPReturnValue _result = null;
             try
             {
                 ManageRecipesDAL _ManageRecipeInRecipeBook = new ManageRecipesDAL();
                 _result = new ManageUSPReturnValue(_ManageRecipeInRecipeBook.USP_ManageRecipeBookRecipe(_IDRecipeBookRecipe, _IDUser, _IDRecipe, _RecipeAddedOn, _RecipeOrder, false));
             }
             catch
             {
                 _result = new ManageUSPReturnValue("RC-ER-0010", "", true);
             }
             return _result;
         }

         public ManageUSPReturnValue DeleteRecipe()
         {
             ManageUSPReturnValue _result = null;
             try
             {
                 ManageRecipesDAL _ManageRecipeInRecipeBook = new ManageRecipesDAL();
                 _result = new ManageUSPReturnValue(_ManageRecipeInRecipeBook.USP_ManageRecipeBookRecipe(_IDRecipeBookRecipe, _IDUser, _IDRecipe, _RecipeAddedOn, _RecipeOrder, true));
             }
             catch
             {
                 _result = new ManageUSPReturnValue("RC-ER-0010", "", true);

             }
             return _result;
         }
     }
}
