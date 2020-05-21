using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.ObjectManager.RecipeManager;
using MyCookin.ObjectManager.IngredientManager;
using MyCookin.Common;
using MyCookin.ObjectManager.MediaManager;
using MyCookin.ObjectManager.UserManager;

namespace MyCookinWeb.CustomControls
{
    public partial class ctrlBeverageRecipe : System.Web.UI.UserControl
    {
        #region PublicFiled

        public string IDBeverageRecipe
        {
            get { return hfIDBeverageRecipe.Value; }
            set { hfIDBeverageRecipe.Value = value; }
        }

        public string IDRecipe
        {
            get { return hfIDRecipe.Value; }
            set { hfIDRecipe.Value = value; }
        }

        public string IDBeverage
        {
            get { return hfIDBeverage.Value; }
            set { hfIDBeverage.Value = value; }
        }

        public string IDUserSuggestedBy
        {
            get { return hfIDUserSuggestedBy.Value; }
            set { hfIDUserSuggestedBy.Value = value; }
        }

        public string DateSuggestion
        {
            get { return hfDateSuggestion.Value ; }
            set { hfDateSuggestion.Value = value; }
        }

        public string BeverageRecipeAvgRating
        {
            get { return hfBeverageRecipeAvgRating.Value; }
            set { hfBeverageRecipeAvgRating.Value = value; }
        }

        public string IDLanguage
        {
            get { return hfIDLanguage.Value; }
            set { hfIDLanguage.Value = value; }
        }

        public string ShowEditButton
        {
            get { return hfShowEditButton.Value; }
            set { hfShowEditButton.Value = value; }
        }

        public string DeleteButtonToolTip
        {
            get { return btnDeleteRecipeBeverage.ToolTip; }
            set { btnDeleteRecipeBeverage.ToolTip = value; }
        }

        public string DeleteButtonOnClientClick
        {
            get { return btnDeleteRecipeBeverage.OnClientClick; }
            set { btnDeleteRecipeBeverage.OnClientClick = value; }
        }

        public bool ShowInfoPanel
        {
            get { return MyConvert.ToBoolean(hfShowInfoPanel.Value, false); }
            set { hfShowInfoPanel.Value = value.ToString(); }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void DataBindChildren()
        {
            LoadControl();
            
        }

        private void LoadControl()
        {
            if (ShowInfoPanel)
            {
                lnkBeverage.NavigateUrl = "#";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                 "StartPopBox('#" + pnlBeverageInfoBox.ClientID + "','#" + lnkBeverage.ClientID + "','#" + pnlBoxInternal.ClientID + "');", true);
            }
            else
            {
                lnkBeverage.NavigateUrl = "";
            }

            if (hfShowEditButton.Value == "true")
            {
                btnDeleteRecipeBeverage.Visible = true;
            }
            else
            {
                btnDeleteRecipeBeverage.Visible = false;
            }

            if (hfControlLoaded.Value != "true")
            {
                 IngredientLanguage _beverage;

                //Get Ingredient Information
                 try
                 {
                     _beverage = new IngredientLanguage(new Guid(IDBeverage), MyConvert.ToInt32(IDLanguage, 1));

                     _beverage.QueryIngredientLanguageInfo();

                     LoadBeverageInfoBox(_beverage);

                     lnkBeverage.Text = _beverage.IngredientSingular;

                     if (ShowInfoPanel)
                     {
                         try
                         {
                             MyUser _user = new MyUser(new Guid(IDUserSuggestedBy));
                             _user.GetUserBasicInfoByID();

                             lnkUser.Text = _user.UserName;
                             lnkUser.NavigateUrl = ("/" + _user.UserName + "/").ToLower();
                             lnkUser.Target = "_blank";
                         }
                         catch
                         {
                         }

                         lblDateInfo.Text = DateSuggestion;
                     }
                     hfControlLoaded.Value = "true";
                 }
                catch (Exception ex)
                {
                    //lnkBeverage.Text = "ERROR " + ex.Message;
                    hfControlLoaded.Value = "false";
                }
            }
        }

        protected void LoadBeverageInfoBox(IngredientLanguage _beverage)
        {

            #region BoxBeverageInfo

            if (ShowInfoPanel)
            {

                try
                {
                    if (_beverage.IngredientImage != null && _beverage.IngredientImage.IDMedia != new Guid())
                    {
                        Photo _ingrPhoto = new Photo(_beverage.IngredientImage.IDMedia);
                        _ingrPhoto.QueryMediaInfo();

                        imgBeveragePhoto.ImageUrl = _ingrPhoto.GetCompletePath();
                        imgBeveragePhoto.AlternateText = _beverage.IngredientSingular;
                    }

                    lblBeverageLink.Text = "<a href=\"" + AppConfig.GetValue("RoutingIngredient", AppDomain.CurrentDomain) + "" + _beverage.IngredientSingular + "\" target=\"_blank\">" + _beverage.IngredientSingular + "</a>";

                    lblBeverageBasicInfo.Text = _beverage.IngredientDescription;

                    _beverage.GetIngredientCategories();

                    if (_beverage.IngredientCategories != null && _beverage.IngredientCategories[0] != null)
                    {
                        lblBeverageBasicInfo.Text += IngredientCategoryLanguage.GetIngredientCategoryLang(_beverage.IngredientCategories[0].IDIngredientCategory, MyConvert.ToInt32(IDLanguage, 1));
                    }

                }
                catch
                {

                }
            }

            #endregion

        }

        protected void btnDeleteRecipeBeverage_Click(object sender, ImageClickEventArgs e)
        {
            BeverageRecipe _brDelete = new BeverageRecipe(new Guid(hfIDBeverageRecipe.Value));
            ManageUSPReturnValue delResult = _brDelete.Delete();
            if (!delResult.IsError)
            {
                pnlBeverageInfoBox.Visible = false;
            }
        }
    }
}