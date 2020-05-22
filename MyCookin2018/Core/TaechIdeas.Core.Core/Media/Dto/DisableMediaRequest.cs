using System;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.Core.Media.Dto
{
    public class DisableMediaRequest
    {
        public CheckTokenRequest CheckTokenRequest { get; set; }
        public Guid MediaId { get; set; }
    }
}