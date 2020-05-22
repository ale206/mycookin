using System;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.Core.Media.Dto
{
    public class DisableMediaInput : TokenRequiredInput
    {
        public Guid MediaId { get; set; }
    }
}