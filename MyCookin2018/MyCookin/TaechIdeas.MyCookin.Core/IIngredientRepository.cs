using System.Collections.Generic;
using TaechIdeas.MyCookin.Core.Dto;

namespace TaechIdeas.MyCookin.Core
{
    public interface IIngredientRepository
    {
        IEnumerable<IngredientLanguageListOut>
            IngredientLanguageList(IngredientLanguageListIn ingredientLanguageListIn);

        IEnumerable<SearchIngredientByLanguageAndNameOut> SearchIngredientByLanguageAndName(
            SearchIngredientByLanguageAndNameIn searchIngredientByLanguageAndNameIn);

        IEnumerable<IngredientQuantityTypeListOut> IngredientQuantityTypeList(
            IngredientQuantityTypeListIn ingredientQuantityTypeListIn);

        IEnumerable<IngredientModifiedByUserOut> IngredientModifiedByUser(
            IngredientModifiedByUserIn ingredientModifiedByUserIn);

        IEnumerable<IngredientCheckedOut> IngredientsChecked(IngredientCheckedIn ingredientCheckedIn);

        GenerateIngredientFriendlyIdOut GenerateFriendlyId(
            GenerateIngredientFriendlyIdIn generateIngredientFriendlyIdIn);

        GenerateAllIngredientFriendlyIdOut GenerateAllIngredientFriendlyId();
        IngredientByFriendlyIdOut IngredientByFriendlyId(IngredientByFriendlyIdIn ingredientByFriendlyIdIn);

        IEnumerable<CategoriesByIngredientIdOut> CategoriesByIngredientId(
            CategoriesByIngredientIdIn categoriesByIngredientIdIn);

        IEnumerable<AllowedQuantitiesByIngredientIdOut> AllowedQuantitiesByIngredientId(
            AllowedQuantitiesByIngredientIdIn allowedQuantitiesByIngredientIdIn);

        InsertIngredientOut InsertIngredient(InsertIngredientIn insertIngredientIn);
        UpdateIngredientOut UpdateIngredient(UpdateIngredientIn updateIngredientIn);
        InsertIngredientLanguageOut InsertIngredientLanguage(InsertIngredientLanguageIn insertIngredientLanguageIn);
        UpdateIngredientLanguageOut UpdateIngredientLanguage(UpdateIngredientLanguageIn updateIngredientLanguageIn);
        DeleteIngredientOut DeleteIngredient(DeleteIngredientIn deleteIngredientIn);

        IngredientQuantityTypeLanguageOut IngredientQuantityTypeLanguage(
            IngredientQuantityTypeLanguageIn ingredientQuantityTypeLanguageIn);

        QuantityNotStdByIdAndLanguageOut QuantityNotStdByIdAndLanguage(
            QuantityNotStdByIdAndLanguageIn quantityNotStdByIdAndLanguageIn);

        IEnumerable<IngredientsByIdRecipeAndLanguageOut> IngredientsByIdRecipeAndLanguage(
            IngredientsByIdRecipeAndLanguageIn ingredientsByIdRecipeAndLanguageIn);

        IEnumerable<IngredientsByIdRecipeOut> IngredientsByIdRecipe(IngredientsByIdRecipeIn ingredientsByIdRecipeIn);

        IEnumerable<IngredientsToTranslateOut>
            IngredientsToTranslate(IngredientsToTranslateIn ingredientsToTranslateIn);

        AddNewIngredientTranslationOut AddNewIngredientTranslation(
            AddNewIngredientTranslationIn addNewIngredientTranslationIn);

        /*****************************************************/
        //BEVERAGE
        /*****************************************************/
        IEnumerable<SearchBeverageByLanguageOut> SearchBeverageByLanguage(
            SearchBeverageByLanguageIn searchBeverageByLanguageIn);

        AddRecipeBeverageOut AddRecipeBeverage(AddRecipeBeverageIn addRecipeBeverageIn);
        DeleteRecipeBeverageOut DeleteRecipeBeverage(DeleteRecipeBeverageIn deleteRecipeBeverageIn);

        IEnumerable<SuggestedBeverageByRecipeOut> SuggestedBeverageByRecipe(
            SuggestedBeverageByRecipeIn suggestedBeverageByRecipeIn);
    }
}