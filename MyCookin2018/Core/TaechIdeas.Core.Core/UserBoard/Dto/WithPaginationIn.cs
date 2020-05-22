using System;
using TaechIdeas.Core.Core.Common.Dto;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class WithPaginationIn : PaginationFieldsIn
    {
        public Guid UserId { get; set; }
    }
}