using System.ComponentModel.DataAnnotations;
using ServiceStack;

namespace TaechIdeas.Core.Core.Media.Dto
{
    public class CropPictureRequest
    {
        [ApiMember(Name = "StartX", DataType = "int", IsRequired = true)]
        [Required(ErrorMessage = "StartX Required")]
        public int StartX { get; set; }

        [ApiMember(Name = "StartY", DataType = "int", IsRequired = true)]
        [Required(ErrorMessage = "StartY Required")]
        public int StartY { get; set; }

        [ApiMember(Name = "ImgWidth", DataType = "int", IsRequired = true)]
        [Required(ErrorMessage = "ImgWidth Required")]
        public int ImgWidth { get; set; }

        [ApiMember(Name = "ImgHeight", DataType = "int", IsRequired = true)]
        [Required(ErrorMessage = "ImgHeight Required")]
        public int ImgHeight { get; set; }
    }
}