namespace TaechIdeas.Core.Core.Configuration
{
    public interface ITokenConfig
    {
        int TokenExpireMinutes { get; }
        int TokenRenewMinutes { get; }
    }
}