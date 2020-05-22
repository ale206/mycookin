using System;
using TaechIdeas.Core.Core.Common;
using TaechIdeas.Core.Core.Configuration;

namespace TaechIdeas.Core.BusinessLogic.Configuration
{
    public class MediaConfig : IMediaConfig
    {
        private readonly IAppConfigManager _appConfigManager;
        private readonly IMyConvertManager _myConvertManager;

        public MediaConfig(IAppConfigManager appConfigManager, IMyConvertManager myConvertManager)
        {
            _appConfigManager = appConfigManager;
            _myConvertManager = myConvertManager;
        }

        public string StandardNoImagePath => _appConfigManager.GetValue("StandardNoImagePath", AppDomain.CurrentDomain); //TODO INSERT IN WEB CONFIG AND OCTOPUS
        public string RecipePhotoNoImagePath => _appConfigManager.GetValue("RecipePhoto", AppDomain.CurrentDomain);
        public string ProfilePhotoNoImagePath => _appConfigManager.GetValue("ProfileImagePhoto", AppDomain.CurrentDomain);
        public int DefaultImageQuality => _myConvertManager.ToInt32(_appConfigManager.GetValue("DefaultImageQuality", AppDomain.CurrentDomain), 50);
        public string AwsAccessKey => _appConfigManager.GetValue("AWSAccessKey", AppDomain.CurrentDomain);
        public string AwsSecretKey => _appConfigManager.GetValue("AWSSecretKey", AppDomain.CurrentDomain);
        public string BucketName => _appConfigManager.GetValue("BucketName", AppDomain.CurrentDomain);
        public string CdnBasePath => _appConfigManager.GetValue("CDNBasePath", AppDomain.CurrentDomain);
        public int MaxNumMovedObject => _myConvertManager.ToInt32(_appConfigManager.GetValue("MaxNumMovedObject", AppDomain.CurrentDomain), 1);
        public int MaxErrorBeforeAbort => _myConvertManager.ToInt32(_appConfigManager.GetValue("MaxErrorBeforeAbort", AppDomain.CurrentDomain), 3);
        public bool UploadOnCdn => _myConvertManager.ToBoolean(_appConfigManager.GetValue("UploadOnCdn", AppDomain.CurrentDomain), false);
        public string AllowedExtensions => _appConfigManager.GetValue("AllowedExtensions", AppDomain.CurrentDomain);
    }
}