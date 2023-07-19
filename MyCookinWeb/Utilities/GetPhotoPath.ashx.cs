using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyCookin.Common;
using MyCookin.ObjectManager.MediaManager;

namespace MyCookinWeb.Utilities
{
    /// <summary>
    /// Summary description for GetPhotoPath
    /// </summary>
    public class GetPhotoPath : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                Guid IDMedia = new Guid(context.Request.QueryString["IDMedia"].ToString());
                MediaSizeTypes MediaSizeType = (MediaSizeTypes)Enum.Parse(typeof(MediaSizeTypes), context.Request.QueryString["MediaSizeType"].ToString());
                bool FromBackupServer = MyConvert.ToBoolean(context.Request.QueryString["FromBackupServer"], false);
                bool AppendGuid = MyConvert.ToBoolean(context.Request.QueryString["AppendGuid"], false);

                Photo _photo = new Photo(IDMedia);
                string _responcePath = _photo.GetAlternativeSizePath(MediaSizeType, FromBackupServer, AppendGuid,true);
                context.Response.ContentType = "text/plain";
                context.Response.Write(_responcePath);
            }
            catch
            {
            }
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