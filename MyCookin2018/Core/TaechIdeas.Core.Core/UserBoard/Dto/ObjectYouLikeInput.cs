using System;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class ObjectYouLikeInput : TokenRequiredInput
    {
        public Guid UserActionFatherId { get; set; }
    }
}