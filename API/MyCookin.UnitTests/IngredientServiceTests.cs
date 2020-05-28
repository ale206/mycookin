using Moq;
using MyCookin.Business.Implementations;
using MyCookin.Domain.Repositories;
using MyCookin.UnitTests.Helpers;
using Serilog;
using Xunit;

namespace MyCookin.UnitTests
{
    public class IngredientsServiceTests
    {
        private readonly Mock<IIngredientRepository> _mockIIngredientRepository = new Mock<IIngredientRepository>();
        private readonly Mock<ILogger> _mockILogger = new Mock<ILogger>();

        [Fact]
        public void GetSupportedLanguages_Returns_Languages()
        {
            // Configure
            var ingredient = IngredientsHelper.GetIngredient();
            _mockILogger.Setup(x => x.Debug("Debug message"));
            _mockIIngredientRepository.Setup(x => x.GetIngredientById(It.IsAny<long>())).ReturnsAsync(ingredient);

            // Act
            var ingredientService = new IngredientService(_mockIIngredientRepository.Object, _mockILogger.Object);
            var result = ingredientService.GetIngredientById(1);

            // Assert
            _mockILogger.Verify(x=>x.Debug("Debug Message"), Times.Once);
            _mockIIngredientRepository.Verify(x => x.GetIngredientById(It.IsAny<long>()), Times.Once);
            Assert.NotNull(result);
        }
    }
}