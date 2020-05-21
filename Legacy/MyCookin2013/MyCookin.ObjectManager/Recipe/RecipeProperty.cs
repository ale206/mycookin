using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MyCookin.DAL.Recipe.ds_RecipeTableAdapters;
using MyCookin.Common;

namespace MyCookin.ObjectManager.RecipeManager
{
    public class RecipePropertyType
    {
        #region PrivateFields

        private int _IDRecipePropertyType;
        private bool _isDishType;
        private bool _isCookingType;
        private bool _isColorType;
        private bool _isEatType;
        private bool _isUseType;
        private bool _isPeriodType;
        private int _OrderPosition;
        private bool _Enabled;
        private int _IDLanguage;
        private string _RecipePropertyType;
        private string _RecipePropertyTypeToolTip;

        #endregion

        #region PublicPorperties

        public int IDRecipePropertyType
        {
            get { return _IDRecipePropertyType; }
        }
        public bool isDishType
        {
            get { return _isDishType; }
            set { _isDishType = value; }
        }
        public bool isCookingType
        {
            get { return _isCookingType; }
            set { _isCookingType = value; }
        }
        public bool isColorType
        {
            get { return _isColorType; }
            set { _isColorType = value; }
        }
        public bool isEatType
        {
            get { return _isEatType; }
            set { _isEatType = value; }
        }
        public bool isUseType
        {
            get { return _isUseType; }
            set { _isUseType = value; }
        }
        public bool isPeriodType
        {
            get { return _isPeriodType; }
            set { _isPeriodType = value; }
        }
        public int OrderPosition
        {
            get { return _OrderPosition; }
            set { _OrderPosition = value; }
        }
        public bool Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; }
        }
        public int IDLanguage
        {
            get { return _IDLanguage; }
        }
        public string RecipePropType
        {
            get { return _RecipePropertyType; }
            set { _RecipePropertyType = value; }
        }
        public string RecipePropertyTypeToolTip
        {
            get { return _RecipePropertyTypeToolTip; }
            set { _RecipePropertyTypeToolTip = value; }
        }

        #endregion

        #region Costructors

        protected RecipePropertyType()
        {
        }

        public RecipePropertyType(int IDRecipePropertyType, int IDLanguage)
        {
            _IDRecipePropertyType = IDRecipePropertyType;
            _IDLanguage = IDLanguage;

            GetRecipePropertiesTypesDAL recipePropTypeDAL = new GetRecipePropertiesTypesDAL();
            DataTable dtRecipePropType = recipePropTypeDAL.GetPropTypeByIDPropIDLang(_IDRecipePropertyType, _IDLanguage);

            if (dtRecipePropType.Rows.Count > 0)
            {
                _isDishType = dtRecipePropType.Rows[0].Field<bool>("isDishType");
                _isCookingType = dtRecipePropType.Rows[0].Field<bool>("isCookingType");
                _isColorType = dtRecipePropType.Rows[0].Field<bool>("isColorType");
                _isEatType = dtRecipePropType.Rows[0].Field<bool>("isEatType");
                _isUseType = dtRecipePropType.Rows[0].Field<bool>("isUseType");
                _isPeriodType = dtRecipePropType.Rows[0].Field<bool>("isPeriodType");
                _OrderPosition = dtRecipePropType.Rows[0].Field<int>("OrderPosition");
                _Enabled = dtRecipePropType.Rows[0].Field<bool>("Enabled");
                _RecipePropertyType = dtRecipePropType.Rows[0].Field<string>("RecipePropertyType");
                _RecipePropertyTypeToolTip = dtRecipePropType.Rows[0].Field<string>("RecipePropertyTypeToolTip");
            }
        }

        #endregion

        #region Methods

        //public static DataTable GetAllRecipePropertyTypeList(int IDLanguage)
        //{
        //    GetRecipePropertiesTypesDAL recipePropTypeDAL = new GetRecipePropertiesTypesDAL();
        //    return recipePropTypeDAL.GetAllPropertyTypeEnabledByIDLang(IDLanguage);
        //}

        public static List<RecipePropertyType> GetAllRecipePropertyTypeList(int IDLanguage)
        {
            GetRecipePropertiesTypesDAL recipePropTypeDAL = new GetRecipePropertiesTypesDAL();

            DataTable dtRecipePropType = recipePropTypeDAL.GetAllPropertyTypeEnabledByIDLang(IDLanguage);

            List<RecipePropertyType> RecipePropertyTypeList = new List<RecipePropertyType>();

            if (dtRecipePropType.Rows.Count > 0)
            {
                for (int i = 0; i < dtRecipePropType.Rows.Count; i++)
                {
                    RecipePropertyTypeList.Add(new RecipePropertyType()
                    {
                        _IDRecipePropertyType = dtRecipePropType.Rows[i].Field<int>("IDRecipePropertyType"),
                        _Enabled = dtRecipePropType.Rows[i].Field<bool>("Enabled"),
                        _RecipePropertyType = dtRecipePropType.Rows[i].Field<string>("RecipePropertyType"),
                        _RecipePropertyTypeToolTip = dtRecipePropType.Rows[i].Field<string>("RecipePropertyTypeToolTip"),
                    });
                }
            }

            return RecipePropertyTypeList;
        }

        #endregion

    }

    public class RecipeProperty
    {
        #region PrivateFields

        private int _IDRecipeProperty;
        private RecipePropertyType _RecipePropType;
        private int _OrderPosition;
        private bool _Enabled;
        private int _IDLanguage;
        private string _RecipeProperty;
        private string _RecipePropertyToolTip;

        #endregion

        #region PublicFields

        public int IDRecipeProperty
        {
            get { return _IDRecipeProperty; }
        }
        public RecipePropertyType RecipePropType
        {
            get { return _RecipePropType; }
            set { _RecipePropType = value; }
        }
        public int OrderPosition
        {
            get { return _OrderPosition; }
            set { _OrderPosition = value; }
        }
        public bool Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; }
        }
        public int IDLanguage
        {
            get { return _IDLanguage; }
        }
        public string RecipeProp
        {
            get { return _RecipeProperty; }
            set { _RecipeProperty = value; }
        }
        public string RecipePropertyToolTip
        {
            get { return _RecipePropertyToolTip; }
            set { _RecipePropertyToolTip = value; }
        }

        #endregion

        #region Costructors

        protected RecipeProperty()
        {
        }

        public RecipeProperty(int IDRecipeProperty, int IDLanguage)
        {
            _IDRecipeProperty = IDRecipeProperty;
            _IDLanguage = IDLanguage;

            GetRecipePropertiesDAL recipePropDAL = new GetRecipePropertiesDAL();
            DataTable dtRecipeProp = recipePropDAL.GetPropertyByIDPropIDLang(_IDRecipeProperty, _IDLanguage);

            if (dtRecipeProp.Rows.Count > 0)
            {
                int _IDRecipePropType;
                _IDRecipePropType = dtRecipeProp.Rows[0].Field<int>("IDRecipePropertyType");
                _OrderPosition = dtRecipeProp.Rows[0].Field<int>("OrderPosition");
                _Enabled = dtRecipeProp.Rows[0].Field<bool>("Enabled");
                _RecipeProperty = dtRecipeProp.Rows[0].Field<string>("RecipeProperty");
                _RecipePropertyToolTip = dtRecipeProp.Rows[0].Field<string>("RecipePropertyToolTip");

                _RecipePropType = new RecipePropertyType(_IDRecipePropType, _IDLanguage);
            }

        }

        #endregion

        #region Methods

        //public static DataTable GetAllRecipePropertyListByType(int IDRecipePropertyType, int IDLanguage)
        //{
        //    GetRecipePropertiesDAL recipePropDAL = new GetRecipePropertiesDAL();
        //    return recipePropDAL.GetAllPropertyByIDTypeIDLang(IDRecipePropertyType, IDLanguage);
        //}


        public static List<RecipeProperty> GetAllRecipePropertyListByType(int IDRecipePropertyType, int IDLanguage)
        {
            GetRecipePropertiesDAL recipePropDAL = new GetRecipePropertiesDAL();

            DataTable dtRecipeProp = recipePropDAL.GetAllPropertyByIDTypeIDLang(IDRecipePropertyType, IDLanguage);

            List<RecipeProperty> RecipePropertyList = new List<RecipeProperty>();

            if (dtRecipeProp.Rows.Count > 0)
            {
                for (int i = 0; i < dtRecipeProp.Rows.Count; i++)
                {
                    RecipePropertyList.Add(new RecipeProperty()
                    {
                        _IDRecipeProperty = dtRecipeProp.Rows[i].Field<int>("IDRecipeProperty"),
                        _Enabled = dtRecipeProp.Rows[i].Field<bool>("Enabled"),
                        _RecipeProperty = dtRecipeProp.Rows[i].Field<string>("RecipeProperty"),
                        _RecipePropertyToolTip = dtRecipeProp.Rows[i].Field<string>("RecipePropertyToolTip"),
                        _OrderPosition = dtRecipeProp.Rows[i].Field<int>("OrderPosition"),
                    });
                }
            }

            return RecipePropertyList;
        }

        public static List<RecipeProperty> SearchPropertiesValues(int IDRecipePropertyType, int IDLanguage, string RecipeProperty)
        {
            GetRecipePropertiesDAL recipePropDAL = new GetRecipePropertiesDAL();

            DataTable dtRecipeProp = recipePropDAL.SearchRecipeProperiesByName(IDRecipePropertyType, IDLanguage, RecipeProperty);

            List<RecipeProperty> RecipePropertyList = new List<RecipeProperty>();

            if (dtRecipeProp.Rows.Count > 0)
            {
                for (int i = 0; i < dtRecipeProp.Rows.Count; i++)
                {
                    RecipePropertyList.Add(new RecipeProperty()
                    {
                        _IDRecipeProperty = dtRecipeProp.Rows[i].Field<int>("IDRecipeProperty"),
                        _Enabled = dtRecipeProp.Rows[i].Field<bool>("Enabled"),
                        _RecipeProperty = dtRecipeProp.Rows[i].Field<string>("RecipeProperty"),
                        _RecipePropertyToolTip = dtRecipeProp.Rows[i].Field<string>("RecipePropertyToolTip"),
                        _OrderPosition = dtRecipeProp.Rows[i].Field<int>("OrderPosition"),
                    });
                }
            }

            return RecipePropertyList;
        }

        #endregion

        #region Operators
        
        public static implicit operator int(RecipeProperty recipeProp)
        {
            int _int = 0;
            if (recipeProp == null)
            {
                return _int;
            }
            else
            {
                return recipeProp.IDRecipeProperty;
            }
        }

        public static bool operator ==(RecipeProperty recipeProp1, RecipeProperty recipeProp2)
        {
            if ((Object)recipeProp1 == null)
            {
                recipeProp1 = new RecipeProperty();
            }
            if ((Object)recipeProp2 == null)
            {
                recipeProp2 = new RecipeProperty();
            }
            if ((Object)recipeProp1 == null || (Object)recipeProp2 == null)
            {
                return (Object)recipeProp1 == (Object)recipeProp2;
            }
            else
            {
                return recipeProp1.IDRecipeProperty == recipeProp2.IDRecipeProperty;
            }
        }

        public static bool operator !=(RecipeProperty recipeProp1, RecipeProperty recipeProp2)
        {
            return !(recipeProp1 == recipeProp2);
        }

        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Recipe return false.
            RecipeProperty recipeProp = obj as RecipeProperty;
            if ((System.Object)recipeProp == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (IDRecipeProperty == recipeProp.IDRecipeProperty);
        }

        public bool Equals(RecipeProperty recipeProp)
        {
            // If parameter is null return false:
            if ((object)recipeProp == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (IDRecipeProperty == recipeProp.IDRecipeProperty);
        }

        public override int GetHashCode()
        {
            return IDRecipeProperty.GetHashCode();
        }

        #endregion
    }

    public class RecipePropertyValue
    {
        #region PrivateFileds

        private Guid _IDRecipePropertyValue;
        private Recipe _IDRecipe;
        private RecipeProperty _RecipeProp;
        private bool _Value;

        #endregion

        #region PublicProperties

        public Guid IDRecipePropertyValue
        {
            get { return _IDRecipePropertyValue; }
        }
        public Recipe IDRecipe
        {
            get { return _IDRecipe; }
        }
        public RecipeProperty IDRecipeProp
        {
            get { return _RecipeProp; }
            set { _RecipeProp = value; }
        }
        public bool Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        #endregion

        #region Costructors

        public RecipePropertyValue(Guid IDRecipe, RecipeProperty RecipeProperty, bool Value)
        {
            _IDRecipePropertyValue = Guid.NewGuid();
            _IDRecipe = IDRecipe;
            _RecipeProp = RecipeProperty;
            _Value = Value;

        }

        public RecipePropertyValue(Guid PropertyValue, int IDLanguage)
        {
            _IDRecipePropertyValue = PropertyValue;

            GetRecipesPropertiesValuesDAL recipePropValueDAL = new GetRecipesPropertiesValuesDAL();
            DataTable dtrecipePropValue = recipePropValueDAL.GetPropValueByIDRecipePropValue(_IDRecipePropertyValue);

            if (dtrecipePropValue.Rows.Count > 0)
            {
                int _IDRecipeProperty;
                _IDRecipePropertyValue = dtrecipePropValue.Rows[0].Field<Guid>("IDRecipePropertyValue");
                _IDRecipe = dtrecipePropValue.Rows[0].Field<Guid>("IDRecipe");
                _IDRecipeProperty = dtrecipePropValue.Rows[0].Field<int>("IDRecipeProperty");
                _Value = dtrecipePropValue.Rows[0].Field<bool>("Value");

                _RecipeProp = new RecipeProperty(_IDRecipeProperty, IDLanguage);

            }
        }

        RecipePropertyValue()
        {
        }

        #endregion

        #region Methods

        public static DataTable GetRecipeListByPropertyValue(int RecipeProp, bool value)
        {
            GetRecipesPropertiesValuesDAL recipePropValueDAL = new GetRecipesPropertiesValuesDAL();
            return recipePropValueDAL.GetRecipeByPropertyValue(RecipeProp, value);
        }

        public ManageUSPReturnValue Save()
        {
            ManageRecipesDAL manageDAL = new ManageRecipesDAL();
            DataTable _dtReturn = manageDAL.USP_ManageRecipePropertyValue(_IDRecipePropertyValue, _IDRecipe.IDRecipe, 
                                                                            _RecipeProp, _Value, false);
            ManageUSPReturnValue _result = new ManageUSPReturnValue(_dtReturn);
            return _result;
        }

        public ManageUSPReturnValue Delete()
        {
            ManageRecipesDAL manageDAL = new ManageRecipesDAL();
            DataTable _dtReturn = manageDAL.USP_ManageRecipePropertyValue(_IDRecipePropertyValue, null,null, null, true);
            ManageUSPReturnValue _result = new ManageUSPReturnValue(_dtReturn);
            return _result;
        }

        #endregion

        #region Operators

        public static bool operator ==(RecipePropertyValue recipePropVal1, RecipePropertyValue recipePropVal2)
        {
            if ((Object)recipePropVal1 == null)
            {
                recipePropVal1 = new RecipePropertyValue();
            }
            if ((Object)recipePropVal2 == null)
            {
                recipePropVal2 = new RecipePropertyValue();
            }
            if ((Object)recipePropVal1 == null || (Object)recipePropVal2 == null)
            {
                return (Object)recipePropVal1 == (Object)recipePropVal2;
            }
            else
            {
                return recipePropVal1.IDRecipePropertyValue == recipePropVal2.IDRecipePropertyValue;
            }
        }

        public static bool operator !=(RecipePropertyValue recipePropVal1, RecipePropertyValue recipePropVal2)
        {
            return !(recipePropVal1 == recipePropVal2);
        }

        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Recipe return false.
            RecipePropertyValue recipePropVal = obj as RecipePropertyValue;
            if ((System.Object)recipePropVal == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (IDRecipePropertyValue == recipePropVal.IDRecipePropertyValue);
        }

        public bool Equals(RecipePropertyValue recipePropVal)
        {
            // If parameter is null return false:
            if ((object)recipePropVal == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (IDRecipePropertyValue == recipePropVal.IDRecipePropertyValue);
        }

        public override int GetHashCode()
        {
            return IDRecipePropertyValue.GetHashCode();
        }

        #endregion
    }

    public class RecipeLanguageTag
    {
        #region PrivatePublicic

        private int _IDRecipeLanguageTag;
        private int _IDLanguage;
        private string _RecipeLanguageTag;

        public int IDRecipeLanguageTag
        {
            get { return _IDRecipeLanguageTag; }
        }

        public int IDLanguage
        {
            get { return _IDLanguage; }
        }

        public string Tag
        {
            get { return _RecipeLanguageTag; }
        }

        #endregion

        public RecipeLanguageTag(int IDRecipeLanguageTag, int IDLanguage)
        {
            _IDRecipeLanguageTag = IDRecipeLanguageTag;
            _IDLanguage = IDLanguage;
        }

        public RecipeLanguageTag(int IDRecipeLanguageTag, int IDLanguage, string Tag)
        {
            _IDRecipeLanguageTag = IDRecipeLanguageTag;
            _IDLanguage = IDLanguage;
            _RecipeLanguageTag = Tag;
        }

        public RecipeLanguageTag()
        {
        }

        public static List<RecipeLanguageTag> GetRecipeLangTags(string TagStartWord ,int IDLanguage)
        {
            List<RecipeLanguageTag> tagList = new List<RecipeLanguageTag>();

            RecipesLanguagesTagsDAL dalRecipeLangTags = new RecipesLanguagesTagsDAL();
            DataTable dtRecipeLangTags = dalRecipeLangTags.GetRecipeLangTags(IDLanguage, TagStartWord);

            if (dtRecipeLangTags.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRecipeLangTags.Rows)
                {
                    RecipeLanguageTag _recipeTag = new RecipeLanguageTag(
                        dr.Field<int>("IDRecipeLanguageTag"), dr.Field<int>("IDLanguage"), dr.Field<string>("RecipeLanguageTag"));
                    tagList.Add(_recipeTag);
                }
            }

            return tagList;
        }

        public void QueryDBInfo()
        {
             RecipesLanguagesTagsDAL dalRecipeLangTags = new RecipesLanguagesTagsDAL();
             DataTable dtRecipeLangTags = dalRecipeLangTags.GetRecipeTagsByID(_IDRecipeLanguageTag, _IDLanguage);

             if (dtRecipeLangTags.Rows.Count > 0)
             {
                 _RecipeLanguageTag = dtRecipeLangTags.Rows[0].Field<string>("RecipeLanguageTag");
             }
        }

    }

    public class DBRecipesConfigParameter
    {
        #region PrivatePublicic
        int _IDDBRecipeConfigParameter;
        string _DBRecipeConfigParameterName;
        string _DBRecipeConfigParameterValue;

        public int IDDBRecipeConfigParameter
        {
            get { return _IDDBRecipeConfigParameter; }
        }

        public string DBRecipeConfigParameterName
        {
            get { return _DBRecipeConfigParameterName; }
            set { _DBRecipeConfigParameterName = value; }
        }

        public string DBRecipeConfigParameterValue
        {
            get { return _DBRecipeConfigParameterValue; }
            set { _DBRecipeConfigParameterValue = value; }
        }

        #endregion

        #region Costructors
        /// <summary>
        /// Get a Config Parameter from DBRecipes
        /// </summary>
        /// <param name="IDDBRecipeConfigParameter">The ID of the configuration parameter</param>
        public DBRecipesConfigParameter(int IDDBRecipeConfigParameter)
        {
            _IDDBRecipeConfigParameter = IDDBRecipeConfigParameter;

            GetDBRecipesConfigParametersDAL recipeConfigParametersDAL = new GetDBRecipesConfigParametersDAL();
            DataTable dtrecipeConfigParametersValue = recipeConfigParametersDAL.GetDBRecipesConfigParameter(_IDDBRecipeConfigParameter);

            if (dtrecipeConfigParametersValue.Rows.Count > 0)
            {
                _DBRecipeConfigParameterName = dtrecipeConfigParametersValue.Rows[0].Field<string>("DBRecipeConfigParameterName");
                _DBRecipeConfigParameterValue = dtrecipeConfigParametersValue.Rows[0].Field<string>("DBRecipeConfigParameterValue");
            }
        }

        //public DBRecipesConfigParameter(string DBRecipeConfigParameterName, string DBRecipeConfigParameterValue)
        //{
        //    _DBRecipeConfigParameterName = DBRecipeConfigParameterName;
        //    _DBRecipeConfigParameterValue = DBRecipeConfigParameterValue;
        //}

        #endregion
    }
}
