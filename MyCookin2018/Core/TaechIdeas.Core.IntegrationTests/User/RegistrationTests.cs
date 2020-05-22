using System;
using System.Globalization;
using TaechIdeas.Core.Core.Common;
using TaechIdeas.Core.Core.Common.Enums;
using TaechIdeas.Core.Core.Token.Dto;
using TaechIdeas.Core.Core.User;
using TaechIdeas.Core.Core.User.Dto;
using TaechIdeas.Core.IntegrationTests.User.Helper;
using Xunit;

namespace TaechIdeas.Core.IntegrationTests.User
{
    public class RegistrationTests
    {
        private readonly IUserManager _userManager;
        private readonly NewUserInput _newUserInput;

        private readonly IMySecurityManager _mySecurityManager;
        // private IContainer _container;

        public RegistrationTests(IUserManager userManager, NewUserInput newUserInput, IMySecurityManager mySecurityManager)
        {
            _userManager = userManager;
            _newUserInput = newUserInput;
            _mySecurityManager = mySecurityManager;

            _newUserInput = UserHelper.GetNewUserInput();
        }

        [Theory]
        [InlineData(MyWebsite.MyCookin)]
        [InlineData(MyWebsite.PiggyBanky)]
        [InlineData(MyWebsite.MindTheLandlord)]
        [InlineData(MyWebsite.MySoapRecipes)]
        public void NewUser_Success(MyWebsite myWebsite)
        {
            //Try all Websites
            _newUserInput.WebsiteId = myWebsite;

            //Register User
            var newUserResult = _userManager.NewUser(_newUserInput);

            Assert.NotNull(newUserResult);
            Assert.NotNull(newUserResult.UserId);
            Assert.NotNull(newUserResult.UserToken);
            Assert.NotEqual(new Guid(), newUserResult.UserId);
            Assert.NotEqual(new Guid(), newUserResult.UserToken);
        }

        [InlineData(MyWebsite.MyCookin)]
        [InlineData(MyWebsite.PiggyBanky)]
        [InlineData(MyWebsite.MindTheLandlord)]
        [InlineData(MyWebsite.MySoapRecipes)]
        public void NewUser_WithSameData_Fail(MyWebsite myWebsite)
        {
            //Try all Websites
            _newUserInput.WebsiteId = myWebsite;

            //Register User
            var newUserResult = _userManager.NewUser(_newUserInput);

            Assert.NotNull(newUserResult);
            Assert.NotNull(newUserResult.UserId);
            Assert.NotNull(newUserResult.UserToken);
            Assert.NotEqual(new Guid(), newUserResult.UserId);
            Assert.NotEqual(new Guid(), newUserResult.UserToken);

            //Retry Registration
            Assert.Throws<ArgumentException>(() => _userManager.NewUser(_newUserInput));
        }

        [InlineData(MyWebsite.MyCookin)]
        [InlineData(MyWebsite.PiggyBanky)]
        [InlineData(MyWebsite.MindTheLandlord)]
        [InlineData(MyWebsite.MySoapRecipes)]
        public void NewUser_WithSameEmail_Fail(MyWebsite myWebsite)
        {
            //Try all Websites
            _newUserInput.WebsiteId = myWebsite;

            //Register User
            var newUserResult = _userManager.NewUser(_newUserInput);

            Assert.NotNull(newUserResult);
            Assert.NotNull(newUserResult.UserId);
            Assert.NotNull(newUserResult.UserToken);
            Assert.NotEqual(new Guid(), newUserResult.UserId);
            Assert.NotEqual(new Guid(), newUserResult.UserToken);

            //Changing Username (Email will be the same)
            _newUserInput.Username = "NewUsername";

            //Retry Registration
            Assert.Throws<ArgumentException>(() => _userManager.NewUser(_newUserInput));
        }

        [InlineData(MyWebsite.MyCookin)]
        [InlineData(MyWebsite.PiggyBanky)]
        [InlineData(MyWebsite.MindTheLandlord)]
        [InlineData(MyWebsite.MySoapRecipes)]
        public void NewUser_WithSameUsername_Fail(MyWebsite myWebsite)
        {
            //Try all Websites
            _newUserInput.WebsiteId = myWebsite;

            //Register User
            var newUserResult = _userManager.NewUser(_newUserInput);

            Assert.NotNull(newUserResult);
            Assert.NotNull(newUserResult.UserId);
            Assert.NotNull(newUserResult.UserToken);
            Assert.NotEqual(new Guid(), newUserResult.UserId);
            Assert.NotEqual(new Guid(), newUserResult.UserToken);

            //Changing Email (Username will be the same)
            _newUserInput.Email = "aaa@aaa.it";

            //Retry Registration
            Assert.Throws<ArgumentException>(() => _userManager.NewUser(_newUserInput));
        }

        [Fact]
        public void NewUser_ObjectNull_ThrowException()
        {
            Assert.Throws<ArgumentException>(() => _userManager.NewUser(null));
        }

        [InlineData(null)]
        [InlineData("")]
        public void NewUser_WithWrongName_ThrowException(string name)
        {
            _newUserInput.Name = name;

            Assert.Throws<ArgumentException>(() => _userManager.NewUser(_newUserInput));
        }

        [InlineData(null)]
        [InlineData("")]
        public void NewUser_WithWrongSurname_ThrowException(string surname)
        {
            _newUserInput.Surname = surname;

            Assert.Throws<ArgumentException>(() => _userManager.NewUser(_newUserInput));
        }

        [InlineData(null)]
        [InlineData("")]
        public void NewUser_WithWrongUsername_ThrowException(string username)
        {
            _newUserInput.Username = username;

            Assert.Throws<ArgumentException>(() => _userManager.NewUser(_newUserInput));
        }

        [InlineData("01/03/2016")]
        [InlineData("01/03/1750")]
        [InlineData("")]
        public void NewUser_WithWrongDateOfBirth_Fail(string dateOfBirth)
        {
            _newUserInput.DateOfBirth = string.IsNullOrEmpty(dateOfBirth) ? new DateTime() : Convert.ToDateTime(dateOfBirth, new CultureInfo("it-IT"));

            Assert.Throws<ArgumentException>(() => _userManager.NewUser(_newUserInput));
        }

        [InlineData(null)]
        [InlineData("")]
        [InlineData("WRONG_EMAIL")]
        public void NewUser_WithWrongEmail_ThrowException(string email)
        {
            _newUserInput.Email = email;

            Assert.Throws<ArgumentException>(() => _userManager.NewUser(_newUserInput));
        }

        [InlineData(null)]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("thispasswordistoolongtobeconsideredavalidpassword")]
        public void NewUser_WithWrongPassword_ThrowException(string password)
        {
            _newUserInput.Password = password;

            Assert.Throws<ArgumentException>(() => _userManager.NewUser(_newUserInput));
        }

        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(4)]
        public void NewUser_WithWrongLanguageId_ThrowException(int languageId)
        {
            _newUserInput.LanguageId = languageId;

            Assert.Throws<ArgumentException>(() => _userManager.NewUser(_newUserInput));
        }

        [InlineData(null)]
        [InlineData("")]
        [InlineData("1")]
        [InlineData("1.1.1.1.1.1.1.1.1.1")]
        [InlineData("192.168.1.aaa")]
        public void NewUser_WithWrongIp_ThrowException(string ip)
        {
            _newUserInput.Ip = ip;

            Assert.Throws<ArgumentException>(() => _userManager.NewUser(_newUserInput));
        }

        [InlineData(false)]
        public void NewUser_WithContractNotSigned_ThrowException(bool contractSigned)
        {
            _newUserInput.ContractSigned = contractSigned;

            Assert.Throws<ArgumentException>(() => _userManager.NewUser(_newUserInput));
        }

        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(null)]
        public void NewUser_WithWrongCityId_ThrowException(int cityId)
        {
            _newUserInput.CityId = cityId;

            Assert.Throws<ArgumentException>(() => _userManager.NewUser(_newUserInput));
        }

        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(3)]
        [InlineData(null)]
        public void NewUser_WithWrongGenderId_ThrowException(int genderId)
        {
            _newUserInput.GenderId = genderId;

            Assert.Throws<ArgumentException>(() => _userManager.NewUser(_newUserInput));
        }

        [InlineData(1)]
        [InlineData(2)]
        public void NewUser_OkWithGenderIdCorrect(int genderId)
        {
            _newUserInput.GenderId = genderId;

            var newUserResult = _userManager.NewUser(_newUserInput);

            Assert.NotNull(newUserResult);
            Assert.NotNull(newUserResult.UserId);
            Assert.NotNull(newUserResult.UserToken);
            Assert.NotEqual(new Guid(), newUserResult.UserId);
            Assert.NotEqual(new Guid(), newUserResult.UserToken);
        }

        [InlineData(-800)]
        [InlineData(800)]
        public void NewUser_WithWrongOffset_ThrowException(int offset)
        {
            _newUserInput.Offset = offset;

            Assert.Throws<ArgumentException>(() => _userManager.NewUser(_newUserInput));
        }

        [InlineData(MyWebsite.MyCookin)]
        [InlineData(MyWebsite.PiggyBanky)]
        [InlineData(MyWebsite.MindTheLandlord)]
        [InlineData(MyWebsite.MySoapRecipes)]
        public void RegisterAndActivateNewUser_Success(MyWebsite myWebsite)
        {
            //Try all Websites
            _newUserInput.WebsiteId = myWebsite;

            //Register User
            var newUserResult = _userManager.NewUser(_newUserInput);

            var passwordHash = _mySecurityManager.GenerateSha1Hash(_newUserInput.Password);
            var confirmationCode = _mySecurityManager.GenerateSha1Hash(_newUserInput.Email + passwordHash);

            var activateUserInput = new ActivateUserInput
            {
                ConfirmationCode = confirmationCode,
                UserId = newUserResult.UserId,
                IpAddress = "1.1.1.1"
            };

            var activateUserResult = _userManager.ActivateUser(activateUserInput);

            Assert.True(activateUserResult.ExecutionResult);
        }

        [InlineData(MyWebsite.MyCookin)]
        [InlineData(MyWebsite.PiggyBanky)]
        [InlineData(MyWebsite.MindTheLandlord)]
        [InlineData(MyWebsite.MySoapRecipes)]
        public void Register_Activate_Delete_NewUser_Success(MyWebsite myWebsite)
        {
            //Try all Websites
            _newUserInput.WebsiteId = myWebsite;

            //Register User
            var newUserResult = _userManager.NewUser(_newUserInput);

            var passwordHash = _mySecurityManager.GenerateSha1Hash(_newUserInput.Password);
            var confirmationCode = _mySecurityManager.GenerateSha1Hash(_newUserInput.Email + passwordHash);

            var activateUserInput = new ActivateUserInput
            {
                ConfirmationCode = confirmationCode,
                UserId = newUserResult.UserId,
                IpAddress = "1.1.1.1"
            };

            var activateUserResult = _userManager.ActivateUser(activateUserInput);
            Assert.True(activateUserResult.ExecutionResult);

            var deleteAccountInput = new DeleteAccountInput
            {
                CheckTokenInput = new CheckTokenInput
                {
                    UserToken = newUserResult.UserToken,
                    UserId = newUserResult.UserId
                }
            };

            var deleteAccountResult = _userManager.DeleteAccount(deleteAccountInput);
            Assert.True(deleteAccountResult.AccountDeleted);
        }
    }
}