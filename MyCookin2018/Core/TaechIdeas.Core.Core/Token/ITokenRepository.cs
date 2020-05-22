using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.Core.Token
{
    public interface ITokenRepository
    {
        NewTokenOut NewToken(NewTokenIn newTokenIn);
        CheckTokenOut CheckToken(CheckTokenIn checkTokenIn);
        ExpireTokenDataOutput ExpireToken(ExpireTokenDataInput expireTokenDataInput);
    }
}