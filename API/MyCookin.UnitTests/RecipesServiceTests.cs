using System.Linq;
using Moq;
using MyCookin.Business.Implementations;
using MyCookin.Domain.Repositories;
using MyCookin.UnitTests.Helpers;
using Xunit;

namespace MyCookin.UnitTests
{
    public class RecipesServiceTests
    {
        private readonly Mock<IRecipeRepository> _mockIRecipeRepository = new Mock<IRecipeRepository>();

        [Fact]
        public void GetSupportedLanguages_Returns_Languages()
        {
            // Configure
            var supportedLanguages = SupportedLanguagesHelper.GetSupportedLanguages();
            _mockIRecipeRepository.Setup(x => x.GetSupportedLanguages()).ReturnsAsync(supportedLanguages);

            // Act
            var recipeService = new RecipeService(_mockIRecipeRepository.Object);
            var result = recipeService.GetSupportedLanguages();

            // Assert
            _mockIRecipeRepository.Verify(x => x.GetSupportedLanguages(), Times.Once);
            Assert.NotNull(result);
            Assert.True(result.Result.Any());
        }
    }
}