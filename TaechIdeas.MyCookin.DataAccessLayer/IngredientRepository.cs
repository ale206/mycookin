using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using TaechIdeas.MyCookin.Core;
using TaechIdeas.MyCookin.Core.Dto;

namespace TaechIdeas.MyCookin.DataAccessLayer
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly string _connectionString;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public IngredientRepository(IDbConnectionFactory dbConnectionFactory, IConfiguration configuration)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _connectionString = configuration.GetConnectionString("DBRecipesConnectionString");
        }

        public IEnumerable<IngredientLanguageListOut> IngredientLanguageList(IngredientLanguageListIn ingredientLanguageListIn)
        {
            IEnumerable<IngredientLanguageListOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<IngredientLanguageListOut>>("USP_IngredientLanguageList",
                    new
                    {
                        IDLanguage = ingredientLanguageListIn.LanguageId,
                        offset = ingredientLanguageListIn.Offset,
                        count = ingredientLanguageListIn.Count,
                        orderBy = ingredientLanguageListIn.OrderBy,
                        isAscendent = ingredientLanguageListIn.IsAscendant,
                        search = ingredientLanguageListIn.Search
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<SearchIngredientByLanguageAndNameOut> SearchIngredientByLanguageAndName(SearchIngredientByLanguageAndNameIn searchIngredientByLanguageAndNameIn)
        {
            IEnumerable<SearchIngredientByLanguageAndNameOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<SearchIngredientByLanguageAndNameOut>>("USP_SearchIngredientByLanguageAndName",
                    new
                    {
                        searchIngredientByLanguageAndNameIn.IngredientName,
                        IDLanguage = searchIngredientByLanguageAndNameIn.LanguageId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<IngredientQuantityTypeListOut> IngredientQuantityTypeList(IngredientQuantityTypeListIn ingredientQuantityTypeListIn)
        {
            IEnumerable<IngredientQuantityTypeListOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<IngredientQuantityTypeListOut>>("USP_GetIngredientQuantityTypeList",
                    new
                    {
                        IDLanguage = ingredientQuantityTypeListIn.LanguageId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<IngredientModifiedByUserOut> IngredientModifiedByUser(IngredientModifiedByUserIn ingredientModifiedByUserIn)
        {
            IEnumerable<IngredientModifiedByUserOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<IngredientModifiedByUserOut>>("USP_IngredientModifiedByUser",
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<IngredientCheckedOut> IngredientsChecked(IngredientCheckedIn ingredientCheckedIn)
        {
            IEnumerable<IngredientCheckedOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<IngredientCheckedOut>>("USP_GetAllIngredientsChecked",
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public GenerateIngredientFriendlyIdOut GenerateFriendlyId(GenerateIngredientFriendlyIdIn generateIngredientFriendlyIdIn)
        {
            GenerateIngredientFriendlyIdOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<GenerateIngredientFriendlyIdOut>("USP_GenerateIngredientFriendlyId",
                    new
                    {
                        IDRecipeLanguage = generateIngredientFriendlyIdIn.IngredientLanguageId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public GenerateAllIngredientFriendlyIdOut GenerateAllIngredientFriendlyId()
        {
            GenerateAllIngredientFriendlyIdOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<GenerateAllIngredientFriendlyIdOut>("USP_GenerateAllIngredientFriendlyId",
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IngredientByFriendlyIdOut IngredientByFriendlyId(IngredientByFriendlyIdIn ingredientByFriendlyIdIn)
        {
            IngredientByFriendlyIdOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IngredientByFriendlyIdOut>("USP_GetIngredientByFriendlyId",
                    new
                    {
                        ingredientByFriendlyIdIn.FriendlyId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<CategoriesByIngredientIdOut> CategoriesByIngredientId(CategoriesByIngredientIdIn categoriesByIngredientIdIn)
        {
            IEnumerable<CategoriesByIngredientIdOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<CategoriesByIngredientIdOut>>("USP_CategoriesByIngredientId",
                    new
                    {
                        IDIngredient = categoriesByIngredientIdIn.IngredientId,
                        IDLanguage = categoriesByIngredientIdIn.LanguageId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<AllowedQuantitiesByIngredientIdOut> AllowedQuantitiesByIngredientId(AllowedQuantitiesByIngredientIdIn allowedQuantitiesByIngredientIdIn)
        {
            IEnumerable<AllowedQuantitiesByIngredientIdOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<AllowedQuantitiesByIngredientIdOut>>("USP_AllowedQuantitiesByIngredientId",
                    new
                    {
                        IDIngredient = allowedQuantitiesByIngredientIdIn.IngredientId,
                        IDLanguage = allowedQuantitiesByIngredientIdIn.LanguageId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public InsertIngredientOut InsertIngredient(InsertIngredientIn insertIngredientIn)
        {
            InsertIngredientOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<InsertIngredientOut>("USP_InsertIngredient",
                    new
                    {
                        IDIngredientPreparationRecipe = insertIngredientIn.IngredientPreparationRecipeId,
                        IDIngredientImage = insertIngredientIn.IngredientImageId,
                        insertIngredientIn.AverageWeightOfOnePiece,
                        Kcal100gr = insertIngredientIn.Kcal100Gr,
                        grProteins = insertIngredientIn.GrProteins,
                        grFats = insertIngredientIn.GrFats,
                        grCarbohydrates = insertIngredientIn.GrCarbohydrates,
                        grAlcohol = insertIngredientIn.GrAlcohol,
                        mgCalcium = insertIngredientIn.MgCalcium,
                        mgSodium = insertIngredientIn.MgSodium,
                        mgPhosphorus = insertIngredientIn.MgPhosphorus,
                        mgPotassium = insertIngredientIn.MgPotassium,
                        mgIron = insertIngredientIn.MgIron,
                        mgMagnesium = insertIngredientIn.MgMagnesium,
                        mcgVitaminA = insertIngredientIn.McgVitaminA,
                        mgVitaminB1 = insertIngredientIn.MgVitaminB1,
                        mgVitaminB2 = insertIngredientIn.MgVitaminB2,
                        mcgVitaminB9 = insertIngredientIn.McgVitaminB9,
                        mcgVitaminB12 = insertIngredientIn.McgVitaminB12,
                        mgVitaminC = insertIngredientIn.MgVitaminC,
                        grSaturatedFat = insertIngredientIn.GrSaturatedFat,
                        grMonounsaturredFat = insertIngredientIn.GrMonounsaturredFat,
                        grPolyunsaturredFat = insertIngredientIn.GrPolyunsaturredFat,
                        mgCholesterol = insertIngredientIn.MgCholesterol,
                        mgPhytosterols = insertIngredientIn.MgPhytosterols,
                        mgOmega3 = insertIngredientIn.MgOmega3,
                        insertIngredientIn.IsForBaby,
                        insertIngredientIn.IsVegetarian,
                        insertIngredientIn.IsGlutenFree,
                        insertIngredientIn.IsVegan,
                        insertIngredientIn.IsHotSpicy,
                        insertIngredientIn.Checked,
                        insertIngredientIn.IngredientCreatedBy,
                        insertIngredientIn.IngredientCreationDate,
                        insertIngredientIn.IngredientModifiedByUser,
                        insertIngredientIn.IngredientLastMod,
                        insertIngredientIn.IngredientEnabled,
                        insertIngredientIn.January,
                        insertIngredientIn.February,
                        insertIngredientIn.March,
                        insertIngredientIn.April,
                        insertIngredientIn.May,
                        insertIngredientIn.June,
                        insertIngredientIn.July,
                        insertIngredientIn.August,
                        insertIngredientIn.September,
                        insertIngredientIn.October,
                        insertIngredientIn.November,
                        insertIngredientIn.December,
                        grDietaryFiber = insertIngredientIn.GrDietaryFiber,
                        grStarch = insertIngredientIn.GrStarch,
                        grSugar = insertIngredientIn.GrSugar
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public UpdateIngredientOut UpdateIngredient(UpdateIngredientIn updateIngredientIn)
        {
            UpdateIngredientOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<UpdateIngredientOut>("USP_UpdateIngredient",
                    new
                    {
                        IDIngredient = updateIngredientIn.IngredientId,
                        IDIngredientPreparationRecipe = updateIngredientIn.IngredientPreparationRecipeId,
                        IDIngredientImage = updateIngredientIn.IngredientImageId,
                        updateIngredientIn.AverageWeightOfOnePiece,
                        Kcal100gr = updateIngredientIn.Kcal100Gr,
                        grProteins = updateIngredientIn.GrProteins,
                        grFats = updateIngredientIn.GrFats,
                        grCarbohydrates = updateIngredientIn.GrCarbohydrates,
                        grAlcohol = updateIngredientIn.GrAlcohol,
                        mgCalcium = updateIngredientIn.MgCalcium,
                        mgSodium = updateIngredientIn.MgSodium,
                        mgPhosphorus = updateIngredientIn.MgPhosphorus,
                        mgPotassium = updateIngredientIn.MgPotassium,
                        mgIron = updateIngredientIn.MgIron,
                        mgMagnesium = updateIngredientIn.MgMagnesium,
                        mcgVitaminA = updateIngredientIn.McgVitaminA,
                        mgVitaminB1 = updateIngredientIn.MgVitaminB1,
                        mgVitaminB2 = updateIngredientIn.MgVitaminB2,
                        mcgVitaminB9 = updateIngredientIn.McgVitaminB9,
                        mcgVitaminB12 = updateIngredientIn.McgVitaminB12,
                        mgVitaminC = updateIngredientIn.MgVitaminC,
                        grSaturatedFat = updateIngredientIn.GrSaturatedFat,
                        grMonounsaturredFat = updateIngredientIn.GrMonounsaturredFat,
                        grPolyunsaturredFat = updateIngredientIn.GrPolyunsaturredFat,
                        mgCholesterol = updateIngredientIn.MgCholesterol,
                        mgPhytosterols = updateIngredientIn.MgPhytosterols,
                        mgOmega3 = updateIngredientIn.MgOmega3,
                        updateIngredientIn.IsForBaby,
                        updateIngredientIn.IsVegetarian,
                        updateIngredientIn.IsGlutenFree,
                        updateIngredientIn.IsVegan,
                        updateIngredientIn.IsHotSpicy,
                        updateIngredientIn.Checked,
                        updateIngredientIn.IngredientModifiedByUser,
                        updateIngredientIn.January,
                        updateIngredientIn.February,
                        updateIngredientIn.March,
                        updateIngredientIn.April,
                        updateIngredientIn.May,
                        updateIngredientIn.June,
                        updateIngredientIn.July,
                        updateIngredientIn.August,
                        updateIngredientIn.September,
                        updateIngredientIn.October,
                        updateIngredientIn.November,
                        updateIngredientIn.December,
                        grDietaryFiber = updateIngredientIn.GrDietaryFiber,
                        grStarch = updateIngredientIn.GrStarch,
                        grSugar = updateIngredientIn.GrSugar
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public InsertIngredientLanguageOut InsertIngredientLanguage(InsertIngredientLanguageIn insertIngredientLanguageIn)
        {
            InsertIngredientLanguageOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<InsertIngredientLanguageOut>("USP_GetMediaById",
                    new
                    {
                        insertIngredientLanguageIn.IngredientSingular,
                        insertIngredientLanguageIn.IngredientPlural,
                        insertIngredientLanguageIn.IngredientDescription,
                        isAutoTranslate = insertIngredientLanguageIn.IsAutoTranslate,
                        IDIngredient = insertIngredientLanguageIn.IngredientId,
                        IDLanguage = insertIngredientLanguageIn.LanguageId,
                        GeoIDRegion = insertIngredientLanguageIn.GeoRegionId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public UpdateIngredientLanguageOut UpdateIngredientLanguage(UpdateIngredientLanguageIn updateIngredientLanguageIn)
        {
            UpdateIngredientLanguageOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<UpdateIngredientLanguageOut>("USP_UpdateIngredientLanguage",
                    new
                    {
                        IDIngredientLanguage = updateIngredientLanguageIn.IngredientLanguageId,
                        updateIngredientLanguageIn.IngredientSingular,
                        updateIngredientLanguageIn.IngredientPlural,
                        updateIngredientLanguageIn.IngredientDescription,
                        isAutoTranslate = updateIngredientLanguageIn.IsAutoTranslate,
                        IDIngredient = updateIngredientLanguageIn.IngredientId,
                        IDLanguage = updateIngredientLanguageIn.LanguageId,
                        GeoIDRegion = updateIngredientLanguageIn.GeoRegionId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public DeleteIngredientOut DeleteIngredient(DeleteIngredientIn deleteIngredientIn)
        {
            DeleteIngredientOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<DeleteIngredientOut>("USP_DeleteIngredient",
                    new
                    {
                        IDIngredient = deleteIngredientIn.IngredientId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IngredientQuantityTypeLanguageOut IngredientQuantityTypeLanguage(IngredientQuantityTypeLanguageIn ingredientQuantityTypeLanguageIn)
        {
            IngredientQuantityTypeLanguageOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IngredientQuantityTypeLanguageOut>("USP_GetIngredientsQuantityTypesLangByID",
                    new
                    {
                        IDIngredientQuantityTypeLanguage = ingredientQuantityTypeLanguageIn.IngredientQuantityTypeId,
                        IDLanguage = ingredientQuantityTypeLanguageIn.LanguageId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public QuantityNotStdByIdAndLanguageOut QuantityNotStdByIdAndLanguage(QuantityNotStdByIdAndLanguageIn quantityNotStdByIdAndLanguageIn)
        {
            QuantityNotStdByIdAndLanguageOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<QuantityNotStdByIdAndLanguageOut>("USP_GetQuantityNotStdByIdAndLanguage",
                    new
                    {
                        IDQuantityNotStd = quantityNotStdByIdAndLanguageIn.QuantityNotStdId,
                        IDLanguage = quantityNotStdByIdAndLanguageIn.LanguageId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<IngredientsByIdRecipeAndLanguageOut> IngredientsByIdRecipeAndLanguage(IngredientsByIdRecipeAndLanguageIn ingredientsByIdRecipeAndLanguageIn)
        {
            IEnumerable<IngredientsByIdRecipeAndLanguageOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<IngredientsByIdRecipeAndLanguageOut>>("USP_GetDetailedIngredientsByIdRecipeAndLanguage",
                    new
                    {
                        IDRecipe = ingredientsByIdRecipeAndLanguageIn.RecipeId,
                        IDLanguage = ingredientsByIdRecipeAndLanguageIn.LanguageId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<IngredientsByIdRecipeOut> IngredientsByIdRecipe(IngredientsByIdRecipeIn ingredientsByIdRecipeIn)
        {
            IEnumerable<IngredientsByIdRecipeOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<IngredientsByIdRecipeOut>>("USP_GetDetailedIngredientsByIdRecipe",
                    new
                    {
                        IDRecipe = ingredientsByIdRecipeIn.RecipeId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<IngredientsToTranslateOut> IngredientsToTranslate(IngredientsToTranslateIn ingredientsToTranslateIn)
        {
            IEnumerable<IngredientsToTranslateOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<IngredientsToTranslateOut>>("USP_GetIngredientToTranslate",
                    new
                    {
                        IDLanguageFrom = ingredientsToTranslateIn.LanguageIdFrom,
                        IDLanguageTo = ingredientsToTranslateIn.LanguageIdTo,
                        NumRow = ingredientsToTranslateIn.NumberOfIngredientsToTranslate
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public AddNewIngredientTranslationOut AddNewIngredientTranslation(AddNewIngredientTranslationIn addNewIngredientTranslationIn)
        {
            AddNewIngredientTranslationOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<AddNewIngredientTranslationOut>("USP_AddNewIngredientTranslation",
                    new
                    {
                        addNewIngredientTranslationIn.IngredientSingular,
                        addNewIngredientTranslationIn.IngredientPlural,
                        addNewIngredientTranslationIn.IngredientDescription,
                        isAutoTranslate = addNewIngredientTranslationIn.IsAutoTranslate,
                        IDIngredient = addNewIngredientTranslationIn.IngredientId,
                        IDLanguage = addNewIngredientTranslationIn.LanguageId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        /*****************************************************/
        //BEVERAGE
        /*****************************************************/

        public IEnumerable<SearchBeverageByLanguageOut> SearchBeverageByLanguage(SearchBeverageByLanguageIn searchBeverageByLanguageIn)
        {
            IEnumerable<SearchBeverageByLanguageOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<SearchBeverageByLanguageOut>>("USP_GetBeverageByNameContains",
                    new
                    {
                        searchBeverageByLanguageIn.BeverageName,
                        IDLanguage = searchBeverageByLanguageIn.LanguageId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public AddRecipeBeverageOut AddRecipeBeverage(AddRecipeBeverageIn addRecipeBeverageIn)
        {
            AddRecipeBeverageOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<AddRecipeBeverageOut>("USP_AddRecipeBeverage",
                    new
                    {
                        IDRecipe = addRecipeBeverageIn.RecipeId,
                        IDBeverage = addRecipeBeverageIn.BeverageId,
                        IDUserSuggestedBy = addRecipeBeverageIn.UserId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public DeleteRecipeBeverageOut DeleteRecipeBeverage(DeleteRecipeBeverageIn deleteRecipeBeverageIn)
        {
            DeleteRecipeBeverageOut result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<DeleteRecipeBeverageOut>("USP_DeleteRecipeBeverage",
                    new
                    {
                        IDBeverageRecipe = deleteRecipeBeverageIn.BeverageRecipeId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }

        public IEnumerable<SuggestedBeverageByRecipeOut> SuggestedBeverageByRecipe(SuggestedBeverageByRecipeIn suggestedBeverageByRecipeIn)
        {
            IEnumerable<SuggestedBeverageByRecipeOut> result;

            using (var connection = _dbConnectionFactory.GetConnection(_connectionString))
            {
                result = connection.ExecuteScalar<IEnumerable<SuggestedBeverageByRecipeOut>>("USP_SuggestedBeverageByRecipe",
                    new
                    {
                        IDRecipe = suggestedBeverageByRecipeIn.RecipeId
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            return result;
        }
    }
}