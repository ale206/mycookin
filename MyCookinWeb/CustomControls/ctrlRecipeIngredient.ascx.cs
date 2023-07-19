using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyCookinWeb.CustomControls
{
    public partial class ctrlRecipeIngredient : System.Web.UI.UserControl
    {
        #region PublicFields
        public bool isInError
        {
            get { return Convert.ToBoolean(txtIsError.Value); }
        }
        public string ReturnMessage
        {
            get { return txtErrorMessage.Value; }
        }
        public string SelectedObject
        {
            get { return txtObjectName.Text; }
            set { txtObjectName.Text = value; }
        }
        public string IDIngredient
        {
            get { return txtObjectID.Value; }
        }
        public bool Enabled
        {
            get { return txtObjectName.Enabled; }
            set { txtObjectName.Enabled = value; }
        }
        public string LanguageCode
        {
            get { return ViewState["_LanguageCode"] == null ? "" : ViewState["_LanguageCode"].ToString(); }
            set { ViewState["_LanguageCode"] = value; }
        }
        public string LangFieldLabel
        {
            get { return ViewState["_LangFieldLabel"] == null ? "" : ViewState["_LangFieldLabel"].ToString(); }
            set { ViewState["_LangFieldLabel"] = value; }
        }
        public string WordFieldLabel
        {
            get { return ViewState["_WordFieldLabel"] == null ? "" : ViewState["_WordFieldLabel"].ToString(); }
            set { ViewState["_WordFieldLabel"] = value; }
        }
        public string MethodName
        {
            get { return ViewState["_MethodName"] == null ? "" : ViewState["_MethodName"].ToString(); }
            set { ViewState["_MethodName"] = value; }
        }
        public string MinLenght
        {
            get { return ViewState["_MinLenght"] == null ? "" : ViewState["_MinLenght"].ToString(); }
            set { ViewState["_MinLenght"] = value; }
        }
        public string ObjectLabelText
        {
            get { return ViewState["_ObjectLabelText"] == null ? "" : ViewState["_ObjectLabelText"].ToString(); }
            set { ViewState["_ObjectLabelText"] = value; }
        }
        public string ObjectLabelIdentifier
        {
            get { return txtObjectLabelIdentifier.Value; }
            set { txtObjectLabelIdentifier.Value = value; }
        }
        public string ObjectIDIdentifier
        {
            get { return txtObjectIDIdentifier.Value; }
            set { txtObjectIDIdentifier.Value = value; }
        }
        public string IDQtaType
        {
            get { return ddlQtaType.SelectedValue; }
        }
        public string IDQtaNotStd
        {
            get { return ddlQtaNotStd.SelectedValue; }
        }
        public string Qta
        {
            get { return txtQtaStd.Text; }
        }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            txtIsError.Value = "false";
            try
            {
                int intMinLenght;
                intMinLenght = Convert.ToInt32(MinLenght);
                if (intMinLenght < 3)
                {
                    MinLenght = "3";
                }
                else
                {
                    MinLenght = intMinLenght.ToString();
                }
            }
            catch
            {
                MinLenght = "3";
            }
            lblObjectName.Text = ObjectLabelText;
            if (String.IsNullOrEmpty(MethodName) ||
                    String.IsNullOrEmpty(txtObjectLabelIdentifier.Value) ||
                    String.IsNullOrEmpty(txtObjectIDIdentifier.Value) ||
                    String.IsNullOrEmpty(LanguageCode))
            {
                txtObjectName.Visible = false;
                throw new System.ArgumentException("You missed some configuration parameters.");
            }
        }
    }
}