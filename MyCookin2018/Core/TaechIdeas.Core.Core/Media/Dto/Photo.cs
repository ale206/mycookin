namespace TaechIdeas.Core.Core.Media.Dto
{
    public class Photo : MediaByIdOutput
    {
        public int OriginalImageWidth { get; set; }
        public int OriginalImageHeight { get; set; }
        public int CroppedImageWidth { get; set; }
        public int CroppedImageHeight { get; set; }
        public string OriginalFilePath { get; set; }
    }
}