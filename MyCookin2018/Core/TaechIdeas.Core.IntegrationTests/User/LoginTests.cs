using System;
using TaechIdeas.Core.Core.Common.Enums;
using TaechIdeas.Core.Core.User;
using TaechIdeas.Core.Core.User.Dto;
using TaechIdeas.Core.IntegrationTests.User.Helper;
using Xunit;

namespace TaechIdeas.Core.IntegrationTests.User
{
    public class LoginFacts
    {
        private readonly IUserManager _userManager;

        private readonly LoginUserInput _loginUserInput;
        //private IContainer _container;

        public LoginFacts(IUserManager userManager)
        {
            _userManager = userManager;
            _loginUserInput = UserHelper.GetLoginUserInput();
        }

        //PUBLIC METHOD (HELPER TO LOGIN A USER)
        public LoginUserOutput LoginUser(MyWebsite myWebsite)
        {
            //If we are calling LoginUser from other Facts, _loginUserInput will be null.
            //if (_loginUserInput == null)
            //    Init();

            if (_loginUserInput == null) return null; //Never you should go here

            _loginUserInput.WebsiteId = myWebsite;

            //Login User
            var loginUserOutput = _userManager.LoginUser(_loginUserInput);

            Assert.NotNull(loginUserOutput);
            Assert.NotNull(loginUserOutput.UserId);
            Assert.NotNull(loginUserOutput.UserToken);
            Assert.NotEqual(new Guid(), loginUserOutput.UserId);
            Assert.NotEqual(new Guid(), loginUserOutput.UserToken);

            return loginUserOutput;
        }

        [InlineData(MyWebsite.MyCookin)]
        [InlineData(MyWebsite.PiggyBanky)]
        [InlineData(MyWebsite.MindTheLandlord)]
        [InlineData(MyWebsite.MySoapRecipes)]
        public void LoginUser_Success(MyWebsite myWebsite)
        {
            //Try all Websites
            _loginUserInput.WebsiteId = myWebsite;

            //Login User
            var loginUserOutput = _userManager.LoginUser(_loginUserInput);

            Assert.NotNull(loginUserOutput);
            Assert.NotNull(loginUserOutput.UserId);
            Assert.NotNull(loginUserOutput.UserToken);
            Assert.NotEqual(new Guid(), loginUserOutput.UserId);
            Assert.NotEqual(new Guid(), loginUserOutput.UserToken);
        }

        [InlineData(MyWebsite.MyCookin)]
        [InlineData(MyWebsite.PiggyBanky)]
        [InlineData(MyWebsite.MindTheLandlord)]
        [InlineData(MyWebsite.MySoapRecipes)]
        public void SameTokenIfNotLogout(MyWebsite myWebsite)
        {
            //Try all Websites
            _loginUserInput.WebsiteId = myWebsite;

            //Login User
            var loginUserOutput = _userManager.LoginUser(_loginUserInput);

            //Login User
            var newLoginUserOutput = _userManager.LoginUser(_loginUserInput);

            Assert.Equal(loginUserOutput.UserId, newLoginUserOutput.UserId);
            Assert.Equal(loginUserOutput.UserToken, newLoginUserOutput.UserToken);
        }

        [InlineData(MyWebsite.MyCookin)]
        [InlineData(MyWebsite.PiggyBanky)]
        [InlineData(MyWebsite.MindTheLandlord)]
        [InlineData(MyWebsite.MySoapRecipes)]
        public void DifferentTokenAfterLogout(MyWebsite myWebsite)
        {
            //Try all Websites
            _loginUserInput.WebsiteId = myWebsite;

            //Login User
            var loginUserOutput = _userManager.LoginUser(_loginUserInput);

            var logoutUserInput = new LogoutUserInput {UserToken = loginUserOutput.UserToken};
            var logoutUserOutput = _userManager.LogoutUser(logoutUserInput);

            Assert.NotNull(logoutUserOutput);
            Assert.True(logoutUserOutput.UserLoggedOut);

            //Login User
            var newLoginUserOutput = _userManager.LoginUser(_loginUserInput);

            Assert.Equal(loginUserOutput.UserId, newLoginUserOutput.UserId);
            Assert.NotEqual(loginUserOutput.UserToken, newLoginUserOutput.UserToken);
        }

        [Fact]
        public void Login_WithObjectNull_ThrowException()
        {
            Assert.Throws<ArgumentException>(() => _userManager.LoginUser(null));
        }

        [InlineData(null)]
        [InlineData("")]
        [InlineData("WRONG_EMAIL")]
        public void Login_WithWrongEmail_ThrowException(string email)
        {
            _loginUserInput.Email = email;

            Assert.Throws<ArgumentException>(() => _userManager.LoginUser(_loginUserInput));
        }

        [InlineData(null)]
        [InlineData("")]
        [InlineData("shortpw")]
        [InlineData("thispasswordistoolongtobeconsideredavalidpassword")]
        public void Login_WithWrongPassword_ThrowException(string password)
        {
            _loginUserInput.Password = password;

            Assert.Throws<ArgumentException>(() => _userManager.LoginUser(_loginUserInput));
        }

        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(4)]
        public void Login_WithWrongLanguageId_ThrowException(int languageId)
        {
            _loginUserInput.LanguageId = languageId;

            Assert.Throws<ArgumentException>(() => _userManager.LoginUser(_loginUserInput));
        }

        [InlineData(null)]
        [InlineData("")]
        [InlineData("1")]
        [InlineData("1.1.1.1.1.1.1.1.1.1")]
        [InlineData("192.168.1.aaa")]
        public void Login_WithWrongIp_ThrowException(string ip)
        {
            _loginUserInput.Ip = ip;

            Assert.Throws<ArgumentException>(() => _userManager.LoginUser(_loginUserInput));
        }

        [InlineData(MyWebsite.MyCookin)]
        [InlineData(MyWebsite.PiggyBanky)]
        [InlineData(MyWebsite.MindTheLandlord)]
        [InlineData(MyWebsite.MySoapRecipes)]
        public void Logout_Success(MyWebsite myWebsite)
        {
            //Try all Websites
            _loginUserInput.WebsiteId = myWebsite;

            //Login User
            var loginUserOutput = _userManager.LoginUser(_loginUserInput);

            var logoutUserInput = new LogoutUserInput {UserToken = loginUserOutput.UserToken};
            var logoutUserOutput = _userManager.LogoutUser(logoutUserInput);

            Assert.NotNull(logoutUserOutput);
            Assert.True(logoutUserOutput.UserLoggedOut);
        }
    }
}