using AutoMapper;
using TaechIdeas.Core.Core.User;
using TaechIdeas.Core.Core.User.Dto;
using TaechIdeas.Core.IntegrationTests.User.Helper;

namespace TaechIdeas.Core.IntegrationTests.User
{
    public class UserTests
    {
        private IUserManager _userManager;
        private IMapper _mapper;
        private LoginUserInput _loginUserInput;

        private LoginUserOutput _loginUserOutput;
        // private IContainer _container;

        public UserTests(IUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;

            _loginUserInput = UserHelper.GetLoginUserInput();

            //var loginTests = new LoginTests();
            //_loginUserOutput = loginTests.LoginUser(MyWebsite.MyCookin);
        }

        //[InlineData("9B844EF1-9690-4753-8929-5EC722AAA636")] //Alessio
        //[InlineData("EAB87CF4-56CE-4B64-8CF0-B911F0049332")] //Davide
        //public void UserById_Success(string userId)
        //{
        //    //TODO: Register a user and delete at the end!

        //    var userByIdOutput = _userManager.UserById(UserHelper.GetUserByIdInput(_loginUserOutput));

        //    Assert.NotNull(userByIdOutput);
        //    Assert.NotNull(userByIdOutput.UserId);
        //    Assert.NotEqual(new Guid(), userByIdOutput.UserId);
        //}

        //[InlineData("00000000-9690-4753-8929-5EC722AAA636")]
        //public void UserById_NotFound_ThrowAnException(string userId)
        //{
        //    _loginUserOutput.UserId = new Guid(userId);

        //    Assert.Throws<Exception>(() => _userManager.UserById(UserHelper.GetUserByIdInput(_loginUserOutput)));
        //}

        //[InlineData("alessio.disalvo@gmail.com")] //Alessio
        //public void UserByEmail_Success(string email)
        //{
        //    //TODO: Register a user and delete at the end!

        //    var userByEmailOutput = _userManager.UserByEmail(email);

        //    Assert.NotNull(userByEmailOutput);
        //    Assert.NotNull(userByEmailOutput.UserId);
        //    Assert.NotEqual(new Guid(), userByEmailOutput.UserId);
        //}

        //[InlineData("not.existing@email.com")]
        //public void UserByEmail_NotFound_DoNotThrowAnException(string email)
        //{
        //    var userByEmailOutput = _userManager.UserByEmail(email);

        //    Assert.IsNull(userByEmailOutput);
        //}

        //[Fact]
        //public void UserIdByUserToken_Success()
        //{
        //    //TODO: Register a user and delete at the end!

        //    //Login User
        //    var loginUserOutput = _userManager.LoginUser(_loginUserInput);

        //    var userIdByUserTokenOutput = _userManager.UserIdByUserToken(loginUserOutput.UserToken);

        //    Assert.NotNull(userIdByUserTokenOutput);
        //    Assert.NotNull(userIdByUserTokenOutput.UserId);
        //    Assert.NotEqual(new Guid(), userIdByUserTokenOutput.UserId);
        //}

        //[Fact]
        //public void UserIdByUserToken_NotFound_DoNotThrowAnException()
        //{
        //    var userIdByUserTokenOutput = _userManager.UserIdByUserToken(new Guid());

        //    Assert.IsNull(userIdByUserTokenOutput);
        //}

        //[InlineData(1)]
        //[InlineData(2)]
        //[InlineData(3)]
        //public void SecurityQuestionsByLanguage_Success(int languageId)
        //{
        //    var securityQuestionsByLanguageOutput = _userManager.SecurityQuestionsByLanguage(languageId);

        //    Assert.True(securityQuestionsByLanguageOutput.Any());
        //}

        //[InlineData(4)]
        //public void SecurityQuestionsByLanguage_WrongLanguageId_DoNotThrowAnException(int languageId)
        //{
        //    var securityQuestionsByLanguageOutput = _userManager.SecurityQuestionsByLanguage(languageId);

        //    Assert.True(!securityQuestionsByLanguageOutput.Any());
        //}

        //[InlineData(1)]
        //[InlineData(2)]
        //[InlineData(3)]
        //[InlineData(4)] //Frengh
        //[InlineData(5)] //German
        //public void GendersByLanguage_Success(int languageId)
        //{
        //    var gendersByLanguageOutput = _userManager.GendersByLanguage(languageId);

        //    Assert.True(gendersByLanguageOutput.Any());
        //}

        //[InlineData(6)]
        //public void GendersQuestionsByLanguage_WrongLanguageId_DoNotThrowAnException(int languageId)
        //{
        //    var gendersByLanguageOutput = _userManager.GendersByLanguage(languageId);

        //    Assert.True(!gendersByLanguageOutput.Any());
        //}

        //[Fact]
        //public void UpdateUserInfo_Success()
        //{
        //    var userByIdOutput = _userManager.UserById(UserHelper.GetUserByIdInput(_loginUserOutput));

        //    var updateUserInfoInput = _mapper.Map<UpdateUserInfoInput>(userByIdOutput);

        //    updateUserInfoInput.CheckTokenInput = new CheckTokenInput()
        //    {
        //        UserToken = _loginUserOutput.UserToken,
        //        UserId = userByIdOutput.UserId
        //    };

        //    //TODO: ADD NEW FIELDS TO CHANGE    
        //    //UPDATE NAME
        //    updateUserInfoInput.Name = "NEW_NAME";

        //    var updateUserInfoOutput = _userManager.UpdateUserInfo(updateUserInfoInput);

        //    Assert.NotNull(updateUserInfoOutput);
        //    Assert.True(updateUserInfoOutput.UserInfoUpdated);

        //    //Get new user data
        //    userByIdOutput = _userManager.UserById(UserHelper.GetUserByIdInput(_loginUserOutput));
        //    Assert.AreEqual("NEW_NAME", userByIdOutput.Name);
        //    //TODO: CHECK NEW FIELDS CHANGED
        //}
    }
}