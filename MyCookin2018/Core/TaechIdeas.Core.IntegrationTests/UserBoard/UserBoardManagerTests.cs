//using System.Linq;
//using AutoMapper;
//using TaechIdeas.Core.Core.Charlie.Common.Enums;
//using TaechIdeas.Core.Core.Charlie.UserBoard;
//using TaechIdeas.Core.IntegrationTests.Charlie.Common;
//using TaechIdeas.Core.IntegrationTests.Charlie.User;
//using TaechIdeas.Core.IntegrationTests.Charlie.UserBoard.Helper;
//using Xunit;

//namespace TaechIdeas.Core.IntegrationTests.Charlie.UserBoard
//{
//    public class UserBoardManagerTests
//    {
//        private IUserBoardManager _userBoardManager;
//        private IMapper _mapper;

//        //[Fact]
//        //public IEnumerable<WithPaginationOutput> WithPagination_Success()
//        //{
//        //    //Login User
//        //    var loginUserOutput = new LoginTests().LoginUser();

//        //    var withPaginationOutput = _userBoardManager.WithPagination(UserBoardManagerHelper.GetWithPaginationInput(loginUserOutput));

//        //    Assert.NotNull(withPaginationOutput);

//        //    return withPaginationOutput;
//        //}

//        public UserBoardManagerTests(IUserBoardManager userBoardManager, IMapper mapper)
//        {
//            _userBoardManager = userBoardManager;
//            _mapper = mapper;
//        }

//        [InlineData(MyWebsite.MyCookin)]
//        [InlineData(MyWebsite.PiggyBanky)]
//        public void InsertAction_Success(MyWebsite myWebsite)
//        {
//            //Login User
//            var loginUserOutput = new LoginTests().LoginUser(myWebsite);

//            var insertActionOutput = _userBoardManager.InsertAction(UserBoardManagerHelper.GetInsertActionInput(loginUserOutput, _mapper));

//            Assert.NotNull(insertActionOutput);
//            Assert.True(!insertActionOutput.isError);
//            //Assert.AreEqual("US-IN-0040", insertActionOutput.OutputExecutionCode);

//            //return insertActionOutput;
//        }

//        [InlineData(MyWebsite.MyCookin)]
//        [InlineData(MyWebsite.PiggyBanky)]
//        public void UserHasAtLeastOneActionInUserBoard(MyWebsite myWebsite)
//        {
//            //Login User
//            var loginUserOutput = new LoginTests().LoginUser(myWebsite);

//            //Insert Action
//            var insertActionOutput = _userBoardManager.InsertAction(UserBoardManagerHelper.GetInsertActionInput(loginUserOutput, _mapper));

//            //Get Actions
//            var withPaginationOutput = _userBoardManager.WithPagination(UserBoardManagerHelper.GetWithPaginationInput(loginUserOutput, _mapper));

//            Assert.NotNull(withPaginationOutput);
//            Assert.True(withPaginationOutput.Any());
//        }
//    }
//}

