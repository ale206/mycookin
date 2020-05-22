namespace TaechIdeas.Core.Core.Configuration
{
    public interface IThirdPartyConfig
    {
        string MicrosoftClientId { get; }
        string MicrosoftClientSecret { get; }
    }
}