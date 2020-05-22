using System;
using TaechIdeas.Core.Core.Common.Dto;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class WithPaginationInput : PaginationFieldsInput
    {
        public Guid UserId { get; set; }
    }
}