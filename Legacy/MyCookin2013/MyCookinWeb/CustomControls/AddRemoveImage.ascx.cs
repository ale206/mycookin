using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MyCookin.ObjectManager.MediaManager;
using MyCookin.ErrorAndMessage;
using MyCookin.Common;
using System.Drawing;
using System.IO;


namespace MyCookinWeb.CustomControls
{
    public partial class AddRemoveImage : System.Web.UI.UserControl
    {
        private bool _isInError;
        private string _ReturnMessage;


        #region PublicFields
        public bool isInError
        {
            get { return _isInError; }
        }
        public string ImageName
        {
            get { return ViewState["_ImageName"] == null ? "" : ViewState["_ImageName"].ToString(); }
            set { ViewState["_ImageName"] = value; }
        }
        public string ReturnMessage
        {
            get { return _ReturnMessage; }
        }
        public string imgUploadedImgPath
        {
            get { return ViewState["_imgUploadedImgPath"] == null ? "" : ViewState["_imgUploadedImgPath"].ToString(); }
            set { ViewState["_imgUploadedImgPath"] = value; }
        }
        public Guid IDMedia
        {
            get { return ViewState["_IDMedia"] == null ? new Guid() : new Guid(ViewState["_IDMedia"].ToString()); }
            set { ViewState["_IDMedia"] = value; }
        }
        public Guid IDMediaOwner
        {
            get { return ViewState["_IDMediaOwner"] == null ? new Guid() : new Guid(ViewState["_IDMediaOwner"].ToString()); }
            set { ViewState["_IDMediaOwner"] = value; }
        }
        public string MD5Hash
        {
            get { return ViewState["_MD5Hash"] == null ? "" : ViewState["_MD5Hash"].ToString(); }
            set { ViewState["_MD5Hash"] = value; }
        }
        public MediaType ImageMediaType
        {
            get { return (MediaType)Enum.Parse(typeof(MediaType), ViewState["_ImageMediaType"] == null ? "" : ViewState["_ImageMediaType"].ToString()); }
            set { ViewState["_ImageMediaType"] = value; }
        }
        public string CropErrorBoxTitle
        {
            get { return ViewState["_CropErrorBoxTitle"] == null ? "" : ViewState["_CropErrorBoxTitle"].ToString(); }
            set { ViewState["_CropErrorBoxTitle"] = value; }
        }
        public string CropErrorBoxMsg
        {
            get { return ViewState["_CropErrorBoxMsg"] == null ? "" : ViewState["_CropErrorBoxMsg"].ToString(); }
            set { ViewState["_CropErrorBoxMsg"] = value; }
        }
        public string CropButtonText
        {
            get { return ViewState["_CropButtonText"] == null ? "" : ViewState["_CropButtonText"].ToString(); }
            set { ViewState["_CropButtonText"] = value; }
        }
        public string DeleteButtonText
        {
            get { return ViewState["_DeleteButtonText"] == null ? "" : ViewState["_DeleteButtonText"].ToString(); }
            set { ViewState["_DeleteButtonText"] = value; }
        }
        public string CropAspectRatio
        {
            get { return ViewState["_CropAspectRatio"] == null ? "" : ViewState["_CropAspectRatio"].ToString(); }
            set { ViewState["_CropAspectRatio"] = value; }
        }
        public bool UseImegeButton
        {
            get { return Convert.ToBoolean(ViewState["_UseImegeButton"] == null ? 0 : ViewState["_UseImegeButton"]); }
            set { ViewState["_UseImegeButton"] = value; }
        }
        public string CropImgButtonPath
        {
            get { return ViewState["_CropImgButtonPath"] == null ? "" : ViewState["_CropImgButtonPath"].ToString(); }
            set { ViewState["_CropImgButtonPath"] = value; }
        }
        public string DeleteImgButtonPath
        {
            get { return ViewState["_DeleteImgButtonPath"] == null ? "" : ViewState["_DeleteImgButtonPath"].ToString(); }
            set { ViewState["_DeleteImgButtonPath"] = value; }
        }
        public string DeleteWarningMsg
        {
            get { return ViewState["_DeleteWarningMsg"] == null ? "" : ViewState["_DeleteWarningMsg"].ToString(); }
            set { ViewState["_DeleteWarningMsg"] = value; }
        }
        public string DeleteConfirm
        {
            get { return ViewState["_DeleteConfirm"] == null ? "" : ViewState["_DeleteConfirm"].ToString(); }
            set { ViewState["_DeleteConfirm"] = value; }
        }
        public string DeleteUndo
        {
            get { return ViewState["_DeleteUndo"] == null ? "" : ViewState["_DeleteUndo"].ToString(); }
            set { ViewState["_DeleteUndo"] = value; }
        }
        public string BaseFileName
        {
            get { return ViewState["_BaseFileName"] == null ? "" : ViewState["_BaseFileName"].ToString(); }
            set { ViewState["_BaseFileName"] = value; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                               "document.getElementById('uploadControl').style.visibility = 'visible'; " +
                               " top.document.getElementById('ImageToCrop').style.visibility = 'visible'; " +
                                " top.document.getElementById('btnCropContainer').style.visibility = 'hidden'; " +
                            " top.document.getElementById('DeleteBtn').style.visibility = 'hidden'; ", true);

                _isInError = false;

                #region Load Inizialize Component

                if (DeleteWarningMsg == "" || DeleteWarningMsg == null)
                {
                    DeleteWarningMsg = "Are you sure?";
                }

                if (DeleteConfirm == "" || DeleteConfirm == null)
                {
                    DeleteConfirm = "YES, delete";
                }

                if (DeleteUndo == "" || DeleteUndo == null)
                {
                    DeleteUndo = "NO, do not delete";
                }

                if (CropAspectRatio == null || CropAspectRatio == "")
                {
                    CropAspectRatio = "1";
                }

                txtCropAspectRatio.Text = CropAspectRatio.ToString();

                if (CropButtonText == "" || CropButtonText == null)
                {
                    CropButtonText = "Crop Image";
                }

                btnCrop.Text = CropButtonText;

                if (DeleteButtonText == "" || DeleteButtonText == null)
                {
                    DeleteButtonText = "Delete Image";
                }
                btnDeleteImg.Text = DeleteButtonText;

                if (CropImgButtonPath == "" || CropImgButtonPath == null || DeleteImgButtonPath == "" || DeleteImgButtonPath == null)
                {
                    UseImegeButton = false;
                }

                if (UseImegeButton == true)
                {
                    btnCrop.Visible = false;
                    btnCrop2.Visible = true;
                    btnDeleteImg.Visible = false;
                    btnDeleteImg2.Visible = true;
                }
                else
                {
                    btnCrop.Visible = true;
                    btnCrop2.Visible = false;
                    btnDeleteImg.Visible = true;
                    btnDeleteImg2.Visible = false;
                }
                btnCrop2.ImageUrl = CropImgButtonPath;
                btnDeleteImg2.ImageUrl = DeleteImgButtonPath;

                btnDeleteImg.OnClientClick = "return JCOnfirm(this,'" + DeleteButtonText.Replace("'", "\\'") + "','" + DeleteWarningMsg.Replace("'", "\\'") + "','" + DeleteConfirm.Replace("'", "\\'") + "','" + DeleteUndo.Replace("'", "\\'") + "');";
                btnDeleteImg2.OnClientClick = "return JCOnfirm(this,'" + DeleteButtonText.Replace("'", "\\'") + "','" + DeleteWarningMsg.Replace("'", "\\'") + "','" + DeleteConfirm.Replace("'", "\\'") + "','" + DeleteUndo.Replace("'", "\\'") + "');";

                if (CropErrorBoxTitle == "" || CropErrorBoxTitle == null)
                {
                    CropErrorBoxTitle = "Crop Error";
                }

                if (CropErrorBoxMsg == "" || CropErrorBoxMsg == null)
                {
                    CropErrorBoxMsg = "Cannot crop image with an empty selection. Select an area and retry.";
                }

                btnCrop.OnClientClick = "return CheckCrop('" + CropErrorBoxTitle.Replace("'", "\\'") + "','" + CropErrorBoxMsg.Replace("'", "\\'") + "');";
                btnCrop2.OnClientClick = "return CheckCrop('" + CropErrorBoxTitle.Replace("'", "\\'") + "','" + CropErrorBoxMsg.Replace("'", "\\'") + "');";

                #endregion

                if (!String.IsNullOrEmpty(txtIDMedia.Text) && Page.IsPostBack)
                {
                    IDMedia = new Guid(txtIDMedia.Text);
                }

                if (IDMedia != new Guid())
                {

                    Photo _Image;
                    _Image = new Photo(IDMedia);
                    if (_Image.MediaPath != null)
                    {
                        imgUploadedImg.ImageUrl = _Image.GetCompletePath();
                        txtHaveImageLoad.Text = "YES";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                               "document.getElementById('uploadControl').style.visibility = 'hidden'; " +
                                " top.document.getElementById('btnCropContainer').style.visibility = 'hidden'; " +
                            " top.document.getElementById('DeleteBtn').style.visibility = 'visible'; ", true);
                    }
                    else
                    {
                        imgUploadedImg.ImageUrl = "";
                        txtHaveImageLoad.Text = "NO";
                    }

                }
            }
        }

        #region CROP Image

        protected void CropImage()
        {
            _isInError = false;
            try
            {
                SaveImage();
            }
            catch (Exception ex)
            {
                _isInError = true;
                _ReturnMessage = "Error crop image " + ex.Message;
            }
        }

        protected void btnCrop_Click(object sender, EventArgs e)
        {
            CropImage();
        }

        protected void btnCrop2_Click(object sender, ImageClickEventArgs e)
        {
            CropImage();
        }

        #endregion

        #region Delete Image

        protected void btnDeleteImg_Click(object sender, EventArgs e)
        {
            DeleteImage();
        }

        protected void DeleteImage()
        {
            _isInError = false;
            txtHaveImageLoad.Text = "NO";
            try
            {
                Photo _photoToDelete = new Photo(IDMedia);
                _photoToDelete.DeletePhoto();
                imgUploadedImg.ImageUrl = "";
                //btnDeleteImg.Visible = false;
                //btnCrop.Visible = true;
                afuUploadMediaFile.Visible = true;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                        " document.getElementById('imgUploadedImg').src=''; " +
                        " $('#txtCropComplete').val('NO'); " +
                        " $('#txtImageSaved').val(''); " +
                        " top.document.getElementById('DeleteBtn').style.visibility = 'hidden'; " +
                        " top.document.getElementById('btnCropContainer').style.visibility = 'hidden'; " +
                        " document.getElementById('uploadControl').style.visibility = 'visible'; " +
                        " document.getElementById('ImageToCrop').style.visibility = 'visible'; ", true);
            }
            catch (Exception ex)
            {
                _isInError = true;
                _ReturnMessage = "Error delete Image" + ex.Message;
            }
        }

        protected void btnDeleteImg2_Click(object sender, ImageClickEventArgs e)
        {
            DeleteImage();
        }

        #endregion



        public void SaveImage()
        {
            _isInError = false;
            txtHaveImageLoad.Text = "NO";
            ImageName = txtImgName.Text;

            if (ImageName == "" || IDMediaOwner == null)
            {
                _isInError = true;
                _ReturnMessage = "A Media Owner, an image name, and a media type are required";
                throw new ArgumentException("A Media Owner, an image name, and a media type are required");
            }
            else
            {
                if (ImageName != "" && txtCropComplete.Text != "YES" && txtImageSaved.Text == "")
                {
                    Photo _Image;
                    _Image = new Photo(new Guid());
                    MediaUploadConfig _mediaConfig = new MediaUploadConfig(ImageMediaType);
                    _Image.Checked = false;
                    _Image.isEsternalSource = false;
                    _Image.isImage = true;
                    _Image.isLink = false;
                    _Image.isVideo = false;
                    _Image.MediaDisabled = false;
                    _Image.mediaType = ImageMediaType;
                    _Image.MediaServer = "";
                    _Image.MediaBakcupServer = "";

                    if (txtX1.Text != "")
                    {
                        Photo.Crop(Server.MapPath(_mediaConfig.UploadPath + ImageName),
                                            MyConvert.ToInt32(txtX1.Text, 0), MyConvert.ToInt32(txtY1.Text, 0),
                                            MyConvert.ToInt32(txtW.Text, 0), MyConvert.ToInt32(txtH.Text, 0));

                        //int imageQuality = MyConvert.ToInt32(AppConfig.GetValue("DefaultImageQuality", AppDomain.CurrentDomain), 50);
                        string outputImage = "";

                        outputImage = Photo.ResizeCompress(Server.MapPath(_mediaConfig.UploadPath + ImageName),
                                        _mediaConfig.MediaFinalWidth, _mediaConfig.MediaFinalHeight, 100,true);

                        if (!String.IsNullOrEmpty(outputImage))
                        {
                            ImageName = ImageName.Replace(ImageName.Substring(ImageName.LastIndexOf(".")), outputImage.Substring(outputImage.LastIndexOf(".")));
                        }
                    }
                    if (_mediaConfig.ComputeMD5Hash)
                    {
                        MD5Hash = MySecurity.GenerateMD5FileHash(Server.MapPath(_mediaConfig.UploadPath) + ImageName);
                    }
                    else
                    {
                        MD5Hash = "";
                    }

                    _Image.MediaPath = _mediaConfig.UploadPath + ImageName;
                    _Image.MediaMD5Hash = MD5Hash;
                    _Image.MediaUpdatedOn = DateTime.UtcNow;
                    _Image.MediaOwner = IDMediaOwner;
                    try
                    {
                        _Image.SavePhotoDbInfo();
                        IDMedia = _Image.IDMedia;
                        txtIDMedia.Text = _Image.IDMedia.ToString();
                        try
                        {
                            _Image.AddAlternativePhotoSize(MediaSizeTypes.OriginalResized, _mediaConfig.MediaType);
                            _Image.AddAlternativePhotoSize(MediaSizeTypes.Small, _mediaConfig.MediaType);
                        }
                        catch
                        {
                        }
                    }
                    catch (Exception ex)
                    {
                        _isInError = true;
                        _ReturnMessage = "Error save Image" + ex.Message;
                    }


                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                       "document.getElementById('uploadControl').style.visibility = 'hidden'; " +
                       " $('#txtCropComplete').val('YES'); " +
                       " $('#txtImageSaved').val('YES'); " +
                       " top.document.getElementById('btnCropContainer').style.visibility = 'hidden'; " +
                       " top.document.getElementById('DeleteBtn').style.visibility = 'visible'; ", true);

                    imgUploadedImg.ImageUrl = _mediaConfig.UploadPath + ImageName + "?" + Guid.NewGuid();

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                      "document.getElementById('uploadControl').style.visibility = 'hidden'; " +
                      " $('#txtCropComplete').val('YES'); " +
                      " $('#txtImageSaved').val('YES'); " +
                      " top.document.getElementById('btnCropContainer').style.visibility = 'hidden'; " +
                      " top.document.getElementById('DeleteBtn').style.visibility = 'visible'; ", true);
                }
            }
        }     

        protected void afuUploadMediaFile_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {
            string _message = "";
            _isInError = false;
            if (afuUploadMediaFile.HasFile)
            {
                MediaUploadConfig _mediaConfig = new MediaUploadConfig(ImageMediaType);

                int _UploadFileCheck = Media.CheckIfUploadFile(afuUploadMediaFile.PostedFile, _mediaConfig, IDMediaOwner.ToString());

                if (_UploadFileCheck == 0)
                {
                    lblImgInfo.Text = "";

                    if (BaseFileName != "" || BaseFileName != null)
                    {
                        ImageName = BaseFileName + Path.GetExtension(afuUploadMediaFile.FileName);
                    }

                    ImageName = ImageName.Replace(" ", "_").Replace("'","").Replace("\"","");

                    if (MyConvert.ToInt32(AppConfig.GetValue("AddDateToFileName", AppDomain.CurrentDomain), 0) == 1)
                    {
                        ImageName = DateTime.UtcNow.ToString(AppConfig.GetValue("DateFormatString", AppDomain.CurrentDomain)) + "_" + ImageName;
                    }

                    try
                    {
                        afuUploadMediaFile.SaveAs(Server.MapPath(_mediaConfig.UploadOriginalFilePath) + ImageName);

                        Bitmap tmpImage = new Bitmap(Server.MapPath(_mediaConfig.UploadOriginalFilePath) + ImageName);

                        int imageQuality = MyConvert.ToInt32(AppConfig.GetValue("DefaultImageQuality", AppDomain.CurrentDomain), 50);
                        string outputImage = "";
                        if (tmpImage.Width > _mediaConfig.MediaFinalWidth || tmpImage.Height > _mediaConfig.MediaFinalHeight)
                        {


                            CalculatePhotoSize photoSize = new CalculatePhotoSize(tmpImage.Width, tmpImage.Height, _mediaConfig.MediaFinalWidth,
                                                                                    _mediaConfig.MediaFinalHeight, _mediaConfig.MediaPercPlusSizeForCrop);

                           outputImage = Photo.ResizeAndSave(Server.MapPath(_mediaConfig.UploadOriginalFilePath) + ImageName,
                                        Server.MapPath(_mediaConfig.UploadPath) + ImageName, photoSize.Width, photoSize.Height, imageQuality);
                        }
                        else
                        {
                           outputImage = Photo.ResizeAndSave(Server.MapPath(_mediaConfig.UploadOriginalFilePath) + ImageName,
                                         Server.MapPath(_mediaConfig.UploadPath) + ImageName, tmpImage.Width, tmpImage.Height, imageQuality);
                        }
                        
                        if(!String.IsNullOrEmpty(outputImage))
                        {
                            ImageName = ImageName.Replace(ImageName.Substring(ImageName.LastIndexOf(".")), outputImage.Substring(outputImage.LastIndexOf(".")));

                            txtImgName.Text = ImageName;
                            imgUploadedImgPath = Server.MapPath(_mediaConfig.UploadPath) + ImageName;
                        }
                    }
                    catch (Exception ex)
                    {
                        _isInError = true;
                        _ReturnMessage = "Error save Image to FileSystem" + ex.Message;
                    }

                    string MinCropHeight = _mediaConfig.MediaFinalHeight.ToString();
                    string MinCropWidth = _mediaConfig.MediaFinalWidth.ToString();

                    imgUploadedImg.ImageUrl = _mediaConfig.UploadPath + ImageName;
                    //btnCrop.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                        "top.document.getElementById('imgUploadedImg').src='" + _mediaConfig.UploadPath + ImageName +"';"+
                                        " top.document.getElementById('uploadControl').style.visibility = 'hidden'; " +
                                        " top.document.getElementById('btnCropContainer').style.visibility = 'visible'; " +
                                        " top.document.getElementById('txtMinCropWidth').value = '" + MinCropWidth + "'; " +
                                        " top.document.getElementById('txtMinCropHeight').value = '" + MinCropHeight + "'; " +
                                        " top.document.getElementById('" + txtImgName.ClientID + "').value = '" + ImageName + "'; " +
                                        "top.document.getElementById('ImageToCrop').style.visibility = 'visible';", true);

                }
                else
                {
                    _isInError = true;
                    switch (_UploadFileCheck)
                    {
                        case 1:
                            _message = RetrieveMessage.RetrieveDBMessage(2, "MD-ER-0001");
                            break;
                        case 2:
                            _message = RetrieveMessage.RetrieveDBMessage(2, "MD-ER-0002");
                            break;
                        case 3:
                            _message = RetrieveMessage.RetrieveDBMessage(2, "MD-ER-0003");
                            break;
                        case 4:
                            _message = RetrieveMessage.RetrieveDBMessage(2, "MD-ER-0004");
                            break;
                        case 6:
                            _message = RetrieveMessage.RetrieveDBMessage(2, "MD-ER-0006");
                            break;
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                         "top.document.getElementById('lblImgInfo').innerHTML='" + _message + "';" +
                                         " top.document.getElementById('btnCropContainer').style.visibility = 'hidden'; " +
                                         " top.document.getElementById('DeleteBtn').style.visibility = 'hidden'; " +
                                        "top.document.getElementById('ImageToCrop').style.visibility = 'hidden';", true);
                }


            }

        }
    }
}