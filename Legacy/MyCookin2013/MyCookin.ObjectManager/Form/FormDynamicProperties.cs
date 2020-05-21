using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyCookin.Common;
using MyCookin.ObjectManager.UserManager;
using System.Data;
using MyCookin.DAL.User.ds_UserInfoPropertiesTableAdapters;

namespace MyCookin.ObjectManager.Form
{
    public class FormDynamicProperties
    {

        #region PrivateFields
        private Guid _IDUser;
        private int? _IDProperty;
        private int? _IDUserInfoPropertyAllowedValue;
        private string _PropertyValue;
        private Guid? _IDUserInfoPropertyCompiled;
        private int _IDLanguage;
        private int? _IDPropertyCategory;
        private int _IDPropertyCategoryLanguage;
        private string _PropertyCategory;
        private string _PropertyCategoryToolTip;
        private string _desiredProfile;
        #endregion

        #region PublicFields
        public Guid IDUser
        {
            get { return _IDUser;}
            set { _IDUser = value;}
        }
        public int? IDProperty
        {
            get { return _IDProperty;}
            set { _IDProperty = value;}
        }
        public int? IDUserInfoPropertyAllowedValue
        {
            get { return _IDUserInfoPropertyAllowedValue;}
            set { _IDUserInfoPropertyAllowedValue = value;}
        }
        public string PropertyValue
        {
            get { return _PropertyValue;}
            set { _PropertyValue = value;}
        }
        public Guid? IDUserInfoPropertyCompiled
        {
            get { return _IDUserInfoPropertyCompiled; }
            set { _IDUserInfoPropertyCompiled = value; }
        }
        public int IDLanguage
        {
            get { return _IDLanguage; }
            set { _IDLanguage = value; }
        }
        public int? IDPropertyCategory
        {
            get { return _IDPropertyCategory; }
            set { _IDPropertyCategory = value; }
        }
        public int IDPropertyCategoryLanguage
        {
            get { return _IDPropertyCategoryLanguage; }
            set { _IDPropertyCategoryLanguage = value; }
        }
        public string PropertyCategory
        {
            get { return _PropertyCategory; }
            set { _PropertyCategory = value; }
        }
        public string PropertyCategoryTooltip
        {
            get { return _PropertyCategoryToolTip; }
            set { _PropertyCategoryToolTip = value; }
        }
        public string DesiredProfile
        {
            get { return _desiredProfile; }
            set { _desiredProfile = value; }
        }

        #endregion

        #region Constructors
        public FormDynamicProperties(Guid IDUser, Guid? IDUserInfoPropertyCompiled, int IDProperty, int? IDUserInfoPropertyAllowedValue, string PropertyValue, int IDLanguage, int IDPropertyCategory, string DesiredProfile)
        {
            _IDLanguage = IDLanguage;
            _IDProperty = IDProperty;
            _IDPropertyCategory = IDPropertyCategory;
            _IDUser = IDUser;
            _IDUserInfoPropertyAllowedValue = IDUserInfoPropertyAllowedValue;
            _IDUserInfoPropertyCompiled = IDUserInfoPropertyCompiled;
            _PropertyValue = PropertyValue;
            _desiredProfile = DesiredProfile;
        }

        public FormDynamicProperties(int IDLanguage, int? IDPropertyCategory, int? IDProperty, string DesiredProfile)
        {
            _IDLanguage = IDLanguage;
            _IDPropertyCategory = IDPropertyCategory;
            _IDProperty = IDProperty;
            _desiredProfile = DesiredProfile;
        }

        public FormDynamicProperties(Guid IDUser, int IDProperty, string DesiredProfile)
        {
            _IDUser = IDUser;
            _IDProperty = IDProperty;
            _desiredProfile = DesiredProfile;
        }

        public FormDynamicProperties(Guid IDUser, int IDProperty, int? IDUserInfoPropertyAllowedValue, string DesiredProfile)
        {
            _IDUser = IDUser;
            _IDProperty = IDProperty;
            _IDUserInfoPropertyAllowedValue = IDUserInfoPropertyAllowedValue;
            _desiredProfile = DesiredProfile;
        }

        public FormDynamicProperties(Guid IDUserInfoPropertyCompiled)
        {
            _IDUserInfoPropertyCompiled = IDUserInfoPropertyCompiled;
        }

        public FormDynamicProperties(string PropertyValue, Guid IDUserInfoPropertyCompiled, string DesiredProfile)
        {
            _PropertyValue = PropertyValue;
            _IDUserInfoPropertyCompiled = IDUserInfoPropertyCompiled;
            _desiredProfile = DesiredProfile;
        }

        #endregion

        #region Methods

        public void InsertNewCompiled()
        {
            Guid NewGuid = Guid.NewGuid();

            switch (_desiredProfile)
            {
                case "USER":
                    UsersInfoPropertiesCompiledTableAdapter UserInfoCheckSelectedItem = new UsersInfoPropertiesCompiledTableAdapter();
                    UserInfoCheckSelectedItem.InsertNewCompiled(_IDUser, (int)IDProperty, IDUserInfoPropertyAllowedValue, PropertyValue, DateTime.UtcNow, null, NewGuid);
                    break;
                case "RECIPES":
                    break;
                default:
                    //
                    break;
            }
        }

        public void DeleteCompiled()
        {
            switch (_desiredProfile)
            {
                case "USER":
                    UsersInfoPropertiesCompiledTableAdapter UserInfoCheckSelectedItem = new UsersInfoPropertiesCompiledTableAdapter();
                    UserInfoCheckSelectedItem.DeleteCompiled((Guid)_IDUserInfoPropertyCompiled);
                    break;
                case "RECIPES":
                    break;
                default:
                    //
                    break;
            }
        }

        public void UpdateCompiled()
        {
            switch (_desiredProfile)
            {
                case "USER":
                    UsersInfoPropertiesCompiledTableAdapter UserInfoCheckSelectedItem = new UsersInfoPropertiesCompiledTableAdapter();
                    UserInfoCheckSelectedItem.UpdateCompiled(_PropertyValue, DateTime.UtcNow, (Guid)_IDUserInfoPropertyCompiled);
                    break;
                case "RECIPES":
                    break;
                default:
                    //
                    break;
            }
        }

        public void NewCategoryBox()
        {
            //USP to return a Category Box according to Language and Category
            ManageCategoryTableAdapter CategoryBox = new ManageCategoryTableAdapter();

            ManageUSPReturnValue PanelResult = new ManageUSPReturnValue(CategoryBox.GetCategoryInfo(_IDLanguage, _IDPropertyCategory));

            //USP_GetCategoryInfo always returns 3 values
            string PanelResults = PanelResult.USPReturnValue.ToString();

            string[] PanelResultsSplit = PanelResults.ToString().Split('|');

            _IDPropertyCategoryLanguage = Convert.ToInt32(PanelResultsSplit[0]);    //Category ID according to language - For USER look at [IDUserInfoPropertyCategoryLanguage]
            _PropertyCategory = PanelResultsSplit[1];                               //Name of category - Title of section - For USER look at [UserInfoPropertyCategoryLanguage]
            _PropertyCategoryToolTip = PanelResultsSplit[2];                        //Optional Tooltip - For USER look at [UserInfoPropertyCategoryToolTip]
        }

        public DataTable GetUserInfoFormElements()
        {
            UserInfoPropertiesListTableAdapter PropertyListTableAdapter = new UserInfoPropertiesListTableAdapter();

            DataTable PropertyList = new DataTable();

            PropertyList = PropertyListTableAdapter.GetPropertiesByCategoryAndLanguage((int)_IDPropertyCategory, _IDLanguage);

            return PropertyList;
        }

        public DataTable AcceptedValues()
        {
            DataTable AcceptedValues = new DataTable();

            switch (_desiredProfile)
            {
                case "USER":
                    UserInfoAcceptedValuesTableAdapter UserInfoAcceptedValuesTableAdapter = new UserInfoAcceptedValuesTableAdapter();
                    AcceptedValues = UserInfoAcceptedValuesTableAdapter.GetAcceptedValuesByPropertyAndLanguage(_IDLanguage, (int)_IDProperty);
                    break;
                case "RECIPES":
                    break;
                default:
                    //
                    break;
            }

            return AcceptedValues;
        }

        public DataTable CheckCompiledText()
        {
            DataTable ItemSelected = new DataTable();

            switch (_desiredProfile)
            {
                case "USER":
                    UsersInfoPropertiesCompiledTableAdapter UserInfoCheckSelectedItem = new UsersInfoPropertiesCompiledTableAdapter();
                    ItemSelected = UserInfoCheckSelectedItem.CheckCompiledText(_IDUser, (int)_IDProperty);
                    break;
                case "RECIPES":
                    break;
                default:
                    //
                    break;
            }

            return ItemSelected;
        }

        public DataTable CheckSelectedItem()
        {
            DataTable ItemSelected = new DataTable();

            switch (_desiredProfile)
            {
                case "USER":
                    UsersInfoPropertiesCompiledTableAdapter UserInfoCheckSelectedItem = new UsersInfoPropertiesCompiledTableAdapter();
                    ItemSelected = UserInfoCheckSelectedItem.CheckSelectedItem(_IDUser, (int)_IDProperty, _IDUserInfoPropertyAllowedValue);
                    break;
                case "RECIPES":
                    break;
                default:
                    //
                    break;
            }

            return ItemSelected;
        }

        #endregion

    }
}
