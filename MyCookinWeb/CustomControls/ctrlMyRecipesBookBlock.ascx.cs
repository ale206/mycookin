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

namespace MyCookinWeb.CustomControls
{
    public partial class ctrlMyRecipesBookBlock : System.Web.UI.UserControl
    {
        bool _showEditButton;
        bool _showDeleteButton;
        #region PublicFileds

        public string IDRecipe
        {
            get { return hfIDRecipe.Value; }
            set { hfIDRecipe.Value = value; }
        }

        public string IDUser
        {
            get { return hfIDUser.Value; }
            set { hfIDUser.Value = value; }
        }

        public string IDLanguage
        {
            get { return hfIDLanguage.Value; }
            set { hfIDLanguage.Value = value; }
        }

        public bool ShowDeleteButton
        {
            get { return _showDeleteButton; }
            set { _showDeleteButton = value; }
        }

        public bool ShowEditRecipeButton
        {
            get { return _showEditButton; }
            set { _showEditButton = value; }
        }

        public string AdditionalQueryStringParameters
        {
            get { return hfQueryStringParameters.Value; }
            set { hfQueryStringParameters.Value = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            Guid _idRecipe = new Guid();
            int _idLanguage = 1;
            string _queryParameters = "";

            // MyCulture _culture = new MyCulture(MyCulture.GetBrowserCurrentCulture());
            _idLanguage = MyConvert.ToInt32(IDLanguage,MyConvert.ToInt32(Session["IDLanguage"].ToString(), 1));

            lnkRecipeOwner.Text = GetLocalResourceObject("lnkRecipeOwnerResource1.Text").ToString();
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
            string _idUser = "";
            if (_idRecipe != new Guid())
            {
                try
                {
                    try
                    {
                        _idUser = Session["IDUser"].ToString();
                    }
                    catch
                    {
                    }
                    RecipeLanguage _recipe = new RecipeLanguage(_idRecipe, _idLanguage);
                    lnkRecipeName.Text = _recipe.GetRecipeName(_idLanguage);
                    _recipe.QueryBaseRecipeInfo();
                    string _link = ("/" + MyCulture.GetLangShortCodeFromIDLanguage(_idLanguage) + AppConfig.GetValue("RoutingRecipe" + IDLanguage.ToString(), AppDomain.CurrentDomain) + lnkRecipeName.Text.Replace(" ", "-") + "/" + _recipe.IDRecipe.ToString()).ToLower();
                    //lnkRecipeName.NavigateUrl = ("/RecipeMng/ShowRecipe.aspx?IDRecipe=" + _recipe.IDRecipe.ToString() + _queryParameters).ToLower();
                    //btnImgRecipe.CommandArgument = ("/RecipeMng/ShowRecipe.aspx?IDRecipe=" + _recipe.IDRecipe.ToString() + _queryParameters).ToLower();
                    lnkRecipeName.NavigateUrl = _link;
                    btnImgRecipe.CommandArgument = _link;
                    btnImgRecipe.AlternateText = _recipe.GetRecipeName(_idLanguage);

                    pnlGlutenFree.Visible = MyConvert.ToBoolean(_recipe.GlutenFree,false);
                    pnlHotSpicy.Visible =  MyConvert.ToBoolean(_recipe.HotSpicy,false);
                    pnlVegan.Visible =  MyConvert.ToBoolean(_recipe.Vegan,false);
                    pnlVegetarian.Visible = MyConvert.ToBoolean(_recipe.Vegetarian, false);

                    if (_recipe.Owner != null && _idUser == _recipe.Owner.IDUser.ToString() && _showEditButton)
                    {
                        btnEditRecipe.Visible = true;
                        btnEditRecipe.NavigateUrl = ("/RecipeMng/EditRecipes.aspx?IDRecipe=" + hfIDRecipe.Value).ToLower();
                    }
                    else
                    {
                        btnEditRecipe.Visible = false;
 
                    }

                    if (_showDeleteButton)
                    {
                        if (_recipe.Owner != null && _idUser == _recipe.Owner.IDUser.ToString() && _showDeleteButton)
                        {
                            btnDeleteFromRecipeBook.Visible = false;
                        }
                        else
                        {
                            btnDeleteFromRecipeBook.Visible = true;
                        }
                    }
                    else
                    {
                        btnDeleteFromRecipeBook.Visible = false;
                    }

                    if (_recipe.Owner != null && _recipe.Owner.IDUser != new Guid())
                    {
                        try
                        {
                            MyUser _user = new MyUser(_recipe.Owner.IDUser);
                            _user.GetUserBasicInfoByID();
                            if (!String.IsNullOrEmpty(_user.Name) && !String.IsNullOrEmpty(_user.UserName))
                            {
                                lnkRecipeOwner.Text = lnkRecipeOwner.Text.Replace("{0}", _user.Name + " " + _user.Surname);
                                lnkRecipeOwner.NavigateUrl = ("/" + _user.UserName + "/").ToLower();
                                lnkRecipeOwner.Visible = true;
                            }
                            else
                            {
                                lnkRecipeOwner.Visible = false;
                            }
                            
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
                    
                    //try
                    //{
                    //    if (ShowDeleteButton)
                    //    {
                    //        btnDeleteFromRecipeBook.Visible = true;
                    //    }
                    //    else
                    //    {
                    //        btnDeleteFromRecipeBook.Visible = false;
                    //    }
                    //}
                    //catch
                    //{
                    //    btnDeleteFromRecipeBook.Visible = false;
                    //}

                    if (_recipe.RecipeImage != null && _recipe.RecipeImage != new Guid())
                    {
                        string _pathImage = "";
                        _pathImage = _recipe.RecipeImage.GetAlternativeSizePath(MediaSizeTypes.Small, false, false, true);
                        if(String.IsNullOrEmpty(_pathImage))
                        {
                            _pathImage = _recipe.RecipeImage.GetCompletePath(false, false, true);
                        }
                        btnImgRecipe.ImageUrl = _pathImage;
                        btnImgRecipe.CssClass = "imgRecipeSmall";
                    }
                    else
                    {
                        btnImgRecipe.ImageUrl = DefaultMedia.GetDefaultMediaPathFromWebConfig(MediaType.RecipePhoto);
                        btnImgRecipe.CssClass = "imgRecipeNoPhotoSmall";
                    }
                    
                }
                catch
                {
                    //Gestire LOG
                }
            }
        }

        protected void btnDeleteFromRecipeBook_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                RecipeBook _recipeBook = new RecipeBook(new Guid(hfIDUser.Value), new Guid(hfIDRecipe.Value), 1);

                ManageUSPReturnValue delResult = _recipeBook.DeleteRecipe();

                if (!delResult.IsError)
                {
                    MyRecipeBookBlockInt.Visible = false;
                }
            }
            catch
            {
                //Gestire LOG
            }
        }

        protected void btnImgRecipe_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton _btn = (ImageButton)sender;
                Response.Redirect(_btn.CommandArgument, false);
            }
            catch
            {
            }
        }
    }
}