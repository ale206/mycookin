using Moq;
using MyCookin.Business.Implementations;
using MyCookin.Domain.Repositories;
using MyCookin.UnitTests.Helpers;
using Xunit;

namespace MyCookin.UnitTests
{
    public class IngredientsServiceTests
    {
        private readonly Mock<IIngredientRepository> _mockIIngredientRepository = new Mock<IIngredientRepository>();

        [Fact]
        public void GetSupportedLanguages_Returns_Languages()
        {
            // Configure
            var ingredient = IngredientsHelper.GetIngredient();
            _mockIIngredientRepository.Setup(x => x.GetIngredientById(It.IsAny<long>())).ReturnsAsync(ingredient);

            // Act
            var ingredientService = new IngredientService(_mockIIngredientRepository.Object);
            var result = ingredientService.GetIngredientById(1);

            // Assert
            _mockIIngredientRepository.Verify(x => x.GetIngredientById(It.IsAny<long>()), Times.Once);
            Assert.NotNull(result);
        }
    }
}