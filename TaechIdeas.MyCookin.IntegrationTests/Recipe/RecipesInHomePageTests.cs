using TaechIdeas.MyCookin.Core;
using TaechIdeas.MyCookin.Core.Dto;
using Xunit;

namespace TaechIdeas.MyCookin.IntegrationTests.Recipe
{
    public class RecipesInHomePageTests
    {
        private IRecipeManager _recipeManager;

        public RecipesInHomePageTests(IRecipeManager recipeManager)
        {
            _recipeManager = recipeManager;
        }

        [Theory]
        [InlineData(1, false, false, false)]
        [InlineData(1, true, false, false)]
        [InlineData(1, true, true, false)]
        [InlineData(1, true, true, true)]
        [InlineData(1, false, true, false)]
        [InlineData(1, false, true, true)]
        [InlineData(1, false, false, true)]
        [InlineData(2, false, false, false)]
        [InlineData(2, true, false, false)]
        [InlineData(2, true, true, false)]
        [InlineData(2, true, true, true)]
        [InlineData(2, false, true, false)]
        [InlineData(2, false, true, true)]
        [InlineData(2, false, false, true)]
        [InlineData(3, false, false, false)]
        [InlineData(3, true, false, false)]
        [InlineData(3, true, true, false)]
        [InlineData(3, true, true, true)]
        [InlineData(3, false, true, false)]
        [InlineData(3, false, true, true)]
        [InlineData(3, false, false, true)]
        public void TopRecipesByLanguage_Success(int languageId, bool includeIngredients, bool includeSteps, bool includeProperties)
        {
            //var topRecipesByLanguageResult = _recipeManager.TopRecipesByLanguage(languageId, includeIngredients, includeSteps, includeProperties);

            //Assert.True(topRecipesByLanguageResult.Any());
        }

        [InlineData(1, false, false, false)]
        [InlineData(1, true, false, false)]
        [InlineData(1, true, true, false)]
        [InlineData(1, true, true, true)]
        [InlineData(1, false, true, false)]
        [InlineData(1, false, true, true)]
        [InlineData(1, false, false, true)]
        [InlineData(2, false, false, false)]
        [InlineData(2, true, false, false)]
        [InlineData(2, true, true, false)]
        [InlineData(2, true, true, true)]
        [InlineData(2, false, true, false)]
        [InlineData(2, false, true, true)]
        [InlineData(2, false, false, true)]
        [InlineData(3, false, false, false)]
        [InlineData(3, true, false, false)]
        [InlineData(3, true, true, false)]
        [InlineData(3, true, true, true)]
        [InlineData(3, false, true, false)]
        [InlineData(3, false, true, true)]
        [InlineData(3, false, false, true)]
        public void BestRecipesByLanguage_Success(int languageId, bool includeIngredients, bool includeSteps, bool includeProperties)
        {
            //var bestRecipesByLanguageResult = _recipeManager.BestRecipesByLanguage(languageId, includeIngredients, includeSteps, includeProperties);

            //Assert.True(bestRecipesByLanguageResult.Any());
        }

        [InlineData(1, false, false, false, false, false)]
        [InlineData(1, true, false, false, false, false)]
        [InlineData(1, false, true, false, false, false)]
        [InlineData(1, false, false, true, false, false)]
        [InlineData(1, false, false, false, true, false)]
        [InlineData(1, false, false, false, false, true)]
        [InlineData(1, true, true, false, false, false)]
        [InlineData(1, true, false, true, false, false)]
        [InlineData(1, true, false, false, true, false)]
        [InlineData(1, true, false, false, false, true)]
        [InlineData(1, true, true, true, false, false)]
        [InlineData(1, true, true, false, true, false)]
        [InlineData(1, true, true, false, false, true)]
        [InlineData(1, true, true, true, true, false)]
        [InlineData(1, true, true, true, true, true)]
        [InlineData(2, false, false, false, false, false)]
        [InlineData(2, true, false, false, false, false)]
        [InlineData(2, false, true, false, false, false)]
        [InlineData(2, false, false, true, false, false)]
        [InlineData(2, false, false, false, true, false)]
        [InlineData(2, false, false, false, false, true)]
        [InlineData(2, true, true, false, false, false)]
        [InlineData(2, true, false, true, false, false)]
        [InlineData(2, true, false, false, true, false)]
        [InlineData(2, true, false, false, false, true)]
        [InlineData(2, true, true, true, false, false)]
        [InlineData(2, true, true, false, true, false)]
        [InlineData(2, true, true, false, false, true)]
        [InlineData(2, true, true, true, true, false)]
        [InlineData(2, true, true, true, true, true)]
        [InlineData(3, false, false, false, false, false)]
        [InlineData(3, true, false, false, false, false)]
        [InlineData(3, false, true, false, false, false)]
        [InlineData(3, false, false, true, false, false)]
        [InlineData(3, false, false, false, true, false)]
        [InlineData(3, false, false, false, false, true)]
        [InlineData(3, true, true, false, false, false)]
        [InlineData(3, true, false, true, false, false)]
        [InlineData(3, true, false, false, true, false)]
        [InlineData(3, true, false, false, false, true)]
        [InlineData(3, true, true, true, false, false)]
        [InlineData(3, true, true, false, true, false)]
        [InlineData(3, true, true, false, false, true)]
        [InlineData(3, true, true, true, true, false)]
        [InlineData(3, true, true, true, true, true)]
        public void SearchRecipesWithoutEmptyFridge_Success(int languageId, bool glutenFree, bool vegan, bool vegetarian, bool light, bool quick)
        {
            const string searchQuery = "vorrei qualcosa di dolce";
            const bool isEmptyFridge = false;
            const int itemsToDisplay = 1;
            const int rowOffSet = 0;

            var searchRecipesRequest = new SearchRecipesRequest
            {
                LanguageId = languageId,
                GlutenFree = glutenFree,
                Vegan = vegan,
                Vegetarian = vegetarian,
                IsEmptyFridge = isEmptyFridge,
                Offset = rowOffSet,
                Light = light,
                Quick = quick,
                Count = itemsToDisplay,
                IsAscendant = true,
                Search = searchQuery,
                OrderBy = "",
                IncludeProperties = false,
                IncludeSteps = false,
                IncludeIngredients = false
            };

            //var searchRecipesResult = _recipeManager.SearchRecipes(searchRecipesRequest);

            //Assert.NotNull(searchRecipesResult);
        }

        [InlineData(1, false, false, false, false, false)]
        [InlineData(1, true, false, false, false, false)]
        [InlineData(1, false, true, false, false, false)]
        [InlineData(1, false, false, true, false, false)]
        [InlineData(1, false, false, false, true, false)]
        [InlineData(1, false, false, false, false, true)]
        [InlineData(1, true, true, false, false, false)]
        [InlineData(1, true, false, true, false, false)]
        [InlineData(1, true, false, false, true, false)]
        [InlineData(1, true, false, false, false, true)]
        [InlineData(1, true, true, true, false, false)]
        [InlineData(1, true, true, false, true, false)]
        [InlineData(1, true, true, false, false, true)]
        [InlineData(1, true, true, true, true, false)]
        [InlineData(1, true, true, true, true, true)]
        [InlineData(2, false, false, false, false, false)]
        [InlineData(2, true, false, false, false, false)]
        [InlineData(2, false, true, false, false, false)]
        [InlineData(2, false, false, true, false, false)]
        [InlineData(2, false, false, false, true, false)]
        [InlineData(2, false, false, false, false, true)]
        [InlineData(2, true, true, false, false, false)]
        [InlineData(2, true, false, true, false, false)]
        [InlineData(2, true, false, false, true, false)]
        [InlineData(2, true, false, false, false, true)]
        [InlineData(2, true, true, true, false, false)]
        [InlineData(2, true, true, false, true, false)]
        [InlineData(2, true, true, false, false, true)]
        [InlineData(2, true, true, true, true, false)]
        [InlineData(2, true, true, true, true, true)]
        [InlineData(3, false, false, false, false, false)]
        [InlineData(3, true, false, false, false, false)]
        [InlineData(3, false, true, false, false, false)]
        [InlineData(3, false, false, true, false, false)]
        [InlineData(3, false, false, false, true, false)]
        [InlineData(3, false, false, false, false, true)]
        [InlineData(3, true, true, false, false, false)]
        [InlineData(3, true, false, true, false, false)]
        [InlineData(3, true, false, false, true, false)]
        [InlineData(3, true, false, false, false, true)]
        [InlineData(3, true, true, true, false, false)]
        [InlineData(3, true, true, false, true, false)]
        [InlineData(3, true, true, false, false, true)]
        [InlineData(3, true, true, true, true, false)]
        [InlineData(3, true, true, true, true, true)]
        public void SearchRecipesWithEmptyFridge_Success(int languageId, bool glutenFree, bool vegan, bool vegetarian, bool light, bool quick)
        {
            const string searchQuery = "pasta, pomodoro, tonno";
            const bool isEmptyFridge = true;
            const int itemsToDisplay = 1;
            const int rowOffSet = 0;

            var searchRecipesRequest = new SearchRecipesRequest
            {
                LanguageId = languageId,
                GlutenFree = glutenFree,
                Vegan = vegan,
                Vegetarian = vegetarian,
                IsEmptyFridge = isEmptyFridge,
                Offset = rowOffSet,
                Light = light,
                Quick = quick,
                Count = itemsToDisplay,
                IsAscendant = true,
                Search = searchQuery,
                OrderBy = "",
                IncludeProperties = false,
                IncludeSteps = false,
                IncludeIngredients = false
            };

            //var searchRecipesResult = _recipeManager.SearchRecipes(searchRecipesRequest);

            //Assert.NotNull(searchRecipesResult);
        }
    }
}