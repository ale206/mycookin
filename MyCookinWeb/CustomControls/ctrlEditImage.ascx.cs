using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using MyCookin.Common;
using MyCookin.ObjectManager.MediaManager;

namespace MyCookinWeb.CustomControls
{
    public partial class ctrlEditImage : System.Web.UI.UserControl
    {
        #region Public Fields

        public string IDMedia
        {
            get { return hfIDMedia.Value; }
            set { hfIDMedia.Value = value; }
        }

        public MediaSizeTypes MediaSizeType
        {
            get { return (MediaSizeTypes)Enum.Parse(typeof(MediaSizeTypes),hfMediaSizeType.Value); }
            set { hfMediaSizeType.Value = value.ToString(); }
        }

        public bool MediaChanged
        {
            get { return MyConvert.ToBoolean(hfMediaChanged.Value, false); }
            set { hfMediaChanged.Value = value.ToString(); }
        }

        public MediaType MediaType
        {
            get { return (MediaType)Enum.Parse(typeof(MediaType), hfMediaType.Value); }
            set { hfMediaType.Value = value.ToString(); }
        }

        public bool EnableEditing
        {
            get { return MyConvert.ToBoolean(hfEnableEditing.Value, false); }
            set { hfEnableEditing.Value = value.ToString(); }
        }

        public bool EnableUpload
        {
            get { return MyConvert.ToBoolean(hfEnableUpload.Value, false); }
            set { hfEnableUpload.Value = value.ToString(); }
        }

        public string EditButtonText
        {
            get { return btnEditImage.Text; }
            set { btnEditImage.Text = value; }
        }

        public string EditButtonCssClass
        {
            get { return btnEditImage.CssClass; }
            set { btnEditImage.CssClass = value; }
        }

        public string PopUpTitle
        {
            get { return hfPopUpTitle.Value; }
            set { hfPopUpTitle.Value = value; }
        }

        private string CropButtonText
        {
            get { return btnCrop.Text; }
            set { btnCrop.Text = value; }
        }

        private string CropButtonCssClass
        {
            get { return btnCrop.CssClass; }
            set { btnCrop.CssClass = value; }
        }

        public string ImageCssClass
        {
            get { return imgShowedImage.CssClass; }
            set { imgShowedImage.CssClass = value; }
        }

        public Unit ImageWidth
        {
            get { return imgShowedImage.Width; }
            set { imgShowedImage.Width = value; }
        }

        public Unit ImageHeight
        {
            get { return imgShowedImage.Height; }
            set { imgShowedImage.Height = value; }
        }

        //public int CropX1
        //{
        //    get { return MyConvert.ToInt32(hfX1.Value,0); }
        //    set { hfX1.Value = value.ToString(); }
        //}

        //public int CropY1
        //{
        //    get { return MyConvert.ToInt32(hfY1.Value, 0); }
        //    set { hfY1.Value = value.ToString(); }
        //}

        //public int CropX2
        //{
        //    get { return MyConvert.ToInt32(hfX2.Value, 0); }
        //    set { hfX2.Value = value.ToString(); }
        //}

        //public int CropY2
        //{
        //    get { return MyConvert.ToInt32(hfY2.Value, 0); }
        //    set { hfY2.Value = value.ToString(); }
        //}

        //public int CropWidth
        //{
        //    get { return MyConvert.ToInt32(hfWidth.Value, 0); }
        //    set { hfWidth.Value = value.ToString(); }
        //}

        //public int CropHeight
        //{
        //    get { return MyConvert.ToInt32(hfY2.Value, 0); }
        //    set { hfY2.Value = value.ToString(); }
        //}

        #endregion

        protected override void Render(HtmlTextWriter writer)
        {
            Page.ClientScript.RegisterForEventValidation(btnCrop.UniqueID);
            base.Render(writer);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (EnableEditing)
            {
                pnlEditButton.Visible = true;
            }
            else
            {
                pnlEditButton.Visible = false;
            }
            pnlMainCtrlEditImage.Width = imgShowedImage.Width;
            pnlMainCtrlEditImage.Height = imgShowedImage.Height;

            if (String.IsNullOrEmpty(hfIsFirstLoad.Value))
            {
                if (hfPopUpTitle.Value == "")
                {
                    hfPopUpTitle.Value = "Edit photo";
                }

                imgImageToCrop.Attributes.Add("style", "display:none");
                
                pnlEditImage.Attributes.Add("style", "display:none");
                hfMinCropHeight.Value = "150";
                hfMinCropWidth.Value = "150";
                hfCropAspectRatio.Value = "1";
                try
                {
                    Photo _photo = new Photo(new Guid(hfIDMedia.Value));
                    string _photoPath = "";

                    if (String.IsNullOrEmpty(hfMediaSizeType.Value))
                    {
                        _photo.QueryMediaInfo();
                        _photoPath = _photo.GetCompletePath(false, false,false);
                    }
                    else
                    {
                        _photoPath = _photo.GetAlternativeSizePath(MediaSizeType, false, false,false);
                    }
                    imgShowedImage.ImageUrl = _photoPath;
                    imgImageToCrop.ImageUrl = _photo.GetAlternativeSizePath(MediaSizeTypes.OriginalResized, false, false,false);
                    if (!String.IsNullOrEmpty(imgImageToCrop.ImageUrl))
                    {
                        btnEditImage.OnClientClick = "EditImageToCrop('#" + imgImageToCrop.ClientID +
                                                               "', '" + IDMedia +
                                                               "', 'OriginalResized', 'false', 'false','#" +
                                                               hfMinCropWidth.ClientID +
                                                               "','#" + hfMinCropHeight.ClientID +
                                                               "','#" + hfCropAspectRatio.ClientID +
                                                               "','#" + hfX1.ClientID +
                                                               "','#" + hfY1.ClientID +
                                                               "','#" + hfX2.ClientID +
                                                               "','#" + hfY2.ClientID +
                                                               "','#" + hfWidth.ClientID +
                                                               "','#" + hfHeight.ClientID + "','#" + pnlEditImage.ClientID +
                                                               "', '" + hfPopUpTitle.Value + "', '#" + imgImageToCrop.ClientID +
                                                               "', '" + btnCrop.UniqueID + "');";
                        btnEditImage.Visible = true;
                    }
                    else
                    {
                        btnEditImage.Visible = false;
                    }
                }
                catch
                {
                }
                finally
                {
                    hfIsFirstLoad.Value = "FirstLoadPerformed";
                }
            }
        }

        protected void btnCrop_Click(object sender, EventArgs e)
        {
            try
            {
                if (MyConvert.ToInt32(hfWidth.Value, 0) != 0)
                {
                    Photo _photo = new Photo(new Guid(hfIDMedia.Value));
                    _photo.QueryMediaInfo();

                    pnlEditImage.Attributes.Add("style", "display:none");
                    imgShowedImage.Visible = false;

                    //string _fileName = imgImageToCrop.ImageUrl.Substring(imgImageToCrop.ImageUrl.LastIndexOf('/') + 1);

                    File.Copy(Server.MapPath(imgImageToCrop.ImageUrl), Server.MapPath(_photo.MediaPath), true);
                    Photo.Crop(Server.MapPath(_photo.MediaPath), MyConvert.ToInt32(hfX1.Value, 0),
                                        MyConvert.ToInt32(hfY1.Value, 0), MyConvert.ToInt32(hfWidth.Value, 0),
                                        MyConvert.ToInt32(hfHeight.Value, 0));
                    _photo.MediaOnCDN = false;
                    _photo.MediaServer = "";
                    _photo.MediaBakcupServer = "";
                    _photo.SavePhotoDbInfo(false);
                    _photo.AddAlternativePhotoSize(MediaSizeTypes.Small, MediaType);
                    imgShowedImage.ImageUrl += "?" + Guid.NewGuid().ToString();
                    imgShowedImage.Visible = true;
                }
            }
            catch
            {
                //Manage ERROR here...
            }
        }
    }
}