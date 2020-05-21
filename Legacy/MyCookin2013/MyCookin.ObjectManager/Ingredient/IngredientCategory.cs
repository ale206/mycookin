using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MyCookin.DAL.Ingredient.ds_IngredientTableAdapters;
using MyCookin.Common;

namespace MyCookin.ObjectManager.IngredientManager
{
    public class IngredientCategory
    {
        #region PrivateFields

        protected int _IDIngredientCategory;
        protected int? _IDIngredientCategoryFather;
        protected bool _Enabled;

        #endregion

        #region PublicProperties

        public int IDIngredientCategory
        {
            get { return _IDIngredientCategory; }
        }
        public int? IDIngredientCategoryFather
        {
            get { return _IDIngredientCategoryFather; }
        }
        public bool Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; }
        }

        #endregion

        #region Constructors

        protected IngredientCategory()
        {
        }

        public IngredientCategory(int IDIngredientCategory)
        {
            _IDIngredientCategory = IDIngredientCategory;
            GetIngredientsCategoriesDAL dalIngredientcategory = new GetIngredientsCategoriesDAL();

            DataTable dtIngredientcategory = new DataTable();
            dtIngredientcategory = dalIngredientcategory.GetIngredientCategoryByID(IDIngredientCategory);

            if (dtIngredientcategory.Rows.Count > 0)
            {
                _IDIngredientCategoryFather = dtIngredientcategory.Rows[0].Field<int>("IDIngredientCategoryFather");
                _Enabled = dtIngredientcategory.Rows[0].Field<bool>("Enabled");
            }
        }

        #endregion

        #region Methods
        #endregion
    }
    public class IngredientCategoryLanguage : IngredientCategory
    {
        #region PrivateFields

        private int _IDIngredientCategoryLanguage;
        private int _IDLanguage;
        private string _IngredientCategoryLanguage;
        private string _IngredientCategoryLanguageDesc;

        #endregion

        #region PublicProperties

        public int IDIngredientCategoryLanguage
        {
            get { return _IDIngredientCategoryLanguage; }
        }
        public int IDLanguage
        {
            get { return _IDLanguage; }
            set { _IDLanguage = value; }
        }
        public string IngredientCategoriesLanguage
        {
            get { return _IngredientCategoryLanguage; }
            set { _IngredientCategoryLanguage = value; }
        }
        public string IngredientCategoryLanguageDesc
        {
            get { return _IngredientCategoryLanguageDesc; }
            set { _IngredientCategoryLanguageDesc = value; }
        }

        #endregion

        #region Constructors

        public IngredientCategoryLanguage(int IDIngredientCategory, int IDLanguage)
        {
            GetIngredientsCategoriesLanguagesDAL IngredientCategoryByLang = new GetIngredientsCategoriesLanguagesDAL();
            DataTable IngredientCategoryByLangInfo = new DataTable();
            IngredientCategoryByLangInfo = IngredientCategoryByLang.GetIngrCatLangByIDIngrCatAndIDLang(IDIngredientCategory, IDLanguage);

            if (IngredientCategoryByLangInfo.Rows.Count > 0)
            {
                base._IDIngredientCategory = IngredientCategoryByLangInfo.Rows[0].Field<int>("IDIngredientCategory");
                base._IDIngredientCategoryFather = IngredientCategoryByLangInfo.Rows[0].Field<int>("IDIngredientCategoryFather");
                base._Enabled = IngredientCategoryByLangInfo.Rows[0].Field<bool>("Enabled");
                _IDIngredientCategoryLanguage = IngredientCategoryByLangInfo.Rows[0].Field<int>("IDIngredientCategoryLanguage");
                _IDLanguage = IngredientCategoryByLangInfo.Rows[0].Field<int>("IDLanguage");
                _IngredientCategoryLanguage = IngredientCategoryByLangInfo.Rows[0].Field<string>("IngredientCategoryLanguage");
                _IngredientCategoryLanguageDesc = IngredientCategoryByLangInfo.Rows[0].Field<string>("IngredientCategoryLanguageDesc");

                GetIngredientsCategoriesDAL IngredientCategoryComplete = new GetIngredientsCategoriesDAL();
                DataTable dtIngredientCategoryComplete = new DataTable();
                dtIngredientCategoryComplete = IngredientCategoryComplete.USP_GetAllCategoryByIDLanguageIDFatherCategory(IDLanguage, IDIngredientCategory, 0);

                _IngredientCategoryLanguage = dtIngredientCategoryComplete.Rows[0].Field<string>("IngredientCategoryLanguage");
            }

        }

        #endregion

        #region Methods

        public static DataTable GetAllIngredientCategoriesByLang(int IDLanguage)
        {
            GetIngredientsCategoriesLanguagesDAL AllIngredientCategoriesByLang = new GetIngredientsCategoriesLanguagesDAL();
            return AllIngredientCategoriesByLang.GetAllIngredientCategoriesByIDLang(IDLanguage);
        }

        public static DataTable GetCompleteIngedientCategoryTreeByLang(int IDLanguage)
        {
            GetIngredientsCategoriesDAL AllIngredientCategoryComplete = new GetIngredientsCategoriesDAL();
            int RootIngrCategoryFather = Convert.ToInt32(AppConfig.GetValue("RootIngredientCategoryID", AppDomain.CurrentDomain));
            return AllIngredientCategoryComplete.USP_GetAllCategoryByIDLanguageIDFatherCategory(IDLanguage, RootIngrCategoryFather, 20);
        }

        public static string GetIngredientCategoryLang(int idCategory, int idLang)
        {
            string stgReturn = "";
            GetIngredientsCategoriesDAL IngredientCategoryLang = new GetIngredientsCategoriesDAL();
            DataTable dtIngredientCategoryLang = IngredientCategoryLang.USP_GetAllCategoryByIDLanguageIDFatherCategory(idLang, idCategory, 1);

            if (dtIngredientCategoryLang.Rows.Count > 1)
            {
                //There are 2 rows because the first is the null row!!!
                stgReturn = dtIngredientCategoryLang.Rows[1].Field<string>("IngredientCategoryLanguage");
            }

            return stgReturn;
        }

        #endregion
    }

}
