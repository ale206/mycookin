using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MyCookin.DAL.Ingredient.ds_IngredientTableAdapters;
using MyCookin.Common;

namespace MyCookin.ObjectManager.IngredientManager
{
    public class IngredientQuantityType
    {
        #region PrivateFields

        protected int _IDIngredientQuantityType;
        protected bool _isWeight;
        protected bool _isLiquid;
        protected bool _isPiece;
        protected bool _isStandardQuantityType;
        protected double? _NoStdAvgWeight;
        protected bool _EnabledToUser;
        protected bool _ShowInIngredientList;

        #endregion

        #region PublicProterties

        public int IDIngredientQuantityType
        {
            get { return _IDIngredientQuantityType; }
        }
        public bool isWeight
        {
            get { return _isWeight; }
            set { _isWeight = value; }
        }
        public bool isLiquid
        {
            get { return _isLiquid; }
            set { _isLiquid = value; }
        }
        public bool isPiece
        {
            get { return _isPiece; }
            set { _isPiece = value; }
        }
        public bool isStandardQuantityType
        {
            get { return _isStandardQuantityType; }
            set { _isStandardQuantityType = value; }
        }
        public double? NoStdAvgWeight
        {
            get { return _NoStdAvgWeight; }
            set { _NoStdAvgWeight = value; }
        }
        public bool EnabledToUser
        {
            get { return _EnabledToUser; }
            set { _EnabledToUser = value; }
        }
        public bool ShowInIngredientList
        {
            get { return _ShowInIngredientList; }
            set { _ShowInIngredientList = value; }
        }

        #endregion

        #region Costructors

        protected IngredientQuantityType()
        {
        }


        public IngredientQuantityType(int IDIngredientQuantityType)
        {
            _IDIngredientQuantityType = IDIngredientQuantityType;
        }

        #endregion

        #region Methods

        public void QueryDbInfo()
        {
            GetIngredientsQuantityTypesDAL dalIngredientQuantityType = new GetIngredientsQuantityTypesDAL();

            DataTable dtIngredientQuantityType = new DataTable();
            dtIngredientQuantityType = dalIngredientQuantityType.GetIngredientsQuantityTypesByID(_IDIngredientQuantityType);

            if (dtIngredientQuantityType.Rows.Count > 0)
            {
                _IDIngredientQuantityType = dtIngredientQuantityType.Rows[0].Field<int>("IDIngredientQuantityType");
                _isWeight = dtIngredientQuantityType.Rows[0].Field<bool>("isWeight");
                _isLiquid = dtIngredientQuantityType.Rows[0].Field<bool>("isLiquid");
                _isPiece = dtIngredientQuantityType.Rows[0].Field<bool>("isPiece");
                _isStandardQuantityType = dtIngredientQuantityType.Rows[0].Field<bool>("isStandardQuantityType");
                _NoStdAvgWeight = dtIngredientQuantityType.Rows[0].Field<double?>("NoStdAvgWeight");
                _EnabledToUser = dtIngredientQuantityType.Rows[0].Field<bool>("EnabledToUser");
                _ShowInIngredientList = dtIngredientQuantityType.Rows[0].Field<bool>("ShowInIngredientList");
            }
        }

        public string GetIngredientQuantityTypeName(int IDLanguage, bool Plural)
        {
            string IngredientQuantityTypeName = "";
            string DbFieldName = "IngredientQuantityTypeSingular";
            if (Plural)
            {
                DbFieldName = "IngredientQuantityTypePlural";
            }
            GetIngredientsQuantityTypesLanguagesDAL dalIngredientsQuantityTypesLanguages = new GetIngredientsQuantityTypesLanguagesDAL();

            DataTable dtIngredientsQuantityTypesLanguages = new DataTable();
            dtIngredientsQuantityTypesLanguages = dalIngredientsQuantityTypesLanguages.USP_GetIngredientsQuantityTypesLangByID(IDIngredientQuantityType, IDLanguage);

            if (dtIngredientsQuantityTypesLanguages.Rows.Count > 0)
            {
                IngredientQuantityTypeName = dtIngredientsQuantityTypesLanguages.Rows[0].Field<string>(DbFieldName);
            }

            return IngredientQuantityTypeName;
        }

        #endregion

        #region Operators

        public static implicit operator IngredientQuantityType(int value)
        {
            IngredientQuantityType qtaType = new IngredientQuantityType(value);
            return qtaType;
        }

        public static implicit operator int(IngredientQuantityType qtaType)
        {
            if (qtaType == null)
            {
                return 0;
            }
            else
            {
                return qtaType.IDIngredientQuantityType;
            }
        }
        #endregion
    }


    public class IngredientQuantityTypeLanguage : IngredientQuantityType
    {
        #region PrivateFields

        private int _IDIngredientQuantityTypeLanguage;
        private string _IngredientQuantityTypeSingular;
        private string _IngredientQuantityTypePlural;
        private double _ConvertionRatio;
        private string _IngredientQuantityTypeX1000Singular;
        private string _IngredientQuantityTypeX1000Plural;
        private string _IngredientQuantityTypeWordsShowBefore;
        private string _IngredientQuantityTypeWordsShowAfter;

        #endregion

        #region PublicFields

        public int IDIngredientQuantityTypeLanguage
        {
            get { return _IDIngredientQuantityTypeLanguage; }
        }
        public string IngredientQuantityTypeSingular
        {
            get { return _IngredientQuantityTypeSingular; }
            set { _IngredientQuantityTypeSingular = value; }
        }
        public string IngredientQuantityTypePlural
        {
            get { return _IngredientQuantityTypePlural; }
            set { _IngredientQuantityTypePlural = value; }
        }
        public double ConvertionRatio
        {
            get { return _ConvertionRatio; }
            set { _ConvertionRatio = value; }
        }
        public string IngredientQuantityTypeX1000Singular
        {
            get { return _IngredientQuantityTypeX1000Singular; }
            set { _IngredientQuantityTypeX1000Singular = value; }
        }
        public string IngredientQuantityTypeX1000Plural
        {
            get { return _IngredientQuantityTypeX1000Plural; }
            set { _IngredientQuantityTypeX1000Plural = value; }
        }
        public string IngredientQuantityTypeWordsShowBefore
        {
            get { return _IngredientQuantityTypeWordsShowBefore; }
            set { _IngredientQuantityTypeWordsShowBefore = value; }
        }
        public string IngredientQuantityTypeWordsShowAfter
        {
            get { return _IngredientQuantityTypeWordsShowAfter; }
            set { _IngredientQuantityTypeWordsShowAfter = value; }
        }

        #endregion

        #region Costructors

        public IngredientQuantityTypeLanguage(int IDIngredientQuantityType, int IDLanguage)
        {
            _IDIngredientQuantityType = IDIngredientQuantityType;

            GetIngredientsQuantityTypesLanguagesDAL dalIngredientsQuantityTypesLanguages = new GetIngredientsQuantityTypesLanguagesDAL();

            DataTable dtIngredientsQuantityTypesLanguages = new DataTable();
            dtIngredientsQuantityTypesLanguages = dalIngredientsQuantityTypesLanguages.USP_GetIngredientsQuantityTypesLangByID(IDIngredientQuantityType, IDLanguage);

            if (dtIngredientsQuantityTypesLanguages.Rows.Count > 0)
            {
                _IDIngredientQuantityTypeLanguage = dtIngredientsQuantityTypesLanguages.Rows[0].Field<int>("IDIngredientQuantityTypeLanguage");
                _IngredientQuantityTypeSingular = dtIngredientsQuantityTypesLanguages.Rows[0].Field<string>("IngredientQuantityTypeSingular");
                _IngredientQuantityTypePlural = dtIngredientsQuantityTypesLanguages.Rows[0].Field<string>("IngredientQuantityTypePlural");
                _ConvertionRatio = dtIngredientsQuantityTypesLanguages.Rows[0].Field<double>("ConvertionRatio");
                _IngredientQuantityTypeX1000Singular = dtIngredientsQuantityTypesLanguages.Rows[0].Field<string>("IngredientQuantityTypeX1000Singular");
                _IngredientQuantityTypeX1000Plural = dtIngredientsQuantityTypesLanguages.Rows[0].Field<string>("IngredientQuantityTypeX1000Plural");
                _IngredientQuantityTypeWordsShowBefore = dtIngredientsQuantityTypesLanguages.Rows[0].Field<string>("IngredientQuantityTypeWordsShowBefore");
                _IngredientQuantityTypeWordsShowAfter = dtIngredientsQuantityTypesLanguages.Rows[0].Field<string>("IngredientQuantityTypeWordsShowAfter");
            }

        }

        public IngredientQuantityTypeLanguage()
        {
        }

        #endregion

        #region Methods

        public void QueryIngredientsQuantityTypesBaseInfo()
        {
            IngredientQuantityType baseIngrQtaType = new IngredientQuantityType(_IDIngredientQuantityType);
            baseIngrQtaType.QueryDbInfo();
            _isWeight=baseIngrQtaType.isWeight;
            _isLiquid=baseIngrQtaType.isLiquid;
            _isPiece=baseIngrQtaType.isPiece;
            _isStandardQuantityType=baseIngrQtaType.isStandardQuantityType;
            _NoStdAvgWeight=baseIngrQtaType.NoStdAvgWeight;
            _EnabledToUser = baseIngrQtaType.EnabledToUser;
            _ShowInIngredientList = baseIngrQtaType.ShowInIngredientList;
        }

        public static DataTable GetAllGetIngredientsQuantityTypes(int IDLang)
        {
            GetIngredientsQuantityTypesLanguagesDAL dalIngredientsQuantityTypesLanguages = new GetIngredientsQuantityTypesLanguagesDAL();
            return dalIngredientsQuantityTypesLanguages.USP_GetAllIngredientsQuantityTypesByIDLang(IDLang);
        }

        public static List<IngredientQuantityTypeLanguage > GetIngredientQtaTypeLangList(string IDIngredient, int IDLanguage)
        {
            GetIngredientsQuantityTypesLanguagesDAL dalIngredientQtaTypeLang = new GetIngredientsQuantityTypesLanguagesDAL();

            DataTable dtIngredientQtaTypeLang = dalIngredientQtaTypeLang.GetAllowedQtaTypeByIDIngr(new Guid(IDIngredient), IDLanguage);

            List<IngredientQuantityTypeLanguage> IngredientQtaTypeLangList = new List<IngredientQuantityTypeLanguage>();

            if (dtIngredientQtaTypeLang.Rows.Count > 0)
            {
                for (int i = 0; i < dtIngredientQtaTypeLang.Rows.Count; i++)
                {
                    IngredientQtaTypeLangList.Add(new IngredientQuantityTypeLanguage()
                    {
                        _IDIngredientQuantityType = dtIngredientQtaTypeLang.Rows[i].Field<int>("IDIngredientQuantityType"),
                        _IngredientQuantityTypeSingular = dtIngredientQtaTypeLang.Rows[i].Field<string>("IngredientQuantityTypeSingular"),
                    });
                }
            }

            return IngredientQtaTypeLangList;
        }

        #endregion


    }
}
