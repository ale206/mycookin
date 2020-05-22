using System;
using TaechIdeas.Core.Core.Common.Dto;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class WithPaginationRequest : PaginationFieldsRequest
    {
        public CheckTokenRequest CheckTokenRequest { get; set; }
        public Guid UserId { get; set; }
    }
}