using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCookinWeb.Utilities
{
    public class UploadCheck
    {
        public static int CheckIfUploadFile(string FileExt, string AcceptedFileExtension, 
                                                int ContentLength, int MaxSize, string ContentType, 
                                                string AcceptedContentTypes, bool EnableUploadForMediaType)
        {
            if (EnableUploadForMediaType)
            {
                if (AcceptedFileExtension.IndexOf(FileExt) > -1)
                {
                    if (ContentLength <= MaxSize)
                    {
                        if (AcceptedContentTypes.IndexOf(ContentType) > -1)
                        {
                            //Upload Allowed
                            return 0;
                        }
                        else
                        {
                            //Mime Type non Accepted
                            return 3;
                        }
                    }
                    else
                    {
                        //File too big
                        return 2;
                    }
                }
                else
                {
                    //Not Valid Extension
                    return 1;
                }
            }
            else
            {
                //Upload not Allowed
                return 4;
            }
        }
    }
}