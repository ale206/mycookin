namespace TaechIdeas.MyCookin.UnitTests.Recipe
{
    //[TestFixture]
    //public class RecipeManagerTests
    //{
    //    private Mock<IRetrieveMessageManager> _mockIRetrieveMessageManager;
    //    private Mock<IMyConvertManager> _mockIMyConvert;
    //    private Mock<IRecipeConfig> _mockIRecipeConfig;
    //    private Mock<IRecipeRepository> _mockIDataServiceRecipe;
    //    private Mock<INetworkManager> _mockINetworkManager;
    //    private Mock<ILogManager> _mockILogManager;
    //    private Mock<IUtilsManager> _mockIUtilsManager;
    //    private Mock<IMediaManager> _mockIMediaManager;
    //    private Mock<IUserManager> _mockIUserManager;
    //    private Mock<INetworkConfig> _mockINetworkConfig;
    //    private Mock<ITokenManager> _mockITokenManager;

    //    [SetUp]
    //    public void Init()
    //    {
    //        //TODO: Find a better way to test automapper
    //        MapForRecipe.Setup();

    //        _mockIRetrieveMessageManager = new Mock<IRetrieveMessageManager>();
    //        _mockIMyConvert = new Mock<IMyConvertManager>();
    //        _mockIRecipeConfig = new Mock<IRecipeConfig>();
    //        _mockIDataServiceRecipe = new Mock<IRecipeRepository>();
    //        _mockINetworkManager = new Mock<INetworkManager>();
    //        _mockILogManager = new Mock<ILogManager>();
    //        _mockIUtilsManager = new Mock<IUtilsManager>();
    //        _mockIMediaManager = new Mock<IMediaManager>();
    //        _mockIUserManager = new Mock<IUserManager>();
    //        _mockINetworkConfig = new Mock<INetworkConfig>();
    //        _mockITokenManager = new Mock<ITokenManager>();
    //    }

    //    [TearDown]
    //    public void Cleanup()
    //    {
    //        _mockIRetrieveMessageManager = null;
    //        _mockIMyConvert = null;
    //        _mockIRecipeConfig = null;
    //        _mockIDataServiceRecipe = null;
    //        _mockINetworkManager = null;
    //        _mockILogManager = null;
    //        _mockIUtilsManager = null;
    //        _mockIMediaManager = null;
    //        _mockIUserManager = null;
    //        _mockINetworkConfig = null;
    //        _mockITokenManager = null;
    //    }

    //[Test]
    //public void SearchRecipe_SearchRecipesInputIsNull_Test()
    //{
    //    var recipeManager = new RecipeManager(_mockIRetrieveMessageManager.Object,
    //        _mockIUspReturnValueManager.Object,
    //        _mockIMyConvert.Object, _mockIRecipeConfig.Object, _mockIDataServiceRecipe.Object, _mockIMapperHelper.Object, _mockINetworkManager.Object, _mockILogManager.Object,
    //        _mockIRecipeStepManager.Object,
    //        _mockIUtilsManager.Object, _mockIRecipePropertyManager.Object, _mockIMediaManager.Object, _mockIUserManager.Object, _mockINetworkConfig.Object,
    //        _mockITokenManager.Object);

    //    Assert.Throws<ArgumentException>(() => recipeManager.SearchRecipes(null));
    //}

    //[Test]
    //public void SearchRecipe_SearchQueryIsNullAndSearchRecipesReturnsNoRecipes_Test()
    //{
    //    var recipeManager = new RecipeManager(_mockIRetrieveMessageManager.Object, _mockIRecipeIngredientManager.Object,
    //        _mockIUspReturnValueManager.Object,
    //        _mockIMyConvert.Object, _mockIRecipeConfig.Object, _mockIDataServiceRecipe.Object, _mockIMapperHelper.Object, _mockINetworkManager.Object, _mockILogManager.Object,
    //        _mockIRecipeStepManager.Object,
    //        _mockIUtilsManager.Object, _mockIRecipePropertyManager.Object, _mockIMediaManager.Object, _mockIUserManager.Object, _mockINetworkConfig.Object,
    //        _mockITokenManager.Object);

    //    var searchRecipesInput = new SearchRecipesInput
    //    {
    //        SearchQuery = null
    //    };

    //    var searchRecipesOutput = recipeManager.SearchRecipes(searchRecipesInput);

    //    Assert.AreEqual(0, searchRecipesOutput.Count());
    //}

    //[Test]
    //public void SearchRecipe_QuickFalseAndQuickThresholdIsEqualTo10000_Test()
    //{
    //    var recipeManager = new RecipeManager(_mockIRetrieveMessageManager.Object
    //        _mockIUspReturnValueManager.Object,
    //        _mockIMyConvert.Object, _mockIRecipeConfig.Object, _mockIDataServiceRecipe.Object, _mockIMapperHelper.Object, _mockINetworkManager.Object, _mockILogManager.Object,
    //        _mockIRecipeStepManager.Object,
    //        _mockIUtilsManager.Object, _mockIRecipePropertyManager.Object, _mockIMediaManager.Object, _mockIUserManager.Object, _mockINetworkConfig.Object,
    //        _mockITokenManager.Object);

    //    var searchRecipesInput = new SearchRecipesInput
    //    {
    //        Quick = false,
    //    };

    //    var threshold = recipeManager.GetQuickThreshold(searchRecipesInput);

    //    Assert.AreEqual(10000, threshold);
    //}

    //[Test]
    //public void SearchRecipe_QuickTrueAndQuickThresholdIsEqualTo30_Test()
    //{
    //    var recipeManager = new RecipeManager(_mockIRetrieveMessageManager.Object, _mockIRecipeIngredientManager.Object,
    //        _mockIUspReturnValueManager.Object,
    //        _mockIMyConvert.Object, _mockIRecipeConfig.Object, _mockIDataServiceRecipe.Object, _mockIMapperHelper.Object, _mockINetworkManager.Object, _mockILogManager.Object,
    //        _mockIRecipeStepManager.Object,
    //        _mockIUtilsManager.Object, _mockIRecipePropertyManager.Object, _mockIMediaManager.Object, _mockIUserManager.Object, _mockINetworkConfig.Object,
    //        _mockITokenManager.Object);

    //    var searchRecipesInput = new SearchRecipesInput
    //    {
    //        Quick = true,
    //    };

    //    _mockIRecipeConfig.Setup(x => x.QuickRecipeThreshold).Returns(30);

    //    var threshold = recipeManager.GetQuickThreshold(searchRecipesInput);

    //    Assert.AreEqual(30, threshold);
    //}

    //TODO: SearchQuery is null and TopRecipesByLanguage returns null
    //TODO: SearchQuery is null and TopRecipesByLanguage returns something

    //TODO:  Quick true,  _recipeConfig.QuickRecipeThreshold not null
    //TODO: SearchQuery not null, Quick true,  _recipeConfig.QuickRecipeThreshold null

    //TODO: SearchQuery not null, Light true,  _recipeConfig.QuickRecipeThreshold not null
    //TODO: SearchQuery not null, Light true,  _recipeConfig.QuickRecipeThreshold null

    //TODO: IsEmptyFridge true, SearchRecipesWithEmptyFridge returns null
    //TODO: IsEmptyFridge true, SearchRecipesWithEmptyFridge returns something

    //TODO: IsEmptyFridge false, SearchRecipesWithoutEmptyFridge returns null
    //TODO: IsEmptyFridge false, SearchRecipesWithoutEmptyFridge returns something
    //}
}