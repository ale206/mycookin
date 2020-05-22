namespace TaechIdeas.Core.Core.Configuration
{
    public interface IMediaConfig
    {
        string StandardNoImagePath { get; }
        string RecipePhotoNoImagePath { get; }
        string ProfilePhotoNoImagePath { get; }
        int DefaultImageQuality { get; }
        string AwsAccessKey { get; }
        string AwsSecretKey { get; }
        string BucketName { get; }
        string CdnBasePath { get; }
        int MaxNumMovedObject { get; }
        int MaxErrorBeforeAbort { get; }
        bool UploadOnCdn { get; }
        string AllowedExtensions { get; }
    }
}