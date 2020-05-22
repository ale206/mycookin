using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack;
using TaechIdeas.Core.Core.Common.Enums;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.Core.Media.Dto
{
    public class NewMediaRequest
    {
        public CheckTokenRequest CheckTokenRequest { get; set; }

        //TODO: Insert check
        public byte[] ImageBytes { get; set; }

        [ApiMember(Name = "MediaType", DataType = "int", IsRequired = true)]
        [Required(ErrorMessage = "MediaType Required")]
        public MediaType MediaType { get; set; }

        [ApiMember(Name = "ObjectName", DataType = "string", IsRequired = true)]
        [Required(ErrorMessage = "ObjectName Required")]
        public string ObjectName { get; set; }

        [ApiMember(Name = "OwnerId", DataType = "guid", IsRequired = true)]
        [Required(ErrorMessage = "OwnerId Required")]
        public Guid OwnerId { get; set; }

        public CropPictureRequest CropPictureInput { get; set; }
    }
}