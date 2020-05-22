using System.Collections.Generic;
using AutoMapper;
using Moq;
using NUnit.Framework;
using TaechIdeas.Core.BusinessLogic.UserBoard;
using TaechIdeas.Core.Core.Token;
using TaechIdeas.Core.Core.Token.Dto;
using TaechIdeas.Core.Core.UserBoard;
using TaechIdeas.Core.Core.UserBoard.Dto;
using TaechIdeas.Core.UnitTests.UserBoard.Helper;

namespace TaechIdeas.Core.UnitTests.UserBoard
{
    [TestFixture]
    public class UserBoardManagerTests
    {
        private Mock<ITokenManager> _mockITokenManager;
        private Mock<IUserBoardRepository> _mockIDataServiceUserBoard;
        private Mock<IMapper> _mockIMapper;

        [SetUp]
        public void Init()
        {
            _mockITokenManager = new Mock<ITokenManager>();
            _mockIDataServiceUserBoard = new Mock<IUserBoardRepository>();
            _mockIMapper = new Mock<IMapper>();
        }

        [TearDown]
        public void Cleanup()
        {
            _mockITokenManager = null;
            _mockIDataServiceUserBoard = null;
        }

        [Test]
        public void WithPagination_Success()
        {
            _mockITokenManager.Setup(x => x.CheckToken(It.IsAny<CheckTokenInput>())).Returns(UserBoardManagerHelper.GetCheckTokenOutputValid());
            _mockIDataServiceUserBoard.Setup(x => x.WithPagination(It.IsAny<WithPaginationIn>())).Returns(new List<WithPaginationOut>());

            var userBoardManager = new UserBoardManager(_mockIDataServiceUserBoard.Object, _mockITokenManager.Object, _mockIMapper.Object);

            var withPaginationOtput = userBoardManager.WithPagination(new WithPaginationInput());

            Assert.IsNotNull(withPaginationOtput);
        }
    }
}