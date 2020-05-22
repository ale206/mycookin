using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using TaechIdeas.MyCookin.Core;
using TaechIdeas.MyCookin.Core.Dto;

namespace TaechIdeas.MyCookin.DataAccessLayer
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly string _connectionString;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public RecipeRepository(IDbConnectionFactory dbConnectionFactory, IConfiguration configuration)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _connectionString = configuration.GetConnectionString("DBRecipesConnectionString");
        }

        public IEnumerable<SearchRecipesOut> SearchRecipes(SearchRecipesIn searchRecipesIn)
        {
            IEnumerable<SearchRecipesOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<SearchRecipesOut>>("USP_SearchRecipes",
                    new
                    {
                        IDLanguage = searchRecipesIn.LanguageId,
                        searchRecipesIn.Vegan,
                        searchRecipesIn.Vegetarian,
                        searchRecipesIn.GlutenFree,
                        searchRecipesIn.LightThreshold,
                        searchRecipesIn.QuickThreshold,
                        offset = searchRecipesIn.Offset,
                        count = searchRecipesIn.Count,
                        orderBy = searchRecipesIn.OrderBy,
                        isAscendent = searchRecipesIn.IsAscendant,
                        search = searchRecipesIn.Search
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<SearchRecipesWithEmptyFridgeOut> SearchRecipesWithEmptyFridge(
            SearchRecipesWithEmptyFridgeIn searchRecipesWithEmptyFridgeIn)
        {
            IEnumerable<SearchRecipesWithEmptyFridgeOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<SearchRecipesWithEmptyFridgeOut>>(
                    "USP_SearchRecipesWithEmptyFridge",
                    new
                    {
                        IDLanguage = searchRecipesWithEmptyFridgeIn.LanguageId,
                        searchRecipesWithEmptyFridgeIn.Vegan,
                        searchRecipesWithEmptyFridgeIn.Vegetarian,
                        searchRecipesWithEmptyFridgeIn.GlutenFree,
                        searchRecipesWithEmptyFridgeIn.LightThreshold,
                        searchRecipesWithEmptyFridgeIn.QuickThreshold,
                        offset = searchRecipesWithEmptyFridgeIn.Offset,
                        count = searchRecipesWithEmptyFridgeIn.Count,
                        orderBy = searchRecipesWithEmptyFridgeIn.OrderBy,
                        isAscendent = searchRecipesWithEmptyFridgeIn.IsAscendant,
                        search = searchRecipesWithEmptyFridgeIn.Search
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public RecipeByLanguageOut RecipeByLanguage(RecipeByLanguageIn recipeByLanguageIn)
        {
            RecipeByLanguageOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<RecipeByLanguageOut>("USP_GetRecipeByIDRecipeIDLanguage",
                    new
                    {
                        IDRecipe = recipeByLanguageIn.RecipeId,
                        IDLanguage = recipeByLanguageIn.LanguageId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public RecipeByIdOut RecipeById(RecipeByIdIn recipeByIdIn)
        {
            RecipeByIdOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<RecipeByIdOut>("USP_GetRecipeById",
                    new
                    {
                        IDRecipe = recipeByIdIn.RecipeId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public RecipeByFriendlyIdOut RecipeByFriendlyId(RecipeByFriendlyIdIn recipeByFriendlyIdIn)
        {
            RecipeByFriendlyIdOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<RecipeByFriendlyIdOut>("USP_GetRecipeByFriendlyId",
                    new
                    {
                        recipeByFriendlyIdIn.FriendlyId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<StepsByIdRecipeAndLanguageOut> StepsByIdRecipeAndLanguage(
            StepsByIdRecipeAndLanguageIn stepsByIdRecipeAndLanguageIn)
        {
            IEnumerable<StepsByIdRecipeAndLanguageOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<StepsByIdRecipeAndLanguageOut>>("USP_GetMediaById",
                    new
                    {
                        IDRecipe = stepsByIdRecipeAndLanguageIn.RecipeId,
                        IDLanguage = stepsByIdRecipeAndLanguageIn.LanguageId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<StepsForRecipeOut> StepsForRecipe(StepsForRecipeIn stepsForRecipeIn)
        {
            IEnumerable<StepsForRecipeOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<StepsForRecipeOut>>("USP_GetStepsForRecipe",
                    new
                    {
                        IDRecipeLanguage = stepsForRecipeIn.RecipeLanguageId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<RecipePropertyByIdRecipeAndLanguageOut> RecipePropertyByIdRecipeAndLanguage(
            RecipePropertyByIdRecipeAndLanguageIn recipePropertyByIdRecipeAndLanguageIn)
        {
            IEnumerable<RecipePropertyByIdRecipeAndLanguageOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<RecipePropertyByIdRecipeAndLanguageOut>>(
                    "USP_GetRecipePropertyByIdRecipeAndLanguage",
                    new
                    {
                        IDRecipe = recipePropertyByIdRecipeAndLanguageIn.RecipeId,
                        IDLanguage = recipePropertyByIdRecipeAndLanguageIn.LanguageId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<RecipePropertyByRecipeIdOut> RecipePropertyByRecipeId(
            RecipePropertyByRecipeIdIn recipePropertyByRecipeIdIn)
        {
            IEnumerable<RecipePropertyByRecipeIdOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<RecipePropertyByRecipeIdOut>>(
                    "USP_GetRecipePropertyByIdRecipe",
                    new
                    {
                        IDRecipe = recipePropertyByRecipeIdIn.RecipeId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<TopRecipesByLanguageOut> TopRecipesByLanguage(TopRecipesByLanguageIn topRecipesByLanguageIn)
        {
            IEnumerable<TopRecipesByLanguageOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<TopRecipesByLanguageOut>>("USP_GetHomeTopRecipes",
                    new
                    {
                        IDLanguage = topRecipesByLanguageIn.LanguageId,
                        topRecipesByLanguageIn.RecipeToShow
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<BestRecipesByLanguageOut> BestRecipesByLanguage(
            BestRecipesByLanguageIn bestRecipesByLanguageIn)
        {
            IEnumerable<BestRecipesByLanguageOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<BestRecipesByLanguageOut>>("USP_BestRecipesByLanguage",
                    new
                    {
                        IDLanguage = bestRecipesByLanguageIn.LanguageId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public GenerateFriendlyIdOut GenerateFriendlyId(GenerateFriendlyIdIn generateFriendlyIdIn)
        {
            GenerateFriendlyIdOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<GenerateFriendlyIdOut>("USP_GenerateFriendlyId",
                    new
                    {
                        IDRecipeLanguage = generateFriendlyIdIn.RecipeLanguageId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public GenerateAllFriendlyIdOut GenerateAllFriendlyId()
        {
            GenerateAllFriendlyIdOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<GenerateAllFriendlyIdOut>("USP_GenerateAllFriendlyId",
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public FriendlyIdByRecipeLanguageIdOut FriendlyIdByRecipeLanguageId(
            FriendlyIdByRecipeLanguageIdIn friendlyIdByRecipeLanguageIdIn)
        {
            FriendlyIdByRecipeLanguageIdOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<FriendlyIdByRecipeLanguageIdOut>("USP_FriendlyIdByRecipeLanguageId",
                    new
                    {
                        IDRecipeLanguage = friendlyIdByRecipeLanguageIdIn.RecipeLanguageId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public NewRecipeOut NewRecipe(NewRecipeIn newRecipeIn)
        {
            NewRecipeOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<NewRecipeOut>("USP_NewRecipe",
                    new
                    {
                        IDLanguage = newRecipeIn.LanguageId,
                        IDRecipeImage = newRecipeIn.ImageId,
                        IDOwner = newRecipeIn.UserId,
                        newRecipeIn.RecipeName
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public UpdateRecipeLanguageOut UpdateRecipeLanguage(UpdateRecipeLanguageIn updateRecipeLanguageIn)
        {
            UpdateRecipeLanguageOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<UpdateRecipeLanguageOut>("USP_UpdateRecipeLanguage",
                    new
                    {
                        IDRecipeLanguage = updateRecipeLanguageIn.RecipeLanguageId,
                        IDLanguage = updateRecipeLanguageIn.LanguageId,
                        updateRecipeLanguageIn.RecipeName,
                        updateRecipeLanguageIn.RecipeHistory,
                        updateRecipeLanguageIn.RecipeHistoryDate,
                        updateRecipeLanguageIn.RecipeNote,
                        updateRecipeLanguageIn.RecipeSuggestion,
                        GeoIDRegion = updateRecipeLanguageIn.GeoRegionId,
                        updateRecipeLanguageIn.RecipeLanguageTags
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public UpdateRecipeLanguageStepOut UpdateRecipeLanguageStep(
            UpdateRecipeLanguageStepIn updateRecipeLanguageStepIn)
        {
            UpdateRecipeLanguageStepOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<UpdateRecipeLanguageStepOut>("USP_UpdateRecipeLanguageStep",
                    new
                    {
                        IDRecipeStep = updateRecipeLanguageStepIn.RecipeStepId,
                        IDRecipeLanguage = updateRecipeLanguageStepIn.RecipeLanguageId,
                        updateRecipeLanguageStepIn.StepGroup,
                        updateRecipeLanguageStepIn.StepNumber,
                        RecipeStep = updateRecipeLanguageStepIn.StepNumber,
                        StepTimeMinute = updateRecipeLanguageStepIn.Minutes,
                        IDRecipeStepImage = updateRecipeLanguageStepIn.RecipeStepImageId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public NewRecipeLanguageStepOut NewRecipeLanguageStep(NewRecipeLanguageStepIn newRecipeLanguageStepIn)
        {
            NewRecipeLanguageStepOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<NewRecipeLanguageStepOut>("USP_NewRecipeLanguageStep",
                    new
                    {
                        IDRecipeLanguage = newRecipeLanguageStepIn.RecipeLanguageId,
                        newRecipeLanguageStepIn.StepGroup,
                        newRecipeLanguageStepIn.StepNumber,
                        RecipeStep = newRecipeLanguageStepIn.StepNumber,
                        StepTimeMinute = newRecipeLanguageStepIn.Minutes,
                        IDRecipeStepImage = newRecipeLanguageStepIn.RecipeStepImageId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public UpdateRecipeIngredientOut UpdateRecipeIngredient(UpdateRecipeIngredientIn updateRecipeIngredientIn)
        {
            UpdateRecipeIngredientOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<UpdateRecipeIngredientOut>("USP_UpdateRecipeIngredient",
                    new
                    {
                        IDRecipeIngredient = updateRecipeIngredientIn.RecipeIngredientId,
                        IDRecipe = updateRecipeIngredientIn.RecipeId,
                        IDIngredient = updateRecipeIngredientIn.IngredientId,
                        updateRecipeIngredientIn.IsPrincipalIngredient,
                        updateRecipeIngredientIn.QuantityNotStd,
                        IDQuantityNotStd = updateRecipeIngredientIn.QuantityNotStdId,
                        updateRecipeIngredientIn.Quantity,
                        IDQuantityType = updateRecipeIngredientIn.QuantityTypeId,
                        QuantityNotSpecified = updateRecipeIngredientIn.IsQuantityNotSpecified,
                        RecipeIngredientGroupNumber = updateRecipeIngredientIn.GroupNumber,
                        IDRecipeIngredientAlternative = updateRecipeIngredientIn.AlternativeIngredientId,
                        IngredientRelevance = updateRecipeIngredientIn.Relevance
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public AddNewIngredientToRecipeOut AddNewIngredientToRecipe(
            AddNewIngredientToRecipeIn addNewIngredientToRecipeIn)
        {
            AddNewIngredientToRecipeOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<AddNewIngredientToRecipeOut>("USP_AddNewIngredientToRecipe",
                    new
                    {
                        IDRecipe = addNewIngredientToRecipeIn.RecipeId,
                        IDIngredient = addNewIngredientToRecipeIn.IngredientId,
                        addNewIngredientToRecipeIn.IsPrincipalIngredient,
                        addNewIngredientToRecipeIn.QuantityNotStd,
                        IDQuantityNotStd = addNewIngredientToRecipeIn.QuantityNotStdId,
                        addNewIngredientToRecipeIn.Quantity,
                        IDQuantityType = addNewIngredientToRecipeIn.QuantityTypeId,
                        QuantityNotSpecified = addNewIngredientToRecipeIn.IsQuantityNotSpecified,
                        RecipeIngredientGroupNumber = addNewIngredientToRecipeIn.GroupNumber,
                        IDRecipeIngredientAlternative = addNewIngredientToRecipeIn.AlternativeIngredientId,
                        IngredientRelevance = addNewIngredientToRecipeIn.Relevance
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<RecipeLanguageListOut> RecipeLanguageList(RecipeLanguageListIn recipeLanguageListIn)
        {
            IEnumerable<RecipeLanguageListOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<RecipeLanguageListOut>>("USP_RecipeLanguageList",
                    new
                    {
                        IDLanguage = recipeLanguageListIn.LanguageId,
                        offset = recipeLanguageListIn.Offset,
                        count = recipeLanguageListIn.Count,
                        orderBy = recipeLanguageListIn.OrderBy,
                        isAscendent = recipeLanguageListIn.IsAscendant,
                        search = recipeLanguageListIn.Search
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public CalculateNutritionalFactsOut CalculateNutritionalFacts(
            CalculateNutritionalFactsIn calculateNutritionalFactsIn)
        {
            CalculateNutritionalFactsOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<CalculateNutritionalFactsOut>(
                    "USP_CalculateRecipeNutritionalFacts_withTableDirectUpdate",
                    new
                    {
                        IDRecipe = calculateNutritionalFactsIn.RecipeId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public DeleteRecipeOut DeleteRecipe(DeleteRecipeIn deleteRecipeIn)
        {
            DeleteRecipeOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<DeleteRecipeOut>("USP_DeleteRecipe",
                    new
                    {
                        IDRecipe = deleteRecipeIn.RecipeId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<PropertiesListByTypeAndLanguageOut> PropertiesListByTypeAndLanguage(
            PropertiesListByTypeAndLanguageIn propertiesListByTypeAndLanguageIn)
        {
            IEnumerable<PropertiesListByTypeAndLanguageOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<PropertiesListByTypeAndLanguageOut>>(
                    "USP_GetPropertiesListByTypeAndLanguage",
                    new
                    {
                        IDLanguage = propertiesListByTypeAndLanguageIn.LanguageId,
                        IDRecipePropertyType = propertiesListByTypeAndLanguageIn.RecipePropertyTypeId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<PropertiesListByTypeLanguageAndRecipeOut> PropertiesListByTypeLanguageAndRecipe(
            PropertiesListByTypeLanguageAndRecipeIn propertiesListByTypeLanguageAndRecipeIn)
        {
            IEnumerable<PropertiesListByTypeLanguageAndRecipeOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<PropertiesListByTypeLanguageAndRecipeOut>>(
                    "USP_GetPropertiesListByTypeLanguageAndRecipe",
                    new
                    {
                        IDLanguage = propertiesListByTypeLanguageAndRecipeIn.LanguageId,
                        IDRecipePropertyType = propertiesListByTypeLanguageAndRecipeIn.RecipePropertyTypeId,
                        IDRecipe = propertiesListByTypeLanguageAndRecipeIn.RecipeId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public AddOrUpdatePropertyValueOut AddOrUpdatePropertyValue(
            AddOrUpdatePropertyValueIn addOrUpdatePropertyValueIn)
        {
            AddOrUpdatePropertyValueOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<AddOrUpdatePropertyValueOut>("USP_AddOrUpdatePropertyValue",
                    new
                    {
                        IDRecipe = addOrUpdatePropertyValueIn.RecipeId,
                        IDRecipeProperty = addOrUpdatePropertyValueIn.RecipePropertyId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public DeletePropertyValueOut DeletePropertyValue(DeletePropertyValueIn deletePropertyValueIn)
        {
            DeletePropertyValueOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<DeletePropertyValueOut>("USP_DeletePropertyValue",
                    new
                    {
                        IDRecipe = deletePropertyValueIn.RecipeId,
                        IDRecipePropertyValue = deletePropertyValueIn.RecipePropertyValueId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<RecipeSiteMapOut> RecipeSiteMap(RecipeSiteMapIn recipeSiteMapIn)
        {
            IEnumerable<RecipeSiteMapOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<RecipeSiteMapOut>>("USP_SiteMapRecipe",
                    new
                    {
                        IDLanguage = recipeSiteMapIn.LanguageId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<RecipesByOwnerOut> RecipesByOwner(RecipesByOwnerIn recipesByOwnerIn)
        {
            IEnumerable<RecipesByOwnerOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<RecipesByOwnerOut>>("USP_RecipesByOwner",
                    new
                    {
                        IDOwner = recipesByOwnerIn.OwnerId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<RecipesToTranslateOut> RecipesToTranslate(RecipesToTranslateIn recipesToTranslateIn)
        {
            IEnumerable<RecipesToTranslateOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<RecipesToTranslateOut>>("USP_GetRecipeToTranslate",
                    new
                    {
                        IDLanguageFrom = recipesToTranslateIn.LanguageIdFrom,
                        IDLanguageTo = recipesToTranslateIn.LanguageIdTo,
                        NumRow = recipesToTranslateIn.NumberOfRecipesToTranslate
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<SimilarRecipesOut> SimilarRecipes(SimilarRecipesIn similarRecipesIn)
        {
            IEnumerable<SimilarRecipesOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<SimilarRecipesOut>>("USP_GetSimilarRecipes",
                    new
                    {
                        IDRecipe = similarRecipesIn.RecipeId,
                        similarRecipesIn.RecipeName,
                        similarRecipesIn.Vegan,
                        similarRecipesIn.Vegetarian,
                        similarRecipesIn.GlutenFree,
                        IDLanguage = similarRecipesIn.LanguageId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public CalculateRecipeTagsOut CalculateRecipeTags(CalculateRecipeTagsIn calculateRecipeTagsIn)
        {
            CalculateRecipeTagsOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<CalculateRecipeTagsOut>("USP_CalculateRecipeTags",
                    new
                    {
                        IDRecipe = calculateRecipeTagsIn.RecipeId,
                        IDLanguage = calculateRecipeTagsIn.LanguageId,
                        AddIngredientList = calculateRecipeTagsIn.IncludeIngredientList,
                        AddDynamicPropertyList = calculateRecipeTagsIn.IncludeDynamicProp,
                        calculateRecipeTagsIn.IncludeIngredientCategory
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<StepsToTranslateOut> StepsToTranslate(StepsToTranslateIn stepsToTranslateIn)
        {
            IEnumerable<StepsToTranslateOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<StepsToTranslateOut>>("USP_GetRecipeStepsToTranslate",
                    new
                    {
                        IDLanguageFrom = stepsToTranslateIn.LanguageIdFrom,
                        IDLanguageTo = stepsToTranslateIn.LanguageIdTo,
                        NumRow = stepsToTranslateIn.NumberOfStepsToTranslate
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<RecipeLanguageTagsOut> RecipeLanguageTags(RecipeLanguageTagsIn recipeLanguageTagsIn)
        {
            IEnumerable<RecipeLanguageTagsOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<RecipeLanguageTagsOut>>("USP_RecipeLanguageTags",
                    new
                    {
                        IDLanguage = recipeLanguageTagsIn.LanguageId,
                        recipeLanguageTagsIn.TagStartWord
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<TagsByRecipeAndLanguageOut> TagsByRecipeAndLanguage(
            TagsByRecipeAndLanguageIn recipeAndLanguageIn)
        {
            IEnumerable<TagsByRecipeAndLanguageOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<TagsByRecipeAndLanguageOut>>(
                    "USP_TagsByRecipeAndLanguage",
                    new
                    {
                        IDLanguage = recipeAndLanguageIn.LanguageId,
                        IDRecipeTag = recipeAndLanguageIn.RecipeLanguageTagId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<PropertiesByRecipeAndLanguageOut> PropertiesByRecipeAndLanguage(
            PropertiesByRecipeAndLanguageIn propertiesByRecipeAndLanguageIn)
        {
            IEnumerable<PropertiesByRecipeAndLanguageOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<PropertiesByRecipeAndLanguageOut>>(
                    "USP_PropertiesByRecipeAndLanguage",
                    new
                    {
                        IDLanguage = propertiesByRecipeAndLanguageIn.LanguageId,
                        IDRecipe = propertiesByRecipeAndLanguageIn.RecipeId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<RecipesInRecipeBookOut> RecipesInRecipeBook(RecipesInRecipeBookIn recipesInRecipeBookIn)
        {
            IEnumerable<RecipesInRecipeBookOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<RecipesInRecipeBookOut>>("USP_GetRecipesInRecipeBook",
                    new
                    {
                        IDUser = recipesInRecipeBookIn.UserId,
                        RecipeType = recipesInRecipeBookIn.recipeType,
                        ShowFilter = recipesInRecipeBookIn.showFilter,
                        RecipeNameFilter = recipesInRecipeBookIn.recipeNameFilter,
                        Vegan = recipesInRecipeBookIn.vegan,
                        Vegetarian = recipesInRecipeBookIn.vegetarian,
                        GlutenFree = recipesInRecipeBookIn.glutenFree,
                        LightThreshold = recipesInRecipeBookIn.lightThreshold,
                        QuickThreshold = recipesInRecipeBookIn.quickThreshold,
                        ShowDraft = recipesInRecipeBookIn.showDraft,
                        IDLanguage = recipesInRecipeBookIn.LanguageId,
                        OffsetRows = recipesInRecipeBookIn.rowOffSet,
                        FetchRows = recipesInRecipeBookIn.fetchRows
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public NumberOfRecipesInsertedByUserOut NumberOfRecipesInsertedByUser(
            NumberOfRecipesInsertedByUserIn numberOfRecipesInsertedByUserIn)
        {
            NumberOfRecipesInsertedByUserOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<NumberOfRecipesInsertedByUserOut>("USP_NumberOfRecipesInsertedByUser",
                    new
                    {
                        IDUser = numberOfRecipesInsertedByUserIn.UserId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public CheckIfRecipeIsInBookOut CheckIfRecipeIsInBook(CheckIfRecipeIsInBookIn checkIfRecipeIsInBookIn)
        {
            CheckIfRecipeIsInBookOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<CheckIfRecipeIsInBookOut>("USP_CheckIfRecipeIsInBook",
                    new
                    {
                        IDUser = checkIfRecipeIsInBookIn.UserId,
                        IDRecipe = checkIfRecipeIsInBookIn.RecipeId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public SaveRecipeInBookOut SaveRecipeInBook(SaveRecipeInBookIn saveRecipeInBookIn)
        {
            SaveRecipeInBookOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<SaveRecipeInBookOut>("USP_ManageRecipeBookRecipe",
                    new
                    {
                        IDRecipeBookRecipe = saveRecipeInBookIn.idRecipeBookRecipe,
                        IDUser = saveRecipeInBookIn.idUser,
                        IDRecipe = saveRecipeInBookIn.idRecipe,
                        RecipeAddedOn = saveRecipeInBookIn.recipeAddedOn,
                        RecipeOrder = saveRecipeInBookIn.recipeOrder,
                        isDeleteOperation = false
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public DeleteRecipeFromBookOut DeleteRecipeFromBook(DeleteRecipeFromBookIn deleteRecipeFromBookIn)
        {
            DeleteRecipeFromBookOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<DeleteRecipeFromBookOut>("USP_ManageRecipeBookRecipe",
                    new
                    {
                        IDRecipeBookRecipe = deleteRecipeFromBookIn.idRecipeBookRecipe,
                        IDUser = deleteRecipeFromBookIn.idUser,
                        IDRecipe = deleteRecipeFromBookIn.idRecipe,
                        RecipeAddedOn = deleteRecipeFromBookIn.recipeAddedOn,
                        RecipeOrder = deleteRecipeFromBookIn.recipeOrder,
                        isDeleteOperation = false
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public RecipeVoteOut RecipeVote(RecipeVoteIn recipeVoteIn)
        {
            RecipeVoteOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<RecipeVoteOut>("USP_RecipeVote",
                    new
                    {
                        IDRecipe = recipeVoteIn.RecipeId,
                        IDUser = recipeVoteIn.UserId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public RecipeAvgVoteOut RecipeAvgVote(RecipeAvgVoteIn recipeAvgVoteIn)
        {
            RecipeAvgVoteOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<RecipeAvgVoteOut>("USP_RecipeVote",
                    new
                    {
                        IDRecipe = recipeAvgVoteIn.RecipeId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public SaveVoteOut SaveVote(SaveVoteIn saveVoteIn)
        {
            SaveVoteOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<SaveVoteOut>("USP_ManageRecipeVote",
                    new
                    {
                        IDRecipeVote = saveVoteIn.IdRecipeVote,
                        IDRecipe = saveVoteIn.IdRecipe,
                        IDUser = saveVoteIn.IdUser,
                        saveVoteIn.RecipeVoteDate,
                        RecipeVote = saveVoteIn.Vote,
                        isDeleteOperation = false
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public DeleteVoteOut DeleteVote(DeleteVoteIn deleteVoteIn)
        {
            DeleteVoteOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<DeleteVoteOut>("USP_ManageRecipeVote",
                    new
                    {
                        IDRecipeVote = deleteVoteIn.IdRecipeVote,
                        IDRecipe = deleteVoteIn.IdRecipe,
                        IDUser = deleteVoteIn.IdUser,
                        deleteVoteIn.RecipeVoteDate,
                        RecipeVote = deleteVoteIn.Vote,
                        isDeleteOperation = true
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public FeedbackInfoOut FeedbackInfo(FeedbackInfoIn feedbackInfoIn)
        {
            FeedbackInfoOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<FeedbackInfoOut>("USP_FeedbackInfo",
                    new
                    {
                        IDRecipeFeedback = feedbackInfoIn.FeedbackId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public FeedbackByRecipeAndUserOut FeedbackByRecipeAndUser(FeedbackByRecipeAndUserIn feedbackByRecipeAndUserIn)
        {
            FeedbackByRecipeAndUserOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<FeedbackByRecipeAndUserOut>("USP_GetRecipeLikeForUser",
                    new
                    {
                        IDRecipe = feedbackByRecipeAndUserIn.RecipeId,
                        IDUser = feedbackByRecipeAndUserIn.UserId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public SaveFeedbackOut SaveFeedback(SaveFeedbackIn saveFeedbackIn)
        {
            SaveFeedbackOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<SaveFeedbackOut>("USP_ManageRecipeFeedback",
                    new
                    {
                        IDRecipeFeedback = saveFeedbackIn.idRecipeFeedback,
                        IDRecipe = saveFeedbackIn.recipeId,
                        IDUser = saveFeedbackIn.userId,
                        FeedbackType = saveFeedbackIn.feedbackType,
                        FeedbackText = saveFeedbackIn.feedbackText,
                        FeedbackDate = DateTime.UtcNow,
                        isDeleteOperation = false
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public DeleteFeedbackOut DeleteFeedback(DeleteFeedbackIn deleteFeedbackIn)
        {
            DeleteFeedbackOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<DeleteFeedbackOut>("USP_ManageRecipeFeedback",
                    new
                    {
                        IDRecipeFeedback = deleteFeedbackIn.IdRecipeFeedback
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<LikesForRecipeOut> LikesForRecipe(LikesForRecipeIn likesForRecipeIn)
        {
            IEnumerable<LikesForRecipeOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<LikesForRecipeOut>>("USP_GetRecipeFeedbacks",
                    new
                    {
                        IDRecipe = likesForRecipeIn.RecipeId,
                        IDFeedbackType = 1,
                        OffsetRows = likesForRecipeIn.rowOffset,
                        FetchRows = likesForRecipeIn.fetchRows
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<CommentsForRecipeOut> CommentsForRecipe(CommentsForRecipeIn commentsForRecipeIn)
        {
            IEnumerable<CommentsForRecipeOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<CommentsForRecipeOut>>("USP_GetRecipeFeedbacks",
                    new
                    {
                        IDRecipe = commentsForRecipeIn.RecipeId,
                        IDFeedbackType = 2,
                        OffsetRows = commentsForRecipeIn.rowOffset,
                        FetchRows = commentsForRecipeIn.fetchRows
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public AddNewRecipeTranslationOut AddNewRecipeTranslation(AddNewRecipeTranslationIn addNewRecipeTranslationIn)
        {
            AddNewRecipeTranslationOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<AddNewRecipeTranslationOut>("USP_AddNewRecipeTranslation",
                    new
                    {
                        IDRecipe = addNewRecipeTranslationIn.RecipeId,
                        IDLanguage = addNewRecipeTranslationIn.LanguageId,
                        addNewRecipeTranslationIn.RecipeName,
                        RecipeLanguageAutoTranslate = addNewRecipeTranslationIn.IsAutoTranslate,
                        addNewRecipeTranslationIn.RecipeHistory,
                        addNewRecipeTranslationIn.RecipeNote,
                        addNewRecipeTranslationIn.RecipeSuggestion,
                        addNewRecipeTranslationIn.RecipeLanguageTags,
                        addNewRecipeTranslationIn.TranslatedBy
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }
    }
}