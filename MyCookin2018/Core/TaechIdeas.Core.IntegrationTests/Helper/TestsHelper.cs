using TaechIdeas.Core.Core.Common;
using TaechIdeas.Core.Core.User;

namespace TaechIdeas.Core.IntegrationTests.Helper
{
    public class TestsHelper
    {
        private IUserManager _userManager;

        //private IContainer _container;
        private IMySecurityManager _mySecurityManager;

        //public CharlieContext PrepareTest()
        //{
        //    if (_container == null)
        //    {
        //        _container = CommonHelper.IocSetup();
        //        _userManager = _container.Resolve<IUserManager>();
        //        _mySecurityManager = _container.Resolve<IMySecurityManager>();
        //    }
        //    _newUserRequest = UserHelper.GetNewUserRequest();
        //}

        //public NewUserResult NewUser(MyWebsite myWebsite)
        //{
        //    //If we are calling NewUser from other tests, _newUserRequest will be null.
        //    if (_newUserRequest == null)
        //        Init();

        //    if (_newUserRequest == null) return null; //Never you should go here

        //    _newUserRequest.WebsiteId = myWebsite;

        //    //Register User
        //    var newUserResult = _userApiService.NewUser(_newUserRequest);

        //    Assert.NotNull(newUserResult);
        //    Assert.NotNull(newUserResult.UserId);
        //    Assert.NotNull(newUserResult.UserToken);
        //    Assert.NotEqual(new Guid(), newUserResult.UserId);
        //    Assert.NotEqual(new Guid(), newUserResult.UserToken);

        //    return newUserResult;
        //}
    }
}