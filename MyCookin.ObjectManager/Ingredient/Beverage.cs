using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MyCookin.Common;
using MyCookin.ObjectManager.RecipeManager;
using MyCookin.ObjectManager.UserManager;
using MyCookin.DAL.Ingredient.ds_IngredientTableAdapters;
using MyCookin.DAL.Recipe.ds_RecipeTableAdapters;

namespace MyCookin.ObjectManager.IngredientManager
{
    public class Beverage
    {
        #region PrivateFileds

        private Guid _IDBeverage;

        #endregion

        #region PublicProperties

        public Guid IDBeverage
        {
            get { return _IDBeverage; }
        }

        #endregion

        #region Costructors

        public Beverage()
        {
        }

        public Beverage(Guid IDBeverage)
        {
            _IDBeverage = IDBeverage;
        }

        #endregion

        #region Methods



        #endregion

        #region Operators

        public static implicit operator Beverage(Guid guid)
        {
            Beverage beverage = new Beverage(guid);
            return beverage;
        }

        public static implicit operator Guid(Beverage beverage)
        {
            Guid guid = new Guid();
            if (beverage == null)
            {
                return guid;
            }
            else
            {
                return beverage.IDBeverage;
            }
        }

        public static bool operator ==(Beverage beverage1, Beverage beverage2)
        {
            if ((Object)beverage1 == null)
            {
                beverage1 = new Beverage(new Guid());
            }
            if ((Object)beverage2 == null)
            {
                beverage2 = new Beverage(new Guid());
            }
            if ((Object)beverage1 == null || (Object)beverage2 == null)
            {
                return (Object)beverage1 == (Object)beverage2;
            }
            else
            {
                return beverage1.IDBeverage == beverage2.IDBeverage;
            }
        }

        public static bool operator !=(Beverage beverage1, Beverage beverage2)
        {
            return !(beverage1 == beverage2);
        }

        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Recipe return false.
            Beverage beverage = obj as Beverage;
            if ((System.Object)beverage == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (IDBeverage == beverage.IDBeverage);
        }

        public bool Equals(Beverage beverage)
        {
            // If parameter is null return false:
            if ((object)beverage == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (IDBeverage == beverage.IDBeverage);
        }

        public override int GetHashCode()
        {
            return IDBeverage.GetHashCode();
        }

        #endregion
    }

    public class BeverageLanguage : IngredientLanguage
    {
        #region PrivateFileds

        private Guid _IDBeverageLanguage;
        private Beverage _IDBeverage;
        private string _BeverageInfoLanguage;

        #endregion

        #region PublicProperties

        public Guid IDBeverageLanguage
        {
            get {return _IDBeverageLanguage; }
        }

        public Beverage IDBeverage
        {
            get { return _IDBeverage; }
            set { _IDBeverage = value; }
        }

        public string BeverageInfoLanguage
        {
            get { return _BeverageInfoLanguage; }
            set { _BeverageInfoLanguage = value; }
        }

        #endregion

        #region Costructors

        public BeverageLanguage()
        {
        }

        public BeverageLanguage(Guid IDBeverage, int IDLanguage)
        {
            base._IDIngredient = IDBeverage;
            base.IDLanguage = IDLanguage;
            _IDBeverage = IDBeverage;
        }
        
        

        #endregion

        #region Methods

        public static List<BeverageLanguage> GetBeverageLanguageList(string BeverageName, int IDLanguage)
        {
            List<BeverageLanguage> _listBeverage = new List<BeverageLanguage>();
            GetIngredientsLanguagesDAL dalBeverageLang = new GetIngredientsLanguagesDAL();
            DataTable dtBeverageLang = dalBeverageLang.GetBeverageByName(BeverageName, IDLanguage);

            if (dtBeverageLang.Rows.Count > 0)
            {
                foreach (DataRow drBeverageLang in dtBeverageLang.Rows)
                {
                    BeverageLanguage _bevLang = new BeverageLanguage(drBeverageLang.Field<Guid>("IDIngredient"),IDLanguage);
                    _bevLang.QueryIngredientLanguageInfo();
                    _listBeverage.Add(_bevLang);
                }
            }

            return _listBeverage;
        }

        #endregion
    }

    public class BeverageRecipe
    {
        #region PrivateFileds

        private Guid _IDBeverageRecipe;
        private Recipe _IDRecipe;
        private Beverage _IDBeverage;
        private MyUser _IDUserSuggestedBy;
        private DateTime _DateSuggestion;
        private double _BeverageRecipeAvgRating;

        #endregion

        #region PublicProperties

        public Guid IDBeverageRecipe
        {
            get { return _IDBeverageRecipe; }
        }

        public Recipe IDRecipe
        {
            get { return _IDRecipe; }
            set { _IDRecipe = value; }
        }

        public Beverage IDBeverage
        {
            get { return _IDBeverage; }
            set { _IDBeverage = value; }
        }

        public MyUser IDUser
        {
            get { return _IDUserSuggestedBy; }
            set { _IDUserSuggestedBy = value; }
        }

        public DateTime DateSuggestion
        {
            get { return _DateSuggestion; }
            set { _DateSuggestion = value; }
        }

        public double AvgRating
        {
            get { return _BeverageRecipeAvgRating; }
            set { _BeverageRecipeAvgRating = value; }
        }

        #endregion

        #region Costructors

        public BeverageRecipe()
        {
        }

        public BeverageRecipe(Guid IDBeverageRecipe)
        {
            _IDBeverageRecipe = IDBeverageRecipe;
        }

        /// <summary>
        /// Create a new association Beverage/Recipe
        /// </summary>
        /// <param name="IDRecipe"></param>
        /// <param name="IDBeverage"></param>
        /// <param name="IDUser"></param>
        public BeverageRecipe(Recipe IDRecipe, Beverage IDBeverage, MyUser IDUser)
        {
            _IDBeverageRecipe = Guid.NewGuid();
            _IDRecipe = IDRecipe;
            _IDBeverage = IDBeverage;
            _IDUserSuggestedBy = IDUser;
            _DateSuggestion = DateTime.UtcNow;
            _BeverageRecipeAvgRating = 0;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get info from DB for a specific BeverageRecipe combination
        /// </summary>
        public void QueryBeverageInfo()
        {
            if (_IDBeverageRecipe != null && _IDBeverageRecipe!=new Guid())
            {
                GetBeveragesRecipesDAL dalBeverageRecipe = new GetBeveragesRecipesDAL();
                DataTable dtBeverageRecipe = dalBeverageRecipe.GetBeverageRecipeByID(_IDBeverageRecipe);

                if (dtBeverageRecipe.Rows.Count > 0)
                {
                    _IDRecipe = dtBeverageRecipe.Rows[0].Field<Guid>("IDRecipe");
                    _IDBeverage = dtBeverageRecipe.Rows[0].Field<Guid>("IDBeverage");
                    _IDUserSuggestedBy = dtBeverageRecipe.Rows[0].Field<Guid>("IDUserSuggestedBy");
                    _DateSuggestion = dtBeverageRecipe.Rows[0].Field<DateTime>("DateSuggestion");
                    _BeverageRecipeAvgRating = dtBeverageRecipe.Rows[0].Field<double>("BeverageRecipeAvgRating");

                }
            }
        }

        public ManageUSPReturnValue Save()
        {
            ManageUSPReturnValue _return;
            try
            {
                ManageIngredientDAL dalSave = new ManageIngredientDAL();
                DataTable dtSave = dalSave.USP_ManageRecipeBeverage(_IDBeverageRecipe, _IDRecipe, _IDBeverage, _IDUserSuggestedBy.IDUser,
                                                                       _DateSuggestion, _BeverageRecipeAvgRating, false);
                _return = new ManageUSPReturnValue(dtSave);
            }
            catch
            {
                _return = new ManageUSPReturnValue("RC-ER-9999", "", true);
            }

            return _return;
        }

        public ManageUSPReturnValue Delete()
        {
            ManageUSPReturnValue _return;
            try
            {
                ManageIngredientDAL dalSave = new ManageIngredientDAL();
                DataTable dtSave = dalSave.USP_ManageRecipeBeverage(_IDBeverageRecipe, null, null, null,
                                                                       null, null, true);
                _return = new ManageUSPReturnValue(dtSave);
            }
            catch
            {
                _return = new ManageUSPReturnValue("RC-ER-9999", "", true);
            }

            return _return;
        }

        /// <summary>
        /// Get a list of prefered beverage for a recipe
        /// </summary>
        /// <param name="IDRecipe">ID of recipe</param>
        /// <returns></returns>
        public static List<BeverageRecipe> GetBeverageForRecipe(Guid IDRecipe)
        {
            List<BeverageRecipe> _BeverageLangList = new List<BeverageRecipe>();

             GetBeveragesRecipesDAL dalBeverageRecipe = new GetBeveragesRecipesDAL();
             DataTable dtBeverageRecipe = dalBeverageRecipe.GetBeveragesByIDRecipe(IDRecipe);

             if (dtBeverageRecipe.Rows.Count > 0)
             {
                 for (int i = 0; i < dtBeverageRecipe.Rows.Count; i++)
                 {
                     BeverageRecipe _beverageRecipe = new BeverageRecipe(dtBeverageRecipe.Rows[i].Field<Guid>("IDBeverageRecipe"));
                     _beverageRecipe.QueryBeverageInfo();
                     _BeverageLangList.Add(_beverageRecipe);
                 }
             }

            return _BeverageLangList;
        }

        /// <summary>
        /// TO BE IMPLEMENTED
        /// </summary>
        /// <param name="IDBeverage"></param>
        /// <param name="IDLanguage"></param>
        /// <returns></returns>
        public static List<BeverageRecipe> GetRecipeForBeverage(Guid IDBeverage)
        {
            List<BeverageRecipe> _RecipeLangList = new List<BeverageRecipe>();

            return _RecipeLangList;
        }

        #endregion
    }
}
