using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyCookin.ObjectManager.MediaManager;
using MyCookin.Common;

namespace PlUploadTest
{
    public partial class ctrlMultiUpload : System.Web.UI.UserControl
    {
        public event EventHandler FilesUploaded;
        private bool _loadControl;
        private string _clientClick;

        #region PublicProperties
        /// <summary>
        /// Text for the browse file link button
        /// </summary>
        public string SelectFilesText
        {
            get { return lnkSelectFiles.Text; }
            set { lnkSelectFiles.Text = value; }
        }
        public string OnSelectFileClientClick
        {
            get { return _clientClick; }
            set { _clientClick = value; }
        }
        /// <summary>
        /// CssClass for the browse file link button
        /// </summary>
        public string SelectFilesCssClass
        {
            get { return lnkSelectFiles.CssClass; }
            set { lnkSelectFiles.CssClass = value; }
        }
        /// <summary>
        /// Text for the upload file link button
        /// </summary>
        public string UploadFilesText
        {
            get { return lnkUploadFiles.Text; }
            set { lnkUploadFiles.Text = value; }
        }
        /// <summary>
        /// CssClass for the upload file link button
        /// </summary>
        public string UploadFilesCssClass
        {
            get { return lnkUploadFiles.CssClass; }
            set { lnkUploadFiles.CssClass = value; }
        }
        public int MaxFileNumber
        {
            get { return MyConvert.ToInt32(hfMaxFileNumber.Value,0); }
            set { hfMaxFileNumber.Value = value.ToString(); }
        }
        public int MaxFileSizeInMB
        {
            get { return MyConvert.ToInt32(hfMaxFileSizeInMB.Value,0); }
            set { hfMaxFileSizeInMB.Value = value.ToString(); }
        }
        /// <summary>
        /// List of file extention allowed (es: jpg,jpeg,png)
        /// </summary>
        public string AllowedFileTypes
        {
            get { return hfAllowedFileTypes.Value; }
            set { hfAllowedFileTypes.Value = value; }
        }
        public MediaType UploadConfig
        {
            get { return (MediaType)Enum.Parse(typeof(MediaType), hfUploadConfig.Value); }
            set { hfUploadConfig.Value = value.ToString(); }
        }
        public string BaseFileName
        {
            get { return hfBaseFileName.Value; }
            set { hfBaseFileName.Value = value.Replace("'",""); }
        }
        public ClientIDMode BaseFileNameClientIDMode
        {
            get { return hfBaseFileName.ClientIDMode; }
            set { hfBaseFileName.ClientIDMode = value; }
        }
        /// <summary>
        /// Error message showed when the file selected are upper of MaxFileNumber parameter
        /// </summary>
        public string MaxFileNumErrorMessage
        {
            get { return hfMaxFileNumErrorMessage.Value; }
            set { hfMaxFileNumErrorMessage.Value = value; }
        }
        /// <summary>
        /// Upload url (/folder/handler.ashx)
        /// </summary>
        public string UploadHandlerURL
        {
            get { return hfUploadHandlerURL.Value; }
            set { hfUploadHandlerURL.Value = value; }
        }
        /// <summary>
        /// ID of the media owner for the file
        /// </summary>
        public string IDMediaOwner
        {
            get { return hfMediaOwner.Value; }
            set { hfMediaOwner.Value = value; }
        }
        /// <summary>
        /// Get the IDs list of all media created
        /// </summary>
        public string MediaCreatedIDs
        {
            get { return hfFileCreatedIDsList.Value; }
        }
        /// <summary>
        /// Show specific text in the upload Control
        /// </summary>
        public string TextToDisplay
        {
            get { return lblTextToDisplay.Text; }
            set { lblTextToDisplay.Text = value; }
        }
        /// <summary>
        /// Display the text specified in property TextToDisplay 
        /// </summary>
        public bool TextToDisplayVisible
        {
            get { return lblTextToDisplay.Visible; }
            set { lblTextToDisplay.Visible = value; }
        }
        public string ErrorReportFromServer
        {
            get { return HttpUtility.HtmlDecode(hfErrorReportFromServer.Value); }
        }
        public bool LoadControl
        {
            get { return _loadControl; }
            set { _loadControl = value; }
        }
        #endregion

        

        protected override void Render(HtmlTextWriter writer)
        {
            Page.ClientScript.RegisterForEventValidation(btnPostBackEvent.UniqueID);
            Page.ClientScript.RegisterForEventValidation(btnReset.UniqueID);
            base.Render(writer);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(_clientClick))
            {
                lnkSelectFiles.Attributes.Add("onclick", _clientClick);
            }

            if (_loadControl)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                            " InizializeMultiUpload('" + hfMaxFileNumber.Value + "', '" + hfMaxFileSizeInMB.Value + "', '" + lnkSelectFiles.ClientID +
                            "','#"+pnlUploadButton.ClientID+"', '#"+pnlUploading.ClientID+"', '"+pnlUploadContainer.ClientID+
                            "','"+hfUploadHandlerURL.Value+"', '"+hfUploadConfig.Value+"', '"+hfBaseFileName.Value+
                            "','"+hfMediaOwner.Value+"', '"+hfAllowedFileTypes.Value+"', '"+pnlFileList.ClientID+
                            "','"+pnlErrorAndWarning.ClientID+"','"+hfMaxFileNumErrorMessage.Value+" (','#"+hfErrorReportFromServer.ClientID+
                            "','"+btnReset.UniqueID+"', '#"+hfFileCreatedIDsList.ClientID+"', '"+btnPostBackEvent.UniqueID+
                            "','"+lnkUploadFiles.ClientID+"');", true);
            }
        }

        internal void ResetMultiUpload()
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                           " InizializeMultiUpload('" + hfMaxFileNumber.Value + "', '" + hfMaxFileSizeInMB.Value + "', '" + lnkSelectFiles.ClientID +
                            "','#" + pnlUploadButton.ClientID + "', '#" + pnlUploading.ClientID + "', '" + pnlUploadContainer.ClientID +
                            "','" + hfUploadHandlerURL.Value + "', '" + hfUploadConfig.Value + "', '" + hfBaseFileName.Value +
                            "','" + hfMediaOwner.Value + "', '" + hfAllowedFileTypes.Value + "', '" + pnlFileList.ClientID +
                            "','" + pnlErrorAndWarning.ClientID + "','" + hfMaxFileNumErrorMessage.Value + " (','#" + hfErrorReportFromServer.ClientID +
                            "','" + btnReset.UniqueID + "', '#" + hfFileCreatedIDsList.ClientID + "', '" + btnPostBackEvent.UniqueID +
                            "','" + lnkUploadFiles.ClientID + "');", true);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ResetMultiUpload();
        }

        protected void btnPostBackEvent_Click(object sender, EventArgs e)
        {
            //Label1.Text = "ECCELLENTE!!!!";
            //qui si dovrà scatenare un evento da gestire poi sulla pagina su cui risiede il controllo
            FilesUploaded(this, EventArgs.Empty);
        }
        /// <summary>
        /// Clear error message form handler and all IDs created
        /// </summary>
        public void Clear()
        {
            hfErrorReportFromServer.Value = "";
            hfFileCreatedIDsList.Value = "";
        }

        protected void btnPostBackEvent_Load(object sender, EventArgs e)
        {
            //Page.ClientScript.GetPostBackEventReference(this, "");
        }
    }
}