using TaechIdeas.Core.Core.Token.Dto;
using TaechIdeas.Core.Core.User.Dto;

namespace TaechIdeas.Core.IntegrationTests.Common
{
    public static class CommonHelper
    {
        //public static IContainer IocSetup()
        //{
        //    //return AppBuilderExtensions.IoCSetup();
        //}

        public static CheckTokenRequest GetCheckTokenRequest(LoginUserResult loginUserResult)
        {
            return new CheckTokenRequest
            {
                UserToken = loginUserResult.UserToken,
                UserId = loginUserResult.UserId
            };
        }

        public static CheckTokenInput GetCheckTokenInput(LoginUserOutput loginUserOutput)
        {
            return new CheckTokenInput
            {
                UserToken = loginUserOutput.UserToken,
                UserId = loginUserOutput.UserId
            };
        }
    }
}