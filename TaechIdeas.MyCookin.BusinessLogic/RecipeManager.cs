using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using AutoMapper;
using TaechIdeas.Core.Core.Common;
using TaechIdeas.Core.Core.Common.Enums;
using TaechIdeas.Core.Core.Configuration;
using TaechIdeas.Core.Core.LogAndMessage;
using TaechIdeas.Core.Core.LogAndMessage.Dto;
using TaechIdeas.Core.Core.Media;
using TaechIdeas.Core.Core.Media.Dto;
using TaechIdeas.Core.Core.MicrosoftTranslator;
using TaechIdeas.Core.Core.Token;
using TaechIdeas.Core.Core.Token.Dto;
using TaechIdeas.Core.Core.User;
using TaechIdeas.Core.Core.User.Dto;
using TaechIdeas.MyCookin.Core;
using TaechIdeas.MyCookin.Core.Dto;
using TaechIdeas.MyCookin.Core.Enums;
using IRecipeConfig = TaechIdeas.MyCookin.Core.Configuration.IRecipeConfig;

namespace TaechIdeas.MyCookin.BusinessLogic
{
    public class RecipeManager : IRecipeManager
    {
        private readonly IRecipeConfig _recipeConfig;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IUtilsManager _utilsManager;
        private readonly IMediaManager _mediaManager;
        private readonly IUserManager _userManager;
        private readonly INetworkConfig _networkConfig;
        private readonly ITokenManager _tokenManager;
        private readonly IIngredientManager _ingredientManager;
        private readonly IMicrosoftTranslationManager _microsoftTranslationManager;
        private readonly ILogManager _logManager;
        private readonly IMapper _mapper;

        public RecipeManager(IRecipeConfig recipeConfig, IRecipeRepository recipeRepository, IUtilsManager utilsManager,
            IMediaManager mediaManager, IUserManager userManager, INetworkConfig networkConfig, ITokenManager tokenManager, IIngredientManager ingredientManager,
            IMicrosoftTranslationManager microsoftTranslationManager, ILogManager logManager, IMapper mapper)
        {
            _recipeConfig = recipeConfig;
            _recipeRepository = recipeRepository;
            _utilsManager = utilsManager;
            _mediaManager = mediaManager;
            _userManager = userManager;
            _networkConfig = networkConfig;
            _tokenManager = tokenManager;
            _ingredientManager = ingredientManager;
            _microsoftTranslationManager = microsoftTranslationManager;
            _logManager = logManager;
            _mapper = mapper;
        }

        #region SearchRecipes

        public IEnumerable<SearchRecipesOutput> SearchRecipes(SearchRecipesInput searchRecipesInput)
        {
            if (searchRecipesInput == null)
            {
                throw new ArgumentException("SearchRecipesInput Object Is Null");
            }

            IEnumerable<SearchRecipesOutput> searchRecipesOutput;

            //If Search Query is empty, return top by language
            if (string.IsNullOrEmpty(searchRecipesInput.Search))
            {
                var topRecipeResult = TopRecipesByLanguage(_mapper.Map<TopRecipesByLanguageInput>(searchRecipesInput));

                searchRecipesOutput = _mapper.Map<IEnumerable<SearchRecipesOutput>>(topRecipeResult);
            }
            else
            {
                //ADD THRESHOLDS FROM CONFIG
                searchRecipesInput.QuickThreshold = GetQuickThreshold(searchRecipesInput);

                searchRecipesInput.LightThreshold = GetLightThreshold(searchRecipesInput);

                searchRecipesOutput = searchRecipesInput.IsEmptyFridge ? SearchRecipesWithEmptyFridge(searchRecipesInput) : SearchRecipesWithoutEmptyFridge(searchRecipesInput);
            }

            return searchRecipesOutput;
        }

        #endregion

        #region GetQuickThreshold

        public int GetQuickThreshold(SearchRecipesInput searchRecipesInput)
        {
            return searchRecipesInput.Quick ? _recipeConfig.QuickRecipeThreshold : 10000;
        }

        #endregion

        #region GetLightThreshold

        public int GetLightThreshold(SearchRecipesInput searchRecipesInput)
        {
            return searchRecipesInput.Light ? _recipeConfig.LightRecipeThreshold : 10000;
        }

        #endregion

        #region SearchRecipesWithoutEmptyFridge

        private IEnumerable<SearchRecipesOutput> SearchRecipesWithoutEmptyFridge(SearchRecipesInput searchRecipesInput)
        {
            if (_recipeConfig.UseGoogleSuggestionsForSearchRecipes)
            {
                var suggestions = GetGoogleSuggestions(searchRecipesInput.Search);

                searchRecipesInput.Search = $"{searchRecipesInput.Search} {suggestions}";
            }

            IList<SearchRecipesOut> searchRecipesResultDto = _recipeRepository.SearchRecipes(_mapper.Map<SearchRecipesIn>(searchRecipesInput)).ToList();

            var searchRecipesOutputsList = new List<SearchRecipesOutput>();

            //GetByFriendlyId
            foreach (var searchRecipesOut in searchRecipesResultDto)
            {
                searchRecipesOutputsList.Add(
                    _mapper.Map<SearchRecipesOutput>(RecipeByFriendlyId(new RecipeByFriendlyIdInput {FriendlyId = searchRecipesOut.FriendlyId}))
                );
            }

            return searchRecipesOutputsList;
        }

        #endregion

        #region SearchRecipesWithEmptyFridge

        private IEnumerable<SearchRecipesOutput> SearchRecipesWithEmptyFridge(SearchRecipesInput searchRecipesInput)
        {
            var completeIngredientList = "";
            var suggestedName = "";

            var ingredientSplitted = searchRecipesInput.Search.Split(',');

            foreach (var ingrName in ingredientSplitted)
            {
                //non va bene, bisogna fare una ricerca più generica e farsi tornare tutti gli id
                //    // degli ingredienti che contengono la parola inserita 2 grandi balle!!!!
                completeIngredientList += ingrName.Trim() + ',';
                if (_recipeConfig.UseGoogleSuggestionsForEmptyFridge)
                {
                    suggestedName = GetGoogleSuggestions(ingrName.Trim());
                }

                if (!string.IsNullOrEmpty(suggestedName))
                {
                    completeIngredientList += suggestedName.Trim() + ',';
                }
            }

            searchRecipesInput.Search = completeIngredientList.Substring(0, completeIngredientList.Length - 1);

            var searchEmptyFridgeRecipesResultDto = _recipeRepository.SearchRecipesWithEmptyFridge(_mapper.Map<SearchRecipesWithEmptyFridgeIn>(searchRecipesInput)).ToList();

            var searchRecipesOutputsList = new List<SearchRecipesOutput>();

            //GetByFriendlyId
            foreach (var searchRecipesOut in searchEmptyFridgeRecipesResultDto)
            {
                searchRecipesOutputsList.Add(
                    _mapper.Map<SearchRecipesOutput>(RecipeByFriendlyId(new RecipeByFriendlyIdInput {FriendlyId = searchRecipesOut.FriendlyId}))
                );
            }

            return searchRecipesOutputsList;
        }

        #endregion

        #region RecipeByIdAndLanguage

        private RecipeByIdAndLanguageOutput RecipeByIdAndLanguage(RecipeByIdAndLanguageInput recipeByIdAndLanguageInput)
        {
            if (recipeByIdAndLanguageInput == null)
            {
                return null;
            }

            var recipeByLanguageOutput = _mapper.Map<RecipeByIdAndLanguageOutput>(_recipeRepository.RecipeByLanguage(_mapper.Map<RecipeByLanguageIn>(recipeByIdAndLanguageInput)));

            return recipeByLanguageOutput;
        }

        #endregion

        #region AddPropertiesJoined

        public string AddPropertiesJoined(int idLanguage, bool isVegan, bool isVegetarian, bool isGlutenFree)
        {
            //TODO: Improve this with translations from DB or in configuration!

            var propertiesJoined = new StringBuilder();

            switch (idLanguage)
            {
                case 1:
                    if (isVegan)
                    {
                        propertiesJoined.Append("Vegan, ");
                    }

                    if (isVegetarian)
                    {
                        propertiesJoined.Append("Vegetarian, ");
                    }

                    if (isGlutenFree)
                    {
                        propertiesJoined.Append("Gluten Free");
                    }

                    break;
                case 2:
                    if (isVegan)
                    {
                        propertiesJoined.Append("Vegana, ");
                    }

                    if (isVegetarian)
                    {
                        propertiesJoined.Append("Vegetariana, ");
                    }

                    if (isGlutenFree)
                    {
                        propertiesJoined.Append("Senza Glutine");
                    }

                    break;
                case 3:
                    if (isVegan)
                    {
                        propertiesJoined.Append("Vegana, ");
                    }

                    if (isVegetarian)
                    {
                        propertiesJoined.Append("Vegetariana, ");
                    }

                    if (isGlutenFree)
                    {
                        propertiesJoined.Append("Sin Gluten");
                    }

                    break;
            }

            return propertiesJoined.ToString().Trim().TrimEnd(',');
        }

        #endregion

        #region TopRecipesByLanguage

        public IEnumerable<TopRecipesByLanguageOutput> TopRecipesByLanguage(TopRecipesByLanguageInput topRecipesByLanguageInput)
        {
            topRecipesByLanguageInput.RecipeToShow = _recipeConfig.TopRecipesToShow;

            var topRecipeResultList =
                _mapper.Map<IEnumerable<TopRecipesByLanguageOutput>>(
                    _recipeRepository.TopRecipesByLanguage(_mapper.Map<TopRecipesByLanguageIn>(topRecipesByLanguageInput)));

            var topRecipesByLanguageOutputs = topRecipeResultList as IList<TopRecipesByLanguageOutput> ?? topRecipeResultList.ToList();

            //Add FriendlyId (For path url) //TODO: GET FROM QUERY
            topRecipesByLanguageOutputs.ToList()
                .ForEach(c => c.FriendlyId = FriendlyIdByRecipeLanguageId(new FriendlyIdByRecipeLanguageIdInput {RecipeLanguageId = c.RecipeLanguageId}).FriendlyId);

            var topRecipesByLanguageOutputList = new List<TopRecipesByLanguageOutput>();

            var recipeByFriendlyIdInput = _mapper.Map<RecipeByFriendlyIdInput>(topRecipesByLanguageInput);

            //GetByFriendlyId
            foreach (var topRecipesByLanguageOutput in topRecipesByLanguageOutputs)
            {
                recipeByFriendlyIdInput.FriendlyId = topRecipesByLanguageOutput.FriendlyId;

                topRecipesByLanguageOutputList.Add(
                    _mapper.Map<TopRecipesByLanguageOutput>(RecipeByFriendlyId(recipeByFriendlyIdInput))
                );
            }

            return topRecipesByLanguageOutputList;
        }

        #endregion

        #region BestRecipesByLanguage

        public IEnumerable<BestRecipesByLanguageOutput> BestRecipesByLanguage(BestRecipesByLanguageInput bestRecipesByLanguageInput)
        {
            var bestRecipesByLanguageOut = _recipeRepository.BestRecipesByLanguage(_mapper.Map<BestRecipesByLanguageIn>(bestRecipesByLanguageInput));

            var bestRecipeResultList =
                _mapper.Map<IEnumerable<BestRecipesByLanguageOutput>>(bestRecipesByLanguageOut);

            var bestRecipesByLanguageOutputs = bestRecipeResultList as IList<BestRecipesByLanguageOutput> ?? bestRecipeResultList.ToList();

            //Add FriendlyId (For path url) //TODO: GET FROM QUERY
            bestRecipesByLanguageOutputs.ToList()
                .ForEach(c => c.FriendlyId = FriendlyIdByRecipeLanguageId(new FriendlyIdByRecipeLanguageIdInput {RecipeLanguageId = c.RecipeLanguageId}).FriendlyId);

            var bestRecipesByLanguageOutputList = new List<BestRecipesByLanguageOutput>();

            var recipeByFriendlyIdInput = _mapper.Map<RecipeByFriendlyIdInput>(bestRecipesByLanguageInput);

            //GetByFriendlyId
            foreach (var bestRecipesByLanguageOutput in bestRecipesByLanguageOutputs)
            {
                recipeByFriendlyIdInput.FriendlyId = bestRecipesByLanguageOutput.FriendlyId;

                bestRecipesByLanguageOutputList.Add(
                    _mapper.Map<BestRecipesByLanguageOutput>(RecipeByFriendlyId(recipeByFriendlyIdInput))
                );
            }

            return bestRecipesByLanguageOutputList;
        }

        #endregion

        #region GetGoogleSuggestions

        public string GetGoogleSuggestions(string words)
        {
            try
            {
                if (!_recipeConfig.UseGoogleSuggestionsForSearchRecipes)
                {
                    return string.Empty;
                }

                var uri = $"{_recipeConfig.GoogleSuggestUri}{words}";

                //TODO: DOESN'T WORK ON DEV???
                var request = (HttpWebRequest) WebRequest.Create(uri);
                request.Timeout = 1000;
                var response = (HttpWebResponse) request.GetResponse();

                var responseStream = response.GetResponseStream();

                if (responseStream == null)
                {
                    return null;
                }

                string returnValue;

                using (var sr = new StreamReader(responseStream))
                {
                    returnValue = sr.ReadToEnd();
                }

                var doc = XDocument.Parse(returnValue);

                if (doc.Root == null)
                {
                    return null;
                }

                var xElement = doc.Root.Element("CompleteSuggestion");

                var element = xElement?.Element("suggestion");

                //TODO: This take only the first suggestion: Use a good Deserialize Object for XML and take all suggestions!

                if (element != null)
                {
                    var attr = element.Attribute("data");
                    return attr.Value;
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    //TODO: CHANGE
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "RC-ER-9999",
                    ErrorSeverity = LogLevel.Warnings,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | Words: {words}"
                };

                _logManager.WriteLog(logRowIn);

                //Do not throw exception here!
                //throw;
                return string.Empty;
            }
        }

        #endregion

        #region RecipeById

        public RecipeByIdOutput RecipeById(RecipeByIdInput recipeByIdInput)
        {
            var recipeByIdOutput = _mapper.Map<RecipeByIdOutput>(_recipeRepository.RecipeById(_mapper.Map<RecipeByIdIn>(recipeByIdInput)));

            return recipeByIdOutput;
        }

        #endregion

        #region RecipeByFriendlyId

        public RecipeByFriendlyIdOutput RecipeByFriendlyId(RecipeByFriendlyIdInput recipeByFriendlyIdInput)
        {
            var recipeByFriendlyIdOutput = _mapper.Map<RecipeByFriendlyIdOutput>(_recipeRepository.RecipeByFriendlyId(_mapper.Map<RecipeByFriendlyIdIn>(recipeByFriendlyIdInput)));

            if (recipeByFriendlyIdOutput == null)
            {
                // ReSharper disable once ExpressionIsAlwaysNull (it can be null!)
                return recipeByFriendlyIdOutput;
            }

            //Add Recipe fields in language
            var recipeByIdAndLanguageOutput =
                RecipeByIdAndLanguage(new RecipeByIdAndLanguageInput {LanguageId = recipeByFriendlyIdOutput.LanguageId, RecipeId = recipeByFriendlyIdOutput.RecipeId});
            recipeByFriendlyIdOutput.RecipeLanguageId = recipeByIdAndLanguageOutput.RecipeLanguageId;
            recipeByFriendlyIdOutput.RecipeName = recipeByIdAndLanguageOutput.RecipeName;
            recipeByFriendlyIdOutput.RecipeNote = recipeByIdAndLanguageOutput.RecipeNote;
            recipeByFriendlyIdOutput.RecipeSuggestion = recipeByIdAndLanguageOutput.RecipeSuggestion;

            //Add Properties Joined
            recipeByFriendlyIdOutput.PropertiesJoined = AddPropertiesJoined(recipeByIdAndLanguageOutput.LanguageId, recipeByIdAndLanguageOutput.IsVegan, recipeByIdAndLanguageOutput.IsVegetarian,
                recipeByIdAndLanguageOutput.IsGlutenFree);

            //Add ImageUrl
            recipeByFriendlyIdOutput.ImageUrl = recipeByFriendlyIdOutput.RecipeImageId == null
                ? _mediaManager.GetDefaultMediaPath(MediaType.RecipePhoto)
                : _mediaManager.MediaPathByMediaId(new MediaPathByMediaIdInput {MediaId = (Guid) recipeByFriendlyIdOutput.RecipeImageId}).MediaPath;

            //Add RecipeOwner
            if (recipeByFriendlyIdOutput.OwnerId == null)
            {
                recipeByFriendlyIdOutput.RecipeOwner = null;
            }
            else
            {
                var userByIdInput = new UserByIdInput
                {
                    CheckTokenInput = new CheckTokenInput
                    {
                        UserId = (Guid) recipeByFriendlyIdOutput.OwnerId,
                        UserToken = new Guid(),
                        WebsiteId = (int) MyWebsite.MyCookin,
                        TokenRenewMinutes = 0
                    }
                };

                //Call UserById avoiding the user token check
                recipeByFriendlyIdOutput.RecipeOwner = _mapper.Map<RecipeOwnerOutput>(_userManager.UserById(userByIdInput, true));
            }

            //Add FriendlyId
            recipeByFriendlyIdOutput.FriendlyId = recipeByFriendlyIdInput.FriendlyId;

            //Add Steps
            if (recipeByFriendlyIdInput.IncludeSteps)
            {
                recipeByFriendlyIdOutput.Steps = StepsForRecipe(new StepsForRecipeInput {RecipeLanguageId = recipeByFriendlyIdOutput.RecipeLanguageId});

                var stepForRecipeOutputs = recipeByFriendlyIdOutput.Steps as IList<StepForRecipeOutput> ?? recipeByFriendlyIdOutput.Steps.ToList();
                if (stepForRecipeOutputs.Any())
                {
                    //Add Steps ImageUrl
                    stepForRecipeOutputs.ToList()
                        .ForEach(c => c.ImageUrl = c.RecipeStepImageId == null ? null : _mediaManager.MediaPathByMediaId(new MediaPathByMediaIdInput {MediaId = (Guid) c.RecipeStepImageId}).MediaPath);
                }
            }

            //Add Ingredients
            if (recipeByFriendlyIdInput.IncludeIngredients)
            {
                var allIngredients = _ingredientManager.IngredientsByIdRecipeAndLanguage(new IngredientsByIdRecipeAndLanguageInput
                {
                    RecipeId = recipeByFriendlyIdOutput.RecipeId,
                    LanguageId = recipeByIdAndLanguageOutput.LanguageId
                });

                var ingredientByIdRecipeAndLanguageOutputs = allIngredients as IList<IngredientsByIdRecipeAndLanguageOutput> ?? allIngredients.ToList();

                recipeByFriendlyIdOutput.IngredientsForRecipes = ingredientByIdRecipeAndLanguageOutputs.Where(x => x.RecipeIngredientGroupNumber == (int) IngredientGroupNumber.Recipe);
                recipeByFriendlyIdOutput.IngredientsForDough = ingredientByIdRecipeAndLanguageOutputs.Where(x => x.RecipeIngredientGroupNumber == (int) IngredientGroupNumber.Dough);
                recipeByFriendlyIdOutput.IngredientsForDressing = ingredientByIdRecipeAndLanguageOutputs.Where(x => x.RecipeIngredientGroupNumber == (int) IngredientGroupNumber.Dressing);
                recipeByFriendlyIdOutput.IngredientsForFilling = ingredientByIdRecipeAndLanguageOutputs.Where(x => x.RecipeIngredientGroupNumber == (int) IngredientGroupNumber.Filling);
                recipeByFriendlyIdOutput.IngredientsForSauce = ingredientByIdRecipeAndLanguageOutputs.Where(x => x.RecipeIngredientGroupNumber == (int) IngredientGroupNumber.Sauce);
                recipeByFriendlyIdOutput.IngredientsForDecoration = ingredientByIdRecipeAndLanguageOutputs.Where(x => x.RecipeIngredientGroupNumber == (int) IngredientGroupNumber.Decoration);
            }

            //Add Properties
            if (recipeByFriendlyIdInput.IncludeProperties)
            {
                var propertiesByRecipeAndLanguageInput = new PropertiesByRecipeAndLanguageInput
                {
                    LanguageId = recipeByIdAndLanguageOutput.LanguageId,
                    RecipeId = recipeByFriendlyIdOutput.RecipeId
                };

                recipeByFriendlyIdOutput.Properties = PropertiesByRecipeAndLanguage(propertiesByRecipeAndLanguageInput);
            }

            return recipeByFriendlyIdOutput;
        }

        #endregion

        #region RecipePath

        public RecipePathOutput RecipePath(RecipePathInput recipePathInput)
        {
            var recipe = RecipeByIdAndLanguage(_mapper.Map<RecipeByIdAndLanguageInput>(recipePathInput));

            var baseUri = new Uri(_networkConfig.WebUrl);
            var routingUrl = "";

            switch (recipePathInput.LanguageId)
            {
                case 1:
                    routingUrl = _networkConfig.RoutingRecipeEn;
                    break;
                case 2:
                    routingUrl = _networkConfig.RoutingRecipeIt;
                    break;
                case 3:
                    routingUrl = _networkConfig.RoutingRecipeEs;
                    break;
                default:
                    routingUrl = _networkConfig.RoutingRecipeEn;
                    break;
            }

            var url = new Uri(baseUri, $"{routingUrl}{recipe.RecipeName}");

            return new RecipePathOutput {RecipePath = url.ToString()};
        }

        #endregion

        #region GenerateFriendlyId

        public GenerateFriendlyIdOutput GenerateFriendlyId(GenerateFriendlyIdInput generateFriendlyIdInput)
        {
            return _mapper.Map<GenerateFriendlyIdOutput>(_recipeRepository.GenerateFriendlyId(_mapper.Map<GenerateFriendlyIdIn>(generateFriendlyIdInput)));
        }

        #endregion

        #region GenerateAllFriendlyId

        public bool GenerateAllFriendlyId()
        {
            _recipeRepository.GenerateAllFriendlyId();
            return true;
        }

        #endregion

        #region FriendlyIdByRecipeLanguageId

        public FriendlyIdByRecipeLanguageIdOutput FriendlyIdByRecipeLanguageId(FriendlyIdByRecipeLanguageIdInput friendlyIdByRecipeLanguageIdInput)
        {
            return _mapper.Map<FriendlyIdByRecipeLanguageIdOutput>(_recipeRepository.FriendlyIdByRecipeLanguageId(_mapper.Map<FriendlyIdByRecipeLanguageIdIn>(friendlyIdByRecipeLanguageIdInput)));
        }

        #endregion

        #region NewRecipe

        public NewRecipeOutput NewRecipe(NewRecipeInput newRecipeInput)
        {
            //Check for Valid Token
            var checkTokenOutput = _tokenManager.CheckToken(newRecipeInput.CheckTokenInput);

            if (!checkTokenOutput.IsTokenValid)
            {
                throw new Exception("Token not valid for the user.");
            }

            var newRecipeOut = _recipeRepository.NewRecipe(_mapper.Map<NewRecipeIn>(newRecipeInput));

            if (newRecipeOut.RecipeId == new Guid() || newRecipeOut.RecipeLanguageId == new Guid())
            {
                throw new Exception("Error in USP_NewRecipe: RecipeId or RecipeLanguageId returned as Empty Guid");
            }

            return _mapper.Map<NewRecipeOutput>(newRecipeOut);
        }

        #endregion

        #region UpdateRecipeLanguage

        public UpdateRecipeLanguageOutput UpdateRecipeLanguage(UpdateRecipeLanguageInput updateRecipeLanguageInput)
        {
            //Check for Valid Token
            var checkTokenOutput = _tokenManager.CheckToken(updateRecipeLanguageInput.CheckTokenInput);

            if (!checkTokenOutput.IsTokenValid)
            {
                throw new Exception("Token not valid for the user.");
            }

            var calculateRecipeTagsInput = new CalculateRecipeTagsInput
            {
                LanguageId = updateRecipeLanguageInput.LanguageId,
                RecipeId = updateRecipeLanguageInput.RecipeId,
                IncludeIngredientCategory = true, //TODO: Use configuration!
                IncludeDynamicProp = false, //TODO: Use configuration!
                IncludeIngredientList = true //TODO: Use configuration!
            };

            var updateRecipeLanguageIn = _mapper.Map<UpdateRecipeLanguageIn>(updateRecipeLanguageInput);

            //TODO: CHECK!
            updateRecipeLanguageIn.RecipeLanguageTags = CalculateRecipeTags(calculateRecipeTagsInput).Tags;

            //Update RecipeLanguage
            var updateRecipeLanguageOutput = _mapper.Map<UpdateRecipeLanguageOutput>(_recipeRepository.UpdateRecipeLanguage(updateRecipeLanguageIn));
            if (!updateRecipeLanguageOutput.RecipeLanguageUpdated) throw new Exception("Error on UpdateRecipeLanguageOutput");

            return new UpdateRecipeLanguageOutput {RecipeLanguageUpdated = true};
        }

        #endregion

        #region UpdateRecipeLanguageStep

        public UpdateRecipeLanguageStepOutput UpdateRecipeLanguageStep(UpdateRecipeLanguageStepInput updateRecipeLanguageStepInput)
        {
            //Check for Valid Token
            var checkTokenOutput = _tokenManager.CheckToken(updateRecipeLanguageStepInput.CheckTokenInput);

            if (!checkTokenOutput.IsTokenValid)
            {
                throw new Exception("Token not valid for the user.");
            }

            //Update RecipeLanguage
            var updateRecipeLanguageStepsOutput =
                _mapper.Map<UpdateRecipeLanguageStepOutput>(_recipeRepository.UpdateRecipeLanguageStep(_mapper.Map<UpdateRecipeLanguageStepIn>(updateRecipeLanguageStepInput)));
            if (!updateRecipeLanguageStepsOutput.RecipeLanguageStepsUpdated) throw new Exception("Error on UpdateRecipeLanguageStep");

            return new UpdateRecipeLanguageStepOutput {RecipeLanguageStepsUpdated = true};
        }

        #endregion

        #region NewRecipeLanguageSteps

        public IEnumerable<NewRecipeLanguageStepsOutput> NewRecipeLanguageSteps(IEnumerable<NewRecipeLanguageStepsInput> newRecipeLanguageStepsInput)
        {
            //Check for Valid Token
            var recipeLanguageStepsInputs = newRecipeLanguageStepsInput as IList<NewRecipeLanguageStepsInput> ?? newRecipeLanguageStepsInput.ToList();
            var checkTokenOutput = _tokenManager.CheckToken(recipeLanguageStepsInputs.First().CheckTokenInput);

            if (!checkTokenOutput.IsTokenValid)
            {
                throw new Exception("Token not valid for the user.");
            }

            var newRecipeLanguageStepsOutputList = new List<NewRecipeLanguageStepsOutput>();

            var newRecipeLanguageStepsInputs = newRecipeLanguageStepsInput as IList<NewRecipeLanguageStepsInput> ?? recipeLanguageStepsInputs.ToList();

            foreach (var newRecipeLanguageStepInput in newRecipeLanguageStepsInputs)
            {
                //Update RecipeLanguage
                var newRecipeLanguageStepsOutput =
                    _mapper.Map<NewRecipeLanguageStepsOutput>(_recipeRepository.NewRecipeLanguageStep(_mapper.Map<NewRecipeLanguageStepIn>(newRecipeLanguageStepInput)));
                if (!newRecipeLanguageStepsOutput.NewRecipeLanguageStepInserted) throw new Exception("Error on NewRecipeLanguageStep");

                newRecipeLanguageStepsOutputList.Add(
                    new NewRecipeLanguageStepsOutput
                    {
                        NewRecipeLanguageStepInserted = true,
                        NewRecipeLanguageStepId = newRecipeLanguageStepsOutput.NewRecipeLanguageStepId
                    });
            }

            return newRecipeLanguageStepsOutputList;
        }

        #endregion

        #region UpdateRecipeIngredient

        public UpdateRecipeIngredientOutput UpdateRecipeIngredient(UpdateRecipeIngredientInput updateRecipeIngredientInput)
        {
            //Check for Valid Token
            var checkTokenOutput = _tokenManager.CheckToken(updateRecipeIngredientInput.CheckTokenInput);

            if (!checkTokenOutput.IsTokenValid)
            {
                throw new Exception("Token not valid for the user.");
            }

            //Update RecipeLanguage
            var updateRecipeIngredientOutput = _mapper.Map<UpdateRecipeIngredientOutput>(_recipeRepository.UpdateRecipeIngredient(_mapper.Map<UpdateRecipeIngredientIn>(updateRecipeIngredientInput)));
            if (!updateRecipeIngredientOutput.RecipeIngredientUpdated) throw new Exception("Error on UpdateRecipeIngredient");

            //Update NutritionalFacts //TODO CHECK!!
            var calculateNutritionalFactsOutput = CalculateNutritionalFacts(_mapper.Map<CalculateNutritionalFactsInput>(updateRecipeIngredientInput));

            if (!calculateNutritionalFactsOutput.NutritionaFactsCalculated)
            {
                throw new Exception("Nutritional Facts not calculated in UpdateRecipeIngredient!");
            }

            return new UpdateRecipeIngredientOutput {RecipeIngredientUpdated = true};
        }

        #endregion

        #region AddNewIngredientToRecipe

        public IEnumerable<AddNewIngredientToRecipeOutput> AddNewIngredientToRecipe(IEnumerable<AddNewIngredientToRecipeInput> addNewIngredientToRecipeInput)
        {
            //Check for Valid Token
            var newIngredientToRecipeInputs = addNewIngredientToRecipeInput as IList<AddNewIngredientToRecipeInput> ?? addNewIngredientToRecipeInput.ToList();
            var checkTokenOutput = _tokenManager.CheckToken(newIngredientToRecipeInputs.First().CheckTokenInput);

            if (!checkTokenOutput.IsTokenValid)
            {
                throw new Exception("Token not valid for the user.");
            }

            var addNewIngredientToRecipeOutputList = new List<AddNewIngredientToRecipeOutput>();

            var addNewIngredientToRecipeInputs = addNewIngredientToRecipeInput as IList<AddNewIngredientToRecipeInput> ?? newIngredientToRecipeInputs.ToList();

            foreach (var newIngredientToRecipeInput in addNewIngredientToRecipeInputs)
            {
                //Update RecipeLanguage
                var updateRecipeLanguageOutput =
                    _mapper.Map<AddNewIngredientToRecipeOutput>(_recipeRepository.AddNewIngredientToRecipe(_mapper.Map<AddNewIngredientToRecipeIn>(newIngredientToRecipeInput)));
                if (!updateRecipeLanguageOutput.NewIngredientToRecipeAdded) throw new Exception("Error on AddNewIngredientToRecipe");

                addNewIngredientToRecipeOutputList.Add(
                    new AddNewIngredientToRecipeOutput
                    {
                        NewIngredientToRecipeAdded = true,
                        NewRecipeIngredientId = updateRecipeLanguageOutput.NewRecipeIngredientId
                    });
            }

            //Calculate Nutritional Facts //TODO CHECK!
            var calculateNutritionalFactsInput = new CalculateNutritionalFactsInput {RecipeId = addNewIngredientToRecipeInputs.First().RecipeId};
            var calculateNutritionalFactsOutput = CalculateNutritionalFacts(_mapper.Map<CalculateNutritionalFactsInput>(calculateNutritionalFactsInput));

            if (!calculateNutritionalFactsOutput.NutritionaFactsCalculated)
            {
                throw new Exception("Nutritional Facts not calculted in AddNewIngredientToRecipe!");
            }

            return addNewIngredientToRecipeOutputList;
        }

        #endregion

        #region RecipeLanguageList

        public IEnumerable<RecipeLanguageListOutput> RecipeLanguageList(RecipeLanguageListInput recipeLanguageListInput)
        {
            //Check for Valid Token
            var checkTokenOutput = _tokenManager.CheckToken(recipeLanguageListInput.CheckTokenInput);

            if (!checkTokenOutput.IsTokenValid)
            {
                throw new Exception("Token not valid for the user.");
            }

            return _mapper.Map<IEnumerable<RecipeLanguageListOutput>>(_recipeRepository.RecipeLanguageList(_mapper.Map<RecipeLanguageListIn>(recipeLanguageListInput)));
        }

        #endregion

        #region CalculateNutritionalFacts

        public CalculateNutritionalFactsOutput CalculateNutritionalFacts(CalculateNutritionalFactsInput calculateNutritionalFactsInput)
        {
            return _mapper.Map<CalculateNutritionalFactsOutput>(_recipeRepository.CalculateNutritionalFacts(_mapper.Map<CalculateNutritionalFactsIn>(calculateNutritionalFactsInput)));
        }

        #endregion

        #region DeleteRecipe

        public DeleteRecipeOutput DeleteRecipe(DeleteRecipeInput deleteRecipeInput)
        {
            //Check for Valid Token
            var checkTokenOutput = _tokenManager.CheckToken(deleteRecipeInput.CheckTokenInput);

            if (!checkTokenOutput.IsTokenValid)
            {
                throw new Exception("Token not valid for the user.");
            }

            var deleteRecipeLanguageOutput = _mapper.Map<DeleteRecipeOutput>(_recipeRepository.DeleteRecipe(_mapper.Map<DeleteRecipeIn>(deleteRecipeInput)));

            return deleteRecipeLanguageOutput;
        }

        #endregion

        #region PropertiesListByTypeAndLanguage

        public IEnumerable<PropertiesListByTypeAndLanguageOutput> PropertiesListByTypeAndLanguage(PropertiesListByTypeAndLanguageInput propertiesListByTypeAndLanguageInput)
        {
            return
                _mapper.Map<IEnumerable<PropertiesListByTypeAndLanguageOutput>>(
                    _recipeRepository.PropertiesListByTypeAndLanguage(_mapper.Map<PropertiesListByTypeAndLanguageIn>(propertiesListByTypeAndLanguageInput)));
        }

        public IEnumerable<PropertiesListByTypeLanguageAndRecipeOutput> PropertiesListByTypeLanguageAndRecipe(PropertiesListByTypeLanguageAndRecipeInput propertiesListByTypeLanguageAndRecipeInput)
        {
            return
                _mapper.Map<IEnumerable<PropertiesListByTypeLanguageAndRecipeOutput>>(
                    _recipeRepository.PropertiesListByTypeLanguageAndRecipe(_mapper.Map<PropertiesListByTypeLanguageAndRecipeIn>(propertiesListByTypeLanguageAndRecipeInput)));
        }

        #endregion

        #region AddOrUpdatePropertyValue

        public IEnumerable<AddOrUpdatePropertyValueOutput> AddOrUpdatePropertyValue(CheckTokenInput checkTokenInput, IEnumerable<AddOrUpdatePropertyValueInput> addOrUpdatePropertyValueInput)
        {
            //Check for Valid Token
            var checkTokenOutput = _tokenManager.CheckToken(checkTokenInput);

            if (!checkTokenOutput.IsTokenValid)
            {
                throw new Exception("Token not valid for the user.");
            }

            return
                addOrUpdatePropertyValueInput.Select(
                    propertyValueInput => _mapper.Map<AddOrUpdatePropertyValueOutput>(_recipeRepository.AddOrUpdatePropertyValue(_mapper.Map<AddOrUpdatePropertyValueIn>(propertyValueInput))));

            //foreach (var propertyValueInput in addOrUpdatePropertyValueInput)
            //{
            //    addOrUpdatePropertyValueOutputList.Add(
            //        _mapper.Map<AddOrUpdatePropertyValueOutput>(_recipeRepository.AddOrUpdatePropertyValue(_mapper.Map<AddOrUpdatePropertyValueIn>(propertyValueInput)))
            //        );
            //}
        }

        #endregion

        #region DeletePropertyValue

        public DeletePropertyValueOutput DeletePropertyValue(DeletePropertyValueInput deletePropertyValueInput)
        {
            //Check for Valid Token
            var checkTokenOutput = _tokenManager.CheckToken(deletePropertyValueInput.CheckTokenInput);

            if (!checkTokenOutput.IsTokenValid)
            {
                throw new Exception("Token not valid for the user.");
            }

            return _mapper.Map<DeletePropertyValueOutput>(_recipeRepository.DeletePropertyValue(_mapper.Map<DeletePropertyValueIn>(deletePropertyValueInput)));
        }

        #endregion

        #region RecipePropertyByIdRecipeAndLanguage

        public IEnumerable<RecipePropertyByIdRecipeAndLanguageOutput> RecipePropertyByIdRecipeAndLanguage(RecipePropertyByIdRecipeAndLanguageInput recipePropertyByIdRecipeAndLanguageInput)
        {
            return
                _mapper.Map<IEnumerable<RecipePropertyByIdRecipeAndLanguageOutput>>(
                    _recipeRepository.RecipePropertyByIdRecipeAndLanguage(_mapper.Map<RecipePropertyByIdRecipeAndLanguageIn>(recipePropertyByIdRecipeAndLanguageInput)));
        }

        #endregion

        #region StepsByIdRecipeAndLanguage

        public IEnumerable<StepsByIdRecipeAndLanguageOutput> StepsByIdRecipeAndLanguage(StepsByIdRecipeAndLanguageInput stepsByIdRecipeAndLanguageInput)
        {
            var stepsByIdRecipeAndLanguageOutput =
                _mapper.Map<IEnumerable<StepsByIdRecipeAndLanguageOutput>>(_recipeRepository.StepsByIdRecipeAndLanguage(_mapper.Map<StepsByIdRecipeAndLanguageIn>(stepsByIdRecipeAndLanguageInput)));

            //Add Images
            var stepsByIdRecipeAndLanguageOutputs = stepsByIdRecipeAndLanguageOutput as IList<StepsByIdRecipeAndLanguageOutput> ?? stepsByIdRecipeAndLanguageOutput.ToList();
            stepsByIdRecipeAndLanguageOutputs.ToList()
                .ForEach(c => c.ImageUrl = c.RecipeStepImageId == null ? null : _mediaManager.MediaPathByMediaId(new MediaPathByMediaIdInput {MediaId = (Guid) c.RecipeStepImageId}).MediaPath);

            return stepsByIdRecipeAndLanguageOutputs;
        }

        #endregion

        #region StepsForRecipe

        public IEnumerable<StepForRecipeOutput> StepsForRecipe(StepsForRecipeInput stepsForRecipeInput)
        {
            return _mapper.Map<IEnumerable<StepForRecipeOutput>>(_recipeRepository.StepsForRecipe(_mapper.Map<StepsForRecipeIn>(stepsForRecipeInput)));
        }

        #endregion

        #region RecipeSiteMap

        public IEnumerable<RecipeSiteMapOutput> RecipeSiteMap(RecipeSiteMapInput recipeSiteMapInput)
        {
            return _mapper.Map<IEnumerable<RecipeSiteMapOutput>>(_recipeRepository.RecipeSiteMap(_mapper.Map<RecipeSiteMapIn>(recipeSiteMapInput)));
        }

        #endregion

        #region RecipesByOwner

        public IEnumerable<RecipesByOwnerOutput> RecipesByOwner(RecipesByOwnerInput recipesByOwnerInput)
        {
            return _mapper.Map<IEnumerable<RecipesByOwnerOutput>>(_recipeRepository.RecipesByOwner(_mapper.Map<RecipesByOwnerIn>(recipesByOwnerInput)));
        }

        #endregion

        #region TranslateBunchOfRecipes

        public TranslateBunchOfRecipesOutput TranslateBunchOfRecipes(TranslateBunchOfRecipesInput translateBunchOfRecipesInput)
        {
            var recipesToTranslateOut = _recipeRepository.RecipesToTranslate(_mapper.Map<RecipesToTranslateIn>(translateBunchOfRecipesInput));

            var recipesToTranslateOuts = recipesToTranslateOut as IList<RecipesToTranslateOut> ?? recipesToTranslateOut.ToList();
            if (!recipesToTranslateOuts.Any())
            {
                return null;
            }

            var addNewRecipeTranslationIn = new AddNewRecipeTranslationIn();

            var languageFrom = _utilsManager.GetLanguageCodeFromId(translateBunchOfRecipesInput.LanguageIdFrom);
            var languageTo = _utilsManager.GetLanguageCodeFromId(translateBunchOfRecipesInput.LanguageIdTo);

            foreach (var recipeToTranslate in recipesToTranslateOuts)
            {
                addNewRecipeTranslationIn.RecipeId = recipeToTranslate.IDRecipe;
                addNewRecipeTranslationIn.LanguageId = translateBunchOfRecipesInput.LanguageIdTo;
                addNewRecipeTranslationIn.RecipeName = string.IsNullOrEmpty(recipeToTranslate.RecipeName)
                    ? null
                    : _microsoftTranslationManager.TranslateSentence(languageFrom, languageTo, recipeToTranslate.RecipeName).TranslatedSentence;
                addNewRecipeTranslationIn.RecipeHistory = string.IsNullOrEmpty(recipeToTranslate.RecipeHistory)
                    ? null
                    : _microsoftTranslationManager.TranslateSentence(languageFrom, languageTo, recipeToTranslate.RecipeHistory).TranslatedSentence;
                addNewRecipeTranslationIn.RecipeNote = string.IsNullOrEmpty(recipeToTranslate.RecipeNote)
                    ? null
                    : _microsoftTranslationManager.TranslateSentence(languageFrom, languageTo, recipeToTranslate.RecipeNote).TranslatedSentence;
                addNewRecipeTranslationIn.RecipeSuggestion = string.IsNullOrEmpty(recipeToTranslate.RecipeSuggestion)
                    ? null
                    : _microsoftTranslationManager.TranslateSentence(languageFrom, languageTo, recipeToTranslate.RecipeSuggestion).TranslatedSentence;
                addNewRecipeTranslationIn.RecipeLanguageTags = string.IsNullOrEmpty(recipeToTranslate.RecipeLanguageTags)
                    ? null
                    : _microsoftTranslationManager.TranslateSentence(languageFrom, languageTo, recipeToTranslate.RecipeLanguageTags).TranslatedSentence;
                addNewRecipeTranslationIn.IsAutoTranslate = true;
                addNewRecipeTranslationIn.TranslatedBy = null;

                var addNewRecipeTranslationOut = _recipeRepository.AddNewRecipeTranslation(addNewRecipeTranslationIn);

                if (!addNewRecipeTranslationOut.NewTranslationAdded)
                {
                    throw new Exception("Error Translating Recipe from Stored Procedure.");
                }
            }

            return new TranslateBunchOfRecipesOutput {BunchOfRecipesTranslated = true};
        }

        #endregion

        #region SimilarRecipes

        public IEnumerable<SimilarRecipesOutput> SimilarRecipes(SimilarRecipesInput similarRecipesInput)
        {
            return _mapper.Map<IEnumerable<SimilarRecipesOutput>>(_recipeRepository.SimilarRecipes(_mapper.Map<SimilarRecipesIn>(similarRecipesInput)));
        }

        #endregion

        #region PercentageComplete

        public PercentageCompleteOutput PercentageComplete(PercentageCompleteInput percentageCompleteInput)
        {
            throw new NotImplementedException();

            //double _return = 0;

            //if (percentageCompleteInput.NumberOfPeople != 0) _return += 2;
            //if (percentageCompleteInput.PreparationTimeMinutes != 0) _return += 3;
            //if (percentageCompleteInput.CookingTimeMinutes != null && percentageCompleteInput.CookingTimeMinutes != 0) _return += 2;
            //if (percentageCompleteInput.IdRecipeImage != null) _return += 5;
            //if (percentageCompleteInput.IdCity != null) _return++;
            //if (percentageCompleteInput.IngredientsByIdRecipeOutput != null) _return += 5;
            //if (percentageCompleteInput.RecipePortionKcal != null) _return++;
            //if (percentageCompleteInput.RecipePortionProteins != null) _return++;
            //if (percentageCompleteInput.RecipePortionFats != null) _return++;
            //if (percentageCompleteInput.RecipePortionCarbohydrates != null) _return++;
            //if (percentageCompleteInput.RecipeName != null) _return++;
            //if (!string.IsNullOrEmpty(percentageCompleteInput.RecipeHistory)) _return += 3;
            //if (!string.IsNullOrEmpty(percentageCompleteInput.RecipeNote)) _return += 2;
            //if (!string.IsNullOrEmpty(percentageCompleteInput.RecipeSuggestion)) _return += 5;
            //if (percentageCompleteInput.StepsForRecipeOutput != null) _return++;
            //if (percentageCompleteInput.StepsForRecipeOutput != null && percentageCompleteInput.StepsForRecipeOutput.Count() > 1) _return += 10;
            //if (percentageCompleteInput.RecipePropertyValues != null && percentageCompleteInput.RecipePropertyValues.Count() > 4) _return += 5;
            //if (percentageCompleteInput.RecipeLanguageTags != null) _return++;

            //return Convert.ToInt32(_return / 50 * 100);
        }

        #endregion

        #region CalculateRecipeTags

        public CalculateRecipeTagsOutput CalculateRecipeTags(CalculateRecipeTagsInput calculateRecipeTagsInput)
        {
            return _mapper.Map<CalculateRecipeTagsOutput>(_recipeRepository.CalculateRecipeTags(_mapper.Map<CalculateRecipeTagsIn>(calculateRecipeTagsInput)));
        }

        #endregion

        #region StepsToTranslate

        public IEnumerable<StepsToTranslateOutput> StepsToTranslate(StepsToTranslateInput stepsToTranslateInput)
        {
            return _mapper.Map<IEnumerable<StepsToTranslateOutput>>(_recipeRepository.StepsToTranslate(_mapper.Map<StepsToTranslateIn>(stepsToTranslateInput)));
        }

        #endregion

        #region RecipeLanguageTags

        public IEnumerable<RecipeLanguageTagsOutput> RecipeLanguageTags(RecipeLanguageTagsInput recipeLanguageTagsInput)
        {
            return _mapper.Map<IEnumerable<RecipeLanguageTagsOutput>>(_recipeRepository.RecipeLanguageTags(_mapper.Map<RecipeLanguageTagsIn>(recipeLanguageTagsInput)));
        }

        #endregion

        #region TagsByRecipeAndLanguage

        public IEnumerable<TagsByRecipeAndLanguageOutput> TagsByRecipeAndLanguage(TagsByRecipeAndLanguageInput recipeAndLanguageInput)
        {
            return _mapper.Map<IEnumerable<TagsByRecipeAndLanguageOutput>>(_recipeRepository.TagsByRecipeAndLanguage(_mapper.Map<TagsByRecipeAndLanguageIn>(recipeAndLanguageInput)));
        }

        #endregion

        #region PropertiesByRecipeAndLanguage

        public PropertiesByRecipeAndLanguageOutput PropertiesByRecipeAndLanguage(PropertiesByRecipeAndLanguageInput propertiesByRecipeAndLanguageInput)
        {
            var propertiesByRecipeAndLanguageOutput = new PropertiesByRecipeAndLanguageOutput();
            var groupList = new List<PropertiesByRecipeAndLanguageGroupOutput>();

            var propertiesByRecipeAndLanguageOut = _recipeRepository.PropertiesByRecipeAndLanguage(_mapper.Map<PropertiesByRecipeAndLanguageIn>(propertiesByRecipeAndLanguageInput));

            var propertiesByRecipeAndLanguageOuts = propertiesByRecipeAndLanguageOut as IList<PropertiesByRecipeAndLanguageOut> ?? propertiesByRecipeAndLanguageOut.ToList();
            var recipePropertyTypeIds = propertiesByRecipeAndLanguageOuts.Select(x => x.IDRecipePropertyType).Distinct();

            //TODO: IMPROVE!!
            foreach (var recipePropertyTypeId in recipePropertyTypeIds)
            {
                var byRecipeAndLanguageOut = propertiesByRecipeAndLanguageOuts.FirstOrDefault(x => x.IDRecipePropertyType.Equals(recipePropertyTypeId));
                if (byRecipeAndLanguageOut != null)
                {
                    groupList.Add(
                        new PropertiesByRecipeAndLanguageGroupOutput
                        {
                            RecipePropertyTypeId = recipePropertyTypeId,
                            RecipePropertyType = byRecipeAndLanguageOut.RecipePropertyType,
                            RecipeProperties = propertiesByRecipeAndLanguageOuts.Where(x => x.IDRecipePropertyType.Equals(recipePropertyTypeId)).Select(x => x.RecipeProperty)
                        });
                }
            }

            propertiesByRecipeAndLanguageOutput.PropertiesByRecipeAndLanguageGroups = groupList;

            return propertiesByRecipeAndLanguageOutput;
        }

        #endregion
    }
}