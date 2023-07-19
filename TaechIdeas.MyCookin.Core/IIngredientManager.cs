using System.Collections.Generic;
using TaechIdeas.MyCookin.Core.Dto;

namespace TaechIdeas.MyCookin.Core
{
    public interface IIngredientManager
    {
        IEnumerable<IngredientLanguageListOutput> IngredientLanguageList(IngredientLanguageListInput ingredientLanguageListInput);
        IEnumerable<SearchIngredientByLanguageAndNameOutput> SearchIngredientByLanguageAndName(SearchIngredientByLanguageAndNameInput searchIngredientByLanguageAndNameInput);
        IEnumerable<IngredientQuantityTypeListOutput> IngredientQuantityTypeList(IngredientQuantityTypeListInput ingredientQuantityTypeListInput);
        GenerateIngredientFriendlyIdOutput GenerateIngredientFriendlyId(GenerateIngredientFriendlyIdInput generateIngredientFriendlyIdInput);
        bool GenerateAllIngredientFriendlyId();
        IngredientByFriendlyIdOutput IngredientByFriendlyId(IngredientByFriendlyIdInput ingredientByFriendlyIdIn);
        IEnumerable<AllowedQuantitiesByIngredientIdOutput> AllowedQuantitiesByIngredientId(AllowedQuantitiesByIngredientIdInput allowedQuantitiesByIngredientIdInput);
        IEnumerable<CategoriesByIngredientIdOutput> CategoriesByIngredientId(CategoriesByIngredientIdInput categoriesByIngredientIdInput);
        InsertIngredientOutput InsertIngredient(InsertIngredientInput insertIngredientInput);
        UpdateIngredientOutput UpdateIngredient(UpdateIngredientInput updateIngredientInput);
        InsertIngredientLanguageOutput InsertIngredientLanguage(InsertIngredientLanguageInput insertIngredientLanguageInput);
        UpdateIngredientLanguageOutput UpdateIngredientLanguage(UpdateIngredientLanguageInput updateIngredientLanguageInput);
        DeleteIngredientOutput DeleteIngredient(DeleteIngredientInput deleteIngredientInput);
        QuantityNotStdByIdAndLanguageOutput QuantityNotStdByIdAndLanguage(QuantityNotStdByIdAndLanguageInput quantityNotStdByIdAndLanguageInput);
        IngredientQuantityTypeLanguageOutput IngredientQuantityTypeLanguage(IngredientQuantityTypeLanguageInput ingredientQuantityTypeLanguageInput);
        IEnumerable<IngredientsByIdRecipeAndLanguageOutput> IngredientsByIdRecipeAndLanguage(IngredientsByIdRecipeAndLanguageInput ingredientsByIdRecipeAndLanguageInput);
        IEnumerable<IngredientsByIdRecipeAndLanguageOutput> AddQuantityTypeLanguage(IEnumerable<IngredientsByIdRecipeAndLanguageOutput> recipeIngredientDetailsLanguageMapped);
        string CalculateQuantityTypeLanguage(IngredientsByIdRecipeAndLanguageOutput ingredientsesByIdRecipeAndLanguageOutput);
        IEnumerable<IngredientsByIdRecipeAndLanguageOutput> AddCalculatedIngredient(IEnumerable<IngredientsByIdRecipeAndLanguageOutput> recipeIngredientDetailsLanguageMapped);
        string GetCalculatedIngredient(IngredientsByIdRecipeAndLanguageOutput andLanguageOutput);
        IEnumerable<IngredientsByIdRecipeOutput> IngredientsByIdRecipe(IngredientsByIdRecipeInput ingredientsByIdRecipeInput);
        TranslateBunchOfIngredientsOutput TranslateBunchOfIngredients(TranslateBunchOfIngredientsInput translateBunchOfIngredientsInput);

        //TODO: USP_GetIngredientToTranslate

        /****************************************************************/
        //BEVERAGES
        /****************************************************************/
        IEnumerable<SearchBeverageByLanguageOutput> SearchBeverageByLanguage(SearchBeverageByLanguageInput searchBeverageByLanguageInput);
        AddRecipeBeverageOutput AddRecipeBeverage(AddRecipeBeverageInput addRecipeBeverageInput);
        DeleteRecipeBeverageOutput DeleteRecipeBeverage(DeleteRecipeBeverageInput deleteRecipeBeverageInput);
        IEnumerable<SuggestedBeverageByRecipeOutput> SuggestedBeverageByRecipe(SuggestedBeverageByRecipeInput suggestedBeverageByRecipeInput);

        /****************************************************************/
    }
}