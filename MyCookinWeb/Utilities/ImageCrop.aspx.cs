using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using MyCookin.Common;
using MyCookin.Log;
using MyCookin.ErrorAndMessage;
using MyCookin.ObjectManager.MediaManager;

namespace MyCookinWeb.Utilities
{
    public partial class ImageCrop :  MyCookinWeb.Form.MyPageBase
    {
        bool _readOnly = true;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (!PageSecurity.IsPublicProfile())
            {
                this.Page.MasterPageFile = "~/Styles/SiteStyle/Private.Master";
                _readOnly = false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["IDMedia"] != null && Request.QueryString["ReturnURL"] != null && Request.QueryString["MediaType"] != null)
            {
                Media _photoToCrop = new Media(new Guid(Request.QueryString["IDMedia"].ToString()));
                MediaType _mediaType = MediaType.NotSpecified;
                _photoToCrop.QueryMediaInfo();
                if (_photoToCrop.mediaType == MediaType.NotSpecified)
                {
                    _mediaType = (MediaType)Enum.Parse(typeof(MediaType), Request.QueryString["MediaType"].ToString());
                }
                else
                {
                    _mediaType = _photoToCrop.mediaType;
                }

                MediaUploadConfig _upConfig = new MediaUploadConfig(_mediaType);
                hfMinCropWidth.Value = _upConfig.MediaFinalWidth.ToString();
                hfMinCropHeight.Value = _upConfig.MediaFinalHeight.ToString();
                hfCropAspectRatio.Value = (_upConfig.MediaFinalWidth / _upConfig.MediaFinalHeight).ToString();

                string _imageUrl = "";
                _imageUrl = _photoToCrop.GetAlternativeSizePath(MediaSizeTypes.OriginalResized, false, false, true);
                if (!String.IsNullOrEmpty(_imageUrl))
                {
                    imgToCrop.ImageUrl = _imageUrl;
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(),
                    //          "CallJCropNew('#" + hfMinCropWidth.ClientID + "', '#" + hfMinCropHeight.ClientID + "', '#" + hfCropAspectRatio.ClientID + "', '#" + imgToCrop.ClientID + "', '#" + hfX1.ClientID + "','#" + hfY1.ClientID + "','#" + hfX2.ClientID + "','#" + hfY2.ClientID + "','#" + hfWidth.ClientID + "','#" + hfHeight.ClientID + "')", true);
                }
                else
                {
                    Response.Redirect("/Default.aspx", true);
                }
            }
            else
            {
                Response.Redirect("/Default.aspx", true);
            }
        }

        protected void btnCrop_Click(object sender, EventArgs e)
        {
            try
            {
                if (MyConvert.ToInt32(hfWidth.Value, 0) != 0)
                {
                    Photo _photo = new Photo(new Guid(Request.QueryString["IDMedia"].ToString()));
                    _photo.QueryMediaInfo();

                    MediaType _mediaType = MediaType.NotSpecified;
                    if (_photo.mediaType == MediaType.NotSpecified)
                    {
                        _mediaType = (MediaType)Enum.Parse(typeof(MediaType), Request.QueryString["MediaType"].ToString());
                    }
                    else
                    {
                        _mediaType = _photo.mediaType;
                    }

                    File.Copy(Server.MapPath(imgToCrop.ImageUrl), Server.MapPath(_photo.MediaPath), true);
                    Photo.Crop(Server.MapPath(_photo.MediaPath), MyConvert.ToInt32(hfX1.Value, 0),
                                        MyConvert.ToInt32(hfY1.Value, 0), MyConvert.ToInt32(hfWidth.Value, 0),
                                        MyConvert.ToInt32(hfHeight.Value, 0));
                    _photo.MediaOnCDN = false;
                    _photo.SavePhotoDbInfo(false);
                    _photo.AddAlternativePhotoSize(MediaSizeTypes.Small, _mediaType);
                    Response.Redirect(Request.QueryString["ReturnURL"].ToString(), true);
                }
            }
            catch
            {
                if (Request.QueryString["ReturnURL"] != null)
                {
                    Response.Redirect(Request.QueryString["ReturnURL"].ToString(), true);
                }
                else
                {
                    Response.Redirect("/Default.aspx", true);
                }
            }
        }
    }
}