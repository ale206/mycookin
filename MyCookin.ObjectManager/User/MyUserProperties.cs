using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MyCookin.DAL.User.ds_UserPropertiesTableAdapters;


namespace MyCookin.ObjectManager.UserManager
{
    public class MyUserProperty
    {
        #region PrivateFields

        private int _IDProperty;
        private MyUserPropertyTypes _PropertyType;
        private MyUserPropertyCategory _PropertyCategory;
        private bool _Enabled;
        private bool _Mandatory;
        private int _PropertySortOrder;
        private int _IDLanguage;
        private string _Property;
        private string _PropertyToolTip;
        private string _Description;
        private MyUserPropertyAllowedValue[] _PropertyAllowedValues;

        #endregion

        #region PublicFields

        public int IDProperty
        {
            get { return _IDProperty; }
        }
        public MyUserPropertyTypes PropertyType
        {
            get { return _PropertyType; }
            set { _PropertyType = value; }
        }
        public MyUserPropertyCategory PropertyCategory
        {
            get { return _PropertyCategory; }
            set { _PropertyCategory = value; }
        }
        public bool Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; }
        }
        public bool Mandatory
        {
            get { return _Mandatory; }
            set { _Mandatory = value; }
        }
        public int PropertySortOrder
        {
            get { return _PropertySortOrder; }
            set { _PropertySortOrder = value; }
        }

        public int IDLanguage
        {
            get { return _IDLanguage; }
            set { _IDLanguage = value; }
        }

        public string PropertyName
        {
            get { return _Property; }
            set { _Property = value; }
        }

        public string PropertyToolTip
        {
            get { return _PropertyToolTip; }
            set { _PropertyToolTip = value; }
        }

        public string PropertyDescription
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public MyUserPropertyAllowedValue[] PropertyAllowedValues
        {
            get { return _PropertyAllowedValues; }
            set { _PropertyAllowedValues = value; }
        }

        #endregion

        #region Constructors

        public MyUserProperty()
        {
        }

        public MyUserProperty(int IDProperty, int IDLanguage)
        {
            _IDProperty = IDProperty;
            _IDLanguage = IDLanguage;

            QueryDBInfo();
        }

        #endregion

        #region Methods

        private void QueryDBInfo()
        {
            GetUsersInfoPropertiesLanguagesDAL dalProperty = new GetUsersInfoPropertiesLanguagesDAL();
            DataTable dtProperty = dalProperty.GetUserInfoProperty(_IDProperty, _IDLanguage);

            if (dtProperty.Rows.Count > 0)
            {
                int _IDPropertyCategory;
                _PropertyType = (MyUserPropertyTypes)dtProperty.Rows[0].Field<int>("IDUserInfoPropertyType");
                _IDPropertyCategory = dtProperty.Rows[0].Field<int>("IDUserInfoPropertyCategory");
                _Enabled = dtProperty.Rows[0].Field<bool>("Enabled");
                _Mandatory = dtProperty.Rows[0].Field<bool>("Mandatory");
                _PropertySortOrder = dtProperty.Rows[0].Field<int>("PropertySortOrder");
                _Property = dtProperty.Rows[0].Field<string>("UserInfoProperty");
                _PropertyToolTip = dtProperty.Rows[0].Field<string>("UserInfoPropertyToolTip");
                _Description = dtProperty.Rows[0].Field<string>("Description");

                _PropertyCategory = new MyUserPropertyCategory(_IDPropertyCategory, _IDLanguage);
            }
        }

        public void GetPropertyAllowedValues()
        {
            DataTable dtPropertyAllowedValues = MyUserPropertyAllowedValue.GetAllAllowedValue(this);
            int x = 0;

            if (dtPropertyAllowedValues.Rows.Count > 0)
            {
                _PropertyAllowedValues = new MyUserPropertyAllowedValue[dtPropertyAllowedValues.Rows.Count];

                foreach (DataRow AllowedValue in dtPropertyAllowedValues.Rows)
                {
                    MyUserPropertyAllowedValue PropertyAllowedValue = new MyUserPropertyAllowedValue(AllowedValue.Field<int>("IDUserInfoPropertyAllowedValue"), _IDLanguage);
                    _PropertyAllowedValues[x] = PropertyAllowedValue;
                    x++;
                }
            }
        }

        public static List<MyUserProperty> GetAllMyUserPropertyByCategory(int IDCategory, int IDLanguage)
        {

            GetUsersInfoPropertiesLanguagesDAL dalMyUserPropertyList = new GetUsersInfoPropertiesLanguagesDAL();

            DataTable dtMyUserPropertyList = dalMyUserPropertyList.GetAllPropertyByIDCategory(IDLanguage, IDCategory);

            List<MyUserProperty> MyUserPropertyList = new List<MyUserProperty>();

            if (dtMyUserPropertyList.Rows.Count > 0)
            {
                for (int i = 0; i < dtMyUserPropertyList.Rows.Count; i++)
                {
                    MyUserPropertyList.Add(new MyUserProperty()
                    {
                        _IDProperty = dtMyUserPropertyList.Rows[i].Field<int>("IDUserInfoProperty"),
                        _Property = dtMyUserPropertyList.Rows[i].Field<string>("UserInfoProperty"),
                        _PropertyToolTip = dtMyUserPropertyList.Rows[i].Field<string>("UserInfoPropertyToolTip"),
                        _Mandatory = dtMyUserPropertyList.Rows[i].Field<bool>("Mandatory"),
                    });
                }
            }

            return MyUserPropertyList;
        }

        #endregion

    }

    public class MyUserPropertyCategory
    {
        #region PrivateFileds

        int _IDPropertyCategory;
        bool _Enabled;
        int _IDLanguage;
        string _PropertyCategory;
        string _PropertyCategoryToolTip;

        #endregion

        #region PublicProperties

        public int IDPropertyCategory
        {
            get { return _IDPropertyCategory; }
        }

        public bool Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; }
        }

        public int IDLanguage
        {
            get { return _IDLanguage; }
            set { _IDLanguage = value; }
        }

        public string PropertyCategoryName
        {
            get { return _PropertyCategory; }
            set { _PropertyCategory = value; }
        }

        public string PropertyCategoryToolTip
        {
            get { return _PropertyCategoryToolTip; }
            set { _PropertyCategoryToolTip = value; }
        }

        #endregion

        #region Costructors

        public MyUserPropertyCategory()
        {
        }

        public MyUserPropertyCategory(int IDPropertyCategory, int IDLanguage)
        {
            _IDPropertyCategory = IDPropertyCategory;
            _IDLanguage = IDLanguage;

            QueryDBInfo();
        }

        #endregion

        #region Methods

        private void QueryDBInfo()
        {
            GetUsersInfoPropertiesCategoriesLanguagesDAL dalPropertyCategory = new GetUsersInfoPropertiesCategoriesLanguagesDAL();
            DataTable dtPropertyCategory = dalPropertyCategory.GetPropertyCategory(_IDPropertyCategory,_IDLanguage);

            if (dtPropertyCategory.Rows.Count > 0)
            {
                _Enabled = dtPropertyCategory.Rows[0].Field<bool>("Enabled");
                _PropertyCategory = dtPropertyCategory.Rows[0].Field<string>("UserInfoPropertyCategory");
                _PropertyCategoryToolTip = dtPropertyCategory.Rows[0].Field<string>("UserInfoPropertyCategoryToolTip");
            }
        }

        public static List<MyUserPropertyCategory> GetAllMyUserPropertyCategory(int IDLanguage)
        {
            GetUsersInfoPropertiesCategoriesLanguagesDAL dalMyUserPropertyCategoryList = new GetUsersInfoPropertiesCategoriesLanguagesDAL();

            DataTable dtMyUserPropertyCategoryList = dalMyUserPropertyCategoryList.GetAllPropertyCategories(IDLanguage);

            List<MyUserPropertyCategory> MyUserPropertyCategoryList = new List<MyUserPropertyCategory>();

            if (dtMyUserPropertyCategoryList.Rows.Count > 0)
            {
                for (int i = 0; i < dtMyUserPropertyCategoryList.Rows.Count; i++)
                {
                    MyUserPropertyCategoryList.Add(new MyUserPropertyCategory()
                    {
                        _IDPropertyCategory = dtMyUserPropertyCategoryList.Rows[i].Field<int>("IDUserInfoPropertyCategory"),
                        _PropertyCategory = dtMyUserPropertyCategoryList.Rows[i].Field<string>("UserInfoPropertyCategory"),
                        _PropertyCategoryToolTip = dtMyUserPropertyCategoryList.Rows[i].Field<string>("UserInfoPropertyCategoryToolTip"),
                    });
                }
            }

            return MyUserPropertyCategoryList;
        }

        #endregion
    }

    public enum MyUserPropertyTypes : int
    {
        DropDownList=1,
        TextBox=2,
        CheckBoxList=3,
        RadioButtonList=4
    }

    public class MyUserPropertyAllowedValue
    {
        #region PrivateFileds

        private int _IDPropertyAllowedValue;
        //private MyUserProperty _Property;
        private int _PropertyAllowedValueOrder;
        private bool _PropertyAllowedValueSelected;
        private int _IDLanguage;
        private string _PropertyAllowedValueLanguage;

        #endregion

        #region PublicProperties

        public int IDPropertyAllowedValue
        {
            get { return _IDPropertyAllowedValue; }
        }

        //public MyUserProperty Property
        //{
        //    get { return _Property; }
        //    set { _Property = value; }
        //}

        public int PropertyAllowedValueOrder
        {
            get { return _PropertyAllowedValueOrder; }
            set { _PropertyAllowedValueOrder = value; }
        }

        public bool PropertyAllowedValueSelected
        {
            get { return _PropertyAllowedValueSelected; }
            set { _PropertyAllowedValueSelected = value; }
        }

        public int IDLanguage
        {
            get { return _IDLanguage; }
            set { _IDLanguage = value; }
        }

        public string PropertyAllowedValueLanguage
        {
            get { return _PropertyAllowedValueLanguage; }
            set { _PropertyAllowedValueLanguage = value; }
        }

        #endregion

        #region Costructors

        public MyUserPropertyAllowedValue(int IDPropertyAllowedValue, int IDLanguage)
        {
            _IDPropertyAllowedValue = IDPropertyAllowedValue;
            _IDLanguage = IDLanguage;

            QueryDBInfo();
        }

        #endregion

        #region Methods

        private void QueryDBInfo()
        {
            GetUsersInfoPropertiesAllowedValuesLanguagesDAL dalPropertyAllowedValue = new GetUsersInfoPropertiesAllowedValuesLanguagesDAL();
            DataTable dtPropertyAllowedValue = dalPropertyAllowedValue.GetPropertyAllowedValue(_IDPropertyAllowedValue, _IDLanguage);

            if (dtPropertyAllowedValue.Rows.Count > 0)
            {
                //int _IDProperty;
                //_IDProperty = dtPropertyAllowedValue.Rows[0].Field<int>("IDUserInfoProperty");
                _PropertyAllowedValueOrder = dtPropertyAllowedValue.Rows[0].Field<int>("PropertyAllowedValueOrder");
                _PropertyAllowedValueSelected = dtPropertyAllowedValue.Rows[0].Field<bool>("PropertyAllowedValueSelected");
                _PropertyAllowedValueLanguage = dtPropertyAllowedValue.Rows[0].Field<string>("PropertyAllowedValueLanguage");

                //if (_Property == null)
                //{
                //    _Property = new MyUserProperty(_IDProperty, _IDLanguage);
                //}

            }
        }

        public override string ToString()
        {
            return _PropertyAllowedValueLanguage;
        }

        public static DataTable GetAllAllowedValue(MyUserProperty Property)
        {
            GetUsersInfoPropertiesAllowedValuesLanguagesDAL dalPropertyAllowedValue = new GetUsersInfoPropertiesAllowedValuesLanguagesDAL();
            return dalPropertyAllowedValue.GetAllAllowedValuesByIDUserInfoProperty(Property.IDLanguage, Property.IDProperty);
        }

        #endregion
    }

    public class MyUserPropertyCompiled
    {
        #region PrivateFileds

        private Guid _IDUserInfoPropertyCompiled;
        private MyUser _User;
        private int _IDLanguage;
        private MyUserProperty _Property;
        private MyUserPropertyAllowedValue[] _AllowedValue;
        private string _PropertyValue;
        private DateTime _LastUpdate;

        #endregion

        #region PublicProperties

        public Guid IDUserInfoPropertyCompiled
        {
            get { return _IDUserInfoPropertyCompiled; }
        }

        public MyUser User
        {
            get { return _User; }
        }

        public MyUserProperty Property
        {
            get { return _Property; }
        }

        public string PropertyValue
        {
            get { return _PropertyValue; }
        }
        public DateTime LastUpdate
        {
            get { return _LastUpdate; }
        }


        #endregion

        #region Costructors

        public MyUserPropertyCompiled()
        {
        }

        public MyUserPropertyCompiled(Guid IDUserInfoPropertyCompiled, int IDLanguage)
        {
            _IDUserInfoPropertyCompiled = IDUserInfoPropertyCompiled;
            _IDLanguage = IDLanguage;
            QueryDBInfo();
        }

        public MyUserPropertyCompiled(Guid IDUser, int IDProperty, int IDLanguage)
        {
            _User = IDUser;
            _Property = new MyUserProperty(IDProperty, IDLanguage);
            _IDLanguage = IDLanguage;
            QueryDBInfo2();
        }

        #endregion

        #region Methods

        private void QueryDBInfo()
        {
            GetUsersInfoPropertiesCompiledDAL dalPropertiesCompiled = new GetUsersInfoPropertiesCompiledDAL();
            DataTable dtPropertiesCompiled = dalPropertiesCompiled.GetPropertyCompiled(_IDUserInfoPropertyCompiled);

            if (dtPropertiesCompiled.Rows.Count > 0)
            {
                _AllowedValue = new MyUserPropertyAllowedValue[dtPropertiesCompiled.Rows.Count];
                int _IDProperty;
                _IDProperty = dtPropertiesCompiled.Rows[0].Field<int>("IDUserInfoProperty");
                int? _IDAllowedValue;
                _IDAllowedValue = dtPropertiesCompiled.Rows[0].Field<int?>("IDUserInfoPropertyAllowedValue");
                _PropertyValue = dtPropertiesCompiled.Rows[0].Field<string>("PropertyValue");
                _LastUpdate = dtPropertiesCompiled.Rows[0].Field<DateTime>("LastUpdate");

                _Property = new MyUserProperty(_IDProperty, _IDLanguage);

                if (_IDAllowedValue != null)
                {
                    _AllowedValue[0] = new MyUserPropertyAllowedValue((int)_IDAllowedValue, _IDLanguage);
                }

                if (_Property.PropertyType != MyUserPropertyTypes.TextBox)
                {
                    _PropertyValue = _AllowedValue[0].PropertyAllowedValueLanguage;
                }
            }
        }

        private void QueryDBInfo2()
        {
            GetUsersInfoPropertiesCompiledDAL dalPropertiesCompiled = new GetUsersInfoPropertiesCompiledDAL();
            DataTable dtPropertiesCompiled = dalPropertiesCompiled.GetPropertyValueByIDUserIDProp(_User.IDUser,_Property.IDProperty);
            MyUserPropertyAllowedValue[] PropValue;

            if (dtPropertiesCompiled.Rows.Count > 0)
            {
                _AllowedValue = new MyUserPropertyAllowedValue[dtPropertiesCompiled.Rows.Count];
                PropValue = new MyUserPropertyAllowedValue[dtPropertiesCompiled.Rows.Count];
                int? _IDAllowedValue=null;
                for (int i = 0; i < dtPropertiesCompiled.Rows.Count; i++)
                {
                    int _IDProperty;
                    _IDProperty = dtPropertiesCompiled.Rows[i].Field<int>("IDUserInfoProperty");
                    
                    _IDAllowedValue = dtPropertiesCompiled.Rows[i].Field<int?>("IDUserInfoPropertyAllowedValue");
                    _PropertyValue = dtPropertiesCompiled.Rows[i].Field<string>("PropertyValue");
                    _LastUpdate = dtPropertiesCompiled.Rows[i].Field<DateTime>("LastUpdate");

                    if (_IDAllowedValue != null)
                    {
                        _AllowedValue[i] = new MyUserPropertyAllowedValue((int)_IDAllowedValue, _IDLanguage);

                        if (_Property.PropertyType != MyUserPropertyTypes.TextBox && _PropertyValue == "true")
                        {
                            PropValue[i] = _AllowedValue[i];
                        }
                    }
                }
                if (_IDAllowedValue != null)
                {
                    _PropertyValue = "";
                    foreach (MyUserPropertyAllowedValue allowValue in PropValue)
                    {
                        if (allowValue != null)
                        {
                            _PropertyValue += allowValue.PropertyAllowedValueLanguage + ", ";
                        }
                    }
                    _PropertyValue = _PropertyValue.Substring(0, _PropertyValue.Length - 2);
                }
            }
            
        }

        public static DataTable GetCountPropertyCompiledByUser(Guid IDUser)
        {
            GetUsersInfoPropertiesCompiledDAL dalPropertycompiled = new GetUsersInfoPropertiesCompiledDAL();
            return dalPropertycompiled.GetCountCompiledValueByIDUser(IDUser);
        }

        public static DataTable GetAllPropertyCompiled(Guid IDUser)
        {
            GetUsersInfoPropertiesCompiledDAL dalPropertycompiled = new GetUsersInfoPropertiesCompiledDAL();
            return dalPropertycompiled.GetAllCompiledValueByIDUser(IDUser);
        }

        #endregion
    }

    public class MyUserConfigParameter
    {
        #region PrivatePublicic
        Guid _IDUserConfigurationParameter;
        string _ConfigurationName;
        string _ConfigurationValue;
        string _ConfigurationNote;

        public Guid IDUserConfigurationParameter
        {
            get { return _IDUserConfigurationParameter; }
        }

        public string ConfigurationName
        {
            get { return _ConfigurationName; }
        }

        public string ConfigurationValue
        {
            get { return _ConfigurationValue; }
            set { _ConfigurationValue = value; }
        }

         public string ConfigurationNote
        {
            get { return _ConfigurationNote; }
            set { _ConfigurationNote = value; }
        }

        #endregion

        #region Costructors

         public MyUserConfigParameter(string ConfigurationName)
        {
            _ConfigurationName = ConfigurationName;

            GetUserConfigurationParametersDAL userConfigParametersDAL = new GetUserConfigurationParametersDAL();
            DataTable dtuserConfigParametersValue = userConfigParametersDAL.GetParameterByConfigurationName(_ConfigurationName);

            if (dtuserConfigParametersValue.Rows.Count > 0)
            {
                _IDUserConfigurationParameter = dtuserConfigParametersValue.Rows[0].Field<Guid>("IDUserConfigurationParameter");
                _ConfigurationValue = dtuserConfigParametersValue.Rows[0].Field<string>("ConfigurationValue");
                _ConfigurationNote = dtuserConfigParametersValue.Rows[0].Field<string>("ConfigurationNote");
            }
        }

        #endregion
    }
}
