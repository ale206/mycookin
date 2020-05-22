using System;
using TaechIdeas.Core.Core.Token.Dto;
using TaechIdeas.Core.Core.UserBoard.Dto;

namespace TaechIdeas.Core.UnitTests.UserBoard.Helper
{
    public static class UserBoardManagerHelper
    {
        public static CheckTokenOutput GetCheckTokenOutputValid()
        {
            return new CheckTokenOutput {IsTokenValid = true};
        }

        public static WithPaginationInput GetWithPaginationInput()
        {
            return new WithPaginationInput
            {
                CheckTokenInput = new CheckTokenInput(),
                Offset = 0,
                Count = 5,
                IsAscendant = false,
                Search = "",
                OrderBy = "UserActionDate",
                UserId = new Guid()
            };
        }
    }
}