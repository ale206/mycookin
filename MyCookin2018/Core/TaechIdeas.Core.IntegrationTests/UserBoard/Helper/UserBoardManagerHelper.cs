using System;
using AutoMapper;
using TaechIdeas.Core.Core.Token.Dto;
using TaechIdeas.Core.Core.User.Dto;
using TaechIdeas.Core.Core.UserBoard.Dto;

namespace TaechIdeas.Core.IntegrationTests.UserBoard.Helper
{
    public static class UserBoardManagerHelper
    {
        public static WithPaginationInput GetWithPaginationInput(LoginUserOutput loginUserOutput, IMapper mapper)
        {
            var withPaginationInput = new WithPaginationInput
            {
                CheckTokenInput = mapper.Map<CheckTokenInput>(loginUserOutput),
                UserId = loginUserOutput.UserId,
                Offset = 0,
                Count = 5,
                IsAscendant = false,
                Search = "",
                OrderBy = "UserActionDate"
            };

            return withPaginationInput;
        }

        public static InsertActionInput GetInsertActionInput(LoginUserOutput loginUserOutput, IMapper mapper)
        {
            var insertActionInput = new InsertActionInput
            {
                UserId = loginUserOutput.UserId,
                CheckTokenInput = mapper.Map<CheckTokenInput>(loginUserOutput),
                ActionRelatedObjectId = new Guid("F989779C-EAF4-4C0B-AF50-585C92C84F49"), //RecipeId
                UserActionFatherId = null,
                UserActionMessage = "TestMessage",
                UserActionTypeId = 6, //New Recipe //TODO: Confirm and make Enum (?)
                VisibilityId = 1
            };

            return insertActionInput;
        }
    }
}