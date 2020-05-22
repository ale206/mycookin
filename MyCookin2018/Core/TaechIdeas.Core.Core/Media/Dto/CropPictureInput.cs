namespace TaechIdeas.Core.Core.Media.Dto
{
    public class CropPictureInput
    {
        public string OriginalFilePathAndFileName { get; set; }
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int ImgWidth { get; set; }
        public int ImgHeight { get; set; }
        public string FilePathForCropped { get; set; }
    }
}