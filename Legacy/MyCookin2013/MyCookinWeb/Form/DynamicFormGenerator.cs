using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.ObjectManager.Form;
using System.Data;
using MyCookin.ErrorAndMessage;
using MyCookin.Log;
using MyCookin.Common;
using MyCookin.ObjectManager.UserManager;

namespace MyCookinWeb.Form
{
    public class DynamicFormGenerator
    {
        #region PrivateFields
        Page _currentPage;
        private int _IDLanguage;
        private int _IDPropertyCategory;
        UpdatePanel _updatePanelID;
        string _desiredProfile;
        string _IDUser;

        Guid _IDUserGuid;

        int _IDProperty;
        int _IDUserInfoProperty;
        int _IDUserInfoPropertyType;
        bool _mandatory;
        string _userInfoPropertyType;
        int _IDUserInfoPropertyLanguage;
        string _userInfoProperty;
        string _userInfoPropertyToolTip;
        string _description;
        bool _isTextBox;
        int? _IDUserInfoPropertyAllowedValue;
        string[,] _acceptedValuesArray;
        bool _propertyAllowedValueSelected;
        string _propertyAllowedValueLanguage;
        string _checked;
        string _propertyValue;
        Guid _IDUserInfoPropertyCompiled;
        string _Tooltip;

        Panel _pnlNew;
        #endregion

        #region PublicFields
        public Page CurrentPage
        {
            get { return _currentPage; }
            //set { _currentPage = value; }
        }
        public int IDLanguage
        {
            get { return _IDLanguage; }
            //set { _IDLanguage = value; }
        }
        public int IDPropertyCategory
        {
            get { return _IDPropertyCategory; }
            //set { _IDPropertyCategory = value; }
        }
        public UpdatePanel UpdatePanelID
        {
            get { return _updatePanelID; }
            //set { _updatePanelID = value; }
        }
        public string DesiredProfile
        {
            get { return _desiredProfile; }
            //set { _desiredProfile = value; }
        }
        public string IDUser
        {
            get { return _IDUser; }
            //set { _IDUser = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Create new form
        /// </summary>
        /// <param name="CurrentPage">Form Page - Use "this"</param>
        /// <param name="IDLanguage">Language of form</param>
        /// <param name="IDPropertyCategory">ID of category to display - (Ex.: For User Look at [UsersInfoPropertiesCategoriesLanguages]) </param>
        /// <param name="UpdatePanelID">ID of the UpdatePanel where we want to put objects</param>
        /// <param name="DesiredProfile">Profile of the form - (Ex.: USER, RECIPES, or empty for general form)</param>
        public DynamicFormGenerator(Page CurrentPage, int IDLanguage, int IDPropertyCategory, UpdatePanel UpdatePanelID, string DesiredProfile)
        {
            _currentPage = CurrentPage;
            _IDLanguage = IDLanguage;
            _IDPropertyCategory = IDPropertyCategory;
            _updatePanelID = UpdatePanelID;
            _desiredProfile = DesiredProfile;
        }

        /// <summary>
        /// Create new form for Protected Area
        /// </summary>
        /// <param name="CurrentPage">Form Page - Use "this"</param>
        /// <param name="IDLanguage">Language of form</param>
        /// <param name="IDPropertyCategory">ID of category to display - (Ex.: For User Look at [UsersInfoPropertiesCategoriesLanguages]) </param>
        /// <param name="UpdatePanelID">ID of the UpdatePanel where we want to put objects</param>
        /// <param name="DesiredProfile">Profile of the form - (Ex.: USER, RECIPES, or empty for general form)</param>
        /// <param name="IDUser">User ID - to check if the field is already compiled</param>
        public DynamicFormGenerator(Page CurrentPage, int IDLanguage, int IDPropertyCategory, UpdatePanel UpdatePanelID, string DesiredProfile, string IDUser)
        {
            _currentPage = CurrentPage;
            _IDLanguage = IDLanguage;
            _IDPropertyCategory = IDPropertyCategory;
            _updatePanelID = UpdatePanelID;
            _desiredProfile = DesiredProfile;

            _IDUser = IDUser;
            _IDUserGuid = new Guid(_IDUser);
        }
        #endregion

        #region Methods

        #region FillPanel
        /// <summary>
        /// Fill the panel with form Fields
        /// </summary>
        public void FillPanel()
        {
            try
            {
                FormDynamicProperties FormDynamicProperties = new FormDynamicProperties(_IDLanguage, _IDPropertyCategory, _IDProperty, _desiredProfile.ToUpper());
                FormDynamicProperties.NewCategoryBox();

                int _IDPropertyCategoryLanguage = FormDynamicProperties.IDPropertyCategoryLanguage;     //Category ID according to language - For USER look at [IDUserInfoPropertyCategoryLanguage]
                string _PropertyCategory = FormDynamicProperties.PropertyCategory;                      //Name of category - Title of section - For USER look at [UserInfoPropertyCategoryLanguage]
                string _PropertyCategoryToolTip = FormDynamicProperties.PropertyCategoryTooltip;        //Optional Tooltip - For USER look at [UserInfoPropertyCategoryToolTip]

                

                //Get FORM ELEMENTS according to the desired profile 
                switch (_desiredProfile.ToUpper())
                {
                    case "USER":
                        //Get ELEMENTS
                        GetInfoUserFormElements(false);

                        //Add Button (DEFAULT ID will be "btn_[UpdatePanelID]")
                        NewButton("btn_" + UpdatePanelID.ClientID);
                        break;
                    case "RECIPES":
                        //GetRecipesFormElements(false);
                        
                    //Add Button (DEFAULT ID will be "btn_[UpdatePanelID]")
                        //NewButton("btn_" + UpdatePanelID.ClientID);
                        break;
                    default:
                        //GetGeneralFormElements(false);
                        break;
                }
            }
            catch (Exception ex)
            {
                string ErrorMessage = ex.Message;

                _updatePanelID.ContentTemplateContainer.Controls.Add(new LiteralControl("<p>" + RetrieveMessage.RetrieveDBMessage(_IDLanguage, "US-ER-9999") + "</p>"));

                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in FillPanel(): " + _userInfoPropertyType + " - " + _userInfoProperty + " - " + ErrorMessage + "", IDUser, true, false);
                    LogManager.WriteDBLog(LogLevel.CriticalErrors, NewRow);
                    LogManager.WriteFileLog(LogLevel.CriticalErrors, false, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region GetGeneralFormElements
        /// <summary>
        /// Get General Form Elements
        /// </summary>
        private void GetGeneralFormElements()
        {
            //"not yet implemented";
        }
        #endregion

        #region GetRecipesFormElements
        /// <summary>
        /// Get all Recipes Form Elements
        /// </summary>
        private void GetRecipesFormElements()
        {
            // "not yet implemented";
        }
        #endregion

        #region GetInfoUserElements
        /// <summary>
        /// Get all InfoUser Form Elements
        /// </summary>
        /// <param name="isForRetrieveAndInsertOrUpdate">True if Get All Form Element To Insert or Update Fields Values</param>
        public void GetInfoUserFormElements(bool isForRetrieveAndInsertOrUpdate)
        {
            try
            {
                //DEFINE CLASSES NAME
                string UserInfoFieldTitleClassName = "UserInfoFieldTitle";
                string UserInfoFieldDescriptionClassName = "UserInfoFieldDescription";

                //Get all form elements for this CategoryBlock

                FormDynamicProperties FormDynamicProperties = new FormDynamicProperties(_IDLanguage, _IDPropertyCategory, _IDProperty, _desiredProfile.ToUpper());
                DataTable PropertyList = FormDynamicProperties.GetUserInfoFormElements();

                int numberOfRows = PropertyList.Rows.Count;

                //Loop to extract all properties
                for (int i = 0; i < numberOfRows; i++)
                {
                    _IDUserInfoProperty = PropertyList.Rows[i].Field<int>("IDUserInfoProperty");                                //ID of the Property (Ex.: Id of "GENDER" is 1) 
                    _IDUserInfoPropertyType = PropertyList.Rows[i].Field<int>("IDUserInfoPropertyType");                        //ID that determine if is a TextBox, or DropDonwList, or...
                    _mandatory = PropertyList.Rows[i].Field<bool>("Mandatory");                                           // * Da implementare (!)
                    _userInfoPropertyType = PropertyList.Rows[i].Field<string>("UserInfoPropertyType");                         //Not Used - Just Admin Info
                    _IDUserInfoPropertyLanguage = PropertyList.Rows[i].Field<int>("IDUserInfoPropertyLanguage");                //Not Used
                    _userInfoProperty = PropertyList.Rows[i].Field<string>("UserInfoProperty");                                 //Is the name of Property (Ex.: GENDER)
                    _userInfoPropertyToolTip = PropertyList.Rows[i].Field<string>("UserInfoPropertyToolTip");                   //Optional Tooltip
                    _description = PropertyList.Rows[i].Field<string>("Description");                                           //Optional Description

                    if (isForRetrieveAndInsertOrUpdate)
                    {
                        //Get FieldType according to IDUserInfoPropertyType And Insert or Update in Compiled Table
                        RetrieveFieldType(_IDUserInfoPropertyType, _IDUserInfoProperty);
                    }
                    else
                    {

                        //New Panel, to group property
                        #region NewPanel
                        Panel pnlNew = new Panel();
                        pnlNew.ID = "pnl_" + _IDUserInfoProperty;
                        pnlNew.CssClass = "pnlProperty";
                        pnlNew.ClientIDMode = ClientIDMode.Static;

                        //_pnlNew is now the new panel created
                        _pnlNew = pnlNew;

                        //Add this panel to UpdatePanel 
                        _updatePanelID.ContentTemplateContainer.Controls.Add(_pnlNew);
                        #endregion



                        //START PARAGRAPH
                        //_updatePanelID.ContentTemplateContainer.Controls.Add(new LiteralControl("<p>"));
                        _pnlNew.Controls.Add(new LiteralControl("<p>"));

                        //TITLE OF FIELD
                        Label lblFieldTitle = new Label();
                        lblFieldTitle.Text = _userInfoProperty;
                        lblFieldTitle.Attributes.Add("class", UserInfoFieldTitleClassName);
                        //_updatePanelID.ContentTemplateContainer.Controls.Add(lblFieldTitle);
                        _pnlNew.Controls.Add(lblFieldTitle);

                        //DESCRIPTION FIELD
                        Label lblDescription = new Label();
                        lblDescription.Text = _description;
                        lblDescription.Attributes.Add("class", UserInfoFieldDescriptionClassName);
                        _updatePanelID.ContentTemplateContainer.Controls.Add(lblDescription);

                        //END PARAGRAPH
                        //_updatePanelID.ContentTemplateContainer.Controls.Add(new LiteralControl("</p>"));
                        _pnlNew.Controls.Add(new LiteralControl("</p>"));

                        //Get FieldType according to IDUserInfoPropertyType
                        AddFieldType(_IDUserInfoPropertyType, _userInfoPropertyToolTip, _IDUserInfoProperty);

                        
                    }
                }
            }
            catch (Exception ex)
            {
                string ErrorMessage = ex.Message;

                _updatePanelID.ContentTemplateContainer.Controls.Add(new LiteralControl("<p>" + RetrieveMessage.RetrieveDBMessage(_IDLanguage, "US-ER-9999") + "</p>"));

                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in GetInfoUserFormElements(): " + _userInfoPropertyType + " - " + _userInfoProperty + " - " + ErrorMessage + "", IDUser, true, false);
                    LogManager.WriteDBLog(LogLevel.CriticalErrors, NewRow);
                    LogManager.WriteFileLog(LogLevel.CriticalErrors, false, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region GenerateFieldsType

        #region AddFieldType
        /// <summary>
        /// Add fields according to rigth type
        /// </summary>
        /// <param name="IdPropertyType">ID that determine if is a TextBox, or DropDonwList, ...</param>
        /// <param name="ToolTip">Optional Tooltip</param>
        /// <param name="IDProperty">ID of the Property used to create relative ID (Ex.: Id of "GENDER" is 1)</param>
        private void AddFieldType(int IdPropertyType, string ToolTip, int IDProperty)
        {
            try
            {
                switch (IdPropertyType)
                {
                    case 1: //DropDownList
                        NewDropDownList(IDProperty);
                        break;
                    case 2: //TextBox
                        NewTextBox(IDProperty, ToolTip);
                        break;
                    case 3: //CheckBoxList
                        NewCheckBoxList(IDProperty);
                        break;
                    case 4: //RadioButton
                        NewRadioButtonList(IDProperty);
                        break;
                    default:
                        //....
                        break;
                }
            }
            catch (Exception ex)
            {
                string ErrorMessage = ex.Message;

                _updatePanelID.ContentTemplateContainer.Controls.Add(new LiteralControl("<p>" + RetrieveMessage.RetrieveDBMessage(_IDLanguage, "US-ER-9999") + "</p>"));

                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in AddFieldType(): " + _userInfoPropertyType + " - " + _userInfoProperty + " - " + ErrorMessage + "", IDUser, true, false);
                    LogManager.WriteDBLog(LogLevel.CriticalErrors, NewRow);
                    LogManager.WriteFileLog(LogLevel.CriticalErrors, false, NewRow);
                }
                catch { }

            }
        }
        #endregion

        #region GetAcceptedValues
        /// <summary>
        /// Get all accepted values for a Form Field according to Property
        /// </summary>
        /// <param name="IDProperty">ID of the Property used to create relative ID (Ex.: Id of "GENDER" is 1)</param>
        /// <returns>Two Dimensions Array</returns>
        private string[,] GetAcceptedValues(int IDProperty)
        {
            _IDProperty = IDProperty;

            //New DataTable with all values
            DataTable AcceptedValues = new DataTable();

            //Get Accepted Values from DAL
            //For different DAL, the switch will be later, on FormDynamicProperties.cs
            FormDynamicProperties FormDynamicProperties = new FormDynamicProperties(_IDLanguage, _IDPropertyCategory, _IDProperty, _desiredProfile.ToUpper());
            AcceptedValues = FormDynamicProperties.AcceptedValues();
                    
            //Number of DataTable Rows (number of Different Accepted Values)
            int numberOfRows = AcceptedValues.Rows.Count;

            //Declare a Multidimensional Array
            string[,] ValuesAcceptedArray = new string[numberOfRows, 3];

            #region LoopToFillArray
            //Loop to fill array with all accepted values and their properties
            switch (_desiredProfile.ToUpper())
            {
                case "USER":
                    for (int i = 0; i < numberOfRows; i++)
                    {
                        ValuesAcceptedArray[i, 0] = (AcceptedValues.Rows[i].Field<int>("IDUserInfoPropertyAllowedValue")).ToString();  //ID of the Accepted Value
                        ValuesAcceptedArray[i, 1] = (AcceptedValues.Rows[i].Field<bool>("PropertyAllowedValueSelected")).ToString();     //Accepted Values Selected (True or False)
                        ValuesAcceptedArray[i, 2] = (AcceptedValues.Rows[i].Field<string>("PropertyAllowedValueLanguage")).ToString();   //Accepted Value's Name
                    }
                    break;
                case "RECIPES":
                    //for (int i = 0; i < numberOfRows; i++)
                    //{
                    //    ValuesAcceptedArray[i, 0] = (AcceptedValues.Rows[i].Field<int>("IDRecipesPropertyAllowedValue")).ToString();  //ID of the Accepted Value
                    //    ValuesAcceptedArray[i, 1] = (AcceptedValues.Rows[i].Field<bool>("PropertyAllowedValueSelected")).ToString();     //Accepted Values Selected (True or False)
                    //    ValuesAcceptedArray[i, 2] = (AcceptedValues.Rows[i].Field<string>("PropertyAllowedValueLanguage")).ToString();   //Accepted Value's Name
                    //}
                    break;
                default:
                    //
                    break;
            }
            #endregion

            //Return Multidimensional Array
            return ValuesAcceptedArray;
        }
        #endregion

        #region SelectedOrCompiledItems
        /// <summary>
        /// Check if an Item is selected when it is already filled
        /// </summary>
        /// <param name="IDProperty">ID of the Property used to create relative ID (Ex.: Id of "GENDER" is 1)</param>
        /// <param name="IsTextBox">If we are looking for the text of a Textbox or Similar</param>
        /// <returns>Array - [0] If checked or not - [1] Value of filled textBox - [2] Relative ID if compiled (for Updates)</returns>
        private string[] CheckSelectedItem(int IDProperty, bool IsTextBox)
        {
            _IDProperty = IDProperty;
            _isTextBox = IsTextBox;

            //New DataTable with all values
            DataTable ItemSelected = new DataTable();

            //Get Accepted Values from DAL
            switch (_desiredProfile.ToUpper())
            {
                case "USER":
                    if (_isTextBox)
                    {
                        FormDynamicProperties FormDynamicProperties = new FormDynamicProperties(_IDUserGuid, _IDProperty, _desiredProfile.ToUpper());
                        ItemSelected = FormDynamicProperties.CheckCompiledText();
                        //For TextBox we don't have _IDUserInfoPropertyAllowedValue (that is null)
                    }
                    else
                    {
                        FormDynamicProperties FormDynamicProperties = new FormDynamicProperties(_IDUserGuid, _IDProperty, _IDUserInfoPropertyAllowedValue, _desiredProfile.ToUpper());
                        ItemSelected = FormDynamicProperties.CheckSelectedItem();
                    }
                    break;
                case "RECIPES":
                    break;
                default:
                    //
                    break;
            }

            string[] ItemSelectedValuesArray = new string[3];

            if (ItemSelected.Rows.Count > 0)
            {
                ItemSelectedValuesArray[0] = ItemSelected.Rows[0].Field<string>("Checked");         
                ItemSelectedValuesArray[1] = ItemSelected.Rows[0].Field<string>("PropertyValue");
                ItemSelectedValuesArray[2] = ItemSelected.Rows[0].Field<Guid>("IDUserInfoPropertyCompiled").ToString();
            }

            return ItemSelectedValuesArray;
        }
        #endregion

        #region NewDropDownList
        /// <summary>
        /// Create New DropDownList and Add it to Panel 
        /// </summary>
        /// <param name="IDProperty">Id of the Property shown by a DropDownList</param>
        private void NewDropDownList(int IDProperty)
        {
            _IDProperty = IDProperty;

            try
            {
                //Create a dropdownlist
                DropDownList DropDownList = new DropDownList();
                DropDownList.ID = "ddl_" + IDProperty.ToString();
                DropDownList.AutoPostBack = false;

                //Set a STATIC Client ID
                DropDownList.ClientIDMode = ClientIDMode.Static;

                DropDownList.ValidationGroup = "VG_" + _IDPropertyCategory;

                //Get all form elements for this Dropdownlist according to Property
                _acceptedValuesArray = GetAcceptedValues(IDProperty);

                for (int i = 0; i <= _acceptedValuesArray.GetUpperBound(0); i++)
                {
                    _IDUserInfoPropertyAllowedValue = Convert.ToInt32(_acceptedValuesArray[i, 0]);
                    _propertyAllowedValueSelected = Convert.ToBoolean(_acceptedValuesArray[i, 1]);
                    _propertyAllowedValueLanguage = _acceptedValuesArray[i, 2];

                    //Create ListItem
                    ListItem ItemsList = new ListItem(_propertyAllowedValueLanguage, _IDUserInfoPropertyAllowedValue.ToString());

                    //If we have IDUser, check if the field is already compiled and the item is selected
                    if (!String.IsNullOrEmpty(_IDUser))
                    {
                        //Check if an Item is selected when it is already filled 
                        string[] CheckSelectedItemArray = CheckSelectedItem(IDProperty, false);

                        //If Elements of the Array we get are not empty 
                        if (!String.IsNullOrEmpty(CheckSelectedItemArray[1]))
                        {
                            _checked = CheckSelectedItemArray[0];                                   //null
                            _propertyValue = CheckSelectedItemArray[1];                             //Text insered by user into a TextBox Or True/False
                            _IDUserInfoPropertyCompiled = new Guid(CheckSelectedItemArray[2]);      //Relative ID if compiled - Not used here but for updates in textbox

                            //If True or Selected set on db, select item
                            if (Convert.ToBoolean(_propertyValue))
                            {
                                ItemsList.Selected = true;
                            }
                        }
                        else if (_propertyAllowedValueSelected)
                        {
                            ItemsList.Selected = true;
                        }
                    }
                    else
                    {
                        //Set the item Selected if set on db
                        if (_propertyAllowedValueSelected)
                        {
                            ItemsList.Selected = true;
                        }
                    }

                    //Add Item to DropDownList
                    DropDownList.Items.Add(ItemsList);
                }

                //Add DropDownList to Panel
                //_updatePanelID.ContentTemplateContainer.pnlNew.Controls.Add(DropDownList);
                _pnlNew.Controls.Add(DropDownList);

            }
            catch (Exception ex)
            {
                string ErrorMessage = ex.Message;

                //_updatePanelID.ContentTemplateContainer.Controls.Add(new LiteralControl("<p>" + RetrieveMessage.RetrieveDBMessage(_IDLanguage, "US-ER-9999") + "</p>"));
                _pnlNew.Controls.Add(new LiteralControl("<p>" + RetrieveMessage.RetrieveDBMessage(_IDLanguage, "US-ER-9999") + "</p>"));

                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in NewDropDown(): " + _userInfoPropertyType + " - " + _userInfoProperty + " - " + ErrorMessage + "", IDUser, true, false);
                    LogManager.WriteDBLog(LogLevel.CriticalErrors, NewRow);
                    LogManager.WriteFileLog(LogLevel.CriticalErrors, false, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region NewTextBox
        /// <summary>
        /// Create New TextBox and Add it to Panel 
        /// </summary>
        /// <param name="IDProperty">Id of the Property shown by a DropDownList</param>
        /// <param name="Tooltip">Optional Tooltip</param>
        private void NewTextBox(int IDProperty, string ToolTip)
        {
            _IDProperty = IDProperty;
            _Tooltip = ToolTip;

            try
            {
                //Create a TextBox
                TextBox TextBox = new TextBox();

                //Set a STATIC Client ID
                TextBox.ClientIDMode = ClientIDMode.Static;

                TextBox.ID = "txt_" + IDProperty.ToString();
                TextBox.ToolTip = ToolTip;

                TextBox.ValidationGroup = "VG_" + _IDPropertyCategory;

                //If we have IDUser, check if the field is already compiled and the item is selected
                if (!String.IsNullOrEmpty(_IDUser))
                {
                    //Check if an Item is selected when it is already filled 
                    string[] CheckSelectedItemArray = CheckSelectedItem(IDProperty, true);

                    //If Elements of the Array we get are not empty 
                    if (!String.IsNullOrEmpty(CheckSelectedItemArray[1]))
                    {
                        _checked = CheckSelectedItemArray[0];                                   //always null! 
                        _propertyValue = CheckSelectedItemArray[1];                             //Text insered by user into a TextBox 
                        _IDUserInfoPropertyCompiled = new Guid(CheckSelectedItemArray[2]);      //Relative ID if compiled   - Not used here

                        //If True, insert Text in the TextBox
                        //if (Convert.ToBoolean(Convert.ToInt32(_checked)))
                        //{
                            TextBox.Text = _propertyValue;
                        //}
                    }
                }

                //Add TextBox to Panel
                //_updatePanelID.ContentTemplateContainer.Controls.Add(TextBox);
                _pnlNew.Controls.Add(TextBox);
            }
            catch (Exception ex)
            {
                string ErrorMessage = ex.Message;

                //_updatePanelID.ContentTemplateContainer.Controls.Add(new LiteralControl("<p>" + RetrieveMessage.RetrieveDBMessage(_IDLanguage, "US-ER-9999") + "</p>"));
                _pnlNew.Controls.Add(new LiteralControl("<p>" + RetrieveMessage.RetrieveDBMessage(_IDLanguage, "US-ER-9999") + "</p>"));

                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in NewTextBox(): " + _userInfoPropertyType + " - " + _userInfoProperty + " - " + ErrorMessage + "", IDUser, true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region NewCheckBoxList
        /// <summary>
        /// Create New CheckBoxList and Add it to Panel 
        /// </summary>
        /// <param name="IDProperty">Id of the Property shown by a DropDownList</param>
        private void NewCheckBoxList(int IDProperty)
        {
            _IDProperty = IDProperty;

            try
            {
                //Create a dropdownlist
                CheckBoxList CheckBoxList = new CheckBoxList();
                CheckBoxList.ID = "chk_" + IDProperty.ToString();
                CheckBoxList.AutoPostBack = false;

                //Set a STATIC Client ID
                CheckBoxList.ClientIDMode = ClientIDMode.Static;

                CheckBoxList.ValidationGroup = "VG_" + _IDPropertyCategory;

                //Get all form elements for this Dropdownlist according to Property
                _acceptedValuesArray = GetAcceptedValues(IDProperty);

                for (int i = 0; i <= _acceptedValuesArray.GetUpperBound(0); i++)
                {
                    _IDUserInfoPropertyAllowedValue = Convert.ToInt32(_acceptedValuesArray[i, 0]);
                    _propertyAllowedValueSelected = Convert.ToBoolean(_acceptedValuesArray[i, 1]);
                    _propertyAllowedValueLanguage = _acceptedValuesArray[i, 2];

                    //Create ListItem
                    ListItem ItemsList = new ListItem(_propertyAllowedValueLanguage, _IDUserInfoPropertyAllowedValue.ToString());

                    //If we have IDUser, check if the field is already compiled and the item is selected
                    if (!String.IsNullOrEmpty(_IDUser))
                    {
                        //Check if an Item is selected when it is already filled
                        string[] CheckSelectedItemArray = CheckSelectedItem(IDProperty, false);

                        //If Elements of the Array we get are not empty 
                        if (!String.IsNullOrEmpty(CheckSelectedItemArray[1]))
                        {
                            _checked = CheckSelectedItemArray[0];                                   //null
                            _propertyValue = CheckSelectedItemArray[1];                             //Text insered by user into a TextBox Or True/False
                            _IDUserInfoPropertyCompiled = new Guid(CheckSelectedItemArray[2]);      //Relative ID if compiled - Not used here but for updates in textbox

                            //If True or Selected set on db, select item
                            if (Convert.ToBoolean(_propertyValue))
                            {
                                ItemsList.Selected = true;
                            }
                        }
                        else if (_propertyAllowedValueSelected)
                        {
                            ItemsList.Selected = true;
                        }
                    }
                    else
                    {
                        //Set the item Selected if set on db
                        if (_propertyAllowedValueSelected)
                        {
                            ItemsList.Selected = true;
                        }
                    }

                    //Add Item to DropDownList
                    CheckBoxList.Items.Add(ItemsList);
                }

                //Add DropDownList to Panel
                //_updatePanelID.ContentTemplateContainer.Controls.Add(CheckBoxList);
                _pnlNew.Controls.Add(CheckBoxList);
            }
            catch (Exception ex)
            {
                string ErrorMessage = ex.Message;

                //_updatePanelID.ContentTemplateContainer.Controls.Add(new LiteralControl("<p>" + RetrieveMessage.RetrieveDBMessage(_IDLanguage, "US-ER-9999") + "</p>"));
                _pnlNew.Controls.Add(new LiteralControl("<p>" + RetrieveMessage.RetrieveDBMessage(_IDLanguage, "US-ER-9999") + "</p>"));

                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in NewCheckBoxList(): " + _userInfoPropertyType + " - " + _userInfoProperty + " - " + ErrorMessage + "", IDUser, true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region NewRadioButtonList
        /// <summary>
        /// Create New CheckBoxList and Add it to Panel 
        /// </summary>
        /// <param name="IDProperty">Id of the Property shown by a DropDownList</param>
        private void NewRadioButtonList(int IDProperty)
        {
            _IDProperty = IDProperty;

            try
            {
                //Create a dropdownlist
                RadioButtonList RadioButtonList = new RadioButtonList();
                RadioButtonList.ID = "rbl_" + IDProperty.ToString();
                RadioButtonList.AutoPostBack = false;

                //Set a STATIC Client ID
                RadioButtonList.ClientIDMode = ClientIDMode.Static;

                RadioButtonList.ValidationGroup = "VG_" + _IDPropertyCategory;

                //Get all form elements for this Dropdownlist according to Property
                _acceptedValuesArray = GetAcceptedValues(IDProperty);

                for (int i = 0; i <= _acceptedValuesArray.GetUpperBound(0); i++)
                {
                    _IDUserInfoPropertyAllowedValue = Convert.ToInt32(_acceptedValuesArray[i, 0]);
                    _propertyAllowedValueSelected = Convert.ToBoolean(_acceptedValuesArray[i, 1]);
                    _propertyAllowedValueLanguage = _acceptedValuesArray[i, 2];

                    //Create ListItem
                    ListItem ItemsList = new ListItem(_propertyAllowedValueLanguage, _IDUserInfoPropertyAllowedValue.ToString());

                    //If we have IDUser, check if the field is already compiled and the item is selected
                    if (!String.IsNullOrEmpty(_IDUser))
                    {
                        //Check if an Item is selected when it is already filled 
                        string[] CheckSelectedItemArray = CheckSelectedItem(IDProperty, false);

                        //If Elements of the Array we get are not empty 
                        if (!String.IsNullOrEmpty(CheckSelectedItemArray[1]))
                        {
                            _checked = CheckSelectedItemArray[0];                                   //always null
                            _propertyValue = CheckSelectedItemArray[1];                             //Text insered by user into a TextBox Or True/False
                            _IDUserInfoPropertyCompiled = new Guid(CheckSelectedItemArray[2]);      //Relative ID if compiled - Not used here but for updates in textbox

                            //If True or Selected set on db, select item
                            if (Convert.ToBoolean(_propertyValue))
                            {
                                ItemsList.Selected = true;
                            }
                            else if (_propertyAllowedValueSelected)
                            {
                                ItemsList.Selected = true;
                            }
                        }
                    }
                    else
                    {
                        //Set the item Selected if set on db
                        if (_propertyAllowedValueSelected)
                        {
                            ItemsList.Selected = true;
                        }
                    }

                    //Add Item to DropDownList
                    RadioButtonList.Items.Add(ItemsList);
                }

                //Add DropDownList to Panel
                //_updatePanelID.ContentTemplateContainer.Controls.Add(RadioButtonList);
                _pnlNew.Controls.Add(RadioButtonList);
            }
            catch (Exception ex)
            {
                string ErrorMessage = ex.Message;

                //_updatePanelID.ContentTemplateContainer.Controls.Add(new LiteralControl("<p>" + RetrieveMessage.RetrieveDBMessage(_IDLanguage, "US-ER-9999") + "</p>"));
                _pnlNew.Controls.Add(new LiteralControl("<p>" + RetrieveMessage.RetrieveDBMessage(_IDLanguage, "US-ER-9999") + "</p>"));

                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in NewRadioButtonList(): " + _userInfoPropertyType + " - " + _userInfoProperty + " - " + ErrorMessage + "", IDUser, true, false);
                    LogManager.WriteDBLog(LogLevel.CriticalErrors, NewRow);
                    LogManager.WriteFileLog(LogLevel.CriticalErrors, false, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region NewButton
        private void NewButton(string ButtonID)
        {
            Button Button = new Button();
            
            //Set a STATIC Client ID
            Button.ClientIDMode = ClientIDMode.Static;
            
            Button.Text = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0067");
            Button.ID = ButtonID;
            Button.CssClass = "MyButton";

            Button.ValidationGroup = "VG_" + _IDPropertyCategory;

            //Add Button to Panel
            //_updatePanelID.ContentTemplateContainer.Controls.Add(Button);
            _pnlNew.Controls.Add(Button);

            //Is not possible to add an Event here.
            //Button.Click += new EventHandler("btn_Click");
        }
        #endregion

        #endregion  //End GenerateFieldsType Region

        #region RetrieveAllFormElements

        #region Insert
        /// <summary>
        /// Insert new record on [UsersInfoPropertiesCompiled]
        /// </summary>
        /// <param name="IDProperty">ID of the Property used to create relative ID (Ex.: Id of "GENDER" is 1)</param>
        /// <param name="IDUserInfoPropertyAllowedValue">Id of the property value to insert (answer)</param>
        /// <param name="PropertyValue">Property Value (answer) if is a textbox</param>
        private void InsertNewCompiled(int IDProperty, int? IDUserInfoPropertyAllowedValue, string PropertyValue)
        {
            _IDProperty = IDProperty;
            _IDUserInfoPropertyAllowedValue = IDUserInfoPropertyAllowedValue;
            _propertyValue = PropertyValue;

            //Get Accepted Values from DAL
            switch (_desiredProfile.ToUpper())
            {
                case "USER":
                    FormDynamicProperties FormProperties = new FormDynamicProperties(_IDUserGuid, _IDUserInfoPropertyCompiled, IDProperty, IDUserInfoPropertyAllowedValue, PropertyValue, _IDLanguage, _IDPropertyCategory, _desiredProfile.ToUpper());
                    FormProperties.InsertNewCompiled();

                    //Update Field LastProfileUpdate in User Table
                    MyUser UserInfo = new MyUser(_IDUserGuid);
                    //UserInfo.GetUserInfoAllByID();
                    UserInfo.UpdateLastProfileUpdateDate();

                    break;
                case "RECIPES":
                default:
                    //
                    break;
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete a record from [UsersInfoPropertiesCompiled] if not more selected - Not used
        /// </summary>
        /// <param name="IDUserInfoPropertyCompiled">Record ID to delete</param>
        private void DeleteCompiled(string IDUserInfoPropertyCompiled)
        {

            //Get Accepted Values from DAL
            switch (_desiredProfile.ToUpper())
            {
                case "USER":
                    FormDynamicProperties FormDynamicProperties = new FormDynamicProperties(_IDUserInfoPropertyCompiled);
                    FormDynamicProperties.DeleteCompiled();
                    break;
                case "RECIPES":
                    break;
                default:
                    //
                    break;
            }
        }
        #endregion

        #region Update
        /// <summary>
        /// Update if Text of Textbox or similar
        /// </summary>
        /// <param name="PropertyValue">Text of the answer</param>
        /// <param name="IDUserInfoPropertyCompiled">Record ID to Update</param>
        private void UpdateCompiled(string PropertyValue, Guid IDUserInfoPropertyCompiled)
        {
            _propertyValue = PropertyValue;
            _IDUserInfoPropertyCompiled = IDUserInfoPropertyCompiled;

            //Get Accepted Values from DAL
            switch (_desiredProfile.ToUpper())
            {
                case "USER":
                    FormDynamicProperties FormDynamicProperties = new FormDynamicProperties(PropertyValue, _IDUserInfoPropertyCompiled, _desiredProfile.ToUpper());
                    FormDynamicProperties.UpdateCompiled();

                    //Update Field LastProfileUpdate in User Table
                    MyUser UserInfo = new MyUser(_IDUserGuid);
                    //UserInfo.GetUserInfoAllByID();
                    UserInfo.UpdateLastProfileUpdateDate();

                    break;
                case "RECIPES":
                    break;
                default:
                    //
                    break;
            }
        }

        #endregion

        #region RetrieveFieldType
        /// <summary>
        /// Retrieve fields according to rigth type
        /// </summary>
        /// <param name="IdPropertyType">ID that determine if is a TextBox, or DropDonwList, ...</param>
        /// <param name="IDProperty">ID of the Property used to create relative ID (Ex.: Id of "GENDER" is 1)</param>
        private void RetrieveFieldType(int IdPropertyType, int IDProperty)
        {
            try
            {
                switch (IdPropertyType)
                {
                    case 1: //DropDownList
                        RetrieveDropDownList(IDProperty);
                        break;
                    case 2: //TextBox
                        RetrieveTextBox(IDProperty);
                        break;
                    case 3: //CheckBoxList
                        RetrieveCheckBoxList(IDProperty);
                        break;
                    case 4: //RadioButton
                        RetrieveRadioButtonList(IDProperty);
                        break;
                    default:
                        //....
                        break;
                }
            }
            catch (Exception ex)
            {
                string ErrorMessage = ex.Message;

                _updatePanelID.ContentTemplateContainer.Controls.Add(new LiteralControl("<p>" + RetrieveMessage.RetrieveDBMessage(_IDLanguage, "US-ER-9999") + "</p>"));

                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in RetrieveFieldType(): " + _userInfoPropertyType + " - " + _userInfoProperty + " - " + ErrorMessage + "", IDUser, true, false);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region RetrieveDropDownList
        /// <summary>
        /// Retrieve DropDownList Selected Values and Insert or Update on DB 
        /// </summary>
        /// <param name="IDProperty">Id of the Property shown by a DropDownList</param>
        private void RetrieveDropDownList(int IDProperty)
        {
            try
            {
                string IDDropDownList = "ddl_" + IDProperty.ToString();

                DropDownList DropDownList = _updatePanelID.FindControl(IDDropDownList) as DropDownList;

                string DropDownListSelectedValue = DropDownList.SelectedValue;

                for (int i = 0; i < DropDownList.Items.Count; i++)
                {
                    //Set ID of Allowed Value
                    _IDUserInfoPropertyAllowedValue = Convert.ToInt32(DropDownList.Items[i].Value);

                    //Check if this item exists in the Table Compiled
                    string[] CheckSelectedItemArray = CheckSelectedItem(IDProperty, false);

                    //Inizialize to False the existence
                    bool ExistsOnDb = false;

                    //If Elements of the Array we get are not empty 
                    if (!String.IsNullOrEmpty(CheckSelectedItemArray[1]))
                    {
                        _checked = CheckSelectedItemArray[0];                                   //null
                        _propertyValue = CheckSelectedItemArray[1];                             //Text insered by user into a TextBox   - Not used here
                        _IDUserInfoPropertyCompiled = new Guid(CheckSelectedItemArray[2]);      //Relative ID if compiled   - Not used here

                        //If True or Selected set on db, select item
                        //if (Convert.ToBoolean(Convert.ToInt32(_checked)))
                        //{
                            ExistsOnDb = true;
                        //}
                    }

                    //If already exists, update its value
                    if (ExistsOnDb)
                    {
                        //If the item is selected set TRUE 
                        if (DropDownList.Items[i].Selected)
                        {
                            UpdateCompiled("true", _IDUserInfoPropertyCompiled);
                        }
                        else
                        {
                            //If the item is NOT selected set FALSE
                            UpdateCompiled("false", _IDUserInfoPropertyCompiled);
                        }
                    }
                    else //If NOT already exists, INSERT 
                    {
                        //If the item is selected set TRUE 
                        if (DropDownList.Items[i].Selected)
                        {
                            InsertNewCompiled(IDProperty, _IDUserInfoPropertyAllowedValue, "true");
                        }
                        else
                        {
                            //If the item is NOT selected set FALSE
                            InsertNewCompiled(IDProperty, _IDUserInfoPropertyAllowedValue, "false");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string ErrorMessage = ex.Message;

                _updatePanelID.ContentTemplateContainer.Controls.Add(new LiteralControl("<p>" + RetrieveMessage.RetrieveDBMessage(_IDLanguage, "US-ER-9999") + "</p>"));

                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in RetrieveDropDownList(): " + _userInfoPropertyType + " - " + _userInfoProperty + " - " + ErrorMessage + "", IDUser, true, false);
                    LogManager.WriteDBLog(LogLevel.CriticalErrors, NewRow);
                    LogManager.WriteFileLog(LogLevel.CriticalErrors, false, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region RetrieveCheckBoxList
        /// <summary>
        /// Retrieve CheckBoxList Selected Values and Insert or Update on DB
        /// </summary>
        /// <param name="IDProperty">Id of the Property shown by a DropDownList</param>
        private void RetrieveCheckBoxList(int IDProperty)
        {
            try
            {
                string IDCheckBoxList = "chk_" + IDProperty.ToString();

                CheckBoxList CheckBoxList = _updatePanelID.FindControl(IDCheckBoxList) as CheckBoxList;

                //Cycle for each Items
                for (int i = 0; i < CheckBoxList.Items.Count; i++)
                {
                    //Set ID of Allowed Value
                    _IDUserInfoPropertyAllowedValue = Convert.ToInt32(CheckBoxList.Items[i].Value);

                    //Check if this item exists in the Table Compiled
                    string[] CheckSelectedItemArray = CheckSelectedItem(IDProperty, false);

                    //Inizialize to False the existence
                    bool ExistsOnDb = false;

                    //If Elements of the Array we get are not empty 
                    if (!String.IsNullOrEmpty(CheckSelectedItemArray[1]))
                    {
                        _checked = CheckSelectedItemArray[0];                                   //null
                        _propertyValue = CheckSelectedItemArray[1];                             //Text insered by user into a TextBox   - Not used here
                        _IDUserInfoPropertyCompiled = new Guid(CheckSelectedItemArray[2]);      //Relative ID if compiled   - Not used here

                        //If True or Selected set on db, select item
                        //if (Convert.ToBoolean(Convert.ToInt32(_propertyValue)))
                        //{
                            ExistsOnDb = true;
                        //}
                    }

                    //If already exists, update its value
                    if (ExistsOnDb)
                    {
                        //If the item is selected set TRUE 
                        if (CheckBoxList.Items[i].Selected)
                        {
                            UpdateCompiled("true", _IDUserInfoPropertyCompiled);
                        }
                        else
                        {
                            //If the item is NOT selected set FALSE
                            UpdateCompiled("false", _IDUserInfoPropertyCompiled);
                        }
                    }
                    else //If NOT already exists, INSERT 
                    {
                        //If the item is selected set TRUE 
                        if (CheckBoxList.Items[i].Selected)
                        {
                            InsertNewCompiled(IDProperty, _IDUserInfoPropertyAllowedValue, "true");
                        }
                        else
                        {
                            //If the item is NOT selected set FALSE
                            InsertNewCompiled(IDProperty, _IDUserInfoPropertyAllowedValue, "false");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string ErrorMessage = ex.Message;

                _updatePanelID.ContentTemplateContainer.Controls.Add(new LiteralControl("<p>" + RetrieveMessage.RetrieveDBMessage(_IDLanguage, "US-ER-9999") + "</p>"));

                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in RetrieveCheckBoxList(): " + _userInfoPropertyType + " - " + _userInfoProperty + " - " + ErrorMessage + "", IDUser, true, false);
                    LogManager.WriteDBLog(LogLevel.CriticalErrors, NewRow);
                    LogManager.WriteFileLog(LogLevel.CriticalErrors, false, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region RetrieveRadioButtonList
        /// <summary>
        /// Retrieve RadioButtonList Selected Value and Insert or Update on DB
        /// </summary>
        /// <param name="IDProperty">Id of the Property shown by a DropDownList</param>
        private void RetrieveRadioButtonList(int IDProperty)
        {
            try
            {
                string IDRadioButtonList = "rbl_" + IDProperty.ToString();

                RadioButtonList RadioButtonList = _updatePanelID.FindControl(IDRadioButtonList) as RadioButtonList;

                for (int i = 0; i < RadioButtonList.Items.Count; i++)
                {
                    //Set ID of Allowed Value
                    _IDUserInfoPropertyAllowedValue = Convert.ToInt32(RadioButtonList.Items[i].Value);

                    //Check if this item exists in the Table Compiled
                    string[] CheckSelectedItemArray = CheckSelectedItem(IDProperty, false);

                    //Inizialize to False the existence
                    bool ExistsOnDb = false;

                    //If Elements of the Array we get are not empty 
                    if (!String.IsNullOrEmpty(CheckSelectedItemArray[1]))
                    {
                        _checked = CheckSelectedItemArray[0];                                   //null
                        _propertyValue = CheckSelectedItemArray[1];                             //Text insered by user into a TextBox   - Not used here
                        _IDUserInfoPropertyCompiled = new Guid(CheckSelectedItemArray[2]);      //Relative ID if compiled   - Not used here

                        //If True or Selected set on db, select item
                        //if (Convert.ToBoolean(Convert.ToInt32(_checked)))
                        //{
                            ExistsOnDb = true;
                        //}
                    }

                    //If already exists, update its value
                    if (ExistsOnDb)
                    {
                        //If the item is selected set TRUE 
                        if (RadioButtonList.Items[i].Selected)
                        {
                            UpdateCompiled("true", _IDUserInfoPropertyCompiled);
                        }
                        else
                        {
                            //If the item is NOT selected set FALSE
                            UpdateCompiled("false", _IDUserInfoPropertyCompiled);
                        }
                    }
                    else //If NOT already exists, INSERT 
                    {
                        //If the item is selected set TRUE 
                        if (RadioButtonList.Items[i].Selected)
                        {
                            InsertNewCompiled(IDProperty, _IDUserInfoPropertyAllowedValue, "true");
                        }
                        else
                        {
                            //If the item is NOT selected set FALSE
                            InsertNewCompiled(IDProperty, _IDUserInfoPropertyAllowedValue, "false");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string ErrorMessage = ex.Message;

                _updatePanelID.ContentTemplateContainer.Controls.Add(new LiteralControl("<p>" + RetrieveMessage.RetrieveDBMessage(_IDLanguage, "US-ER-9999") + "</p>"));

                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in RetrieveRadioButtonList(): " + _userInfoPropertyType + " - " + _userInfoProperty + " - " + ErrorMessage + "", IDUser, true, false);
                    LogManager.WriteDBLog(LogLevel.CriticalErrors, NewRow);
                    LogManager.WriteFileLog(LogLevel.CriticalErrors, false, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #region RetrieveTextBox
        /// <summary>
        /// Retrieve TextBox Text and Insert or Update on DB
        /// </summary>
        /// <param name="IDProperty">Id of the Property shown by a DropDownList</param>
        /// <param name="Tooltip">Optional Tooltip</param>
        private void RetrieveTextBox(int IDProperty)
        {
            try
            {
                string IDTextBox = "txt_" + IDProperty.ToString();

                TextBox TextBox = _updatePanelID.FindControl(IDTextBox) as TextBox;

                string text = TextBox.Text;

                //Check if this item exists in the Table Compiled
                string[] CheckSelectedItemArray = CheckSelectedItem(IDProperty, true);

                //Inizialize to False the existence
                bool ExistsOnDb = false;

                //If Elements of the Array we get are not empty 
                if (!String.IsNullOrEmpty(CheckSelectedItemArray[1]))
                {
                    _checked = CheckSelectedItemArray[0];                                   //null
                    _propertyValue = CheckSelectedItemArray[1];                             //Text insered by user into a TextBox
                    _IDUserInfoPropertyCompiled = new Guid(CheckSelectedItemArray[2]);      //Relative ID if compiled

                    //If True or Selected set on db, select item
                   // if (!String.IsNullOrEmpty(_propertyValue))
                   // {
                        ExistsOnDb = true;
                   // }
                }

                //If already exists, UPDATE (for textbox)
                if (ExistsOnDb)
                {
                    //Update TextBox - not used here
                    UpdateCompiled(text, _IDUserInfoPropertyCompiled);
                }
                else
                {
                    //If NOT already exists, INSERT
                    InsertNewCompiled(IDProperty, null, text);
                }
            }
            catch (Exception ex)
            {
                string ErrorMessage = ex.Message;

                _updatePanelID.ContentTemplateContainer.Controls.Add(new LiteralControl("<p>" + RetrieveMessage.RetrieveDBMessage(_IDLanguage, "US-ER-9999") + "</p>"));

                //WRITE A ROW IN LOG FILE AND DB
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(), "US-ER-9999", "Error in RetrieveTextBox(): " + _userInfoPropertyType + " - " + _userInfoProperty + " - " + ErrorMessage + "", IDUser, true, false);
                    LogManager.WriteDBLog(LogLevel.CriticalErrors, NewRow);
                    LogManager.WriteFileLog(LogLevel.CriticalErrors, false, NewRow);
                }
                catch { }
            }
        }
        #endregion

        #endregion  //End Methods Region

        #endregion

    }
}