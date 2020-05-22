namespace TaechIdeas.Core.Core.Configuration
{
    public interface IRecipeConfig
    {
        string GoogleSuggestUri { get; }
        bool UseGoogleSuggestionsForSearchRecipes { get; }
        bool UseGoogleSuggestionsForEmptyFridge { get; }
        string DateTimeFormatCSharp { get; }
        int QuickRecipeThreshold { get; }
        int LightRecipeThreshold { get; }
        int TopRecipesToShow { get; }
    }
}