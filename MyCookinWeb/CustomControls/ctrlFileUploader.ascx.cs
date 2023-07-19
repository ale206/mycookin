using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MyCookin.ObjectManager.MediaManager;
using MyCookin.ErrorAndMessage;
using MyCookin.Common;

namespace MyCookinWeb.CustomControls
{
    public partial class ctrlFileUploader : System.Web.UI.UserControl
    {

        #region PublicFields

        public string ImageName
        {
            get { return hfImageName.Value; }
            set { hfImageName.Value = value; }
        }
        public string AllowMulti
        {
            get { return hfAllowMulti.Value; }
            set { hfAllowMulti.Value = value; }
        }
        public string MaxNumItem
        {
            get { return hfMaxNumItem.Value; }
            set { hfMaxNumItem.Value = value; }
        }
        public string EndPointPath
        {
            get { return hfEndPointPath.Value; }
            set { hfEndPointPath.Value = value; }
        }
        public string UploadButtonText
        {
            get { return hfUploadButtonText.Value; }
            set { hfUploadButtonText.Value = value; }
        }
        public string IDMedia
        {
            get { return hfIDMedia.Value; }
            set { hfIDMedia.Value = value; }
        }
        public string IDMediaOwner
        {
            get { return hfIDMediaOwner.Value; }
            set { hfIDMediaOwner.Value = value; }
        }
        public string MD5Hash
        {
            get { return hfMD5Hash.Value; }
            set { hfMD5Hash.Value = value; }
        }
        public string ImageMediaType
        {
            get { return hfImageMediaType.Value; }
            set { hfImageMediaType.Value = value; }
        }
        public string BaseFileName
        {
            get { return hfBaseFileName.Value; }
            set { hfBaseFileName.Value = value; }
        }
        public string UploadFailText
        {
            get { return hfUploadFailText.Value; }
            set { hfUploadFailText.Value = value; }
        }
        public string DragAndDropZoneText
        {
            get { return hfDragAndDropZoneText.Value; }
            set { hfDragAndDropZoneText.Value = value; }
        }
        public string IDLanguage
        {
            get { return hfIDLanguage.Value; }
            set { hfIDLanguage.Value = value; }
        }
        public string ButtonBrowseImageUrl
        {
            get { return btnSelectFile.ImageUrl; }
            set { btnSelectFile.ImageUrl = value; }
        }
        public string ButtonBrowseToolTip
        {
            get { return btnSelectFile.ToolTip; }
            set { btnSelectFile.ToolTip = value; }
        }
        public string ButtonUploadImageUrl
        {
            get { return btnUpload.ImageUrl; }
            set { btnUpload.ImageUrl = value; }
        }
        public string ButtonUploadToolTip
        {
            get { return btnUpload.ToolTip; }
            set { btnUpload.ToolTip = value; }
        }
        public string MaxNumFileError
        {
            get { return hfMaxFileNumError.Value; }
            set { hfMaxFileNumError.Value = value; }
        }
        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {

            #region LoadMessageValues
            try
            {
                hfTypeError.Value = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(IDLanguage, 1), "MD-ER-0001") + " \\'{file}\\'";
                hfSizeError.Value = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(IDLanguage, 1), "MD-ER-0002") + " \\'{sizeLimit}\\'";
                hfMinSizeError.Value = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(IDLanguage, 1), "MD-ER-0006");
                hfEmptyError.Value = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(IDLanguage, 1), "MD-ER-0004");
                hfNoFilesError.Value = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(IDLanguage, 1), "MD-ER-0004");
                hfTooManyItemsError.Value = RetrieveMessage.RetrieveDBMessage(MyConvert.ToInt32(IDLanguage, 1), "MD-ER-0007") + "; MAX:{itemLimit}";
            }
            catch
            {
                hfTypeError.Value = "{file} has an invalid extension. Valid extension(s): {extensions}";
                hfSizeError.Value = "{file} is too large, maximum file size is {sizeLimit}";
                hfMinSizeError.Value = "{file} is too small, minimum file size is {minSizeLimit}";
                hfEmptyError.Value = "{file} is empty, please select files again without it";
                hfNoFilesError.Value = "No files to upload";
                hfTooManyItemsError.Value = "Too many items ({netItems}) would be uploaded.  Item limit is {itemLimit}";
            }
            #endregion

            try
            {
                MediaUploadConfig _uploadConfig = new MediaUploadConfig((MediaType)Convert.ToInt32(hfImageMediaType.Value));
                hfUploadImgPath.Value = _uploadConfig.UploadOriginalFilePath;
                hfUploadAllowedFileType.Value = _uploadConfig.AcceptedFileExtension.Replace("|", "','");
                hfUploadImgMaxSize.Value = (_uploadConfig.MaxSizeByte/1024/1024).ToString();
                hfEndPointPath.Value += "?baseFileName=" + hfBaseFileName.Value + "&MediaOwner=" + hfIDMediaOwner.Value;
            }
            catch
            {
                throw new ArgumentException("Media type incorrect");
            }

        }
    }
}