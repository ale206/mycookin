using TaechIdeas.MyCookin.Core;
using Xunit;

namespace TaechIdeas.MyCookin.IntegrationTests.Recipe
{
    public class RecipesTests
    {
        private IRecipeManager _recipeManager;

        public RecipesTests(IRecipeManager recipeManager)
        {
            _recipeManager = recipeManager;
        }

        [Theory]
        [InlineData("scaloppine-al-vino", false, false, false)]
        [InlineData("scaloppine-al-vino", true, false, false)]
        [InlineData("scaloppine-al-vino", true, true, false)]
        [InlineData("scaloppine-al-vino", true, true, true)]
        [InlineData("scaloppine-al-vino", false, true, false)]
        [InlineData("scaloppine-al-vino", false, true, true)]
        [InlineData("scaloppine-al-vino", false, false, true)]
        [InlineData("ahuyento-con-patatas-y-salchicha-mozzarella", false, false, false)]
        [InlineData("ahuyento-con-patatas-y-salchicha-mozzarella", true, false, false)]
        [InlineData("ahuyento-con-patatas-y-salchicha-mozzarella", true, true, false)]
        [InlineData("ahuyento-con-patatas-y-salchicha-mozzarella", true, true, true)]
        [InlineData("ahuyento-con-patatas-y-salchicha-mozzarella", false, true, false)]
        [InlineData("ahuyento-con-patatas-y-salchicha-mozzarella", false, true, true)]
        [InlineData("ahuyento-con-patatas-y-salchicha-mozzarella", false, false, true)]
        public void RecipeByFriendlyId_Success(string friendlyId, bool includeIngredients, bool includeSteps,
            bool includeProperties)
        {
            //var recipeByFriendlyIdResult = _recipeManager.RecipeByFriendlyId(friendlyId, includeIngredients, includeSteps, includeProperties);

            //Assert.NotNull(recipeByFriendlyIdResult);
        }

        [InlineData("ensalada-de-endivias-con-salmón", false, false, false)]
        [InlineData("ensalada-de-endivias-con-salmón", true, false, false)]
        [InlineData("ensalada-de-endivias-con-salmón", true, true, false)]
        [InlineData("ensalada-de-endivias-con-salmón", true, true, true)]
        [InlineData("ensalada-de-endivias-con-salmón", false, true, false)]
        [InlineData("ensalada-de-endivias-con-salmón", false, true, true)]
        [InlineData("ensalada-de-endivias-con-salmón", false, false, true)]
        [InlineData("ensalada-de-pasta-fría-con-verduras-y-chiara-mortadela-bolonia-pgi", false, false, false)]
        [InlineData("ensalada-de-pasta-fría-con-verduras-y-chiara-mortadela-bolonia-pgi", true, false, false)]
        [InlineData("ensalada-de-pasta-fría-con-verduras-y-chiara-mortadela-bolonia-pgi", true, true, false)]
        [InlineData("ensalada-de-pasta-fría-con-verduras-y-chiara-mortadela-bolonia-pgi", true, true, true)]
        [InlineData("ensalada-de-pasta-fría-con-verduras-y-chiara-mortadela-bolonia-pgi", false, true, false)]
        [InlineData("ensalada-de-pasta-fría-con-verduras-y-chiara-mortadela-bolonia-pgi", false, true, true)]
        [InlineData("ensalada-de-pasta-fría-con-verduras-y-chiara-mortadela-bolonia-pgi", false, false, true)]
        public void RecipeByFriendlyIdMissing_DoNotThrowError(string friendlyId, bool includeIngredients,
            bool includeSteps, bool includeProperties)
        {
            //var recipeByFriendlyIdResult = _recipeManager.RecipeByFriendlyId(friendlyId, includeIngredients, includeSteps, includeProperties);

            //Assert.IsNull(recipeByFriendlyIdResult);
        }
    }
}