using System;
using TaechIdeas.Core.Core.Common.Enums;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.Core.Media.Dto
{
    public class NewMediaInput : TokenRequiredInput
    {
        public byte[] ImageBytes { get; set; }
        public MediaType MediaType { get; set; }
        public string ObjectName { get; set; }
        public Guid OwnerId { get; set; }
        public CropPictureInput CropPictureInput { get; set; }
    }
}