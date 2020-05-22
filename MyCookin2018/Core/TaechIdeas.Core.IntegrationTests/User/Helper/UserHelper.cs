using System;
using TaechIdeas.Core.Core.Common.Enums;
using TaechIdeas.Core.Core.User.Dto;
using TaechIdeas.Core.IntegrationTests.Common;

namespace TaechIdeas.Core.IntegrationTests.User.Helper
{
    public static class UserHelper
    {
        public static LoginUserInput GetLoginUserInput()
        {
            return new LoginUserInput
            {
                LanguageId = 1,
                Email = "alessio@gmail.com",
                Ip = "1.1.1.1",
                IsPasswordHashed = false,
                Offset = 0,
                Password = "Alessio1",
                WebsiteId = MyWebsite.MyCookin
            };
        }

        public static NewUserInput GetNewUserInput()
        {
            var rnd = new Random();

            return new NewUserInput
            {
                LanguageId = 1,
                Password = "PASSWORD",
                Ip = "1.1.1.1",
                Email = $"user-{rnd.Next(10000, 9999999)}@mycookin.com",
                Offset = 0,
                CityId = 1,
                ContractSigned = true,
                DateOfBirth = DateTime.UtcNow.AddYears(-18),
                GenderId = 1,
                Mobile = "",
                Name = "Gino",
                Surname = "Pinuzzo",
                Username = $"gino-{rnd.Next(10000, 9999999)}",
                WebsiteId = MyWebsite.MyCookin
            };
        }

        public static UserByIdInput GetUserByIdInput(LoginUserOutput loginUserOutput)
        {
            return new UserByIdInput
            {
                CheckTokenInput = CommonHelper.GetCheckTokenInput(loginUserOutput)
            };
        }
    }
}