using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.Common;
using MyCookin.ObjectManager.RecipeManager;
using MyCookin.ObjectManager.UserManager;
using MyCookin.ObjectManager.MediaManager;
using MyCookin.ObjectManager.StatisticsManager;

namespace MyCookinWeb.CustomControls
{
    public partial class ctrlRecipePolaroid : System.Web.UI.UserControl
    {
        #region PublicFileds

        public string IDRecipe
        {
            get { return hfIDRecipe.Value; }
            set { hfIDRecipe.Value = value; }
        }

        public int IDLanguage
        {
            get { return rcRecipe.IDLanguage; }
            set { rcRecipe.IDLanguage = value; }
        }

        public string AdditionalQueryStringParameters
        {
            get { return hfQueryStringParameters.Value; }
            set { hfQueryStringParameters.Value = value; }
        }

        //public int ControlSize
        //{
        //    get { return rcRecipe.ControlSize; }
        //    set { rcRecipe.ControlSize = value; }
        //}

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            Guid _idRecipe = new Guid();
            int _idLanguage = 1;
            string _queryParameters = "";

           // MyCulture _culture = new MyCulture(MyCulture.GetBrowserCurrentCulture());
            _idLanguage =MyConvert.ToInt32(IDLanguage,MyConvert.ToInt32(Session["IDLanguage"].ToString(),1));
            rcRecipe.IDLanguage = _idLanguage;
            try
            {
                _idRecipe = new Guid(hfIDRecipe.Value);
            }
            catch
            {
            }

            if (!String.IsNullOrEmpty(hfQueryStringParameters.Value))
            {
                _queryParameters = "&" + hfQueryStringParameters.Value;
            }

            rcRecipe.LoadControl = false;

            if (_idRecipe != new Guid())
            {
                rcRecipe.LoadControl = true;

                try
                {
                    RecipeLanguage _recipe = new RecipeLanguage(_idRecipe, _idLanguage);
                    lnkRecipeName.Text = _recipe.GetRecipeName(_idLanguage);
                    _recipe.QueryBaseRecipeInfo();
                    string _link = ("/" + MyCulture.GetLangShortCodeFromIDLanguage(_idLanguage) + AppConfig.GetValue("RoutingRecipe" + IDLanguage.ToString(), AppDomain.CurrentDomain) + lnkRecipeName.Text.Replace(" ", "-") + "/" + _recipe.IDRecipe.ToString()).ToLower();
                    lnkImage.NavigateUrl = _link;
                    lnkRecipeName.NavigateUrl = _link;
                    
                    impRecipePhoto.AlternateText = _recipe.RecipeName;

                    if (_recipe.Owner != null && _recipe.Owner.IDUser != new Guid())
                    {
                        try
                        {
                            MyUser _user = new MyUser(_recipe.Owner.IDUser);
                            _user.GetUserBasicInfoByID();
                            lnkRecipeOwner.Text = _user.Name + " " + _user.Surname;
                            lnkRecipeOwner.NavigateUrl = ("/" + _user.UserName + "/").ToLower();
                            lnkRecipeOwner.Visible = true;
                        }
                        catch
                        {
                            lnkRecipeOwner.Visible = false;
                        }
                    }
                    else
                    {
                        lnkRecipeOwner.Visible = false;
                    }

                    if (_recipe.RecipeImage != null && _recipe.RecipeImage != new Guid())
                    {
                        impRecipePhoto.ImageUrl = _recipe.RecipeImage.GetCompletePath(false, false, true);
                    }
                    else
                    {
                        impRecipePhoto.ImageUrl = DefaultMedia.GetDefaultMediaPathFromWebConfig(MediaType.RecipePhoto);
                    }
                    try
                    {
                        rcRecipe.ComplexityLevel = (int)_recipe.RecipeDifficulties;
                    }
                    catch
                    {
                    }
                }
                catch
                {
                }

            }
        }
    }
}