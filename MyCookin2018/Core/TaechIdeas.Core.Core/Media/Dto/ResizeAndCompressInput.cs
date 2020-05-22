namespace TaechIdeas.Core.Core.Media.Dto
{
    public class ResizeAndCompressInput
    {
        public string FilePath { get; set; }
        public int NewWidth { get; set; }
        public int NewHeigth { get; set; }

        /// <summary>
        ///     Value from 1 to 100
        /// </summary>
        public int Quality { get; set; }

        public bool ConvertToJpeg { get; set; }
    }
}