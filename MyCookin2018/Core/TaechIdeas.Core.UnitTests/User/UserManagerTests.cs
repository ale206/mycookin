using Moq;
using NUnit.Framework;
using TaechIdeas.Core.Core.Common;
using TaechIdeas.Core.Core.Configuration;
using TaechIdeas.Core.Core.LogAndMessage;
using TaechIdeas.Core.Core.Media;
using TaechIdeas.Core.Core.Network;
using TaechIdeas.Core.Core.Statistic;
using TaechIdeas.Core.Core.Token;
using TaechIdeas.Core.Core.User;
using TaechIdeas.Core.Core.Verification;

namespace TaechIdeas.Core.UnitTests.User
{
    [TestFixture]
    public class UserManagerTests
    {
        private Mock<IMySecurityManager> _mockIMySecurityManager;
        private Mock<IUtilsManager> _mockIUtilsManager;
        private Mock<IRetrieveMessageManager> _mockIRetrieveMessageManager;
        private Mock<ILogManager> _mockILogManager;
        private Mock<IStatisticManager> _mockIMyStatisticsManager;
        private Mock<INetworkManager> _mockINetworkManager;
        private Mock<IUserRepository> _mockIDataServiceUser;
        private Mock<ITokenManager> _mockITokenManager;
        private Mock<IUserConfig> _mockIUserConfig;
        private Mock<INetworkConfig> _mockINetworkConfig;
        private Mock<IMyConvertManager> _mockIMyConvert;
        private Mock<ITokenConfig> _mockITokenConfig;
        private Mock<IVerificationManager> _mockIVerificationManager;
        private Mock<IMediaManager> _mockIMediaManager;
        private Mock<IMyCultureManager> _mockIMyCultureManager;

        [SetUp]
        public void Init()
        {
            _mockIMySecurityManager = new Mock<IMySecurityManager>();
            _mockIUtilsManager = new Mock<IUtilsManager>();
            _mockIRetrieveMessageManager = new Mock<IRetrieveMessageManager>();
            _mockILogManager = new Mock<ILogManager>();
            _mockIMyStatisticsManager = new Mock<IStatisticManager>();
            _mockINetworkManager = new Mock<INetworkManager>();
            _mockIDataServiceUser = new Mock<IUserRepository>();
            _mockITokenManager = new Mock<ITokenManager>();
            _mockIUserConfig = new Mock<IUserConfig>();
            _mockIMyConvert = new Mock<IMyConvertManager>();
            _mockITokenConfig = new Mock<ITokenConfig>();
            _mockIVerificationManager = new Mock<IVerificationManager>();
            _mockINetworkConfig = new Mock<INetworkConfig>();
            _mockIMediaManager = new Mock<IMediaManager>();
            _mockIMyCultureManager = new Mock<IMyCultureManager>();
        }

        [TearDown]
        public void Cleanup()
        {
            _mockIMySecurityManager = null;
            _mockIUtilsManager = null;
            _mockIRetrieveMessageManager = null;
            _mockILogManager = null;
            _mockIMyStatisticsManager = null;
            _mockINetworkManager = null;
            _mockIDataServiceUser = null;
            _mockITokenManager = null;
            _mockIUserConfig = null;
            _mockIMyConvert = null;
            _mockITokenConfig = null;
            _mockIVerificationManager = null;
            _mockINetworkConfig = null;
            _mockIMediaManager = null;
            _mockIMyCultureManager = null;
        }

        [Test]
        public void NewUser_NewUserInputIsNull_Test()
        {
            //var userManager = new UserManager(_mockIMySecurityManager.Object, _mockIRetrieveMessageManager.Object,
            //    _mockIMyStatisticsManager.Object,
            //    _mockIDataServiceUser.Object, _mockITokenManager.Object, _mockIUserConfig.Object,
            //    _mockITokenConfig.Object, _mockIVerificationManager.Object, _mockINetworkConfig.Object, _mockIMediaManager.Object
            //    , _mockIMyConvert.Object, _mockISocialManager.Object, _mockIMyCultureManager.Object);

            //Assert.Throws<ArgumentException>(() => userManager.NewUser(null));
        }

        //TODO: NewUser_GenerateSha1Hash_ReturnValueDifferentFromNull_Test()

        [Test]
        public void NewUser_ErrorsOnVerification_Test()
        {
            //var myNewUser = UserTestHelper.GetMyNewUser();

            //_mockIMySecurityManager.Setup(x => x.GenerateSha1Hash(It.IsAny<string>())).Returns("PasswordHAsh");

            //_mockIVerificationManager.Setup(x => x.VerifyNewUserRequest(myNewUser)).Returns(new List<VerificationError>() {new VerificationError() {RejectionReason = "ERROR"}});

            //var userManager = new UserManager(_mockIMySecurityManager.Object, _mockIUtilsManager.Object, _mockIRetrieveMessageManager.Object, _mockILogManager.Object,
            //    _mockIMyStatisticsManager.Object, _mockIMyUserPropertyManager.Object, _mockIMyUserPropertyCompiledManager.Object, _mockINetworkManager.Object,
            //    _mockIUspReturnValueManager.Object, _mockIDataServiceUser.Object, _mockITokenManager.Object, _mockIUserConfig.Object, _mockIMyConvert.Object,
            //    _mockIMapperHelper.Object, _mockITokenConfig.Object, _mockIVerificationManager.Object, _mockINetworkConfig.Object);

            //Assert.Throws<ArgumentException>(() => userManager.NewUser(myNewUser));

            //_mockIVerificationManager.VerifyAll();
        }

        [Test]
        public void UserNameAlreadyExists_ReturnTrue_Test()
        {
            //Prepare an object UsernameAlreadyExistsOutput with UsernameExists = true
            //var usernameAlreadyExistsOutput = new UsernameAlreadyExistsOutput() {UsernameExists = true};

            //_mockIVerificationManager.Setup(x => x.UsernameAlreadyExists(It.IsAny<UsernameAlreadyExistsInput>())).Returns(usernameAlreadyExistsOutput);

            //var userManager = new UserManager(_mockIMySecurityManager.Object, _mockIRetrieveMessageManager.Object,
            //    _mockIMyStatisticsManager.Object,
            //    _mockIDataServiceUser.Object, _mockITokenManager.Object, _mockIUserConfig.Object,
            //    _mockITokenConfig.Object, _mockIVerificationManager.Object, _mockINetworkConfig.Object, _mockIMediaManager.Object
            //    , _mockIMyConvert.Object, _mockISocialManager.Object, _mockIMyCultureManager.Object);

            //var newUserInput = UserManagerHelper.GetNewUserInput();
            //Assert.Throws<ArgumentException>(() => userManager.NewUser(newUserInput));
        }

        [Test]
        public void EmailAlreadyExists_ReturnTrue_Test()
        {
            //Prepare an object UsernameAlreadyExistsOutput with UsernameExists = false
            //var usernameAlreadyExistsOutput = new UsernameAlreadyExistsOutput() {UsernameExists = false};
            //_mockIVerificationManager.Setup(x => x.UsernameAlreadyExists(It.IsAny<UsernameAlreadyExistsInput>())).Returns(usernameAlreadyExistsOutput);

            ////Prepare an object EmailAlreadyExistsOutput with EmailExists = true
            //var emailAlreadyExistsOutput = new EmailAlreadyExistsOutput() {EmailExists = true};
            //_mockIVerificationManager.Setup(x => x.EmailAlreadyExists(It.IsAny<EmailAlreadyExistsInput>())).Returns(emailAlreadyExistsOutput);

            //var userManager = new UserManager(_mockIMySecurityManager.Object, _mockIRetrieveMessageManager.Object,
            //    _mockIMyStatisticsManager.Object,
            //    _mockIDataServiceUser.Object, _mockITokenManager.Object, _mockIUserConfig.Object,
            //    _mockITokenConfig.Object, _mockIVerificationManager.Object, _mockINetworkConfig.Object, _mockIMediaManager.Object
            //    , _mockIMyConvert.Object, _mockISocialManager.Object, _mockIMyCultureManager.Object);

            //var newUserInput = UserManagerHelper.GetNewUserInput();
            //Assert.Throws<ArgumentException>(() => userManager.NewUser(newUserInput));
        }

        //TODO: newUserOut.isError is true

        //TODO: USPReturnValue is not a Guid

        [Test]
        public void NewUser_Success_Test()
        {
            //var spReturnValue = UserTestHelper.GetSpReturnValueSuccessReturnGuid();
            //var uspReturnValue = UserTestHelper.GetUspReturnValueSuccessReturnGuid();
            //var myNewUser = UserTestHelper.GetMyNewUser();

            //_mockIMySecurityManager.Setup(x => x.GenerateSha1Hash(It.IsAny<string>())).Returns("PasswordHAsh");

            ////_mockIVerificationManager.Setup(x => x.VerifyNewUserRequest(myNewUser)).Returns(new List<VerificationError>());

            //_mockIDataServiceUser.Setup(
            //    x => x.NewUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime?>(),
            //        It.IsAny<bool?>(), It.IsAny<int?>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int>())).Returns(spReturnValue);

            //_mockIMapperHelper.Setup(x => x.MapSpReturnValue(spReturnValue)).Returns(uspReturnValue);

            ////_mockITokenManager.Setup(x => x.UserToken(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<string>())).Returns(Guid.NewGuid);
            //_mockINetworkManager.Setup(x => x.GetCurrentPageName()).Returns("PAGENAME");

            //_mockIMyStatisticsManager.Setup(x => x.NewStatistic(It.IsAny<NewStatisticInput>()));

            //_mockIUspReturnValueManager.Setup(x => x.GetUspReturnValue(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()));

            //var userManager = new UserManager(_mockIMySecurityManager.Object, _mockIUtilsManager.Object, _mockIRetrieveMessageManager.Object, _mockILogManager.Object,
            //    _mockIMyStatisticsManager.Object, _mockIMyUserPropertyManager.Object, _mockIMyUserPropertyCompiledManager.Object, _mockINetworkManager.Object,
            //    _mockIUspReturnValueManager.Object, _mockIDataServiceUser.Object, _mockITokenManager.Object, _mockIUserConfig.Object, _mockIMyConvert.Object,
            //    _mockIMapperHelper.Object, _mockITokenConfig.Object, _mockIVerificationManager.Object, _mockINetworkConfig.Object);

            //userManager.NewUser(myNewUser);

            //_mockIVerificationManager.VerifyAll();
            //_mockIMySecurityManager.VerifyAll();
            //_mockIDataServiceUser.VerifyAll();
            //_mockITokenManager.VerifyAll();
            //_mockINetworkManager.VerifyAll();
            //_mockIMyStatisticsManager.VerifyAll();
        }

        [Test]
        public void LoginUser_Success_Test()
        {
            //var spReturnValue = UserTestHelper.GetSpReturnValueSuccessReturnGuid();
            //var uspReturnValue = UserTestHelper.GetUspReturnValueSuccessReturnGuid();
            //var myLoginUser = UserTestHelper.GetMyLoginUser();

            //_mockIMySecurityManager.Setup(x => x.GenerateSha1Hash(It.IsAny<string>())).Returns("PasswordHAsh");

            //_mockIVerificationManager.Setup(x => x.VerifyNewUserLoginRequest(myLoginUser)).Returns(new List<VerificationError>());

            //_mockIDataServiceUser.Setup(
            //    x => x.LoginUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).Returns(spReturnValue);

            //_mockIMapperHelper.Setup(x => x.MapSpReturnValue(spReturnValue)).Returns(uspReturnValue);

            ////_mockITokenManager.Setup(x => x.UserToken(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<string>())).Returns(Guid.NewGuid);
            //_mockINetworkManager.Setup(x => x.GetCurrentPageName()).Returns("PAGENAME");

            //_mockIMyStatisticsManager.Setup(x => x.NewStatistic(It.IsAny<NewStatisticInput>()));

            //_mockIUspReturnValueManager.Setup(x => x.GetUspReturnValue(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()));

            //var userManager = new UserManager(_mockIMySecurityManager.Object, _mockIUtilsManager.Object, _mockIRetrieveMessageManager.Object, _mockILogManager.Object,
            //    _mockIMyStatisticsManager.Object, _mockIMyUserPropertyManager.Object, _mockIMyUserPropertyCompiledManager.Object, _mockINetworkManager.Object,
            //    _mockIUspReturnValueManager.Object, _mockIDataServiceUser.Object, _mockITokenManager.Object, _mockIUserConfig.Object, _mockIMyConvert.Object,
            //    _mockIMapperHelper.Object, _mockITokenConfig.Object, _mockIVerificationManager.Object, _mockINetworkConfig.Object);

            //userManager.LoginUser(myLoginUser);

            //_mockIVerificationManager.VerifyAll();
            //_mockIMySecurityManager.VerifyAll();
            //_mockIDataServiceUser.VerifyAll();
            //_mockITokenManager.VerifyAll();
        }
    }
}