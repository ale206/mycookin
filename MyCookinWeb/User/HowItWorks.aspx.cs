using System;
using System.Web;
using System.Web.UI;
using MyCookin.Common;
using MyCookin.ErrorAndMessage;

namespace MyCookinWeb.User
{
    public partial class HowItWorks : Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (MyCookin.ObjectManager.UserManager.MyUser.CheckUserLogged())
            {
                this.Page.MasterPageFile = "~/Styles/SiteStyle/Private.Master";
            }
            else
            {
                this.Page.MasterPageFile = "~/Styles/SiteStyle/Public.Master";
            }

            //if (!PageSecurity.IsPublicProfile())
            //{
            //    this.Page.MasterPageFile = "~/Styles/SiteStyle/Private.Master";
            //}
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblTitle.Text =
                RetrieveMessage.RetrieveDBMessage(
                    MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0088");

            LoadMenu();

            ShowPnlVoteRecipe();
        }

        #region LoadMenu

        private void LoadMenu()
        {
            imgbVoteRecipeMenu.Width = 50;
            imgbVoteRecipeMenu.ToolTip =
                RetrieveMessage.RetrieveDBMessage(
                    MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0080");
            imgbVoteRecipeMenu.ImageUrl = "/Images/icon-vote-recipe.png";
            imgbVoteRecipeMenu.PostBackUrl = "#";
            imgbVoteRecipeMenu.CssClass = "MenuImages";

            imgbFollowPeopleMenu.Width = 50;
            imgbFollowPeopleMenu.ToolTip =
                RetrieveMessage.RetrieveDBMessage(
                    MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0081");
            imgbFollowPeopleMenu.ImageUrl = "/Images/icon-following-people.png";
            imgbFollowPeopleMenu.PostBackUrl = "#";
            imgbFollowPeopleMenu.CssClass = "MenuImages";

            imgbSearchEngineMenu.Width = 50;
            imgbSearchEngineMenu.ToolTip =
                RetrieveMessage.RetrieveDBMessage(
                    MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0082");
            imgbSearchEngineMenu.ImageUrl = "/Images/icon-search-engine.png";
            imgbSearchEngineMenu.PostBackUrl = "#";
            imgbSearchEngineMenu.CssClass = "MenuImages";

            imgbCommentRecipeMenu.Width = 50;
            imgbCommentRecipeMenu.ToolTip =
                RetrieveMessage.RetrieveDBMessage(
                    MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0083");
            imgbCommentRecipeMenu.ImageUrl = "/Images/icon-manageRecipe-color.png";
            imgbCommentRecipeMenu.PostBackUrl = "#";
            imgbCommentRecipeMenu.CssClass = "MenuImages";

            imgbWriteOnBulletinBoardMenu.Width = 50;
            imgbWriteOnBulletinBoardMenu.ToolTip =
                RetrieveMessage.RetrieveDBMessage(
                    MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0084");
            imgbWriteOnBulletinBoardMenu.ImageUrl = "/Images/icon-pencil-color.png";
            imgbWriteOnBulletinBoardMenu.PostBackUrl = "#";
            imgbWriteOnBulletinBoardMenu.CssClass = "MenuImages";

            imgbLoginFbGoogleMenu.Width = 50;
            imgbLoginFbGoogleMenu.ToolTip =
                RetrieveMessage.RetrieveDBMessage(
                    MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0085");
            imgbLoginFbGoogleMenu.ImageUrl = "/Images/icon-login.png";
            imgbLoginFbGoogleMenu.PostBackUrl = "#";
            imgbLoginFbGoogleMenu.CssClass = "MenuImages";

            imgbShareRecipeMenu.Width = 50;
            imgbShareRecipeMenu.ToolTip =
                RetrieveMessage.RetrieveDBMessage(
                    MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0086");
            imgbShareRecipeMenu.ImageUrl = "/Images/icon-cooking.png";
            imgbShareRecipeMenu.PostBackUrl = "#";
            imgbShareRecipeMenu.CssClass = "MenuImages";

            imgbFoodBloggerMenu.Width = 50;
            imgbFoodBloggerMenu.ToolTip =
                RetrieveMessage.RetrieveDBMessage(
                    MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0087");
            imgbFoodBloggerMenu.ImageUrl = "/Images/food-blogger.png";
            imgbFoodBloggerMenu.PostBackUrl = "#";
            imgbFoodBloggerMenu.CssClass = "MenuImages";
        }

        #endregion

        #region HideAllPanels

        private void HideAllPanels()
        {
            pnlVoteRecipe.Visible = false;
            pnlFollowPeople.Visible = false;
            pnlCommentRecipe.Visible = false;
            pnlFoodBlogger.Visible = false;
            pnlLoginFbGoogle.Visible = false;
            pnlSearchEngine.Visible = false;
            pnlShareRecipe.Visible = false;
            pnlWriteOnBulletinBoard.Visible = false;
        }

        #endregion

        #region ShowPnlVoteRecipe

        private void ShowPnlVoteRecipe()
        {
            pnlVoteRecipe.Visible = true;

            imgVoteRecipe.ImageUrl = "/Images/Tips-It-Votare-una-ricetta.png";
            imgVoteRecipe.Width = 400;
            imgVoteRecipe.AlternateText =
                RetrieveMessage.RetrieveDBMessage(
                    MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0080");

            lblVoteRecipeTitle.Text =
                RetrieveMessage.RetrieveDBMessage(
                    MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0080");
            lblVoteRecipe.Text =
                RetrieveMessage.RetrieveDBMessage(
                    MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0072");
        }

        #endregion

        #region ShowPnlFollowPeople

        private void ShowPnlFollowPeople()
        {
            pnlFollowPeople.Visible = true;

            imgFollowPeople.ImageUrl = "/Images/icon_follower.png";
            imgFollowPeople.Width = 150;
            imgFollowPeople.AlternateText =
                RetrieveMessage.RetrieveDBMessage(
                    MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0081");

            lblFollowPeopleTitle.Text =
                RetrieveMessage.RetrieveDBMessage(
                    MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0081");
            lblFollowPeople.Text =
                RetrieveMessage.RetrieveDBMessage(
                    MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0073");
        }

        #endregion

        #region ShowPnlSearchEngine

        private void ShowPnlSearchEngine()
        {
            pnlSearchEngine.Visible = true;

            switch (MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1))
            {
                case 1:
                    imgSearchEngine.ImageUrl = "/Images/Search-Engine-EN.png";
                    break;
                case 2:
                    imgSearchEngine.ImageUrl = "/Images/Search-Engine-IT.png";
                    break;
                case 3:
                    imgSearchEngine.ImageUrl = "/Images/Search-Engine-ES.png";
                    break;
            }

            imgSearchEngine.Width = 400;
            imgSearchEngine.AlternateText =
                RetrieveMessage.RetrieveDBMessage(
                    MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0082");

            lblSearchEngineTitle.Text =
                RetrieveMessage.RetrieveDBMessage(
                    MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0082");
            lblSearchEngine.Text =
                RetrieveMessage.RetrieveDBMessage(
                    MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0074");
        }

        #endregion

        #region ShowPnlCommentRecipe

        private void ShowPnlCommentRecipe()
        {
            pnlCommentRecipe.Visible = true;

            switch (MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1))
            {
                case 1:
                    imgCommentRecipe.ImageUrl = "/Images/Comment-Recipe-EN.png";
                    break;
                case 2:
                    imgCommentRecipe.ImageUrl = "/Images/Comment-Recipe-IT.png";
                    break;
                case 3:
                    imgCommentRecipe.ImageUrl = "/Images/Comment-Recipe-ES.png";
                    break;
            }

            imgCommentRecipe.Width = 400;
            imgCommentRecipe.AlternateText =
                RetrieveMessage.RetrieveDBMessage(
                    MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0083");

            lblCommentRecipeTitle.Text =
                RetrieveMessage.RetrieveDBMessage(
                    MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0083");
            lblCommentRecipe.Text =
                RetrieveMessage.RetrieveDBMessage(
                    MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0075");
        }

        #endregion

        #region ShowPnlWriteOnBulletinBoard

        private void ShowPnlWriteOnBulletinBoard()
        {
            pnlWriteOnBulletinBoard.Visible = true;

            imgWriteOnBulletinBoard.ImageUrl = "/Images/MyCookin-Messages.png";
            imgWriteOnBulletinBoard.Width = 400;
            imgWriteOnBulletinBoard.AlternateText =
                RetrieveMessage.RetrieveDBMessage(
                    MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0084");

            lblWriteOnBulletinBoardTitle.Text =
                RetrieveMessage.RetrieveDBMessage(
                    MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0084");
            lblWriteOnBulletinBoard.Text =
                RetrieveMessage.RetrieveDBMessage(
                    MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0076");
        }

        #endregion

        #region ShowPnlLoginFbGoogle

        private void ShowPnlLoginFbGoogle()
        {
            pnlLoginFbGoogle.Visible = true;

            switch (MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1))
            {
                case 1:
                    imgLoginFbGoogle.ImageUrl = "/Images/Login-EN.png";
                    break;
                case 2:
                    imgLoginFbGoogle.ImageUrl = "/Images/Login-IT.png";
                    break;
                case 3:
                    imgLoginFbGoogle.ImageUrl = "/Images/Login-ES.png";
                    break;
            }

            imgLoginFbGoogle.Width = 400;
            imgLoginFbGoogle.AlternateText =
                RetrieveMessage.RetrieveDBMessage(
                    MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0085");

            lblLoginFbGoogleTitle.Text =
                RetrieveMessage.RetrieveDBMessage(
                    MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0085");
            lblLoginFbGoogle.Text =
                RetrieveMessage.RetrieveDBMessage(
                    MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0077");
        }

        #endregion

        #region ShowPnlShareRecipe

        private void ShowPnlShareRecipe()
        {
            pnlShareRecipe.Visible = true;

            imgShareRecipe.ImageUrl = "/Images/Tips-It-Votare-una-ricetta.png";
            imgShareRecipe.Width = 400;
            imgShareRecipe.AlternateText =
                RetrieveMessage.RetrieveDBMessage(
                    MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0086");

            lblShareRecipeTitle.Text =
                RetrieveMessage.RetrieveDBMessage(
                    MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0086");
            lblShareRecipe.Text =
                RetrieveMessage.RetrieveDBMessage(
                    MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0078");
        }

        #endregion

        #region ShowPnlFoodBlogger

        private void ShowPnlFoodBlogger()
        {
            pnlFoodBlogger.Visible = true;

            imgFoodBlogger.ImageUrl = "/Images/icon-food-blogger.png";
            imgFoodBlogger.Width = 150;
            imgFoodBlogger.AlternateText =
                RetrieveMessage.RetrieveDBMessage(
                    MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0087");

            lblFoodBloggerTitle.Text =
                RetrieveMessage.RetrieveDBMessage(
                    MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0087");
            lblFoodBlogger.Text =
                RetrieveMessage.RetrieveDBMessage(
                    MyConvert.ToInt32(HttpContext.Current.Session["IDLanguage"].ToString(), 1), "US-IN-0079");
        }

        #endregion

        #region imgButtons

        protected void imgbVoteRecipeMenu_Click(object sender, ImageClickEventArgs e)
        {
            HideAllPanels();
            ShowPnlVoteRecipe();
        }

        protected void imgbFollowPeopleMenu_Click(object sender, ImageClickEventArgs e)
        {
            HideAllPanels();
            ShowPnlFollowPeople();
        }

        protected void imgbSearchEngineMenu_Click(object sender, ImageClickEventArgs e)
        {
            HideAllPanels();
            ShowPnlSearchEngine();
        }

        protected void imgbCommentRecipeMenu_Click(object sender, ImageClickEventArgs e)
        {
            HideAllPanels();
            ShowPnlCommentRecipe();
        }

        protected void imgbWriteOnBulletinBoardMenu_Click(object sender, ImageClickEventArgs e)
        {
            HideAllPanels();
            ShowPnlWriteOnBulletinBoard();
        }

        protected void imgbLoginFbGoogleMenu_Click(object sender, ImageClickEventArgs e)
        {
            HideAllPanels();
            ShowPnlLoginFbGoogle();
        }

        protected void imgbShareRecipeMenu_Click(object sender, ImageClickEventArgs e)
        {
            HideAllPanels();
            ShowPnlShareRecipe();
        }

        protected void imgbFoodBloggerMenu_Click(object sender, ImageClickEventArgs e)
        {
            HideAllPanels();
            ShowPnlFoodBlogger();
        }

        #endregion
    }
}