using System;
using TaechIdeas.Core.Core.User.Dto;

namespace TaechIdeas.Core.UnitTests.User.Helper
{
    public static class UserManagerHelper
    {
        public static NewUserInput GetNewUserInput()
        {
            return new NewUserInput
            {
                CityId = 1,
                ContractSigned = true,
                DateOfBirth = new DateTime(1990, 03, 15),
                Email = "aaa@bbb.cc",
                GenderId = 1,
                Ip = "1.1.1.1",
                LanguageId = 1,
                Mobile = "12346",
                Name = "Name",
                Offset = 0,
                Password = "Password",
                Surname = "Surname",
                Username = "Username"
            };
        }
    }
}