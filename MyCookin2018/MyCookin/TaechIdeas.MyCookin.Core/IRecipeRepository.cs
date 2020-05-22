using System.Collections.Generic;
using TaechIdeas.MyCookin.Core.Dto;

namespace TaechIdeas.MyCookin.Core
{
    public interface IRecipeRepository
    {
        IEnumerable<SearchRecipesOut> SearchRecipes(SearchRecipesIn searchRecipesIn);

        IEnumerable<SearchRecipesWithEmptyFridgeOut> SearchRecipesWithEmptyFridge(
            SearchRecipesWithEmptyFridgeIn searchRecipesWithEmptyFridgeIn);

        RecipeByLanguageOut RecipeByLanguage(RecipeByLanguageIn recipeByLanguageIn);
        RecipeByIdOut RecipeById(RecipeByIdIn recipeByIdIn);
        RecipeByFriendlyIdOut RecipeByFriendlyId(RecipeByFriendlyIdIn recipeByFriendlyIdIn);

        IEnumerable<StepsByIdRecipeAndLanguageOut> StepsByIdRecipeAndLanguage(
            StepsByIdRecipeAndLanguageIn stepsByIdRecipeAndLanguageIn);

        IEnumerable<StepsForRecipeOut> StepsForRecipe(StepsForRecipeIn stepsForRecipeIn);

        IEnumerable<RecipePropertyByIdRecipeAndLanguageOut> RecipePropertyByIdRecipeAndLanguage(
            RecipePropertyByIdRecipeAndLanguageIn recipePropertyByIdRecipeAndLanguageIn);

        IEnumerable<RecipePropertyByRecipeIdOut> RecipePropertyByRecipeId(
            RecipePropertyByRecipeIdIn recipePropertyByRecipeIdIn);

        IEnumerable<TopRecipesByLanguageOut> TopRecipesByLanguage(TopRecipesByLanguageIn topRecipesByLanguageIn);
        IEnumerable<BestRecipesByLanguageOut> BestRecipesByLanguage(BestRecipesByLanguageIn bestRecipesByLanguageIn);
        GenerateFriendlyIdOut GenerateFriendlyId(GenerateFriendlyIdIn generateFriendlyIdIn);
        GenerateAllFriendlyIdOut GenerateAllFriendlyId();

        FriendlyIdByRecipeLanguageIdOut FriendlyIdByRecipeLanguageId(
            FriendlyIdByRecipeLanguageIdIn friendlyIdByRecipeLanguageIdIn);

        NewRecipeOut NewRecipe(NewRecipeIn newRecipeIn);
        UpdateRecipeLanguageOut UpdateRecipeLanguage(UpdateRecipeLanguageIn updateRecipeLanguageIn);
        UpdateRecipeLanguageStepOut UpdateRecipeLanguageStep(UpdateRecipeLanguageStepIn updateRecipeLanguageStepIn);
        NewRecipeLanguageStepOut NewRecipeLanguageStep(NewRecipeLanguageStepIn newRecipeLanguageStepIn);
        UpdateRecipeIngredientOut UpdateRecipeIngredient(UpdateRecipeIngredientIn updateRecipeIngredientIn);
        AddNewIngredientToRecipeOut AddNewIngredientToRecipe(AddNewIngredientToRecipeIn addNewIngredientToRecipeIn);
        IEnumerable<RecipeLanguageListOut> RecipeLanguageList(RecipeLanguageListIn recipeLanguageListIn);
        CalculateNutritionalFactsOut CalculateNutritionalFacts(CalculateNutritionalFactsIn calculateNutritionalFactsIn);
        DeleteRecipeOut DeleteRecipe(DeleteRecipeIn deleteRecipeIn);

        IEnumerable<PropertiesListByTypeAndLanguageOut> PropertiesListByTypeAndLanguage(
            PropertiesListByTypeAndLanguageIn propertiesListByTypeAndLanguageIn);

        IEnumerable<PropertiesListByTypeLanguageAndRecipeOut> PropertiesListByTypeLanguageAndRecipe(
            PropertiesListByTypeLanguageAndRecipeIn propertiesListByTypeLanguageAndRecipeIn);

        AddOrUpdatePropertyValueOut AddOrUpdatePropertyValue(AddOrUpdatePropertyValueIn addOrUpdatePropertyValueIn);
        DeletePropertyValueOut DeletePropertyValue(DeletePropertyValueIn deletePropertyValueIn);
        IEnumerable<RecipeSiteMapOut> RecipeSiteMap(RecipeSiteMapIn recipeSiteMapIn);
        IEnumerable<RecipesByOwnerOut> RecipesByOwner(RecipesByOwnerIn recipesByOwnerIn);
        IEnumerable<RecipesToTranslateOut> RecipesToTranslate(RecipesToTranslateIn recipesToTranslateIn);
        IEnumerable<SimilarRecipesOut> SimilarRecipes(SimilarRecipesIn similarRecipesIn);
        CalculateRecipeTagsOut CalculateRecipeTags(CalculateRecipeTagsIn calculateRecipeTagsIn);
        IEnumerable<StepsToTranslateOut> StepsToTranslate(StepsToTranslateIn stepsToTranslateIn);
        IEnumerable<RecipeLanguageTagsOut> RecipeLanguageTags(RecipeLanguageTagsIn recipeLanguageTagsIn);
        IEnumerable<TagsByRecipeAndLanguageOut> TagsByRecipeAndLanguage(TagsByRecipeAndLanguageIn recipeAndLanguageIn);

        IEnumerable<PropertiesByRecipeAndLanguageOut> PropertiesByRecipeAndLanguage(
            PropertiesByRecipeAndLanguageIn propertiesByRecipeAndLanguageIn);

        //RECIPEBOOK
        IEnumerable<RecipesInRecipeBookOut> RecipesInRecipeBook(RecipesInRecipeBookIn recipesInRecipeBookIn);

        NumberOfRecipesInsertedByUserOut NumberOfRecipesInsertedByUser(
            NumberOfRecipesInsertedByUserIn numberOfRecipesInsertedByUserIn);

        CheckIfRecipeIsInBookOut CheckIfRecipeIsInBook(CheckIfRecipeIsInBookIn checkIfRecipeIsInBookIn);
        SaveRecipeInBookOut SaveRecipeInBook(SaveRecipeInBookIn saveRecipeInBookIn);
        DeleteRecipeFromBookOut DeleteRecipeFromBook(DeleteRecipeFromBookIn deleteRecipeFromBookIn);

        //VOTE
        RecipeVoteOut RecipeVote(RecipeVoteIn recipeVoteIn);
        RecipeAvgVoteOut RecipeAvgVote(RecipeAvgVoteIn recipeAvgVoteIn);
        SaveVoteOut SaveVote(SaveVoteIn saveVoteIn);
        DeleteVoteOut DeleteVote(DeleteVoteIn deleteVoteIn);

        //FEEDBACK
        FeedbackInfoOut FeedbackInfo(FeedbackInfoIn feedbackInfoIn);
        FeedbackByRecipeAndUserOut FeedbackByRecipeAndUser(FeedbackByRecipeAndUserIn feedbackByRecipeAndUserIn);
        SaveFeedbackOut SaveFeedback(SaveFeedbackIn saveFeedbackIn);
        DeleteFeedbackOut DeleteFeedback(DeleteFeedbackIn deleteFeedbackIn);
        IEnumerable<LikesForRecipeOut> LikesForRecipe(LikesForRecipeIn likesForRecipeIn);
        IEnumerable<CommentsForRecipeOut> CommentsForRecipe(CommentsForRecipeIn commentsForRecipeIn);
        AddNewRecipeTranslationOut AddNewRecipeTranslation(AddNewRecipeTranslationIn addNewRecipeTranslationIn);
    }
}