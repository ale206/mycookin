using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;
using MyCookin.Common;
using MyCookin.ObjectManager.MediaManager;
using MyCookin.ErrorAndMessage;

namespace PlUploadTest
{
    /// <summary>
    /// Summary description for upload
    /// </summary>
    public class upload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            bool _continue = true;
            MediaType _mediaType = MediaType.NotSpecified;
            MediaUploadConfig _uploadConfig = null;
            string _returnCode="";
            Guid _idMediaOwner = new Guid();
            string MD5Hash = "";
            string _originalFileName = "";
            int chunk=0;
            string fileName="";

            try
            {
                chunk = context.Request["chunk"] != null ? int.Parse(context.Request["chunk"]) : 0;
                fileName = context.Request["name"] != null ? context.Request["name"] : string.Empty;
            }
            catch
            {
                _continue = false;
                _returnCode = "Error:" + _originalFileName + RetrieveMessage.RetrieveDBMessage(2, "MD-ER-0002");
                context.Response.ContentType = "text/plain";
                context.Response.Write(_returnCode);
            }
            try
            {
                try
                {
                    _mediaType = (MediaType)Enum.Parse(typeof(MediaType),context.Request.QueryString["UploadConfig"].ToString());
                    _uploadConfig = new MediaUploadConfig(_mediaType);
                    _idMediaOwner = new Guid(context.Request.QueryString["IDMediaOwner"].ToString());
                }
                catch
                {
                    _continue = false;
                }
                if (_continue && _idMediaOwner!= new Guid())
                {
                    try
                    {
                        string fileExt = fileName.Substring(fileName.LastIndexOf("."));
                        if (!String.IsNullOrEmpty(context.Request.QueryString["baseFileName"].ToString()))
                        {
                            fileName = context.Request.QueryString["baseFileName"].ToString() + fileExt;
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        fileName = fileName.Replace(" ", "_").Replace("'", "").Replace("\"", "");

                        if (MyConvert.ToInt32(AppConfig.GetValue("AddDateToFileName", AppDomain.CurrentDomain), 0) == 1)
                        {
                            fileName = DateTime.UtcNow.ToString(AppConfig.GetValue("DateFormatString", AppDomain.CurrentDomain)) + "_" + fileName;
                        }

                        HttpPostedFile fileUpload = context.Request.Files[0];
                        //Stream test = fileUpload.InputStream;
                        //Stream test2 = new MemoryStream();
                        //test.CopyTo(test2);
                        _originalFileName = fileUpload.FileName + " ";
                        

                        int _UploadFileCheck = Media.CheckIfUploadFile(fileUpload, _uploadConfig, _idMediaOwner.ToString());

                        if (_UploadFileCheck == 0)
                        {
                            int imageQuality = MyConvert.ToInt32(AppConfig.GetValue("DefaultImageQuality", AppDomain.CurrentDomain), 50);

                            var uploadPath = context.Server.MapPath(_uploadConfig.UploadOriginalFilePath);
                            fileUpload.SaveAs(Path.Combine(uploadPath, fileName));
                            Photo.CompressImage(Path.Combine(uploadPath, fileName), 100, true);
                            //using (var fs = new FileStream(Path.Combine(uploadPath, fileName), chunk == 0 ? FileMode.Create : FileMode.Append))
                            //{
                            //    //var buffer = new byte[fileUpload.InputStream.Length];
                            //    var buffer = new byte[test2.Length];
                            //    //fileUpload.InputStream.Read(buffer, 0, buffer.Length);
                            //    test2.Read(buffer, 0, buffer.Length);
                            //    fs.Write(buffer, 0, buffer.Length);
                            //    fs.Dispose();
                            //    fs.Close();
                            //}

                            //File.Copy(Path.Combine(uploadPath, fileName), context.Server.MapPath(_uploadConfig.UploadPath) + fileName);

                            Image _finalImage = new Bitmap(Path.Combine(uploadPath, fileName));
                            //CalculatePhotoSize

                            CalculatePhotoSize photoSize = new CalculatePhotoSize(_finalImage.Width, _finalImage.Height, _uploadConfig.MediaFinalWidth,
                                                                                   _uploadConfig.MediaFinalHeight, _uploadConfig.MediaPercPlusSizeForCrop);

                            _finalImage.Dispose();

                            string outputImage = Photo.ResizeAndSave(Path.Combine(uploadPath, fileName),
                                         context.Server.MapPath(_uploadConfig.UploadPath) + fileName, photoSize.Width, photoSize.Height, imageQuality);

                            Photo.Crop(outputImage,
                                          ((photoSize.Width-_uploadConfig.MediaFinalWidth)/2),
                                          ((photoSize.Height - _uploadConfig.MediaFinalHeight) / 2)
                                          , _uploadConfig.MediaFinalWidth, _uploadConfig.MediaFinalHeight);

                            if (_uploadConfig.ComputeMD5Hash)
                            {
                                //try
                                //{
                                //    MD5Hash = MySecurity.GenerateMD5FileHash(outputImage);
                                //}
                                //catch
                                //{
                                //}
                            }

                            Photo _Image;
                            _Image = new Photo(new Guid());
                            _Image.Checked = false;
                            _Image.isEsternalSource = false;
                            _Image.isImage = true;
                            _Image.isLink = false;
                            _Image.isVideo = false;
                            _Image.MediaDisabled = false;

                            _Image.MediaServer = "";
                            _Image.MediaBakcupServer = "";

                            _Image.MediaPath = _uploadConfig.UploadPath + outputImage.Substring(outputImage.LastIndexOf("\\")+1);
                            _Image.MediaMD5Hash = MD5Hash;
                            _Image.MediaUpdatedOn = DateTime.UtcNow;
                            _Image.MediaOwner = _idMediaOwner;
                            _Image.mediaType = _mediaType;

                            try
                            {
                                _Image.SavePhotoDbInfo();
                                _returnCode = "Success:"+_Image.IDMedia.ToString();

                                try
                                {
                                    _Image.AddAlternativePhotoSize(MediaSizeTypes.OriginalSize, _mediaType);
                                    _Image.AddAlternativePhotoSize(MediaSizeTypes.OriginalResized, _mediaType);
                                    _Image.AddAlternativePhotoSize(MediaSizeTypes.Small, _mediaType);
                                }
                                catch
                                {
                                    //write log!!!!
                                }
                            }
                            catch (Exception ex)
                            {
                                _returnCode = "Error:" + RetrieveMessage.RetrieveDBMessage(2, "MD-ER-0013");
                            }

                            //_returnCode = "Success:1234fsse234fddsw";
                        }
                        else
                        {
                            switch (_UploadFileCheck)
                            {
                                case 1:
                                    _returnCode = "Error:" + _originalFileName + RetrieveMessage.RetrieveDBMessage(2, "MD-ER-0001");
                                    break;
                                case 2:
                                    _returnCode = "Error:" + _originalFileName + RetrieveMessage.RetrieveDBMessage(2, "MD-ER-0002");
                                    break;
                                case 3:
                                    _returnCode = "Error:" + _originalFileName + RetrieveMessage.RetrieveDBMessage(2, "MD-ER-0003");
                                    break;
                                case 4:
                                    _returnCode = "Error:" + _originalFileName + RetrieveMessage.RetrieveDBMessage(2, "MD-ER-0004");
                                    break;
                                case 6:
                                    _returnCode = "Error:" + _originalFileName + RetrieveMessage.RetrieveDBMessage(2, "MD-ER-0006");
                                    break;
                            }
                        }
                    }
                    catch
                    {
                        _returnCode = "Error:" + RetrieveMessage.RetrieveDBMessage(2, "MD-ER-0013");
                    }
                    finally
                    {
                        
                    }

                    
                }
                else
                {
                    _returnCode = "Error:" + RetrieveMessage.RetrieveDBMessage(2, "MD-ER-0013");
                }
            }
            catch(Exception ex)
            {
                _returnCode = "Error:" + RetrieveMessage.RetrieveDBMessage(2, "MD-ER-0013");
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(_returnCode);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}