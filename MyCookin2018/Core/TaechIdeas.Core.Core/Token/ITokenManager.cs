using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.Core.Token
{
    public interface ITokenManager
    {
        NewTokenOutput NewToken(NewTokenInput newTokenInput);
        CheckTokenOutput CheckToken(CheckTokenInput checkTokenInput);
        ExpireTokenOutput ExpireToken(ExpireTokenInput expireTokenInput);
    }
}