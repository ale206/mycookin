using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using MyCookin.ObjectManager.UserManager;
using MyCookin.DAL.Media.ds_MediaTableAdapters;
using MyCookin.ObjectManager;
using MyCookin.ObjectManager.AuditManager;
using MyCookin.Common;
using MyCookin.Log;
using MyCookin.ErrorAndMessage;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;

namespace MyCookin.ObjectManager.MediaManager
{
    public class Media
    {
        #region PrivateFields

        protected Guid _IDMedia;
        protected MyUser _MediaOwner;
        protected bool _isImage;
        protected bool _isVideo;
        protected bool _isLink;
        protected bool _isEsternalSource;
        protected string _MediaServer;
        protected string _MediaBakcupServer;
        protected string _MediaPath;
        protected string _MediaMD5Hash;
        protected bool _Checked;
        protected MyUser _CheckedByUser;
        protected bool _MediaDisabled;
        protected DateTime _MediaUpdatedOn;
        protected DateTime? _MediaDeletedOn;
        protected bool _UserIsMediaOwner;
        protected bool _MediaOnCDN;
        protected MediaType _MediaType;

        #endregion

        #region PublicProperties

        public Guid IDMedia
        {
            get { return _IDMedia; }
        }
        public MyUser MediaOwner
        {
            get { return _MediaOwner; }
            set { _MediaOwner = value; }
        }
        public bool isImage
        {
            get { return _isImage; }
            set { _isImage = value; }
        }
        public bool isVideo
        {
            get { return _isVideo; }
            set { _isVideo = value; }
        }
        public bool isLink
        {
            get { return _isLink; }
            set { _isLink = value; }
        }
        public bool isEsternalSource
        {
            get { return _isEsternalSource; }
            set { _isEsternalSource = value; }
        }
        public string MediaServer
        {
            get { return _MediaServer; }
            set { _MediaServer = value; }
        }
        public string MediaBakcupServer
        {
            get { return _MediaBakcupServer; }
            set { _MediaBakcupServer = value; }
        }
        public string MediaMD5Hash
        {
            get { return _MediaMD5Hash; }
            set { _MediaMD5Hash = value; }
        }
        public string MediaPath
        {
            get { return _MediaPath; }
            set { _MediaPath = value; }
        }
        public bool Checked
        {
            get { return _Checked; }
            set { _Checked = value; }
        }
        public MyUser CheckedByUser
        {
            get { return _CheckedByUser; }
            set { _CheckedByUser = value; }
        }
        public bool MediaDisabled
        {
            get { return _MediaDisabled; }
            set { _MediaDisabled = value; }
        }
        public DateTime MediaUpdatedOn
        {
            get { return _MediaUpdatedOn; }
            set { _MediaUpdatedOn = value; }
        }
        public DateTime? MediaDeletedOn
        {
            get { return _MediaDeletedOn; }
            set { _MediaDeletedOn = value; }
        }
        public bool UserIsMediaOwner
        {
            get { return _UserIsMediaOwner; }
            set { _UserIsMediaOwner = value; }
        }
        public bool MediaOnCDN
        {
            get { return _MediaOnCDN; }
            set { _MediaOnCDN = value; }
        }
        public MediaType mediaType
        {
            get { return _MediaType; }
            set { _MediaType = value; }
        }
        #endregion

        #region Constructors

        protected Media()
        {
        }

        public Media(Guid IDMedia)
        {
            _IDMedia = IDMedia;

            QueryMediaInfo();

        }

        #endregion

        #region Methods

        /// <summary>
        /// Get Information about media object form database
        /// </summary>
        public void QueryMediaInfo()
        {

            GetMediaDAL MediaDAL = new GetMediaDAL();
            DataTable dtMedia = new DataTable();

            try
            {
                dtMedia = MediaDAL.GetMediaByID(_IDMedia);


                if (dtMedia.Rows.Count > 0)
                {

                    _isImage = dtMedia.Rows[0].Field<bool>("isImage");
                    _isVideo = dtMedia.Rows[0].Field<bool>("isVideo");
                    _isLink = dtMedia.Rows[0].Field<bool>("isLink");
                    _isEsternalSource = dtMedia.Rows[0].Field<bool>("isEsternalSource");
                    _Checked = dtMedia.Rows[0].Field<bool>("Checked");
                    _CheckedByUser = dtMedia.Rows[0].Field<Guid?>("CheckedByUser");
                    _MediaBakcupServer = dtMedia.Rows[0].Field<string>("MediaBakcupServer");
                    _MediaDeletedOn = dtMedia.Rows[0].Field<DateTime?>("MediaDeletedOn");
                    _MediaDisabled = dtMedia.Rows[0].Field<bool>("MediaDisabled");
                    _MediaOwner = dtMedia.Rows[0].Field<Guid?>("IDMediaOwner");
                    _MediaPath = dtMedia.Rows[0].Field<string>("MediaPath");
                    _MediaMD5Hash = dtMedia.Rows[0].Field<string>("MediaMD5Hash");
                    _MediaServer = dtMedia.Rows[0].Field<string>("MediaServer");
                    _MediaUpdatedOn = dtMedia.Rows[0].Field<DateTime>("MediaUpdatedOn");
                    _UserIsMediaOwner = dtMedia.Rows[0].Field<bool>("UserIsMediaOwner");
                    _MediaOnCDN = dtMedia.Rows[0].Field<bool>("MediaOnCDN");
                    try
                    {
                        _MediaType = (MediaType)Enum.Parse(typeof(MediaType), dtMedia.Rows[0].Field<string>("MediaType"));
                    }
                    catch
                    {
                        _MediaType = MediaType.NotSpecified;
                    }

                }
                else
                {
                    _IDMedia = new Guid();
                }
            }
            catch (SqlException sqlex)
            {
                ManageUSPReturnValue retValue = new ManageUSPReturnValue("MD-ER-9999", sqlex.Message + "; IDMedia:" + _IDMedia.ToString(), true);
                MediaLogUSP(LogLevel.Errors, LogLevel.Errors, true, retValue);
                _IDMedia = new Guid();
            }
            catch (Exception ex)
            {
                ManageUSPReturnValue retValue = new ManageUSPReturnValue("MD-ER-9999", ex.Message + "; IDMedia:" + _IDMedia.ToString(), true);
                MediaLogUSP(LogLevel.CriticalErrors, LogLevel.CriticalErrors, true, retValue);
                _IDMedia = new Guid();
            }
        }

        /// <summary>
        /// Manage internal log for class Media
        /// </summary>
        /// <param name="LogDbLevel">LogLevel for database</param>
        /// <param name="LogFsLevel">LogLevel for file log</param>
        /// <param name="SendEmail">Send mail when this error occurs</param>
        /// <param name="USPReturn">A StoredProcedure return or a manual generated return</param>
        public void MediaLogUSP(LogLevel LogDbLevel, LogLevel LogFsLevel, bool SendEmail, ManageUSPReturnValue USPReturn)
        {
            int IDLanguageForLog;

            try
            {
                IDLanguageForLog = Convert.ToInt32(AppConfig.GetValue("IDLanguageForLog", AppDomain.CurrentDomain));
            }
            catch
            {
                IDLanguageForLog = 1;
            }
            try
            {
                LogRow NewRow = new LogRow(DateTime.UtcNow, LogDbLevel.ToString(), "", Network.GetCurrentPageName(), USPReturn.ResultExecutionCode, RetrieveMessage.RetrieveDBMessage(IDLanguageForLog, USPReturn.ResultExecutionCode), USPReturn.USPReturnValue, false, true);
                LogManager.WriteFileLog(LogFsLevel, SendEmail, NewRow);
                LogManager.WriteDBLog(LogDbLevel, NewRow);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Check if a file is uplodable
        /// </summary>
        /// <param name="FileUploaded">The file that you want upload</param>
        /// <param name="FileUploadConfig">An object with che upload configuration info</param>
        /// <param name="IDUser">User that try upload</param>
        /// <returns>Return a int value that specify if upload the file or, if not, the kind of error that occurs</returns>
        public static int CheckIfUploadFile(HttpPostedFile FileUploaded, MediaUploadConfig FileUploadConfig,string IDUser)
        {
            try
            {
                string _fileExt = FileUploaded.FileName.Substring(FileUploaded.FileName.LastIndexOf(".") + 1);
                int imgSmallerDimension = 0;
                Bitmap bmp;
                if (FileUploadConfig.EnableUploadForMediaType)
                {
                    if (FileUploadConfig.AcceptedFileExtension.ToLower().IndexOf(_fileExt.ToLower()) > -1)
                    {
                        if (FileUploaded.ContentLength <= FileUploadConfig.MaxSizeByte)
                        {
                            if (FileUploadConfig.AcceptedContentTypes.IndexOf(FileUploaded.ContentType) > -1)
                            {
                                try
                                {
                                    bmp = new Bitmap(FileUploaded.InputStream);

                                    if (bmp.Width < bmp.Height)
                                    {
                                        imgSmallerDimension = bmp.Width;
                                    }
                                    else
                                    {
                                        imgSmallerDimension = bmp.Height;
                                    }
                                    bmp.Dispose();
                                }
                                catch
                                {
                                    try
                                    {
                                        LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.InfoMessages.ToString(), "", Network.GetCurrentPageName(), "MD-ER-0003", "Mime Type non Accepted | " + FileUploaded.FileName, IDUser, false, true);
                                        LogManager.WriteFileLog(LogLevel.InfoMessages, false, NewRow);
                                        LogManager.WriteDBLog(LogLevel.Warnings, NewRow);
                                    }
                                    catch
                                    {
                                    }
                                    //Mime Type non Accepted
                                    return 3;
                                }
                                if (imgSmallerDimension >= FileUploadConfig.MediaSmallerSideMinSize)
                                {
                                    //Upload Allowed
                                    return 0;
                                }
                                else
                                {
                                    try
                                    {
                                        LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.InfoMessages.ToString(), "", Network.GetCurrentPageName(), "MD-ER-0006", "Image too small | " + FileUploaded.FileName + " | " + imgSmallerDimension.ToString(), IDUser, false, true);
                                        LogManager.WriteFileLog(LogLevel.InfoMessages, false, NewRow);
                                        LogManager.WriteDBLog(LogLevel.Warnings, NewRow);
                                    }
                                    catch
                                    {
                                    }
                                    //Image too small
                                    return 6;
                                }
                            }
                            else
                            {
                                try
                                {
                                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.InfoMessages.ToString(), "", Network.GetCurrentPageName(), "MD-ER-0003", "Mime Type non Accepted | " + FileUploaded.FileName + " | " + FileUploaded.ContentType, IDUser, false, true);
                                    LogManager.WriteFileLog(LogLevel.InfoMessages, false, NewRow);
                                    LogManager.WriteDBLog(LogLevel.Warnings, NewRow);
                                }
                                catch
                                {
                                }
                                //Mime Type non Accepted
                                return 3;
                            }
                        }
                        else
                        {
                            try
                            {
                                LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.InfoMessages.ToString(), "", Network.GetCurrentPageName(), "MD-ER-0002", "File too big | " + FileUploaded.FileName + " | " + FileUploaded.ContentLength.ToString(), IDUser, false, true);
                                LogManager.WriteFileLog(LogLevel.InfoMessages, false, NewRow);
                                LogManager.WriteDBLog(LogLevel.Warnings, NewRow);
                            }
                            catch
                            {
                            }
                            //File too big
                            return 2;
                        }
                    }
                    else
                    {
                        try
                        {
                            LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.InfoMessages.ToString(), "", Network.GetCurrentPageName(), "MD-ER-0001", "Not Valid Extension | " + FileUploaded.FileName, IDUser, false, true);
                            LogManager.WriteFileLog(LogLevel.InfoMessages, false, NewRow);
                            LogManager.WriteDBLog(LogLevel.Warnings, NewRow);
                        }
                        catch
                        {
                        }
                        //Not Valid Extension
                        return 1;
                    }
                }
                else
                {
                    try
                    {
                        LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.InfoMessages.ToString(), "", Network.GetCurrentPageName(), "MD-ER-0004", "Upload not Allowed | " + FileUploaded.FileName, IDUser, false, true);
                        LogManager.WriteFileLog(LogLevel.InfoMessages, false, NewRow);
                        LogManager.WriteDBLog(LogLevel.Warnings, NewRow);
                    }
                    catch
                    {
                    }
                    //Upload not Allowed
                    return 4;
                }
            }
            catch
            {
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "MD-ER-9999", "Error in check file upload | " + FileUploaded.FileName, IDUser, false, true);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                }
                catch
                {
                }
                //Upload not Allowed
                return 4;
            }
        }

        /// <summary>
        /// Check if a file is uplodable
        /// </summary>
        /// <param name="FileUploaded">The file that you want upload</param>
        /// <param name="FileName">The complete file name with extention</param>
        /// <param name="FileUploadConfig">An object with che upload configuration info</param>
        /// <param name="IDUser">User that try upload</param>
        /// <returns>Return a int value that specify if upload the file or, if not, the kind of error that occurs</returns>
        public static int CheckIfUploadFile(HttpPostedFile FileUploaded, string FileName, MediaUploadConfig FileUploadConfig, string IDUser)
        {
            try
            {
                string _fileExt = FileName.Substring(FileName.LastIndexOf(".") + 1);
                int imgSmallerDimension = 0;
                Bitmap bmp;
                if (FileUploadConfig.EnableUploadForMediaType)
                {
                    if (FileUploadConfig.AcceptedFileExtension.ToLower().IndexOf(_fileExt.ToLower()) > -1)
                    {
                        if (FileUploaded.ContentLength <= FileUploadConfig.MaxSizeByte)
                        {
                            try
                            {
                                bmp = new Bitmap(FileUploaded.InputStream);

                                if (bmp.Width < bmp.Height)
                                {
                                    imgSmallerDimension = bmp.Width;
                                }
                                else
                                {
                                    imgSmallerDimension = bmp.Height;
                                }
                                bmp.Dispose();
                            }
                            catch
                            {
                                try
                                {
                                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.InfoMessages.ToString(), "", Network.GetCurrentPageName(), "MD-ER-0003", "Mime Type non Accepted | " + FileUploaded.FileName, IDUser, false, true);
                                    LogManager.WriteFileLog(LogLevel.InfoMessages, false, NewRow);
                                    LogManager.WriteDBLog(LogLevel.Warnings, NewRow);
                                }
                                catch
                                {
                                }
                                //Mime Type non Accepted
                                return 3;
                            }
                            if (imgSmallerDimension >= FileUploadConfig.MediaSmallerSideMinSize)
                            {
                                //Upload Allowed
                                return 0;
                            }
                            else
                            {
                                try
                                {
                                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.InfoMessages.ToString(), "", Network.GetCurrentPageName(), "MD-ER-0006", "Image too small | " + FileUploaded.FileName + " | " + imgSmallerDimension.ToString(), IDUser, false, true);
                                    LogManager.WriteFileLog(LogLevel.InfoMessages, false, NewRow);
                                    LogManager.WriteDBLog(LogLevel.Warnings, NewRow);
                                }
                                catch
                                {
                                }
                                //Image too small
                                return 6;
                            }
                        }
                        else
                        {
                            try
                            {
                                LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.InfoMessages.ToString(), "", Network.GetCurrentPageName(), "MD-ER-0002", "File too big | " + FileUploaded.FileName + " | " + FileUploaded.ContentLength.ToString(), IDUser, false, true);
                                LogManager.WriteFileLog(LogLevel.InfoMessages, false, NewRow);
                                LogManager.WriteDBLog(LogLevel.Warnings, NewRow);
                            }
                            catch
                            {
                            }
                            //File too big
                            return 2;
                        }
                    }
                    else
                    {
                        try
                        {
                            LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.InfoMessages.ToString(), "", Network.GetCurrentPageName(), "MD-ER-0001", "Not Valid Extension | " + FileUploaded.FileName, IDUser, false, true);
                            LogManager.WriteFileLog(LogLevel.InfoMessages, false, NewRow);
                            LogManager.WriteDBLog(LogLevel.Warnings, NewRow);
                        }
                        catch
                        {
                        }
                        //Not Valid Extension
                        return 1;
                    }
                }
                else
                {
                    try
                    {
                        LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.InfoMessages.ToString(), "", Network.GetCurrentPageName(), "MD-ER-0004", "Upload not Allowed | " + FileUploaded.FileName, IDUser, false, true);
                        LogManager.WriteFileLog(LogLevel.InfoMessages, false, NewRow);
                        LogManager.WriteDBLog(LogLevel.Warnings, NewRow);
                    }
                    catch
                    {
                    }
                    //Upload not Allowed
                    return 4;
                }
            }
            catch
            {
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.Errors.ToString(), "", Network.GetCurrentPageName(), "MD-ER-9999", "Error in check file upload | " + FileUploaded.FileName, IDUser, false, true);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                    LogManager.WriteDBLog(LogLevel.Errors, NewRow);
                }
                catch
                {
                }
                //Upload not Allowed
                return 4;
            }
        }

        /// <summary>
        /// Get the media path complete with host server
        /// </summary>
        /// <returns></returns>
        [Obsolete("Use GetCompletePath(getFromBackupServer,appendUniqueGuid,getFromCDN)")]
        public string GetCompletePath()
        {
            string _return = "";
            if (_MediaOnCDN)
            {
                _return = _MediaServer + _MediaPath + "?" + Guid.NewGuid();
            }
            else
            {
                _return = _MediaPath + "?" + Guid.NewGuid();
            }
            return _return;
        }
        /// <summary>
        /// Get the complete path for an image
        /// </summary>
        /// <param name="getFromBackupServer">true for get it from the backup server</param>
        /// <param name="appendUniqueGuid">true to append an unique identifier to the image</param>
        /// <param name="getFromCDN">Get CDN URL if avaible</param>
        /// <returns></returns>
        public string GetCompletePath(bool getFromBackupServer, bool appendUniqueGuid, bool getFromCDN)
        {
            string _return = "";
            string _serverURL = "";
            string _uniqueGuid = "";

            if (appendUniqueGuid)
            {
                _uniqueGuid = "?" + Guid.NewGuid().ToString();
            }

            if (!getFromBackupServer)
            {
                _serverURL = _MediaServer;
            }
            else
            {
                _serverURL = _MediaBakcupServer;
            }

            if (_MediaOnCDN && getFromCDN)
            {
                _return = _serverURL + _MediaPath + _uniqueGuid;
            }
            else
            {
                _return = _MediaPath + _uniqueGuid;
            }
            return _return;
        }

        /// <summary>
        /// Get the media path complete with host backup server
        /// </summary>
        /// <returns></returns>
        [Obsolete("Use GetCompletePath(getFromBackupServer,appendUniqueGuid)")]
        public string GetCompleteBackupPath()
        {
            string _return = "";
            if (_MediaOnCDN)
            {
                _return = _MediaBakcupServer + _MediaPath + "?" + Guid.NewGuid();
            }
            else
            {
                _return = _MediaPath + "?" + Guid.NewGuid();
            }
            return _return;
        }

        /// <summary>
        /// Get a list of media non deleted
        /// </summary>
        /// <param name="MaxRowNumber">The Max number of row returned</param>
        /// <returns></returns>
        public static DataTable GetAllMediaNotDeleted(int MaxRowNumber)
        {
            GetMediaDAL MediaDAL = new GetMediaDAL();
            return MediaDAL.GetAllMediaNotDeleted(MaxRowNumber);
        }

        /// <summary>
        /// Get the image path in a specified size
        /// </summary>
        /// <param name="mediaSizeType">The specific size of the image</param>
        /// <param name="getFromBackupServer">true if you want to get it from backup site</param>
        /// <param name="appendUniqueGuid">true if you want to append an unique guid to the image</param>
        /// <param name="getFromCDN">Get CDN path if avaible</param>
        /// <returns></returns>
        public string GetAlternativeSizePath(MediaSizeTypes mediaSizeType, bool getFromBackupServer, bool appendUniqueGuid, bool getFromCDN)
        {
            string _return = "";
            string _serverURL = "";

            string _uniqueGuid = "";

            if (appendUniqueGuid)
            {
                _uniqueGuid = "?" + Guid.NewGuid().ToString();
            }

            MediaAlternativesSizes altMediaSize = new MediaAlternativesSizes(_IDMedia, mediaSizeType);

            if (!getFromBackupServer)
            {
                _serverURL = altMediaSize.MediaServer;
            }
            else
            {
                _serverURL = altMediaSize.MediaBackupServer;
            }

            if (_MediaOnCDN && getFromCDN)
            {
                _return = _serverURL + altMediaSize.MediaPath + _uniqueGuid;
            }
            else
            {
                _return = altMediaSize.MediaPath + _uniqueGuid;
            }
            return _return;
        }

        public static DataTable GetOneRandomMediaByMediaType(MediaType mediaType)
        {
            GetMediaDAL MediaDAL = new GetMediaDAL();
            return MediaDAL.GetOneRandomMediaByMediaType(mediaType.ToString());
        }

        #endregion

        #region Operators

        public static implicit operator Media(Guid guid)
        {
            Media media = new Media(guid);
            return media;
        }

        public static implicit operator Guid(Media media)
        {
            Guid guid = new Guid();
            if (media == null)
            {
                return guid;
            }
            else
            {
                return media._IDMedia;
            }
        }

        public static bool operator ==(Media media1, Media media2)
        {
            if ((Object)media1 == null)
            {
               media1 = new Media(new Guid());
            }
            if ((Object)media2 == null)
            {
                media2 = new Media(new Guid());
            }
            if ((Object)media1 == null || (Object)media2 == null)
            {
                return (Object)media1 == (Object)media2;
            }
            return media1.IDMedia == media2.IDMedia;
        }

        public static bool operator !=(Media media1, Media media2)
        {
            return !(media1 == media2);
        }

        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Recipe return false.
            Media media = obj as Media;
            if ((System.Object)media == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (IDMedia == media.IDMedia);
        }

        public bool Equals(Media media)
        {
            // If parameter is null return false:
            if ((object)media == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (IDMedia == media.IDMedia);
        }

        public override int GetHashCode()
        {
            return IDMedia.GetHashCode();
        } 

        #endregion

    }

    public class Photo : Media
    {
        #region PrivateFileds

        int _originalImageWidth;
        int _originalImageHeight;
        int _croppedImageWidth;
        int _croppedImageHeight;
        string _originalFilePath;

        #endregion

        #region Costructors

        public Photo()
        { 
        
        }
        
        public Photo(Guid Photo)
        {
            Media media = new Media(Photo);
            base._IDMedia = media.IDMedia;
            base._MediaOwner = media.MediaOwner;
            base._isImage = media.isImage;
            base._isVideo = media.isVideo;
            base._isLink = media.isLink;
            base._isEsternalSource = media.isEsternalSource;
            base._MediaServer = media.MediaServer;
            base._MediaBakcupServer = media.MediaBakcupServer;
            base._MediaPath = media.MediaPath;
            base._MediaMD5Hash = media.MediaMD5Hash;
            base._Checked = media.Checked;
            base._CheckedByUser = media.CheckedByUser;
            base._MediaDisabled = media.MediaDisabled;
            base._MediaUpdatedOn = media.MediaUpdatedOn;
            base._MediaDeletedOn = media.MediaDeletedOn;
            base._UserIsMediaOwner = media.UserIsMediaOwner;
            base.MediaOnCDN = media.MediaOnCDN;
            base.mediaType = media.mediaType;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Save the photo info on media database
        /// </summary>
        /// <param name="GenereteAuditEvent">If true an Audit event can be created if configured on DB</param>
        /// <returns>Result from the save stored procedure</returns>
        public ManageUSPReturnValue SavePhotoDbInfo(bool GenereteAuditEvent)
        {
            ManageMediaDAL saveMediaDAL = new ManageMediaDAL();
            DataTable dtMediaDAL = new DataTable();

            if (mediaType == null)
            {
                mediaType = MediaType.NotSpecified;
            }

            dtMediaDAL = saveMediaDAL.USP_SaveMedia(_IDMedia, _MediaOwner.IDUser, _isImage, _isVideo,
                                        _isLink, _isEsternalSource, _MediaServer,
                                        _MediaBakcupServer, _MediaPath, _MediaMD5Hash, _Checked,
                                        _CheckedByUser, _MediaDisabled, DateTime.UtcNow, _MediaDeletedOn,_UserIsMediaOwner,_MediaOnCDN,_MediaType.ToString());

                ManageUSPReturnValue saveMediaDALResult = new ManageUSPReturnValue(dtMediaDAL);

                if (!saveMediaDALResult.IsError)
                {
                    _IDMedia = new Guid(saveMediaDALResult.USPReturnValue);

                    try
                    {
                        if (GenereteAuditEvent)
                        {
                            //Check if for photo object the Audit is active and, if active, inset a row
                            AutoAuditConfig autoAudit = new AutoAuditConfig(ObjectType.Photo);
                            if (autoAudit.EnableAutoAudit)
                            {
                                Audit photoAudit = new Audit("New photo Inserted", _IDMedia, ObjectType.Photo,
                                                                _MediaPath, autoAudit.AuditEventLevel, DateTime.UtcNow);
                                photoAudit.AddEvent();
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        ManageUSPReturnValue retValue = new ManageUSPReturnValue("MD-ER-0010", "Error creare audit event. " + ex.Message + "; IDMedia:" + _IDMedia.ToString(), true);
                        MediaLogUSP(LogLevel.Warnings, LogLevel.Warnings, true, retValue);
                    }
                }
      
            return saveMediaDALResult;   
        }

         /// <summary>
        /// Save the photo info on media database
        /// </summary>
        /// <returns>Result from the save stored procedure</returns>
        public ManageUSPReturnValue SavePhotoDbInfo()
        {
            return SavePhotoDbInfo(true);
        }

        /// <summary>
        /// Logicaly delete the photo info on media database
        /// </summary>
        /// <returns>Result from the delete stored procedure</returns>
        public ManageUSPReturnValue DeletePhoto()
        {
            _MediaDeletedOn = DateTime.UtcNow;
            return SavePhotoDbInfo(false);
        }

        /// <summary>
        /// Get size info about the original image uploaded
        /// </summary>
        /// <param name="mediaType">Media type (IngredientPhoto, RecipePhoto, ecc.</param>
        private void GetOriginalImageSize(MediaType mediaType)
        {
            MediaUploadConfig imgConfig = new MediaUploadConfig(mediaType);
            string fileName = _MediaPath.Substring(_MediaPath.LastIndexOf("/") + 1);
            _originalFilePath = imgConfig.UploadOriginalFilePath;

            if (_originalImageHeight == null || _originalImageHeight == 0)
            {
                Image bmpOriginal = new Bitmap(HttpContext.Current.Server.MapPath(imgConfig.UploadOriginalFilePath + fileName));
                _originalImageHeight = bmpOriginal.Height;
                _originalImageWidth = bmpOriginal.Width;
                bmpOriginal.Dispose();
            }
        }

        /// <summary>
        /// Get size info about the image
        /// </summary>
        private void GetCroppedImageSize()
        {
            if (_croppedImageHeight == null || _croppedImageHeight == 0)
            {
                Image bmpCropped = new Bitmap(HttpContext.Current.Server.MapPath(_MediaPath));
                _croppedImageHeight = bmpCropped.Height;
                _croppedImageWidth = bmpCropped.Width;
                bmpCropped.Dispose();
            }
        }

        public ManageUSPReturnValue AddAlternativePhotoSize(MediaSizeTypes mediaSizeType, MediaType mediaType)
        {
            ManageUSPReturnValue _result = null;
            ManageUSPReturnValue _saveImageError = new ManageUSPReturnValue("MD-ER-0013", "", true);
            string _resizeResult = "";
            if (!MediaOnCDN)
            {
                MediaAlternativesSizesConfig _mediaSizeConfig = new MediaAlternativesSizesConfig(mediaType, mediaSizeType);
                int _newHeight = 0;
                int _newWidth = 0;

                if (_mediaSizeConfig.MediaHeight != null)
                {
                    _newHeight = (int)_mediaSizeConfig.MediaHeight;
                }

                if (_mediaSizeConfig.MediaWidth != null)
                {
                    _newWidth = (int)_mediaSizeConfig.MediaWidth;
                }


                string fileName = _MediaPath.Substring(_MediaPath.LastIndexOf("/") + 1);
                string md5Hash = "";
               
                //if media size is the orginal we need only save db info on table
                //the image is alredy phisical saved on file system
                if (mediaSizeType == MediaSizeTypes.OriginalSize)
                {

                    //try
                    //{
                    //    md5Hash = MySecurity.GenerateMD5FileHash(HttpContext.Current.Server.MapPath(_mediaSizeConfig.SavePath + fileName));
                    //}
                    //catch
                    //{
                    //}

                    MediaAlternativesSizes originalPhoto = new MediaAlternativesSizes(_IDMedia, mediaSizeType,
                                            _MediaServer, _MediaBakcupServer, _mediaSizeConfig.SavePath + fileName,
                                            md5Hash, false);
                    _result = originalPhoto.SaveDbInfo();
                }
                else if (mediaSizeType == MediaSizeTypes.OriginalResized)
                {
  
                    GetOriginalImageSize(mediaType);

                    string newFilePath = HttpContext.Current.Server.MapPath(_mediaSizeConfig.SavePath + fileName);
                    string oldFilePath = HttpContext.Current.Server.MapPath(_originalFilePath + fileName);

                    CalculatePhotoSize _newPhotoSize = new CalculatePhotoSize(_originalImageWidth, _originalImageHeight, _newWidth, _newHeight, 0);

                    _resizeResult = ResizeAndSave(oldFilePath, newFilePath, _newPhotoSize.Width, _newPhotoSize.Height, MyConvert.ToInt32(AppConfig.GetValue("DefaultImageQuality", AppDomain.CurrentDomain), 50));

                    if (_resizeResult == "")
                    {
                        return _saveImageError;
                    }

                    //try
                    //{
                    //    md5Hash = MySecurity.GenerateMD5FileHash(_resizeResult);
                    //}
                    //catch
                    //{
                    //}

                    MediaAlternativesSizes originalResizedPhoto = new MediaAlternativesSizes(_IDMedia, mediaSizeType,
                                            _MediaServer, _MediaBakcupServer, _mediaSizeConfig.SavePath + fileName,
                                            md5Hash, false);
                    _result = originalResizedPhoto.SaveDbInfo();

                    if (_result.IsError)
                    {
                        try
                        {
                            File.Delete(newFilePath);
                        }
                        catch
                        {
                        }
                    }
                }
                else
                {
                    GetCroppedImageSize();

                    string newFilePath = HttpContext.Current.Server.MapPath(_mediaSizeConfig.SavePath + fileName);
                    string oldFilePath = HttpContext.Current.Server.MapPath(_MediaPath);

                    if (_newWidth == 0 || _newWidth == 0)
                    {
                        CalculatePhotoSize _newPhotoSize = new CalculatePhotoSize(_croppedImageWidth, _croppedImageHeight, _newWidth, _newHeight, 0);

                        _resizeResult = ResizeAndSave(oldFilePath, newFilePath, _newPhotoSize.Width, _newPhotoSize.Height, MyConvert.ToInt32(AppConfig.GetValue("DefaultImageQuality", AppDomain.CurrentDomain), 50));
                    }
                    else
                    {
                        _resizeResult = ResizeAndSave(oldFilePath, newFilePath, _newWidth, _newHeight, MyConvert.ToInt32(AppConfig.GetValue("DefaultImageQuality", AppDomain.CurrentDomain), 50));
                    }


                    if (_resizeResult == "")
                    {
                        return _saveImageError;
                    }

                    //try
                    //{
                    //    md5Hash = MySecurity.GenerateMD5FileHash(_resizeResult);
                    //}
                    //catch
                    //{
                    //    //try
                    //    //{
                    //    //    base.MediaLogUSP(LogLevel.Errors, LogLevel.Errors, false, _result);
                    //    //}
                    //    //catch
                    //    //{
                    //    //}
                    //}

                    MediaAlternativesSizes originalResizedPhoto = new MediaAlternativesSizes(_IDMedia, mediaSizeType,
                                           _MediaServer, _MediaBakcupServer, _mediaSizeConfig.SavePath + fileName,
                                           md5Hash, false);
                    _result = originalResizedPhoto.SaveDbInfo();

                    if (_result.IsError)
                    {
                        try
                        {
                            File.Delete(newFilePath);
                        }
                        catch
                        {
                            try
                            {
                                base.MediaLogUSP(LogLevel.Errors, LogLevel.Errors, false, _result);
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
            else
            {
                //To be implemented in case the photo is alredy on CDN
            }

            return _result;
        }

        #region ImageManipulation

        /// <summary>
        /// Resize a photo and save it in a new path
        /// </summary>
        /// <param name="stgOriginalPath">Old image path</param>
        /// <param name="stgNewPath">New image path</param>
        /// <param name="NewWidth">New image width</param>
        /// <param name="NewHeight">New image height</param>
        /// <returns>true if operation succeed</returns>
        public static string ResizeAndSave(string stgOriginalPath, string stgNewPath, int NewWidth, int NewHeight, int Quality)
        {
            string _return = "";

            if (Quality < 1 || Quality > 100)
            {
                Quality = 50;
            }

            EncoderParameter _qualityParameter = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, Quality);
            string mimeType = "";

            mimeType = "image/jpeg";

            try
            {
                ImageCodecInfo imgEncoder = GetEncoderInfo(mimeType);
                EncoderParameters encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = _qualityParameter;


                Image bmpOriginal = new Bitmap(stgOriginalPath);

                Image bmpResized = new Bitmap(NewWidth, NewHeight);

                Graphics grpImage = Graphics.FromImage((System.Drawing.Image)bmpResized);

                grpImage.InterpolationMode = InterpolationMode.HighQualityBicubic;

                grpImage.DrawImage(bmpOriginal, 0, 0, NewWidth, NewHeight);

                stgNewPath = stgNewPath.Replace(Path.GetExtension(stgNewPath).ToLower(), ".jpg");

                grpImage.Dispose();

                bmpResized.Save(stgNewPath, imgEncoder, encoderParams);

                bmpOriginal.Dispose();
                bmpResized.Dispose();

                _return = stgNewPath;
            }
            catch(Exception ex)
            {
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.InfoMessages.ToString(), "", Network.GetCurrentPageName(), "MD-ER-0011", "Error resize image. "+ex.Message+" | " + stgOriginalPath + " | " + stgNewPath, "", false, true);
                    LogManager.WriteFileLog(LogLevel.InfoMessages, false, NewRow);
                    LogManager.WriteDBLog(LogLevel.Warnings, NewRow);
                }
                catch
                {
                }
                _return = "";
            }
            return _return;
        }

        /// <summary>
        /// Resize and overwrite the original photo file
        /// </summary>
        /// <param name="FilePath">Image complete path</param>
        /// <param name="NewWidth">New image width</param>
        /// <param name="NewHeight">New image height</param>
        /// <returns>true if operation succeed</returns>
        public static bool Resize(string FilePath, int NewWidth, int NewHeight)
        {
            try
            {

                using (Image photo =
                      Image.FromFile(FilePath))
                using (Bitmap result =
                      new Bitmap(NewWidth, NewHeight, photo.PixelFormat))
                {
                    result.SetResolution(
                            photo.HorizontalResolution,
                            photo.VerticalResolution);

                    using (Graphics g = Graphics.FromImage(result))
                    {
                        g.InterpolationMode =
                             InterpolationMode.HighQualityBicubic;
                        g.DrawImage(photo, 0, 0, NewWidth, NewHeight);

                        photo.Dispose();
                        result.Save(FilePath);
                        result.Dispose();
                        g.Dispose();
                    }
                }
                return true;
            }
            catch(Exception ex)
            {
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.InfoMessages.ToString(), "", Network.GetCurrentPageName(), "MD-ER-0011", "Error resize image. " + ex.Message + " | " + FilePath , "", false, true);
                    LogManager.WriteFileLog(LogLevel.InfoMessages, false, NewRow);
                    LogManager.WriteDBLog(LogLevel.Warnings, NewRow);
                }
                catch
                {
                }
                return false;
            }

        }

        /// <summary>
        /// Resize and compress with overwrite of the original photo file
        /// If image will be converted to jpeg a new file will be created
        /// </summary>
        /// <param name="FilePath">Image complete path</param>
        /// <param name="NewWidth">New image width</param>
        /// <param name="NewHeight">New image height</param>
        /// <param name="Quality">Value from 1 to 100 for the photo quality</param>
        /// <param name="ConvertToJpeg">If true convert file type to jpeg</param>
        /// <returns>true if operation succeed</returns>
        public static string ResizeCompress(string FilePath, int NewWidth, int NewHeight,int Quality,bool ConvertToJpeg)
        {
            string _return = "";

            if (Quality < 1 || Quality > 100)
            {
                Quality = 50;
            }

            try
            {
                EncoderParameter _qualityParameter = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, Quality);
                string mimeType = "";

                if (ConvertToJpeg)
                {
                    mimeType = "image/jpeg";
                }
                else
                {
                    mimeType = MIMEAssistant.GetMIMEType(FilePath);
                }


                if (mimeType != "unknown/unknown")
                {
                    ImageCodecInfo imgEncoder = GetEncoderInfo(mimeType);
                    EncoderParameters encoderParams = new EncoderParameters(1);
                    encoderParams.Param[0] = _qualityParameter;


                    using (Image photo =
                          Image.FromFile(FilePath))
                    using (Bitmap result =
                          new Bitmap(NewWidth, NewHeight, photo.PixelFormat))
                    {
                        result.SetResolution(
                                photo.HorizontalResolution,
                                photo.VerticalResolution);

                        using (Graphics g = Graphics.FromImage(result))
                        {
                            g.InterpolationMode =
                                 InterpolationMode.HighQualityBicubic;
                            g.DrawImage(photo, 0, 0, NewWidth, NewHeight);

                            photo.Dispose();
                            if (ConvertToJpeg)
                            {
                                FilePath = FilePath.Replace(Path.GetExtension(FilePath).ToLower(), ".jpg");
                            }
                            result.Save(FilePath, imgEncoder, encoderParams);
                            result.Dispose();
                            g.Dispose();
                        }
                    }
                    _return = FilePath;
                }
                else
                {
                    _return = "";
                }
            }
            catch (Exception ex)
            {
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.InfoMessages.ToString(), "", Network.GetCurrentPageName(), "MD-ER-0011", "Error resize image. " + ex.Message + " | " + FilePath, "", false, true);
                    LogManager.WriteFileLog(LogLevel.InfoMessages, false, NewRow);
                    LogManager.WriteDBLog(LogLevel.Warnings, NewRow);
                }
                catch
                {
                }
                _return = "";
            }
            return _return;
        }

        /// <summary>
        /// Crop and overwrite the original photo file 
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="StartX"></param>
        /// <param name="StartY"></param>
        /// <param name="imgWidth"></param>
        /// <param name="imgHeight"></param>
        /// <returns></returns>
        public static bool Crop(string FilePath, int StartX, int StartY, int imgWidth, int imgHeight)
        {
            try
            {
                int x = StartX;
                int y = StartY;
                int width = imgWidth;
                int height = imgHeight;

                using (Image photo =
                      Image.FromFile(FilePath))
                using (Bitmap result =
                      new Bitmap(width, height, photo.PixelFormat))
                {
                    result.SetResolution(
                            photo.HorizontalResolution,
                            photo.VerticalResolution);

                    using (Graphics g = Graphics.FromImage(result))
                    {
                        g.InterpolationMode =
                             InterpolationMode.HighQualityBicubic;
                        g.DrawImage(photo,
                             new Rectangle(0, 0, width, height),
                             new Rectangle(x, y, width, height),
                             GraphicsUnit.Pixel);
                        photo.Dispose();
                        result.Save(FilePath);
                        result.Dispose();
                        g.Dispose();
                    }
                }
                return true;
            }
            catch(Exception ex)
            {
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.InfoMessages.ToString(), "", Network.GetCurrentPageName(), "MD-ER-0012", "Error crop image. " + ex.Message + " | " + FilePath, "", false, true);
                    LogManager.WriteFileLog(LogLevel.InfoMessages, false, NewRow);
                    LogManager.WriteDBLog(LogLevel.Warnings, NewRow);
                }
                catch
                {
                }
                return false;
            }
        }

        /// <summary>
        /// Compress an image with overwrite of the original photo file
        /// If image will be converted to jpeg a new file will be created 
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="Quality">The jpeg quality, from 1 to 100</param>
        /// <param name="ConvertToJpeg">If true convert file type to jpeg</param>
        /// <returns>true if operation succeed</returns>
        public static bool CompressImage(string FilePath, int Quality, bool ConvertToJpeg)
        {
            bool _return = false;

            if (Quality < 1 || Quality > 100)
            {
                Quality = 50;
            }

            try
            {

                EncoderParameter _qualityParameter = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, Quality);
                string mimeType = "";

                if (ConvertToJpeg)
                {
                    mimeType = "image/jpeg";
                }
                else
                {
                    mimeType = MIMEAssistant.GetMIMEType(FilePath);
                }

                if (mimeType != "unknown/unknown")
                {
                    ImageCodecInfo imgEncoder = GetEncoderInfo(mimeType);
                    EncoderParameters encoderParams = new EncoderParameters(1);
                    encoderParams.Param[0] = _qualityParameter;

                  

                using (Image photo =
                      Image.FromFile(FilePath))
                using (Bitmap result =
                      new Bitmap(photo.Width, photo.Height, photo.PixelFormat))
                {
                    result.SetResolution(
                            photo.HorizontalResolution,
                            photo.VerticalResolution);

                    using (Graphics g = Graphics.FromImage(result))
                    {
                        g.InterpolationMode =
                             InterpolationMode.HighQualityBicubic;
                        g.DrawImage(photo, 0, 0, photo.Width, photo.Height);

                        photo.Dispose();
                        if (ConvertToJpeg)
                        {
                            FilePath = FilePath.Replace(Path.GetExtension(FilePath).ToLower(), ".jpg");
                        }
                        result.Save(FilePath, imgEncoder, encoderParams);
                        result.Dispose();
                        g.Dispose();
                    }
                }

                _return = true;
                }
                else
                {
                    _return = false;
                }
                
            }
            catch
            {
                _return = false;
            }

            return _return;
        }

        /// <summary> 
        /// Returns the image codec with the given mime type 
        /// </summary> 
        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats 
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec 
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }
        #endregion

        /// <summary>
        /// Move a set of photo on CDN
        /// </summary>
        /// <param name="MaxPhotoToMove">Man number of photo to move</param>
        /// <param name="AwsAccessKey">Access key to Amazon S3 Storage</param>
        /// <param name="AwsSecretKey">Secret key to Amazon S3 Storage</param>
        /// <param name="BucketName">Bucket name on Amazon S3 Storage</param>
        /// <param name="CDNBasePath">CDN path to update DBMedia field</param>
        /// <param name="mediaSizeType">Size type to move</param>
        /// <param name="MaxErrorBeforeAbort">Max number of error before the job was aborted</param>
        /// <returns></returns>
        public static ManageUSPReturnValue MovePhotoOnCDN(int MaxPhotoToMove, string AwsAccessKey, 
                                                string AwsSecretKey, string BucketName, string CDNBasePath, int MaxErrorBeforeAbort, MediaSizeTypes mediaSizeType)
        {
            ManageUSPReturnValue _resultMove = new ManageUSPReturnValue("MD-ER-9999", "General Error", true);
            int ErrorNumber = 0;
            int imageMoved = 0;
            DataTable dtMedia;
            StringBuilder _resultOutput = new StringBuilder();

            GetMediaDAL MediaDAL = new GetMediaDAL();
            try
            {
                dtMedia = MediaDAL.USP_GetMediaNotInCDN(MaxPhotoToMove,mediaSizeType.ToString());
            }
            catch(Exception error)
            {
                _resultMove = new ManageUSPReturnValue("MD-ER-9999", "General Error - " + error.Message, true);
                return _resultMove;
            }

            if (dtMedia.Rows.Count > 0)
            {
                AmazonS3Client s3Client;
                try
                {
                    s3Client = new AmazonS3Client(AwsAccessKey, AwsSecretKey);
                }
                catch (AmazonS3Exception amazonS3ClientException)
                {
                    try
                    {
                        LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(),
                                        "MD-ER-9999", "Error connection S3 Storage '" + amazonS3ClientException.Message + " Error Stack: " + amazonS3ClientException.InnerException, "", false, true);
                        LogManager.WriteFileLog(LogLevel.Debug, false, NewRow);
                        LogManager.WriteDBLog(LogLevel.InfoMessages, NewRow);
                    }
                    catch
                    {
                    }
                    _resultMove = new ManageUSPReturnValue("MD-ER-9999", amazonS3ClientException.Message + " Error Stack: " + amazonS3ClientException.InnerException, true);
                    return _resultMove;
                }
                foreach (DataRow _drMedia in dtMedia.Rows)
                {
                    if (ErrorNumber < MaxErrorBeforeAbort)
                    {
                        Photo _photoToMove = new Photo(_drMedia.Field<Guid>("IDMedia"));
                        _photoToMove.QueryMediaInfo();
                        try
                        {
                            PutObjectRequest request = new PutObjectRequest();
                            if (mediaSizeType == MediaSizeTypes.MediaCroppedSize)
                            {
                                request.WithFilePath(HttpContext.Current.Server.MapPath(_photoToMove.MediaPath)).WithTimeout(360000000);
                                request.WithKey(_photoToMove.MediaPath.Substring(1));
                            }
                            else
                            {
                                request.WithFilePath(HttpContext.Current.Server.MapPath(_photoToMove.GetAlternativeSizePath(mediaSizeType, false, false, false))).WithTimeout(360000000);
                                request.WithKey(_photoToMove.GetAlternativeSizePath(mediaSizeType,false,false,false).Substring(1));
                            }
                            request.WithBucketName(BucketName);
                            request.CannedACL = S3CannedACL.PublicRead;
                            request.AddHeader("Expires", DateTime.Now.AddYears(30).ToUniversalTime().ToString("ddd, dd MMM yyyy HH:mm:ss 'GMT'"));

                            S3Response responseWithMetadata = s3Client.PutObject(request);

                            if (mediaSizeType == MediaSizeTypes.MediaCroppedSize)
                            {
                                try
                                {
                                    _photoToMove.MediaOnCDN = true;
                                    _photoToMove.MediaServer = CDNBasePath;
                                    _photoToMove.SavePhotoDbInfo(false);
                                    _resultOutput.Append(_photoToMove.MediaPath + " --> moved on CDN <br/>");
                                }
                                catch(Exception ex)
                                {
                                    _resultOutput.Append(_photoToMove.MediaPath + " --> ERROR moving on CDN - " + ex.Message + "<br/>");
                                }
                            }
                            else
                            {
                                MediaAlternativesSizes _altMedia = new MediaAlternativesSizes(_photoToMove.IDMedia, mediaSizeType);
                                try
                                {
                                    _altMedia.MediaOnCDN = true;
                                    _altMedia.MediaServer = CDNBasePath;
                                    _altMedia.SaveDbInfo();
                                    _resultOutput.Append(_altMedia.MediaPath + " --> moved on CDN <br/>");
                                }
                                catch(Exception ex)
                                {
                                    _resultOutput.Append(_altMedia.MediaPath + " --> ERROR moving on CDN - " + ex.Message + "<br/>");
                                }
                            }
                           
                            imageMoved++;
                            

                        }
                        catch (AmazonS3Exception amazonS3Exception)
                        {
                            ErrorNumber++;
                            try
                            {
                                LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(),
                                            "MD-ER-0015", "Error moving photo '" + _photoToMove.MediaPath + "' on CDN. Error: " + amazonS3Exception.Message, "IDMedia: " + _photoToMove.IDMedia, false, true);
                                LogManager.WriteFileLog(LogLevel.CriticalErrors, false, NewRow);
                                LogManager.WriteDBLog(LogLevel.CriticalErrors, NewRow);
                            }
                            catch
                            {
                            }
                            _resultMove = new ManageUSPReturnValue("MD-ER-9999", amazonS3Exception.Message + " Error Stack: " + amazonS3Exception.InnerException + "<br/>" + _resultOutput, true);
                        }
                        catch (Exception ex)
                        {
                            ErrorNumber++;
                            try
                            {
                                LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(),
                                            "MD-ER-0015", "Error moving photo '" + _photoToMove.MediaPath + "' on CDN. Error: " + ex.Message, "IDMedia: " + _photoToMove.IDMedia, false, true);
                                LogManager.WriteFileLog(LogLevel.CriticalErrors, false, NewRow);
                                LogManager.WriteDBLog(LogLevel.CriticalErrors, NewRow);
                            }
                            catch
                            {
                            }
                            _resultMove = new ManageUSPReturnValue("MD-ER-9999", ex.Message + " Error Stack: " + ex.InnerException + "<br/>" + _resultOutput, true);
                        }

                    }
                    else
                    {
                        try
                        {
                            LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(), "MD-ER-0014", "Move photo job aborted, too many error", "", false, true);
                            LogManager.WriteFileLog(LogLevel.CriticalErrors, true, NewRow);
                            LogManager.WriteDBLog(LogLevel.CriticalErrors, NewRow);
                        }
                        catch
                        {
                        }
                        _resultMove = new ManageUSPReturnValue("MD-ER-9999", "Max error", true);
                        break;
                    }
                }

            }
            _resultMove = new ManageUSPReturnValue("MD-IN-0002", imageMoved.ToString() + " Media Moved on CDN<br/>" + _resultOutput, false);
            return _resultMove;
        }


        public static ManageUSPReturnValue CreateAltSizeForMedia(int MaxAltSizeToCreate, int MaxErrorBeforeAbort, MediaType mediaType, MediaSizeTypes mediaSizeType)
        {
            ManageUSPReturnValue _resultMove = new ManageUSPReturnValue("MD-ER-9999", "General Error", true);
            int ErrorNumber = 0;
            int imageCreated = 0;
            DataTable dtMedia;
            StringBuilder _resultOutput = new StringBuilder();

            GetMediaDAL MediaDAL = new GetMediaDAL();
            try
            {
                dtMedia = MediaDAL.USP_GetMediaWithoutAlternativeSizes(MaxAltSizeToCreate, mediaType.ToString(), mediaSizeType.ToString());
            }
            catch(Exception error)
            {
                _resultMove = new ManageUSPReturnValue("MD-ER-9999", "General Error - " + error.Message, true);
                return _resultMove;
            }

            if (dtMedia.Rows.Count > 0)
            {
                foreach (DataRow _drMedia in dtMedia.Rows)
                {
                    if (ErrorNumber < MaxErrorBeforeAbort)
                    {
                        Photo _photoToMove = new Photo(_drMedia.Field<Guid>("IDMedia"));
                        _photoToMove.QueryMediaInfo();
                        try
                        {
                            _photoToMove.AddAlternativePhotoSize(mediaSizeType, mediaType);
                            imageCreated++;
                            _resultOutput.Append(_photoToMove._MediaPath + " --> OK<br/>");
                        }
                        catch(Exception err)
                        {
                            ErrorNumber++;
                            _resultOutput.Append(_photoToMove._MediaPath + " --> ERROR - " + err.Message + "<br/>");
                        }
                    }
                    else
                    {
                        _resultMove = new ManageUSPReturnValue("MD-ER-9999", imageCreated.ToString() + " Image Created but process stopped. Too Errors.<br/>" + _resultOutput, true);
                        return _resultMove;
                    }
                }
            }
            _resultMove = new ManageUSPReturnValue("", imageCreated.ToString() + " Image created successfully<br/>" + _resultOutput, false);
            return _resultMove;
        }
        #endregion

        #region Operators

        public static implicit operator Photo(Guid guid)
        {
            Photo photo = new Photo(guid);
            return photo;
        }

        public static implicit operator Guid(Photo photo)
        {
            Guid guid = new Guid();
            if (photo == null)
            {
                return guid;
            }
            else
            {
                return photo.IDMedia;
            }
        }

        public static implicit operator Photo(DefaultMedia defaultMedia)
        {
            Photo photo = new Photo(defaultMedia.IDMedia);
            return photo;
        }

        #endregion
    }

    public class CalculatePhotoSize
    {
        #region PrivateFields

        private int _ImgWidth;
        private int _ImgHeight;
        private int _ImgFinalWidth;
        private int _ImgFinalHeight;
        private int _PercPlusSizeForCrop;
        double _PerRatio;
        double _MaxSizeCalc;
        int _Height;
        int _Width;

        #endregion

        #region PublicProperties

        public int ImgWidth
        {
            get { return _ImgWidth; }
            set { _ImgWidth = value; }
        }

        public int ImgHeight
        {
            get { return _ImgHeight; }
            set { _ImgHeight = value; }
        }

        public int ImgFinalWidth
        {
            get { return _ImgFinalWidth; }
            set { _ImgFinalWidth = value; }
        }

        public int ImgFinalHeight
        {
            get { return _ImgFinalHeight; }
            set { _ImgFinalHeight = value; }
        }

        public int Height
        {
            get { return _Height; }
            set { _Height = value; }
        }

        public int Width
        {
            get { return _Width; }
            set { _Width = value; }
        }

        public int PercPlusSizeForCrop
        {
            get { return _PercPlusSizeForCrop; }
            set { _PercPlusSizeForCrop = value; }
        }

        #endregion

        public CalculatePhotoSize(int ImgWidth, int ImgHeight, int ImgFinalWidth, int ImgFinalHeight, int PercPlusSizeForCrop)
        {
            _ImgWidth = ImgWidth;
            _ImgHeight = ImgHeight;
            _ImgFinalWidth = ImgFinalWidth;
            _ImgFinalHeight = ImgFinalHeight;

            if (_ImgFinalHeight == 0)
            {
                _ImgFinalHeight = _ImgFinalWidth;
            }
            else if (_ImgFinalWidth == 0)
            {
                _ImgFinalWidth = _ImgFinalHeight;
            }

            if (_ImgWidth > _ImgHeight)
            {
                _MaxSizeCalc = _ImgFinalHeight + ((_ImgFinalHeight / 100) * PercPlusSizeForCrop);

                _PerRatio = Convert.ToDouble(_ImgHeight) / _MaxSizeCalc;
                _Width = Convert.ToInt32(_ImgWidth / _PerRatio);
                _Height = Convert.ToInt32(_MaxSizeCalc);
            }
            else
            {
                _MaxSizeCalc = _ImgFinalWidth + ((_ImgFinalWidth / 100) * PercPlusSizeForCrop);

                _PerRatio = Convert.ToDouble(_ImgWidth) / _MaxSizeCalc;
                _Height = Convert.ToInt32(_ImgHeight / _PerRatio);
                _Width = Convert.ToInt32(_MaxSizeCalc);

            }
        }
    }

    public class Video : Media
    {
        public Video()
        {
        }

        public Video(Guid Video)
        {
            Media media = new Media(Video);
            base._IDMedia = media.IDMedia;
            base._MediaOwner = media.MediaOwner;
            base._isImage = media.isImage;
            base._isVideo = media.isVideo;
            base._isLink = media.isLink;
            base._isEsternalSource = media.isEsternalSource;
            base._MediaServer = media.MediaServer;
            base._MediaBakcupServer = media.MediaBakcupServer;
            base._MediaPath = media.MediaPath;
            base._MediaMD5Hash = media.MediaMD5Hash;
            base._Checked = media.Checked;
            base._CheckedByUser = media.CheckedByUser;
            base._MediaDisabled = media.MediaDisabled;
            base._MediaUpdatedOn = media.MediaUpdatedOn;
            base._MediaDeletedOn = media.MediaDeletedOn;
            base._UserIsMediaOwner = media.UserIsMediaOwner;
            base.MediaOnCDN = media.MediaOnCDN;
        }

        public static implicit operator Video(Guid guid)
        {
            Video video = new Video(guid);
            return video;
        }

    }

    public class ExternalLink : Media
    {
        public ExternalLink(Guid ExternalLink)
        {
            Media media = new Media(ExternalLink);
            base._IDMedia = media.IDMedia;
            base._MediaOwner = media.MediaOwner;
            base._isImage = media.isImage;
            base._isVideo = media.isVideo;
            base._isLink = media.isLink;
            base._isEsternalSource = media.isEsternalSource;
            base._MediaServer = media.MediaServer;
            base._MediaBakcupServer = media.MediaBakcupServer;
            base._MediaPath = media.MediaPath;
            base._MediaMD5Hash = media.MediaMD5Hash;
            base._Checked = media.Checked;
            base._CheckedByUser = media.CheckedByUser;
            base._MediaDisabled = media.MediaDisabled;
            base._MediaUpdatedOn = media.MediaUpdatedOn;
            base._MediaDeletedOn = media.MediaDeletedOn;
            base._UserIsMediaOwner = media.UserIsMediaOwner;
            base.MediaOnCDN = media.MediaOnCDN;
        }
    }

    public class DefaultMedia : Media
    {
        #region PrivateFields

        private int _IDDefaultMediaForObject;
        private ObjectType _ObjectCode;
        private string _Description;
        private bool _Enabled;

        #endregion

        #region PublicProperties

        public int IDDefaultMediaForObject
        {
            get { return _IDDefaultMediaForObject; }
        }

        public ObjectType ObjectCode
        {
            get { return _ObjectCode; }
            set { _ObjectCode = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public bool Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; }
        }

        #endregion

        #region Constructors

        public DefaultMedia(ObjectType DefaultMedia)
        {
            _ObjectCode = DefaultMedia;
            QueryDefaultMediaInfo();
        }

        #endregion

        #region Methods

        void QueryDefaultMediaInfo()
        {
            GetDefaultMediaForObjectsDAL DefaultMediaDAL = new GetDefaultMediaForObjectsDAL();
            DataTable dtDefaultMedia = new DataTable();
            dtDefaultMedia = DefaultMediaDAL.GetDefaultMediaForObjectCode(_ObjectCode.ToString());

            if (dtDefaultMedia.Rows.Count > 0)
            {
                Guid GuidMedia = new Guid();

                _IDDefaultMediaForObject = dtDefaultMedia.Rows[0].Field<int>("IDDefaultMediaForObject");
                _Description = dtDefaultMedia.Rows[0].Field<string>("Description");
                _Enabled=dtDefaultMedia.Rows[0].Field<bool>("Enabled");
                GuidMedia = dtDefaultMedia.Rows[0].Field<Guid>("IDMedia");

                Media media = new Media(GuidMedia);
                base._IDMedia = media.IDMedia;
                base._MediaOwner = media.MediaOwner;
                base._isImage = media.isImage;
                base._isVideo = media.isVideo;
                base._isLink = media.isLink;
                base._isEsternalSource = media.isEsternalSource;
                base._MediaServer = media.MediaServer;
                base._MediaBakcupServer = media.MediaBakcupServer;
                base._MediaPath = media.MediaPath;
                base._MediaMD5Hash = media.MediaMD5Hash;
                base._Checked = media.Checked;
                base._CheckedByUser = media.CheckedByUser;
                base._MediaDisabled = media.MediaDisabled;
                base._MediaUpdatedOn = media.MediaUpdatedOn;
                base._MediaDeletedOn = media.MediaDeletedOn;
                base._UserIsMediaOwner = media.UserIsMediaOwner;
                base.MediaOnCDN = media.MediaOnCDN;
            }

        }

        public static DataTable GetAllDefaultMediaForObject()
        {
            GetDefaultMediaForObjectsDAL obj = new GetDefaultMediaForObjectsDAL();
            return obj.GetAllDefaultMediaForObject();
        }

        public static DataTable GetAllEnabledMediaForObjectCode()
        {
            GetDefaultMediaForObjectsDAL obj = new GetDefaultMediaForObjectsDAL();
            return obj.GetAllEnabledMediaForObjectCode();
        }

        public static string GetDefaultMediaPathFromWebConfig(MediaType _mediaType)
        {
            string _returnPath = "";
            _returnPath = AppConfig.GetValue(_mediaType.ToString(), AppDomain.CurrentDomain);
            return _returnPath;
        }

        #endregion
    }

    public class MediaUploadConfig
    {
        #region PrivateFields

        private int _IDMediaUploadConfig;
        private MediaType _MediaType;
        private string _UploadPath;
        private string _UploadOriginalFilePath;
        private int _MaxSizeByte;
        private string _AcceptedContentTypes;
        private string _AcceptedFileExtension;
        private string _AcceptedFileExtensionRegex;
        private bool _EnableUploadForMediaType;
        private bool _ComputeMD5Hash;
        private int _MediaFinalHeight;
        private int _MediaFinalWidth;
        private int _MediaPercPlusSizeForCrop;
        private int _MediaSmallerSideMinSize;

        #endregion

        #region PublicProperties

        public int IDMediaUploadConfig
        {
            get { return _IDMediaUploadConfig; }
        }
        public MediaType MediaType
        {
            get { return _MediaType; }
        }
        public string UploadPath
        {
            get { return _UploadPath; }
            set { _UploadPath = value; }
        }
        public string UploadOriginalFilePath
        {
            get { return _UploadOriginalFilePath; }
            set { _UploadOriginalFilePath = value; }
        }
        public int MaxSizeByte
        {
            get { return _MaxSizeByte; }
            set { _MaxSizeByte = value; }
        }
        public int MaxSizeMegaByte
        {
            get { return _MaxSizeByte/1024/1024; }
        }
        public string AcceptedContentTypes
        {
            get { return _AcceptedContentTypes; }
            set { _AcceptedContentTypes = value; }
        }
        public string AcceptedFileExtension
        {
            get { return _AcceptedFileExtension; }
            set { _AcceptedFileExtension = value; }
        }
        public string AcceptedFileExtensionRegex
        {
            get { return _AcceptedFileExtensionRegex; }
            set { _AcceptedFileExtensionRegex = value; }
        }
        public bool EnableUploadForMediaType
        {
            get { return _EnableUploadForMediaType; }
            set { _EnableUploadForMediaType = value; }
        }
        public bool ComputeMD5Hash
        {
            get { return _ComputeMD5Hash; }
            set { _ComputeMD5Hash = value; }
        }
        public int MediaFinalHeight
        {
            get { return _MediaFinalHeight; }
            set { _MediaFinalHeight = value; }
        }
        public int MediaFinalWidth
        {
            get { return _MediaFinalWidth; }
            set { _MediaFinalWidth = value; }
        }
        public int MediaPercPlusSizeForCrop
        {
            get { return _MediaPercPlusSizeForCrop; }
            set { _MediaPercPlusSizeForCrop = value; }
        }
        public int MediaSmallerSideMinSize
        {
            get { return _MediaSmallerSideMinSize; }
            set { _MediaSmallerSideMinSize = value; }
        }
        #endregion

        #region Costructors

        public MediaUploadConfig(MediaType _mediaType)
        {
            _MediaType = _mediaType;
            QueryMediaUploadConfigInfo();
        }
        
        #endregion

        #region Methods

        private void QueryMediaUploadConfigInfo()
        {
            GetMediaUploadConfigDAL MediaUploadConfigDAL = new GetMediaUploadConfigDAL();
            DataTable dtMediaUploadConfig = new DataTable();
            dtMediaUploadConfig = MediaUploadConfigDAL.GetMediaUploadConfigByMediaType(_MediaType.ToString());

            if (dtMediaUploadConfig.Rows.Count > 0)
            {
                _IDMediaUploadConfig = dtMediaUploadConfig.Rows[0].Field<int>("IDMediaUploadConfig");
                _UploadPath = dtMediaUploadConfig.Rows[0].Field<string>("UploadPath");
                _UploadOriginalFilePath = dtMediaUploadConfig.Rows[0].Field<string>("UploadOriginalFilePath");
                _MaxSizeByte = dtMediaUploadConfig.Rows[0].Field<int>("MaxSizeByte");
                _AcceptedContentTypes = dtMediaUploadConfig.Rows[0].Field<string>("AcceptedContentTypes");
                _AcceptedFileExtension = dtMediaUploadConfig.Rows[0].Field<string>("AcceptedFileExtension");
                _AcceptedFileExtensionRegex = dtMediaUploadConfig.Rows[0].Field<string>("AcceptedFileExtensionRegex");
                _EnableUploadForMediaType = dtMediaUploadConfig.Rows[0].Field<bool>("EnableUploadForMediaType");
                _ComputeMD5Hash = dtMediaUploadConfig.Rows[0].Field<bool>("ComputeMD5Hash");
                _MediaFinalHeight = dtMediaUploadConfig.Rows[0].Field<int>("MediaFinalHeight");
                _MediaFinalWidth = dtMediaUploadConfig.Rows[0].Field<int>("MediaFinalWidth");
                _MediaPercPlusSizeForCrop = dtMediaUploadConfig.Rows[0].Field<int>("MediaPercPlusSizeForCrop");
                _MediaSmallerSideMinSize = dtMediaUploadConfig.Rows[0].Field<int>("MediaSmallerSideMinSize");
            }

        }

        public static string GetAcceptedFileExtensionRegex(MediaType _mediaType)
        {
            MediaUploadConfig _mediaUpConfig = new MediaUploadConfig(_mediaType);
            return _mediaUpConfig.AcceptedFileExtensionRegex;
        }
        
        #endregion

    }

    public class MediaAlternativesSizesConfig
    {
        #region PrivateFileds

        private int _IDMediaAlternativeSizeConfig;
        private MediaType _MediaType;
        private MediaSizeTypes _MediaSizeType;
        private string _SavePath;
        private int? _MediaHeight;
        private int? _MediaWidth;

        #endregion

        #region PublicProperties

        public int IDMediaAlternativeSizeConfig
        {
            get { return _IDMediaAlternativeSizeConfig; }
        }
        public MediaType MediaType
        {
            get { return _MediaType; }
            set { _MediaType = value; }
        }
        public MediaSizeTypes MediaSizeType
        {
            get { return _MediaSizeType; }
            set { _MediaSizeType = value; }
        }
        public string SavePath
        {
            get { return _SavePath; }
            set { _SavePath = value; }
        }
        public int? MediaHeight
        {
            get { return _MediaHeight; }
            set { _MediaHeight = value; }
        }
        public int? MediaWidth
        {
            get { return _MediaWidth; }
            set { _MediaWidth = value; }
        }

        #endregion

        #region Costructors

        public MediaAlternativesSizesConfig(MediaType mediaType, MediaSizeTypes mediaSizeType)
        {
            _MediaType = mediaType;
            _MediaSizeType = mediaSizeType;

            try
            {
                QueryMediaAlternativesSizesConfigInfo();
            }
            catch
            {
            }
        }

        #endregion

        #region Methods

        private void QueryMediaAlternativesSizesConfigInfo()
        {
            GetMediaAlternativesSizesConfigDAL MediaAlternativesSizesConfigDAL = new GetMediaAlternativesSizesConfigDAL();
            DataTable dtMediaAlternativesSizesConfig = new DataTable();
            dtMediaAlternativesSizesConfig = MediaAlternativesSizesConfigDAL.GetMediaAltSizeConfig(_MediaType.ToString(), _MediaSizeType.ToString());

            if (dtMediaAlternativesSizesConfig.Rows.Count > 0)
            {
                _IDMediaAlternativeSizeConfig = dtMediaAlternativesSizesConfig.Rows[0].Field<int>("IDMediaAlternativeSizeConfig");
                _SavePath = dtMediaAlternativesSizesConfig.Rows[0].Field<string>("SavePath");
                _MediaHeight = dtMediaAlternativesSizesConfig.Rows[0].Field<int?>("MediaHeight");
                _MediaWidth = dtMediaAlternativesSizesConfig.Rows[0].Field<int?>("MediaWidth");

            }
        }

        #endregion
    }

    public class MediaAlternativesSizes
    {
        #region PrivateFileds

        private Guid _IDMediaAlternativeSize;
        private Guid _IDMedia;
        private MediaSizeTypes _MediaSizeType;
        private string _MediaServer;
        private string _MediaBackupServer;
        private string _MediaPath;
        private string _MediaMD5Hash;
        private bool _MediaOnCDN;
        //private string _MediaFatherPath;
        //private MediaType _MediaType;

        #endregion

        #region PublicProperties

        public Guid IDMediaAlternativeSize
        {
            get { return _IDMediaAlternativeSize; }
        }
        public Guid IDMedia
        {
            get { return _IDMedia; }
        }
        public MediaSizeTypes MediaSizeType
        {
            get { return _MediaSizeType; }
            set { _MediaSizeType = value; }
        }
        public string MediaServer
        {
            get { return _MediaServer; }
            set { _MediaServer = value; }
        }
        public string MediaBackupServer
        {
            get { return _MediaBackupServer; }
            set { _MediaBackupServer = value; }
        }
        public string MediaPath
        {
            get { return _MediaPath; }
            set { _MediaPath = value; }
        }
        public string MediaMD5Hash
        {
            get { return _MediaMD5Hash; }
            set { _MediaMD5Hash = value; }
        }
        public bool MediaOnCDN
        {
            get { return _MediaOnCDN; }
            set { _MediaOnCDN = value; }
        }

        #endregion

        #region Costructors

        /// <summary>
        /// Get Info about alternative media using Alternative Media ID
        /// </summary>
        /// <param name="idMediaAlternativeSize">Alternative Media ID</param>
        public MediaAlternativesSizes(Guid idMediaAlternativeSize)
        {
            _IDMediaAlternativeSize = idMediaAlternativeSize;

            try
            {
                QueryMediaAlternativesSizesByID();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Get Info about alternative media using Media Father ID and Media Size type property
        /// </summary>
        /// <param name="idMedia">ID of media father</param>
        /// <param name="mediaSizeType">Size to get</param>
        public MediaAlternativesSizes(Guid idMedia, MediaSizeTypes mediaSizeType)
        {
            _IDMedia = idMedia;
            _MediaSizeType = mediaSizeType;
            try
            {
                QueryMediaAlternativesSizesByMedia();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Create a new Media alternative size ready to be saved
        /// </summary>
        /// <param name="idMedia">Media ID father</param>
        /// <param name="mediaSizeType">Size of media to save</param>
        /// <param name="mediaServer">Server of the media</param>
        /// <param name="mediaBackupServer">Backup server for the media</param>
        /// <param name="mediaPath">Path of the media saved</param>
        /// <param name="mediaMD5Hash">Hash of the media</param>
        /// <param name="mediaOnCDN">True if media is saved on CDN</param>
        /// <param name="mediaType">Type of media</param>
        /// <param name="mediaFatherPath">True if media is saved on CDN</param>
        public MediaAlternativesSizes(Guid idMedia, MediaSizeTypes mediaSizeType,string mediaServer,string mediaBackupServer,
                                        string mediaPath, string mediaMD5Hash, bool mediaOnCDN)
        {
            _IDMediaAlternativeSize=Guid.NewGuid();
            _IDMedia = idMedia;
            _MediaSizeType = mediaSizeType;
            _MediaServer = mediaServer;
            _MediaBackupServer = mediaBackupServer;
            _MediaPath = mediaPath;
            _MediaMD5Hash = mediaMD5Hash;
            _MediaOnCDN = mediaOnCDN;

            //_MediaType = mediaType;
            //_MediaFatherPath = mediaFatherPath;
        }

        #endregion

        #region Methods

        private void QueryMediaAlternativesSizesByID()
        {
            GetMediaAlternativesSizesDAL MediaAlternativesSizesDAL = new GetMediaAlternativesSizesDAL();
            DataTable dtMediaAlternativesSizes = new DataTable();
            dtMediaAlternativesSizes = MediaAlternativesSizesDAL.GetMediaAlternativeSizeByIDMediaAltSize(_IDMediaAlternativeSize);
            
            if (dtMediaAlternativesSizes.Rows.Count > 0)
            {
                _IDMedia = dtMediaAlternativesSizes.Rows[0].Field<Guid>("IDMedia");
                _MediaSizeType = (MediaSizeTypes)Enum.Parse(typeof(MediaSizeTypes), dtMediaAlternativesSizes.Rows[0].Field<string>("MediaSizeType"));
                _MediaServer = dtMediaAlternativesSizes.Rows[0].Field<string>("MediaServer");
                _MediaBackupServer = dtMediaAlternativesSizes.Rows[0].Field<string>("MediaBackupServer");
                _MediaPath = dtMediaAlternativesSizes.Rows[0].Field<string>("MediaPath");
                _MediaMD5Hash = dtMediaAlternativesSizes.Rows[0].Field<string>("MediaMD5Hash");
                _MediaOnCDN = dtMediaAlternativesSizes.Rows[0].Field<bool>("MediaOnCDN");

            }
        }

        private void QueryMediaAlternativesSizesByMedia()
        {
            GetMediaAlternativesSizesDAL MediaAlternativesSizesDAL = new GetMediaAlternativesSizesDAL();
            DataTable dtMediaAlternativesSizes = new DataTable();
            dtMediaAlternativesSizes = MediaAlternativesSizesDAL.GetMediaAlternativeSizeByIDMedia(_IDMedia, MediaSizeType.ToString());

            if (dtMediaAlternativesSizes.Rows.Count > 0)
            {
                _IDMediaAlternativeSize = dtMediaAlternativesSizes.Rows[0].Field<Guid>("IDMediaAlternativeSize");
                _MediaServer = dtMediaAlternativesSizes.Rows[0].Field<string>("MediaServer");
                _MediaBackupServer = dtMediaAlternativesSizes.Rows[0].Field<string>("MediaBackupServer");
                _MediaPath = dtMediaAlternativesSizes.Rows[0].Field<string>("MediaPath");
                _MediaMD5Hash = dtMediaAlternativesSizes.Rows[0].Field<string>("MediaMD5Hash");
                _MediaOnCDN = dtMediaAlternativesSizes.Rows[0].Field<bool>("MediaOnCDN");

            }
        }

        public ManageUSPReturnValue SaveDbInfo()
        {
            ManageMediaDAL saveMediaAltSizeDAL = new ManageMediaDAL();
            DataTable _dtSaveMediaAltSizeResult = new DataTable();

            _dtSaveMediaAltSizeResult = saveMediaAltSizeDAL.USP_SaveAlternativeSizeMedia(_IDMediaAlternativeSize, _IDMedia,
                                        _MediaSizeType.ToString(), _MediaServer,_MediaBackupServer, _MediaPath, _MediaMD5Hash, _MediaOnCDN);

            ManageUSPReturnValue _return = new ManageUSPReturnValue(_dtSaveMediaAltSizeResult);
            return _return;
        }

        #endregion
    }

    public enum MediaType : int
    {
        NotSpecified = 0,
        RecipePhoto = 1,
        IngredientPhoto = 2,
        RecipeStepPhoto = 3,
        ProfileImagePhoto = 4,
        Video = 5,
        ExternalLink = 6,
        SiteBackgroundBreakfast = 7,
        SiteBackgroundMorning = 8,
        SiteBackgroundLunch = 9,
        SiteBackgroundAfternoon = 10,
        SiteBackgroundDinner = 11,
        SiteBackgroundNight = 12,
        SiteBackgroundLateNight = 13
    }

    class AmazonS3Assistant
    {
        #region Private Fields

        //string _MediaToMove;
        //string _BucketName;
        string _AwsAccessKey;
        string _AwsSecretKey;
        ManageUSPReturnValue _ReturnValue;
        bool _isInError;
        AmazonS3Client s3Client;

        #endregion

        #region Puplic Properties

        //string MediaToMove 
        //{
        //    get { return _MediaToMove; }
        //    set { _MediaToMove = value; }
        //}
        //string BucketName
        //{
        //    get { return _BucketName; }
        //    set { _BucketName = value; }
        //}
        string AwsAccessKey
        {
            get { return _AwsAccessKey; }
            set { _AwsAccessKey = value; }
        }
        string AwsSecretKey
        {
            get { return _AwsSecretKey; }
            set { _AwsSecretKey = value; }
        }
        ManageUSPReturnValue ReturnValue
        {
            get { return _ReturnValue; }
        }
        bool IsInError
        {
            get { return _isInError; }
        }
        #endregion

        #region Costructors

        AmazonS3Assistant(string AccessKey, string SecretKey)
        {
            _AwsAccessKey = AccessKey;
            _AwsSecretKey = SecretKey;
            _isInError = false;

            try
            {
                s3Client = new AmazonS3Client(AwsAccessKey, AwsSecretKey);
            }
            catch (AmazonS3Exception amazonS3ClientException)
            {
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(),
                                    "MD-ER-9999", "Error connection S3 Storage '" + amazonS3ClientException.Message + " Error Stack: " + amazonS3ClientException.InnerException, "", false, true);
                    LogManager.WriteFileLog(LogLevel.CriticalErrors, true, NewRow);
                    LogManager.WriteDBLog(LogLevel.CriticalErrors, NewRow);
                }
                catch
                {
                }
                _isInError = true;
                _ReturnValue = new ManageUSPReturnValue("MD-ER-9999", amazonS3ClientException.Message + " Error Stack: " + amazonS3ClientException.InnerException, true);
            }
        }

        #endregion

        #region Methods

        public void Put(string MediaToMove, string BucketName)
        {
            _isInError = false;

            try
            {
                PutObjectRequest request = new PutObjectRequest();
                request.WithFilePath(HttpContext.Current.Server.MapPath(MediaToMove)).WithTimeout(360000000);
                request.WithBucketName(BucketName);
                request.WithKey(MediaToMove.Substring(1));
                request.CannedACL = S3CannedACL.PublicRead;

                S3Response responseWithMetadata = s3Client.PutObject(request);
            }
            catch (Exception ex)
            {
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(),
                                    "MD-ER-9999", "Error Put file '" + MediaToMove + "' on S3 Storage '" + ex.Message + " Error Stack: " + ex.InnerException, "", false, true);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                    LogManager.WriteDBLog(LogLevel.Debug, NewRow);
                }
                catch
                {
                    _isInError = true;
                    _ReturnValue = new ManageUSPReturnValue("MD-ER-9999", ex.Message + " Error Stack: " + ex.InnerException, true);
                }

            }
        }

        public void Get(string MediaSavePath, string BucketName, string MediaToGet)
        {
            _isInError = false;

            try
            {
                GetObjectRequest request = new GetObjectRequest();
                request.BucketName = BucketName;
                request.Key = MediaToGet.Substring(1);
                GetObjectResponse responce = s3Client.GetObject(request);
                responce.WriteResponseStreamToFile(HttpContext.Current.Server.MapPath(MediaSavePath));
            }
            catch (Exception ex)
            {
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(),
                                    "MD-ER-9999", "Error Get file '" + MediaToGet + "' from S3 Storage '" + ex.Message + " Error Stack: " + ex.InnerException, "", false, true);
                    LogManager.WriteFileLog(LogLevel.Errors, false, NewRow);
                    LogManager.WriteDBLog(LogLevel.Debug, NewRow);
                }
                catch
                {
                    _isInError = true;
                    _ReturnValue = new ManageUSPReturnValue("MD-ER-9999", ex.Message + " Error Stack: " + ex.InnerException, true);
                }
            }
        }
        public void Delete(string MediaToDelete, string BucketName)
        {
            _isInError = false;

            try
            {
                DeleteObjectRequest request = new DeleteObjectRequest();
                request.BucketName = BucketName;
                request.Key = MediaToDelete;
                DeleteObjectResponse responce = s3Client.DeleteObject(request);

            }
            catch (Exception ex)
            {
                try
                {
                    LogRow NewRow = new LogRow(DateTime.UtcNow, LogLevel.CriticalErrors.ToString(), "", Network.GetCurrentPageName(),
                                    "MD-ER-9999", "Error Delete file '" + MediaToDelete + "' from S3 Storage '" + ex.Message + " Error Stack: " + ex.InnerException, "", false, true);
                    LogManager.WriteFileLog(LogLevel.Debug, false, NewRow);
                    LogManager.WriteDBLog(LogLevel.Debug, NewRow);
                }
                catch
                {
                    _isInError = true;
                    _ReturnValue = new ManageUSPReturnValue("MD-ER-9999", ex.Message + " Error Stack: " + ex.InnerException, true);
                }

            }
        }
        #endregion
    }

    public enum MediaSizeTypes : int
    {
        OriginalSize = 0,
        OriginalResized = 1,
        Small = 2,
        MediaCroppedSize = 3
        //Defined but not use for now, add info on DBMedia to use these types
        //Medium = 4,
        //Large = 5
    }

    public static class MIMEAssistant
    {
        private static readonly Dictionary<string, string> MIMETypesDictionary = new Dictionary<string, string>
          {
            {"ai", "application/postscript"},
            {"aif", "audio/x-aiff"},
            {"aifc", "audio/x-aiff"},
            {"aiff", "audio/x-aiff"},
            {"asc", "text/plain"},
            {"atom", "application/atom+xml"},
            {"au", "audio/basic"},
            {"avi", "video/x-msvideo"},
            {"bcpio", "application/x-bcpio"},
            {"bin", "application/octet-stream"},
            {"bmp", "image/bmp"},
            {"cdf", "application/x-netcdf"},
            {"cgm", "image/cgm"},
            {"class", "application/octet-stream"},
            {"cpio", "application/x-cpio"},
            {"cpt", "application/mac-compactpro"},
            {"csh", "application/x-csh"},
            {"css", "text/css"},
            {"dcr", "application/x-director"},
            {"dif", "video/x-dv"},
            {"dir", "application/x-director"},
            {"djv", "image/vnd.djvu"},
            {"djvu", "image/vnd.djvu"},
            {"dll", "application/octet-stream"},
            {"dmg", "application/octet-stream"},
            {"dms", "application/octet-stream"},
            {"doc", "application/msword"},
            {"docx","application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
            {"dotx", "application/vnd.openxmlformats-officedocument.wordprocessingml.template"},
            {"docm","application/vnd.ms-word.document.macroEnabled.12"},
            {"dotm","application/vnd.ms-word.template.macroEnabled.12"},
            {"dtd", "application/xml-dtd"},
            {"dv", "video/x-dv"},
            {"dvi", "application/x-dvi"},
            {"dxr", "application/x-director"},
            {"eps", "application/postscript"},
            {"etx", "text/x-setext"},
            {"exe", "application/octet-stream"},
            {"ez", "application/andrew-inset"},
            {"gif", "image/gif"},
            {"gram", "application/srgs"},
            {"grxml", "application/srgs+xml"},
            {"gtar", "application/x-gtar"},
            {"hdf", "application/x-hdf"},
            {"hqx", "application/mac-binhex40"},
            {"htm", "text/html"},
            {"html", "text/html"},
            {"ice", "x-conference/x-cooltalk"},
            {"ico", "image/x-icon"},
            {"ics", "text/calendar"},
            {"ief", "image/ief"},
            {"ifb", "text/calendar"},
            {"iges", "model/iges"},
            {"igs", "model/iges"},
            {"jnlp", "application/x-java-jnlp-file"},
            {"jp2", "image/jp2"},
            {"jpe", "image/jpeg"},
            {"jpeg", "image/jpeg"},
            {"jpg", "image/jpeg"},
            {"js", "application/x-javascript"},
            {"kar", "audio/midi"},
            {"latex", "application/x-latex"},
            {"lha", "application/octet-stream"},
            {"lzh", "application/octet-stream"},
            {"m3u", "audio/x-mpegurl"},
            {"m4a", "audio/mp4a-latm"},
            {"m4b", "audio/mp4a-latm"},
            {"m4p", "audio/mp4a-latm"},
            {"m4u", "video/vnd.mpegurl"},
            {"m4v", "video/x-m4v"},
            {"mac", "image/x-macpaint"},
            {"man", "application/x-troff-man"},
            {"mathml", "application/mathml+xml"},
            {"me", "application/x-troff-me"},
            {"mesh", "model/mesh"},
            {"mid", "audio/midi"},
            {"midi", "audio/midi"},
            {"mif", "application/vnd.mif"},
            {"mov", "video/quicktime"},
            {"movie", "video/x-sgi-movie"},
            {"mp2", "audio/mpeg"},
            {"mp3", "audio/mpeg"},
            {"mp4", "video/mp4"},
            {"mpe", "video/mpeg"},
            {"mpeg", "video/mpeg"},
            {"mpg", "video/mpeg"},
            {"mpga", "audio/mpeg"},
            {"ms", "application/x-troff-ms"},
            {"msh", "model/mesh"},
            {"mxu", "video/vnd.mpegurl"},
            {"nc", "application/x-netcdf"},
            {"oda", "application/oda"},
            {"ogg", "application/ogg"},
            {"pbm", "image/x-portable-bitmap"},
            {"pct", "image/pict"},
            {"pdb", "chemical/x-pdb"},
            {"pdf", "application/pdf"},
            {"pgm", "image/x-portable-graymap"},
            {"pgn", "application/x-chess-pgn"},
            {"pic", "image/pict"},
            {"pict", "image/pict"},
            {"png", "image/png"}, 
            {"pnm", "image/x-portable-anymap"},
            {"pnt", "image/x-macpaint"},
            {"pntg", "image/x-macpaint"},
            {"ppm", "image/x-portable-pixmap"},
            {"ppt", "application/vnd.ms-powerpoint"},
            {"pptx","application/vnd.openxmlformats-officedocument.presentationml.presentation"},
            {"potx","application/vnd.openxmlformats-officedocument.presentationml.template"},
            {"ppsx","application/vnd.openxmlformats-officedocument.presentationml.slideshow"},
            {"ppam","application/vnd.ms-powerpoint.addin.macroEnabled.12"},
            {"pptm","application/vnd.ms-powerpoint.presentation.macroEnabled.12"},
            {"potm","application/vnd.ms-powerpoint.template.macroEnabled.12"},
            {"ppsm","application/vnd.ms-powerpoint.slideshow.macroEnabled.12"},
            {"ps", "application/postscript"},
            {"qt", "video/quicktime"},
            {"qti", "image/x-quicktime"},
            {"qtif", "image/x-quicktime"},
            {"ra", "audio/x-pn-realaudio"},
            {"ram", "audio/x-pn-realaudio"},
            {"ras", "image/x-cmu-raster"},
            {"rdf", "application/rdf+xml"},
            {"rgb", "image/x-rgb"},
            {"rm", "application/vnd.rn-realmedia"},
            {"roff", "application/x-troff"},
            {"rtf", "text/rtf"},
            {"rtx", "text/richtext"},
            {"sgm", "text/sgml"},
            {"sgml", "text/sgml"},
            {"sh", "application/x-sh"},
            {"shar", "application/x-shar"},
            {"silo", "model/mesh"},
            {"sit", "application/x-stuffit"},
            {"skd", "application/x-koan"},
            {"skm", "application/x-koan"},
            {"skp", "application/x-koan"},
            {"skt", "application/x-koan"},
            {"smi", "application/smil"},
            {"smil", "application/smil"},
            {"snd", "audio/basic"},
            {"so", "application/octet-stream"},
            {"spl", "application/x-futuresplash"},
            {"src", "application/x-wais-source"},
            {"sv4cpio", "application/x-sv4cpio"},
            {"sv4crc", "application/x-sv4crc"},
            {"svg", "image/svg+xml"},
            {"swf", "application/x-shockwave-flash"},
            {"t", "application/x-troff"},
            {"tar", "application/x-tar"},
            {"tcl", "application/x-tcl"},
            {"tex", "application/x-tex"},
            {"texi", "application/x-texinfo"},
            {"texinfo", "application/x-texinfo"},
            {"tif", "image/tiff"},
            {"tiff", "image/tiff"},
            {"tr", "application/x-troff"},
            {"tsv", "text/tab-separated-values"},
            {"txt", "text/plain"},
            {"ustar", "application/x-ustar"},
            {"vcd", "application/x-cdlink"},
            {"vrml", "model/vrml"},
            {"vxml", "application/voicexml+xml"},
            {"wav", "audio/x-wav"},
            {"wbmp", "image/vnd.wap.wbmp"},
            {"wbmxl", "application/vnd.wap.wbxml"},
            {"wml", "text/vnd.wap.wml"},
            {"wmlc", "application/vnd.wap.wmlc"},
            {"wmls", "text/vnd.wap.wmlscript"},
            {"wmlsc", "application/vnd.wap.wmlscriptc"},
            {"wrl", "model/vrml"},
            {"xbm", "image/x-xbitmap"},
            {"xht", "application/xhtml+xml"},
            {"xhtml", "application/xhtml+xml"},
            {"xls", "application/vnd.ms-excel"},                        
            {"xml", "application/xml"},
            {"xpm", "image/x-xpixmap"},
            {"xsl", "application/xml"},
            {"xlsx","application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
            {"xltx","application/vnd.openxmlformats-officedocument.spreadsheetml.template"},
            {"xlsm","application/vnd.ms-excel.sheet.macroEnabled.12"},
            {"xltm","application/vnd.ms-excel.template.macroEnabled.12"},
            {"xlam","application/vnd.ms-excel.addin.macroEnabled.12"},
            {"xlsb","application/vnd.ms-excel.sheet.binary.macroEnabled.12"},
            {"xslt", "application/xslt+xml"},
            {"xul", "application/vnd.mozilla.xul+xml"},
            {"xwd", "image/x-xwindowdump"},
            {"xyz", "chemical/x-xyz"},
            {"zip", "application/zip"}
          };

        public static string GetMIMEType(string fileName)
        {
            if (MIMETypesDictionary.ContainsKey(Path.GetExtension(fileName).ToLower().Remove(0, 1)))
            {
                return MIMETypesDictionary[Path.GetExtension(fileName).ToLower().Remove(0, 1)];
            }
            return "unknown/unknown";
        }
    }

   
}
