using System;

namespace TaechIdeas.Core.Core.Network.Dto
{
    public class UpdateEmailStatusIn
    {
        public Guid EmailId { get; set; }

        public int EmailStatus { get; set; }
    }
}