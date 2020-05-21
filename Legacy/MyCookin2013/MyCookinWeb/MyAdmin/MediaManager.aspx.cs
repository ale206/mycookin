using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.ObjectManager.MediaManager;
using MyCookin.Common;

namespace MyCookinWeb.MyAdmin
{
    public partial class MediaManager : MyCookinWeb.Form.MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*Check Authorization to Visualize this Page
                * If not authorized, redirect to login.
               //*****************************************/
            PageSecurity SecurityPage = new PageSecurity(Session["IDUser"].ToString(), Network.GetCurrentPageName());

            if (!SecurityPage.CheckAuthorization())
            {
                Response.Redirect((AppConfig.GetValue("LoginPage", AppDomain.CurrentDomain) + "?requestedPage=" + Network.GetCurrentPageUrl()).ToLower(), true);
            }
            //******************************************
            //Check if user belong group authorized to view this page
            if (Session["IDSecurityGroupList"] != null && Session["IDSecurityGroupList"].ToString().IndexOf("292d13f2-738f-487b-b739-96c52b9e8d21") >= 0)
            {
            }
            else
            {
                Response.Redirect("/default.aspx", true);
            }

        }
        protected void btnCreateSmallSizeMedia_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtMediaType.Text))
            {
                int _imageToConvert = MyConvert.ToInt32(txtNumMedia.Text, MyConvert.ToInt32(AppConfig.GetValue("MaxNumMovedObject",
                        AppDomain.CurrentDomain), 1));
                try
                {
                    ManageUSPReturnValue _result = Photo.CreateAltSizeForMedia(_imageToConvert, 
                        MyConvert.ToInt32(AppConfig.GetValue("MaxErrorBeforeAbort", AppDomain.CurrentDomain), 1), 
                        (MediaType)Enum.Parse(typeof(MediaType), txtMediaType.Text),MediaSizeTypes.Small);

                    lblResult.Text = _result.ResultExecutionCode + "<br/>" + _result.USPReturnValue;
                }
                catch (Exception ex)
                {
                    lblResult.Text = ex.Message;
                }
            }
            else
            {
                lblResult.Text = "Specify a media type";
            }
        }

        protected void btnCreateOriginaResizedMedia_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtMediaType.Text))
            {
                int _imageToConvert = MyConvert.ToInt32(txtNumMedia.Text, MyConvert.ToInt32(AppConfig.GetValue("MaxNumMovedObject",
                        AppDomain.CurrentDomain), 1));
                try
                {
                    ManageUSPReturnValue _result = Photo.CreateAltSizeForMedia(_imageToConvert,
                        MyConvert.ToInt32(AppConfig.GetValue("MaxErrorBeforeAbort", AppDomain.CurrentDomain), 1),
                        (MediaType)Enum.Parse(typeof(MediaType), txtMediaType.Text), MediaSizeTypes.OriginalResized);

                    lblResult.Text = _result.ResultExecutionCode + "<br/>" + _result.USPReturnValue;
                }
                catch (Exception ex)
                {
                    lblResult.Text = ex.Message;
                }
            }
            else
            {
                lblResult.Text = "Specify a media type";
            }
        }

        protected void btnMoveMediaOnCDN_Click(object sender, EventArgs e)
        {
            int _imageToConvert = MyConvert.ToInt32(txtNumMedia.Text, MyConvert.ToInt32(AppConfig.GetValue("MaxNumMovedObject",
                       AppDomain.CurrentDomain), 1));
            try
            {
                ManageUSPReturnValue _result = Photo.MovePhotoOnCDN(_imageToConvert,
                               AppConfig.GetValue("AWSAccessKey", AppDomain.CurrentDomain), AppConfig.GetValue("AWSSecretKey", AppDomain.CurrentDomain),
                               AppConfig.GetValue("BucketName", AppDomain.CurrentDomain), AppConfig.GetValue("CDNBasePath", AppDomain.CurrentDomain),
                               MyConvert.ToInt32(AppConfig.GetValue("MaxErrorBeforeAbort", AppDomain.CurrentDomain), 1), MediaSizeTypes.MediaCroppedSize);
                lblResult.Text = _result.ResultExecutionCode + "<br/>" + _result.USPReturnValue;
            }
            catch (Exception ex)
            {
                lblResult.Text = ex.Message;
            }
        }

        protected void btnMoveSmallSizeOnCDN_Click(object sender, EventArgs e)
        {
            int _imageToConvert = MyConvert.ToInt32(txtNumMedia.Text, MyConvert.ToInt32(AppConfig.GetValue("MaxNumMovedObject",
                       AppDomain.CurrentDomain), 1));
            try
            {
                ManageUSPReturnValue _result = Photo.MovePhotoOnCDN(_imageToConvert,
                               AppConfig.GetValue("AWSAccessKey", AppDomain.CurrentDomain), AppConfig.GetValue("AWSSecretKey", AppDomain.CurrentDomain),
                               AppConfig.GetValue("BucketName", AppDomain.CurrentDomain), AppConfig.GetValue("CDNBasePath", AppDomain.CurrentDomain),
                               MyConvert.ToInt32(AppConfig.GetValue("MaxErrorBeforeAbort", AppDomain.CurrentDomain), 1), MediaSizeTypes.Small);
                lblResult.Text = _result.ResultExecutionCode + "<br/>" + _result.USPReturnValue;
            }
            catch (Exception ex)
            {
                lblResult.Text = ex.Message;
            }
        }

        protected void btnMoveOriginalResizedOnCDN_Click(object sender, EventArgs e)
        {
            int _imageToConvert = MyConvert.ToInt32(txtNumMedia.Text, MyConvert.ToInt32(AppConfig.GetValue("MaxNumMovedObject",
                       AppDomain.CurrentDomain), 1));
            try
            {
                ManageUSPReturnValue _result = Photo.MovePhotoOnCDN(_imageToConvert,
                               AppConfig.GetValue("AWSAccessKey", AppDomain.CurrentDomain), AppConfig.GetValue("AWSSecretKey", AppDomain.CurrentDomain),
                               AppConfig.GetValue("BucketName", AppDomain.CurrentDomain), AppConfig.GetValue("CDNBasePath", AppDomain.CurrentDomain),
                               MyConvert.ToInt32(AppConfig.GetValue("MaxErrorBeforeAbort", AppDomain.CurrentDomain), 1), MediaSizeTypes.OriginalResized);
                lblResult.Text = _result.ResultExecutionCode + "<br/>" + _result.USPReturnValue;
            }
            catch (Exception ex)
            {
                lblResult.Text = ex.Message;
            }
        }
    }
}