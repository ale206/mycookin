namespace TaechIdeas.Core.Core.Configuration
{
    public interface INetworkConfig
    {
        string ClientSmtp { get; }
        int ClientSmtpPort { get; }
        string SmtpServerUsn { get; }
        string SmtpServerPsw { get; }
        string WebUrl { get; }
        bool EnableSsl { get; }
        string CookieName { get; }
        string RoutingUser { get; }
        string RoutingRecipeEn { get; }
        string RoutingRecipeIt { get; }
        string RoutingRecipeEs { get; }
        string RoutingIngredientEn { get; }
        string RoutingIngredientIt { get; }
        string RoutingIngredientEs { get; }
        string CdnBasePath { get; }
        string HomePage { get; }
        string LoginPage { get; }
        string LogoutPage { get; }
        string ErrorPage { get; }
        string PrincipalUserBoard { get; }
        int EmailDispatcherLifetime { get; }
        int MaxEmailsToSend { get; }
    }
}