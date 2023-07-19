using System.Collections.Generic;
using TaechIdeas.Core.Core.Token.Dto;
using TaechIdeas.MyCookin.Core.Dto;

namespace TaechIdeas.MyCookin.Core
{
    public interface IRecipeManager
    {
        IEnumerable<SearchRecipesOutput> SearchRecipes(SearchRecipesInput searchRecipesInput);
        int GetQuickThreshold(SearchRecipesInput searchRecipesInput);
        int GetLightThreshold(SearchRecipesInput searchRecipesInput);
        IEnumerable<TopRecipesByLanguageOutput> TopRecipesByLanguage(TopRecipesByLanguageInput topRecipesByLanguageInput);
        IEnumerable<BestRecipesByLanguageOutput> BestRecipesByLanguage(BestRecipesByLanguageInput bestRecipesByLanguageInput);
        string AddPropertiesJoined(int idLanguage, bool isVegan, bool isVegetarian, bool isGlutenFree);
        string GetGoogleSuggestions(string words);
        RecipeByIdOutput RecipeById(RecipeByIdInput recipeByIdInput);
        RecipeByFriendlyIdOutput RecipeByFriendlyId(RecipeByFriendlyIdInput recipeByFriendlyIdInput);
        RecipePathOutput RecipePath(RecipePathInput recipePathInput);
        GenerateFriendlyIdOutput GenerateFriendlyId(GenerateFriendlyIdInput generateFriendlyIdInput);
        bool GenerateAllFriendlyId();
        FriendlyIdByRecipeLanguageIdOutput FriendlyIdByRecipeLanguageId(FriendlyIdByRecipeLanguageIdInput friendlyIdByRecipeLanguageIdInput);
        NewRecipeOutput NewRecipe(NewRecipeInput newRecipeInput);
        UpdateRecipeLanguageOutput UpdateRecipeLanguage(UpdateRecipeLanguageInput updateRecipeLanguageInput);
        UpdateRecipeLanguageStepOutput UpdateRecipeLanguageStep(UpdateRecipeLanguageStepInput updateRecipeLanguageStepInput);
        IEnumerable<NewRecipeLanguageStepsOutput> NewRecipeLanguageSteps(IEnumerable<NewRecipeLanguageStepsInput> newRecipeLanguageStepsInput);
        UpdateRecipeIngredientOutput UpdateRecipeIngredient(UpdateRecipeIngredientInput updateRecipeIngredientInput);
        IEnumerable<AddNewIngredientToRecipeOutput> AddNewIngredientToRecipe(IEnumerable<AddNewIngredientToRecipeInput> addNewIngredientToRecipeInput);
        IEnumerable<RecipeLanguageListOutput> RecipeLanguageList(RecipeLanguageListInput recipeLanguageListInput);
        CalculateNutritionalFactsOutput CalculateNutritionalFacts(CalculateNutritionalFactsInput calculateNutritionalFactsInput);
        DeleteRecipeOutput DeleteRecipe(DeleteRecipeInput deleteRecipeInput);
        IEnumerable<PropertiesListByTypeAndLanguageOutput> PropertiesListByTypeAndLanguage(PropertiesListByTypeAndLanguageInput propertiesListByTypeAndLanguageInput);
        IEnumerable<PropertiesListByTypeLanguageAndRecipeOutput> PropertiesListByTypeLanguageAndRecipe(PropertiesListByTypeLanguageAndRecipeInput propertiesListByTypeLanguageAndRecipeInput);
        IEnumerable<AddOrUpdatePropertyValueOutput> AddOrUpdatePropertyValue(CheckTokenInput checkTokenInput, IEnumerable<AddOrUpdatePropertyValueInput> addOrUpdatePropertyValueInput);
        DeletePropertyValueOutput DeletePropertyValue(DeletePropertyValueInput deletePropertyValueInput);
        IEnumerable<RecipePropertyByIdRecipeAndLanguageOutput> RecipePropertyByIdRecipeAndLanguage(RecipePropertyByIdRecipeAndLanguageInput recipePropertyByIdRecipeAndLanguageInput);
        IEnumerable<StepsByIdRecipeAndLanguageOutput> StepsByIdRecipeAndLanguage(StepsByIdRecipeAndLanguageInput stepsByIdRecipeAndLanguageInput);
        IEnumerable<StepForRecipeOutput> StepsForRecipe(StepsForRecipeInput stepsForRecipeInput);

        IEnumerable<RecipeSiteMapOutput> RecipeSiteMap(RecipeSiteMapInput recipeSiteMapInput);
        IEnumerable<RecipesByOwnerOutput> RecipesByOwner(RecipesByOwnerInput recipesByOwnerInput);
        TranslateBunchOfRecipesOutput TranslateBunchOfRecipes(TranslateBunchOfRecipesInput translateBunchOfRecipesInput);
        IEnumerable<SimilarRecipesOutput> SimilarRecipes(SimilarRecipesInput similarRecipesInput);
        PercentageCompleteOutput PercentageComplete(PercentageCompleteInput percentageCompleteInput);
        CalculateRecipeTagsOutput CalculateRecipeTags(CalculateRecipeTagsInput calculateRecipeTagsInput);
        IEnumerable<StepsToTranslateOutput> StepsToTranslate(StepsToTranslateInput stepsToTranslateInput);
        IEnumerable<RecipeLanguageTagsOutput> RecipeLanguageTags(RecipeLanguageTagsInput recipeLanguageTagsInput);
        IEnumerable<TagsByRecipeAndLanguageOutput> TagsByRecipeAndLanguage(TagsByRecipeAndLanguageInput recipeAndLanguageInput);
        PropertiesByRecipeAndLanguageOutput PropertiesByRecipeAndLanguage(PropertiesByRecipeAndLanguageInput propertiesByRecipeAndLanguageInput);
    }
}