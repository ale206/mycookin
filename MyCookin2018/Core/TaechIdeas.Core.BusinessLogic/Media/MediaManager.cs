using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Amazon;
using Amazon.S3;
using AutoMapper;
using TaechIdeas.Core.Core.Audit;
using TaechIdeas.Core.Core.Audit.Dto;
using TaechIdeas.Core.Core.Common;
using TaechIdeas.Core.Core.Common.Enums;
using TaechIdeas.Core.Core.Configuration;
using TaechIdeas.Core.Core.LogAndMessage;
using TaechIdeas.Core.Core.LogAndMessage.Dto;
using TaechIdeas.Core.Core.Media;
using TaechIdeas.Core.Core.Media.Dto;
using TaechIdeas.Core.Core.Network;
using TaechIdeas.Core.Core.Token;
using PutObjectRequest = Amazon.S3.Model.PutObjectRequest;

namespace TaechIdeas.Core.BusinessLogic.Media
{
    public class MediaManager : IMediaManager
    {
        private readonly ILogManager _logManager;
        private readonly INetworkManager _networkManager;
        private readonly IMediaConfig _mediaConfig;
        private readonly INetworkConfig _networkConfig;
        private readonly IUtilsManager _utilsManager;
        private readonly IMediaRepository _mediaRepository;
        private readonly IMySecurityManager _mySecurityManager;
        private readonly IAuditManager _auditManager;
        private readonly ITokenManager _tokenManager;
        private readonly IMapper _mapper;

        public MediaManager(ILogManager logManager, INetworkManager networkManager,
            IMediaConfig mediaConfig, INetworkConfig networkConfig, IMediaRepository mediaRepository, IUtilsManager utilsManager, IMySecurityManager mySecurityManager,
            IAuditManager auditManager, ITokenManager tokenManager, IMapper mapper)
        {
            _logManager = logManager;
            _networkManager = networkManager;
            _mediaConfig = mediaConfig;
            _networkConfig = networkConfig;
            _mediaRepository = mediaRepository;
            _utilsManager = utilsManager;
            _mySecurityManager = mySecurityManager;
            _auditManager = auditManager;
            _tokenManager = tokenManager;
            _mapper = mapper;
        }

        #region ImageToByteArray

        public byte[] ImageToByteArray(Image imageIn)
        {
            //TODO: Insert try and catch

            var ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Gif);
            return ms.ToArray();
        }

        #endregion

        #region ByteArrayToImage

        public Image ByteArrayToImage(byte[] byteArrayIn)
        {
            //TODO: Inserrt try and catch

            var ms = new MemoryStream(byteArrayIn);
            var returnImage = Image.FromStream(ms);
            return returnImage;
        }

        #endregion

        #region MediaUploadConfigParameters

        public MediaUploadConfigParametersOutput MediaUploadConfigParameters(MediaUploadConfigParametersInput mediaUploadConfigParametersInput)
        {
            try
            {
                return _mapper.Map<MediaUploadConfigParametersOutput>(_mediaRepository.MediaUploadConfigParameters(_mapper.Map<MediaUploadConfigParametersIn>(mediaUploadConfigParametersInput)));
            }
            catch (Exception ex)
            {
                //ERROR LOG
                var logRow = new LogRowIn
                {
                    ErrorMessage = $"Error in MediaUploadConfigParameters: {ex.Message}. Object: {_utilsManager.GetAllPropertiesAndValues(mediaUploadConfigParametersInput)}",
                    ErrorMessageCode = "MD-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = _networkManager.GetCurrentPageName()
                };

                _logManager.WriteLog(logRow);

                return null;
            }
        }

        #endregion

        #region NewMedia

        public NewMediaOutput NewMedia(NewMediaInput newMediaInput)
        {
            //Do not move original photo on CDN here

            try
            {
                //Check for Valid Token
                var checkTokenOutput = _tokenManager.CheckToken(newMediaInput.CheckTokenInput);

                if (!checkTokenOutput.IsTokenValid)
                {
                    throw new Exception("Token not valid for the user.");
                }

                var mediaUploadConfigParameters = MediaUploadConfigParameters(_mapper.Map<MediaUploadConfigParametersInput>(newMediaInput));

                var maxSize = mediaUploadConfigParameters.MaxSizeByte;
                if (newMediaInput.ImageBytes.Length > maxSize)
                {
                    throw new Exception($"File too big. Max size is: {maxSize / 1024 / 1024} Mb");
                }

                //Write file
                var originalFilePathOnServer = ControlsOnFilename(Path.Combine(mediaUploadConfigParameters.UploadOriginalFilePath, newMediaInput.ObjectName));
                File.WriteAllBytes(originalFilePathOnServer, newMediaInput.ImageBytes);

                //CropPicture
                var cropPictureInput = _mapper.Map<CropPictureInput>(newMediaInput.CropPictureInput);
                cropPictureInput.OriginalFilePathAndFileName = originalFilePathOnServer;
                cropPictureInput.FilePathForCropped = mediaUploadConfigParameters.UploadPath;
                var cropPictureOutput = CropPicture(cropPictureInput);

                //Resize
                var resizeAndCompressInput = new ResizeAndCompressInput
                {
                    FilePath = cropPictureOutput.PictureCroppedFilePath,
                    Quality = _mediaConfig.DefaultImageQuality,
                    NewWidth = mediaUploadConfigParameters.MediaFinalWidth,
                    NewHeigth = mediaUploadConfigParameters.MediaFinalHeight,
                    ConvertToJpeg = true
                };

                var resizeAndCompressOutput = ResizeAndCompress(resizeAndCompressInput);

                //Md5
                var md5PathHashed = mediaUploadConfigParameters.ComputeMd5Hash ? _mySecurityManager.GenerateMd5FileHash(originalFilePathOnServer) : null;

                //Save on db
                var saveOrUpdateMediaInput = new SaveOrUpdateMediaInput
                {
                    MediaType = newMediaInput.MediaType.ToString(),
                    IsChecked = false,
                    MediaPath = originalFilePathOnServer,
                    IsUserMediaOwner = false, //TODO: What does it mean?
                    MediaServer = _networkConfig.CdnBasePath,
                    IsVideo = false, //Video not supported yet
                    IsImage = true, //Only images supported yet
                    IsLink = false, //Link not supported yet
                    MediaDeletedOn = null,
                    MediaMd5Hash = md5PathHashed,
                    CheckedByUser = null,
                    MediaBakcupServer = null,
                    MediaId = null, //Use null for new media
                    OwnerId = newMediaInput.OwnerId,
                    IsMediaDisabled = false,
                    IsAnExternalSource = false,
                    IsMediaOnCdn = false
                };

                var saveOrUpdateMediaOutput = SaveOrUpdateMedia(saveOrUpdateMediaInput);

                if (saveOrUpdateMediaOutput == null || saveOrUpdateMediaOutput.MediaId.Equals(new Guid()))
                {
                    throw new Exception("Media Not Saved. Error on SaveOrUpdateMedia");
                }

                //Create alternative sizes
                var addAlternativePhotoSizeInput = new AddAlternativePhotoSizeInput
                {
                    MediaType = newMediaInput.MediaType,
                    PictureCroppedFilePath = cropPictureOutput.PictureCroppedFilePath,
                    MediaId = saveOrUpdateMediaOutput.MediaId
                };

                AddAlternativePhotoSize(addAlternativePhotoSizeInput);

                //Add Audit Event
                var autoAuditConfigInfoInput = new AutoAuditConfigInfoInput {ObjectType = ObjectType.Photo};
                var autoAuditConfigInfoOutput = _auditManager.AutoAuditConfigInfo(autoAuditConfigInfoInput);

                if (autoAuditConfigInfoOutput.EnableAutoAudit)
                {
                    var newAuditEventInput = new NewAuditEventInput
                    {
                        ObjectType = ObjectType.Photo,
                        ObjectId = saveOrUpdateMediaOutput.MediaId,
                        AuditEventMessage = "New photo Inserted",
                        AuditEventLevel = autoAuditConfigInfoOutput.AuditEventLevel,
                        ObjectTxtInfo = originalFilePathOnServer
                    };

                    _auditManager.NewAuditEvent(newAuditEventInput);
                }

                return _mapper.Map<NewMediaOutput>(saveOrUpdateMediaOutput);
            }
            catch (Exception ex)
            {
                //ERROR LOG
                var logRow = new LogRowIn
                {
                    ErrorMessage = $"Error in NewMedia: {ex.Message}. Object: {_utilsManager.GetAllPropertiesAndValues(newMediaInput)}",
                    ErrorMessageCode = "MD-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = _networkManager.GetCurrentPageName()
                };

                _logManager.WriteLog(logRow);

                return null;
            }
        }

        #endregion

        #region SaveOrUpdateMedia

        /// <summary>
        ///     Save or Update Media. Use MediaId as NULL to save.
        /// </summary>
        /// <param name="saveOrUpdateMediaInput"></param>
        /// <returns></returns>
        public SaveOrUpdateMediaOutput SaveOrUpdateMedia(SaveOrUpdateMediaInput saveOrUpdateMediaInput)
        {
            try
            {
                return _mapper.Map<SaveOrUpdateMediaOutput>(_mediaRepository.SaveOrUpdateMedia(_mapper.Map<SaveOrUpdateMediaIn>(saveOrUpdateMediaInput)));
            }
            catch (Exception ex)
            {
                //ERROR LOG
                var logRow = new LogRowIn
                {
                    ErrorMessage = $"Error in SaveOrUpdateMedia: {ex.Message}. Object: {_utilsManager.GetAllPropertiesAndValues(saveOrUpdateMediaInput)}",
                    ErrorMessageCode = "MD-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = _networkManager.GetCurrentPageName()
                };

                _logManager.WriteLog(logRow);

                return null;
            }
        }

        #endregion

        #region ControlsOnFilename

        /// <summary>
        ///     Check name and if the file already exists
        /// </summary>
        /// <param name="path">Path with filename where do you want to store the file</param>
        /// <returns>New Path to use</returns>
        public string ControlsOnFilename(string path)
        {
            var fi = new FileInfo(path);

            var fileName = fi.Name.Replace(' ', '-');

            var directoryName = Path.GetDirectoryName(path);

            if (directoryName == null)
            {
                throw new Exception("Path Null!");
            }

            path = Path.Combine(directoryName, fileName);

            var allowedExtensions = _mediaConfig.AllowedExtensions.Split(',');

            if (!allowedExtensions.Contains(fi.Extension))
            {
                throw new Exception("Wrong Extension on FileName");
            }

            var filenameWithoutExtension = Path.GetFileNameWithoutExtension(path);

            while (File.Exists(path))
            {
                var rnd = new Random();

                var newFileName = filenameWithoutExtension + "-" + rnd.Next(1, 99999) + fi.Extension;
                path = Path.Combine(directoryName, newFileName);
            }

            return path;
        }

        #endregion

        #region MediaById

        /// <summary>
        ///     Get Information about media object form database
        /// </summary>
        /// <param name="mediaByIdInput"></param>
        public MediaByIdOutput MediaById(MediaByIdInput mediaByIdInput)
        {
            try
            {
                return _mapper.Map<MediaByIdOutput>(_mediaRepository.MediaById(_mapper.Map<MediaByIdIn>(mediaByIdInput)));
            }
            catch (Exception ex)
            {
                //ERROR LOG
                var logRow = new LogRowIn
                {
                    ErrorMessage = $"Error in MediaById: {ex.Message}. Object: {_utilsManager.GetAllPropertiesAndValues(mediaByIdInput)}",
                    ErrorMessageCode = "MD-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = _networkManager.GetCurrentPageName()
                };

                _logManager.WriteLog(logRow);

                //True in case of error
                return null;
            }
        }

        #endregion

        #region AddAlternativePhotoSize

        public AddAlternativePhotoSizeOutput AddAlternativePhotoSize(AddAlternativePhotoSizeInput addAlternativePhotoSizeInput)
        {
            var alternativePhotoSizeOutput = new AddAlternativePhotoSizeOutput();

            var mediaAlternativeSizeConfigParameters =
                MediaAlternativeSizeConfigParameters(_mapper.Map<MediaAlternativeSizeConfigParametersInput>(addAlternativePhotoSizeInput))
                    .Where(x => x.MediaSizeType != MediaSizeTypes.OriginalSize.ToString());

            foreach (var mediaAlternativeSizeConfigParameter in mediaAlternativeSizeConfigParameters)
            {
                //Get original size
                Image bmpOriginal = new Bitmap(addAlternativePhotoSizeInput.PictureCroppedFilePath);
                var originalImageHeight = bmpOriginal.Height;
                var originalImageWidth = bmpOriginal.Width;
                bmpOriginal.Dispose();

                //Create new FilePath
                var fi = new FileInfo(addAlternativePhotoSizeInput.PictureCroppedFilePath);
                var fileName = fi.Name;
                var filePathForNewFile = ControlsOnFilename(Path.Combine(mediaAlternativeSizeConfigParameter.SavePath, fileName));

                //Calculate New Photo Size
                var newPhotoSize = CalculatePhotoSize(originalImageWidth, originalImageHeight, mediaAlternativeSizeConfigParameter.MediaWidth, mediaAlternativeSizeConfigParameter.MediaHeight, 0);

                //Resize
                var resizeResult = ResizeAndSave(addAlternativePhotoSizeInput.PictureCroppedFilePath, filePathForNewFile, newPhotoSize.Width, newPhotoSize.Height, _mediaConfig.DefaultImageQuality);

                //Md5Hash
                var md5Hash = _mySecurityManager.GenerateMd5FileHash(resizeResult);

                //MovePhotoOnCdn
                var uploadPhotoOnCdnOutput = UploadPhotoOnCdn(new UploadPhotoOnCdnInput {FilePath = filePathForNewFile});

                //Save on AddAlternativePhotoSizeIn Table
                var addAlternativePhotoSizeIn = new AddAlternativePhotoSizeIn
                {
                    MediaId = addAlternativePhotoSizeInput.MediaId,
                    MediaMd5Hash = md5Hash,
                    MediaOnCdn = uploadPhotoOnCdnOutput.PhotoUploaded,
                    MediaBackupServer = null,
                    MediaPath = filePathForNewFile,
                    MediaServer = _networkConfig.CdnBasePath,
                    MediaSizeType = mediaAlternativeSizeConfigParameter.MediaSizeType
                };

                var result = _mediaRepository.AddAlternativePhotoSize(addAlternativePhotoSizeIn);

                alternativePhotoSizeOutput.AlternativePhotoSizeAdded = true;
            }

            return alternativePhotoSizeOutput;
        }

        #endregion

        #region CropPicture

        /// <summary>
        ///     CropPicture and overwrite the original photo file
        /// </summary>
        /// <param name="cropPictureInput"></param>
        /// <returns></returns>
        public CropPictureOutput CropPicture(CropPictureInput cropPictureInput)
        {
            try
            {
                var x = cropPictureInput.StartX;
                var y = cropPictureInput.StartY;
                var width = cropPictureInput.ImgWidth;
                var height = cropPictureInput.ImgHeight;

                //Copy file to be cropped
                var filePathForCroppedOnServer = Path.Combine(cropPictureInput.FilePathForCropped, Path.GetFileName(cropPictureInput.OriginalFilePathAndFileName));

                var destinationFilePath = ControlsOnFilename(filePathForCroppedOnServer);
                File.Copy(cropPictureInput.OriginalFilePathAndFileName, destinationFilePath);

                using (var photo = Image.FromFile(filePathForCroppedOnServer))

                using (var result = new Bitmap(width, height, photo.PixelFormat))
                {
                    result.SetResolution(photo.HorizontalResolution, photo.VerticalResolution);

                    using (var g = Graphics.FromImage(result))
                    {
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.DrawImage(photo,
                            new Rectangle(0, 0, width, height),
                            new Rectangle(x, y, width, height),
                            GraphicsUnit.Pixel);
                        photo.Dispose();
                        result.Save(filePathForCroppedOnServer);
                        result.Dispose();
                        g.Dispose();
                    }
                }

                return new CropPictureOutput {PictureCropped = true, PictureCroppedFilePath = filePathForCroppedOnServer};
            }
            catch (Exception ex)
            {
                //ERROR LOG
                var logRow = new LogRowIn
                {
                    ErrorMessage = $"Error in NewMedia: {ex.Message}. Object: {_utilsManager.GetAllPropertiesAndValues(cropPictureInput)}",
                    ErrorMessageCode = "MD-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = _networkManager.GetCurrentPageName()
                };

                _logManager.WriteLog(logRow);

                return null;
            }
        }

        #endregion

        #region ResizeAndCompress

        /// <summary>
        ///     Resize and compress with overwrite of the original photo file
        ///     If image will be converted to jpeg a new file will be created
        /// </summary>
        /// <param name="resizeAndCompressInput"></param>
        /// <returns>true if operation succeed</returns>
        public ResizeAndCompressOutput ResizeAndCompress(ResizeAndCompressInput resizeAndCompressInput)
        {
            if (resizeAndCompressInput.Quality < 1 || resizeAndCompressInput.Quality > 100)
            {
                resizeAndCompressInput.Quality = 50;
            }

            try
            {
                var qualityParameter = new EncoderParameter(Encoder.Quality, resizeAndCompressInput.Quality);

                var mimeType = resizeAndCompressInput.ConvertToJpeg ? "image/jpeg" : MimeAssistant.GetMimeType(resizeAndCompressInput.FilePath);

                if (mimeType == "unknown/unknown") return new ResizeAndCompressOutput {ResizeAndCompressCompleted = false};

                var imgEncoder = ImageCodecInfo(mimeType);
                var encoderParams = new EncoderParameters(1) {Param = {[0] = qualityParameter}};

                using (var photo = Image.FromFile(resizeAndCompressInput.FilePath))
                using (var result =
                    new Bitmap(resizeAndCompressInput.NewWidth, resizeAndCompressInput.NewHeigth, photo.PixelFormat))
                {
                    result.SetResolution(
                        photo.HorizontalResolution,
                        photo.VerticalResolution);

                    using (var g = Graphics.FromImage(result))
                    {
                        g.InterpolationMode =
                            InterpolationMode.HighQualityBicubic;
                        g.DrawImage(photo, 0, 0, resizeAndCompressInput.NewWidth, resizeAndCompressInput.NewHeigth);

                        photo.Dispose();
                        if (resizeAndCompressInput.ConvertToJpeg)
                        {
                            resizeAndCompressInput.FilePath = resizeAndCompressInput.FilePath.Replace(Path.GetExtension(resizeAndCompressInput.FilePath).ToLower(), ".jpg");
                        }

                        result.Save(resizeAndCompressInput.FilePath, imgEncoder, encoderParams);
                        result.Dispose();
                        g.Dispose();
                    }
                }

                return new ResizeAndCompressOutput {ResizeAndCompressCompleted = true};
            }
            catch (Exception ex)
            {
                var logRow = new LogRowIn
                {
                    ErrorMessage = $"Error resize image: {ex.Message}.  File Path: {resizeAndCompressInput.FilePath}.",
                    ErrorMessageCode = "MD-ER-0011",
                    ErrorSeverity = LogLevel.Warnings,
                    FileOrigin = _networkManager.GetCurrentPageName()
                };

                _logManager.WriteLog(logRow);

                return new ResizeAndCompressOutput {ResizeAndCompressCompleted = false};
            }
        }

        #endregion

        #region MediaAlternativeSizeConfigParameters

        public IEnumerable<MediaAlternativeSizeConfigParametersOutput> MediaAlternativeSizeConfigParameters(MediaAlternativeSizeConfigParametersInput mediaUploadConfigParametersInput)
        {
            try
            {
                return
                    _mapper.Map<IEnumerable<MediaAlternativeSizeConfigParametersOutput>>(
                        _mediaRepository.MediaAlternativeSizeConfigParameters(_mapper.Map<MediaAlternativeSizeConfigParametersIn>(mediaUploadConfigParametersInput)));
            }
            catch (Exception ex)
            {
                //ERROR LOG
                var logRow = new LogRowIn
                {
                    ErrorMessage = $"Error in MediaAlternativeSizeConfigParameters: {ex.Message}. Object: {_utilsManager.GetAllPropertiesAndValues(mediaUploadConfigParametersInput)}",
                    ErrorMessageCode = "MD-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = _networkManager.GetCurrentPageName()
                };

                _logManager.WriteLog(logRow);

                return null;
            }
        }

        #endregion

        #region CalculatePhotoSize

        public PhotoSize CalculatePhotoSize(int imgWidth, int imgHeight, int imgFinalWidth, int imgFinalHeight, int percPlusSizeForCrop)
        {
            var photoSize = new PhotoSize();

            if (imgFinalHeight == 0)
            {
                imgFinalHeight = imgFinalWidth;
            }
            else if (imgFinalWidth == 0)
            {
                imgFinalWidth = imgFinalHeight;
            }

            if (imgWidth > imgHeight)
            {
                photoSize.MaxSizeCalc = imgFinalHeight + imgFinalHeight / 100 * percPlusSizeForCrop;

                photoSize.PerRatio = Convert.ToDouble(imgHeight) / photoSize.MaxSizeCalc;
                photoSize.Width = Convert.ToInt32(imgWidth / photoSize.PerRatio);
                photoSize.Height = Convert.ToInt32(photoSize.MaxSizeCalc);
            }
            else
            {
                photoSize.MaxSizeCalc = imgFinalWidth + imgFinalWidth / 100 * percPlusSizeForCrop;

                photoSize.PerRatio = Convert.ToDouble(imgWidth) / photoSize.MaxSizeCalc;
                photoSize.Height = Convert.ToInt32(imgHeight / photoSize.PerRatio);
                photoSize.Width = Convert.ToInt32(photoSize.MaxSizeCalc);
            }

            return photoSize;
        }

        #endregion

        #region ResizeAndSave

        /// <summary>
        ///     Resize a photo and save it in a new path
        /// </summary>
        /// <param name="stgOriginalPath">Old image path</param>
        /// <param name="stgNewPath">New image path</param>
        /// <param name="newWidth">New image width</param>
        /// <param name="newHeight">New image height</param>
        /// <param name="quality"></param>
        /// <returns>true if operation succeed</returns>
        public string ResizeAndSave(string stgOriginalPath, string stgNewPath, int newWidth, int newHeight, int quality)
        {
            var _return = "";

            if (quality < 1 || quality > 100)
            {
                quality = 50;
            }

            var qualityParameter = new EncoderParameter(Encoder.Quality, quality);
            var mimeType = "";

            mimeType = "image/jpeg";

            try
            {
                var imgEncoder = ImageCodecInfo(mimeType);
                var encoderParams = new EncoderParameters(1) {Param = {[0] = qualityParameter}};

                Image bmpOriginal = new Bitmap(stgOriginalPath);

                Image bmpResized = new Bitmap(newWidth, newHeight);

                var grpImage = Graphics.FromImage(bmpResized);

                grpImage.InterpolationMode = InterpolationMode.HighQualityBicubic;

                grpImage.DrawImage(bmpOriginal, 0, 0, newWidth, newHeight);

                stgNewPath = stgNewPath.Replace(Path.GetExtension(stgNewPath).ToLower(), ".jpg");

                grpImage.Dispose();

                bmpResized.Save(stgNewPath, imgEncoder, encoderParams);

                bmpOriginal.Dispose();
                bmpResized.Dispose();

                _return = stgNewPath;
            }
            catch (Exception ex)
            {
                var logRow = new LogRowIn
                {
                    ErrorMessage = $"Error resizing image: {ex.Message}.  Original Path: {stgOriginalPath}. New Path: {stgNewPath}",
                    ErrorMessageCode = "MD-ER-0011",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = _networkManager.GetCurrentPageName()
                    //IdUser = "",
                };

                _logManager.WriteLog(logRow);

                _return = "";
            }

            return _return;
        }

        #endregion

        #region UploadPhotoOnCdn

        public UploadPhotoOnCdnOutput UploadPhotoOnCdn(UploadPhotoOnCdnInput uploadPhotoOnCdnInput)
        {
            //If disabled from web.config, return.
            if (!_mediaConfig.UploadOnCdn)
            {
                return new UploadPhotoOnCdnOutput {PhotoUploaded = false};
            }

            try
            {
                var s3Client = new AmazonS3Client(_mediaConfig.AwsAccessKey, _mediaConfig.AwsSecretKey, RegionEndpoint.USWest2);

                var request = new PutObjectRequest();
                request.FilePath = uploadPhotoOnCdnInput.FilePath;
                // request.Timeout = new TimeSpan(360000000);
                request.Key = uploadPhotoOnCdnInput.FilePath.Substring(3).Replace("\\", "/"); //MUST BE LIKE: Photo/Test/logoo-97398.jpg
                request.BucketName = _mediaConfig.BucketName;

                request.CannedACL = S3CannedACL.PublicRead;
                request.Headers["Expires"] = DateTime.UtcNow.AddYears(30).ToUniversalTime().ToString("ddd, dd MMM yyyy HH:mm:ss 'GMT'");

                var responseWithMetadata = s3Client.PutObjectAsync(request).Result;
            }
            catch (AmazonS3Exception amazonS3ClientException)
            {
                var logRow = new LogRowIn
                {
                    ErrorMessage = $"Error connection S3 Storage: {amazonS3ClientException.Message}. Error Stack: {amazonS3ClientException.InnerException}.",
                    ErrorMessageCode = "MD-ER-9999",
                    ErrorSeverity = LogLevel.Warnings,
                    FileOrigin = _networkManager.GetCurrentPageName()
                };

                _logManager.WriteLog(logRow);

                return new UploadPhotoOnCdnOutput {PhotoUploaded = false};
            }
            catch (Exception ex)
            {
                //ERROR LOG
                var logRow = new LogRowIn
                {
                    ErrorMessage = $"Error in UploadPhotoOnCdn: {ex.Message}. Object: {_utilsManager.GetAllPropertiesAndValues(uploadPhotoOnCdnInput)}",
                    ErrorMessageCode = "MD-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = _networkManager.GetCurrentPageName()
                };

                _logManager.WriteLog(logRow);

                return new UploadPhotoOnCdnOutput {PhotoUploaded = false};
            }

            return new UploadPhotoOnCdnOutput {PhotoUploaded = true};
        }

        #endregion

        #region DisableMedia

        public DisableMediaOutput DisableMedia(DisableMediaInput disableMediaInput)
        {
            try
            {
                //Check for Valid Token
                var checkTokenOutput = _tokenManager.CheckToken(disableMediaInput.CheckTokenInput);

                if (!checkTokenOutput.IsTokenValid)
                {
                    throw new Exception("Token not valid for the user.");
                }

                return _mapper.Map<DisableMediaOutput>(_mediaRepository.DisableMedia(_mapper.Map<DisableMediaIn>(disableMediaInput)));
            }
            catch (Exception ex)
            {
                //ERROR LOG
                var logRow = new LogRowIn
                {
                    ErrorMessage = $"Error in DisableMedia: {ex.Message}. Object: {_utilsManager.GetAllPropertiesAndValues(disableMediaInput)}",
                    ErrorMessageCode = "IN-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = _networkManager.GetCurrentPageName()
                };

                _logManager.WriteLog(logRow);

                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region GetMediaNotInCdn

        public IEnumerable<GetMediaNotInCdnOutput> GetMediaNotInCdn(GetMediaNotInCdnInput getMediaNotInCdnInput)
        {
            try
            {
                //Check for Valid Token
                var checkTokenOutput = _tokenManager.CheckToken(getMediaNotInCdnInput.CheckTokenInput);

                if (!checkTokenOutput.IsTokenValid)
                {
                    throw new Exception("Token not valid for the user.");
                }

                return _mapper.Map<IEnumerable<GetMediaNotInCdnOutput>>(_mediaRepository.GetMediaNotInCdn(_mapper.Map<GetMediaNotInCdnIn>(getMediaNotInCdnInput)));
            }
            catch (Exception ex)
            {
                //ERROR LOG
                var logRow = new LogRowIn
                {
                    ErrorMessage = $"Error in GetMediaNotInCdn: {ex.Message}. Object: {_utilsManager.GetAllPropertiesAndValues(getMediaNotInCdnInput)}",
                    ErrorMessageCode = "IN-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = _networkManager.GetCurrentPageName()
                };

                _logManager.WriteLog(logRow);

                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region DeleteMedia

        public DeleteMediaOutput DeleteMedia(DeleteMediaInput deleteMediaInput)
        {
            //Just call save or update and update Deletedon field ...
            throw new NotImplementedException();
        }

        #endregion

        #region MediaPathByMediaId

        public MediaPathByMediaIdOutput MediaPathByMediaId(MediaPathByMediaIdInput mediaPathByMediaIdInput)
        {
            try
            {
                var media = MediaById(new MediaByIdInput {MediaId = mediaPathByMediaIdInput.MediaId});

                Uri baseUri;
                string mediaImage;

                // TODO: Deactivated because not having cdn anymore
                //if (media == null)
                //{
                //baseUri = new Uri(_networkConfig.WebUrl); //TODO: In the future upload all the icons on CDN and use them from there
                //mediaImage = GetDefaultMediaPath(MediaType.NotSpecified);
                //}
                //else
                //{
                //    var mediaServer = (string.IsNullOrEmpty(media.MediaServer) && string.IsNullOrEmpty(media.MediaServer)) ? _networkConfig.CdnBasePath : media.MediaServer;
                //    mediaImage = string.IsNullOrEmpty(media.MediaPath) ? GetDefaultMediaPath(MediaType.NotSpecified) : media.MediaPath;
                //    baseUri = new Uri(mediaServer);
                //}

                // TODO: Remove this when re activate the cdn
                baseUri = new Uri("http://www.mycookin.com");
                mediaImage = string.IsNullOrEmpty(media?.MediaPath) ? GetDefaultMediaPath(MediaType.NotSpecified) : media.MediaPath;

                return new MediaPathByMediaIdOutput
                {
                    MediaPath = new Uri(baseUri, mediaImage).ToString()
                };
            }
            catch (Exception ex)
            {
                //ERROR LOG
                var logRow = new LogRowIn
                {
                    ErrorMessage = $"Error in GetImagePathByMediaId: {ex.Message}. MediaId: {mediaPathByMediaIdInput.MediaId}",
                    ErrorMessageCode = "MD-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = _networkManager.GetCurrentPageName()
                };

                _logManager.WriteLog(logRow);

                //Null in case of error
                return null;
            }
        }

        #endregion

        #region ImageCodecInfo

        /// <summary>
        ///     Returns the image codec with the given mime type
        /// </summary>
        public ImageCodecInfo ImageCodecInfo(string mimeType)
        {
            // Get image codecs for all image formats 
            var codecs = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec 
            for (var i = 0; i < codecs.Length; i++)
            {
                if (codecs[i].MimeType == mimeType)
                {
                    return codecs[i];
                }
            }

            return null;
        }

        public string GetDefaultMediaPath(MediaType mediaType)
        {
            switch (mediaType)
            {
                case MediaType.NotSpecified:
                    return _mediaConfig.RecipePhotoNoImagePath;
                case MediaType.RecipePhoto:
                    return _mediaConfig.RecipePhotoNoImagePath;
                case MediaType.ProfileImagePhoto:
                    return _mediaConfig.ProfilePhotoNoImagePath;
                default:
                    return _mediaConfig.RecipePhotoNoImagePath;
            }
        }

        #endregion
    }
}