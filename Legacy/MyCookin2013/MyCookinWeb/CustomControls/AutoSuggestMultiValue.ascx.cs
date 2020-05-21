using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyCookinWeb.CustomControls
{
    public partial class AutoSuggestMultiValue : System.Web.UI.UserControl
    {
        #region PublicFields

        public string ReturnValue
        {
            get { return hdValue.Value; }
        }
        /// <summary>
        /// Ex. /Path/Path/Service.asmx/Method
        /// </summary>
        public string WebServiceURL
        {
            get { return hdWebServiceURL.Value; }
            set { hdWebServiceURL.Value = value; }
        }

        public string SelectedItemProp
        {
            get { return hdselectedItemProp.Value; }
            set { hdselectedItemProp.Value = value; }
        }
        public string SearchObjProps
        {
            get { return hdsearchObjProps.Value; }
            set { hdsearchObjProps.Value = value; }
        }
        public string SelectedValuesProp
        {
            get { return hdselectedValuesProp.Value; }
            set { hdselectedValuesProp.Value = value; }
        }
        public string QueryParam
        {
            get { return hdqueryParam.Value; }
            set { hdqueryParam.Value = value; }
        }
        public string QueryIDLangParam
        {
            get { return hdqueryIDLangParam.Value; }
            set { hdqueryIDLangParam.Value = value; }
        }
        public string QueryIDLangValue
        {
            get { return hdqueryIDLangValue.Value; }
            set { hdqueryIDLangValue.Value = value; }
        }
        public string MinChars
        {
            get { return hdMinChars.Value; }
            set { hdMinChars.Value = value; }
        }
        public string MaxAllowedValues
        {
            get { return hdMaxAllowedValues.Value; }
            set { hdMaxAllowedValues.Value = value; }
        }
        public string StartText
        {
            get { return hdStartText.Value; }
            set { hdStartText.Value = value; }
        }
        public string EmptyText
        {
            get { return hdEmptyText.Value; }
            set { hdEmptyText.Value = value; }
        }
        public string LimitAllowedValuesText
        {
            get { return hdLimitAllowedValuesText.Value; }
            set { hdLimitAllowedValuesText.Value = value; }
        }

        /// <summary>
        /// Other parameters in this form:
        /// ====================================
        /// ,'key1':'value1','key2':'value2',...
        /// ====================================
        /// MUST BEGIN WITH A comma ","
        /// </summary>
        public string OtherQueryParameter
        {
            get { return hdOtherQueryParameter.Value; }
            set { hdOtherQueryParameter.Value = value; }
        }

        /// <summary>
        /// Build data items like this: { IDRecipeLanguageTag: "13", Tag: "Facile" } 
        /// Separated by coma char
        /// </summary>
        public string preFillValue
        {
            get { return hdPreFillValue.Value; }
            set { hdPreFillValue.Value = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int intMaxAllowedValue;
                intMaxAllowedValue = Convert.ToInt32(hdMaxAllowedValues.Value);
            }
            catch
            {
                hdMaxAllowedValues.Value = "";
            }

            try
            {
                int intMinLenght;
                intMinLenght = Convert.ToInt32(hdMinChars.Value);
                if (intMinLenght < 3)
                {
                    hdMinChars.Value = "3";
                }
                else
                {
                    hdMinChars.Value = intMinLenght.ToString();
                }
            }
            catch
            {
                hdMinChars.Value = "3";
            }
            if (String.IsNullOrEmpty(hdWebServiceURL.Value) ||
                    String.IsNullOrEmpty(hdqueryIDLangValue.Value) ||
                    String.IsNullOrEmpty(hdqueryParam.Value) ||
                    String.IsNullOrEmpty(hdsearchObjProps.Value) ||
                    String.IsNullOrEmpty(hdselectedItemProp.Value) ||
                    String.IsNullOrEmpty(hdselectedValuesProp.Value))
            {
                throw new System.ArgumentException("You missed some configuration parameters.");
            }
        }
    }
}