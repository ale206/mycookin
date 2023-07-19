using AutoMapper;
using TaechIdeas.Core.Core.User.Dto;
using TaechIdeas.MyCookin.Core.Dto;

namespace TaechIdeas.MyCookin.IoC.AutoMapper
{
    public class MapForRecipe : Profile
    {
        public MapForRecipe()
        {
            //RECIPE
            /**************************************************************************/
            CreateMap<SearchRecipesInput, TopRecipesByLanguageInput>();

            CreateMap<SearchRecipesRequest, SearchRecipesInput>();
            CreateMap<SearchRecipesInput, SearchRecipesIn>();
            CreateMap<SearchRecipesInput, SearchRecipesWithEmptyFridgeIn>();
            CreateMap<SearchRecipesOut, SearchRecipesOutput>()
                .ForMember(dest => dest.RecipeId, opt => opt.MapFrom(src => src.IdRecipe))
                .ForMember(dest => dest.RecipeImageId, opt => opt.MapFrom(src => src.IDRecipeImage));
            CreateMap<SearchRecipesOutput, SearchRecipesResult>();

            CreateMap<NewRecipeRequest, NewRecipeInput>();

            CreateMap<NewRecipeInput, NewRecipeIn>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.CheckTokenInput.UserId));
            CreateMap<NewRecipeOut, NewRecipeOutput>();
            CreateMap<NewRecipeOutput, NewRecipeResult>();

            // RecipeByLanguageRequest
            CreateMap<RecipeByLanguageRequest, RecipeByIdAndLanguageInput>();
            CreateMap<RecipeByIdAndLanguageInput, RecipeByLanguageIn>();
            CreateMap<RecipeByLanguageOut, RecipeByIdAndLanguageOutput>()
                .ForMember(dest => dest.LanguageId, opt => opt.MapFrom(src => src.IDLanguage))
                .ForMember(dest => dest.IsGlutenFree, opt => opt.MapFrom(src => src.GlutenFree))
                .ForMember(dest => dest.IsVegan, opt => opt.MapFrom(src => src.Vegan))
                .ForMember(dest => dest.IsVegetarian, opt => opt.MapFrom(src => src.Vegetarian))
                .ForMember(dest => dest.RecipeLanguageId, opt => opt.MapFrom(src => src.IDRecipeLanguage))
                .ForMember(dest => dest.RecipeSuggestion, opt => opt.MapFrom(src => src.RecipeSuggestion));

            CreateMap<RecipeByIdAndLanguageOutput, RecipeByIdAndLanguageResult>();
            //orMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Ingredients));

            CreateMap<RecipeByIdAndLanguageInput, RecipeByIdInput>();
            CreateMap<RecipeByIdAndLanguageInput, StepsByIdRecipeAndLanguageInput>();
            CreateMap<RecipeByIdAndLanguageInput, IngredientsByIdRecipeAndLanguageInput>();
            CreateMap<RecipeByIdAndLanguageInput, RecipePropertyByIdRecipeAndLanguageInput>();
            CreateMap<RecipeByIdAndLanguageInput, RecipeByLanguageIn>();
            CreateMap<RecipePathInput, RecipeByIdAndLanguageInput>();

            // RecipeById
            CreateMap<RecipeByIdInput, RecipeByIdIn>();
            CreateMap<RecipeByIdInput, StepsForRecipeInput>();
            CreateMap<RecipeByIdInput, IngredientsByIdRecipeInput>();
            CreateMap<RecipeOwnerOutput, RecipeOwnerResult>();

            CreateMap<TopRecipesByLanguageRequest, TopRecipesByLanguageInput>();
            CreateMap<TopRecipesByLanguageInput, TopRecipesByLanguageIn>();
            CreateMap<TopRecipesByLanguageOut, TopRecipesByLanguageOutput>()
                .ForMember(dest => dest.RecipeId, opt => opt.MapFrom(src => src.IDRecipe))
                .ForMember(dest => dest.RecipeLanguageId, opt => opt.MapFrom(src => src.IDRecipeLanguage))
                .ForMember(dest => dest.RecipeImageId, opt => opt.MapFrom(src => src.IDRecipeImage))
                .ForMember(dest => dest.IsVegan, opt => opt.MapFrom(src => src.Vegan))
                .ForMember(dest => dest.IsVegetarian, opt => opt.MapFrom(src => src.Vegetarian))
                .ForMember(dest => dest.IsGlutenFree, opt => opt.MapFrom(src => src.GlutenFree))
                .ForMember(dest => dest.IsHotSpicy, opt => opt.MapFrom(src => src.HotSpicy))
                .ForMember(dest => dest.CookingTimeMinutes, opt => opt.MapFrom(src => src.CookingTimeMinute))
                .ForMember(dest => dest.PreparationTimeMinutes, opt => opt.MapFrom(src => src.PreparationTimeMinute))
                .ForMember(dest => dest.RecipeImageId, opt => opt.MapFrom(src => src.IDRecipeImage))
                .ForMember(dest => dest.RecipeDifficulty, opt => opt.MapFrom(src => src.RecipeDifficulties))
                .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.IDOwner));
            CreateMap<TopRecipesByLanguageOutput, TopRecipesByLanguageResult>();

            CreateMap<BestRecipesByLanguageRequest, BestRecipesByLanguageInput>();
            CreateMap<BestRecipesByLanguageInput, BestRecipesByLanguageIn>();
            CreateMap<BestRecipesByLanguageOut, BestRecipesByLanguageOutput>()
                .ForMember(dest => dest.RecipeId, opt => opt.MapFrom(src => src.IDRecipe))
                .ForMember(dest => dest.RecipeLanguageId, opt => opt.MapFrom(src => src.IDRecipeLanguage))
                .ForMember(dest => dest.RecipeImageId, opt => opt.MapFrom(src => src.IDRecipeImage))
                .ForMember(dest => dest.IsVegan, opt => opt.MapFrom(src => src.Vegan))
                .ForMember(dest => dest.IsVegetarian, opt => opt.MapFrom(src => src.Vegetarian))
                .ForMember(dest => dest.IsGlutenFree, opt => opt.MapFrom(src => src.GlutenFree))
                .ForMember(dest => dest.IsHotSpicy, opt => opt.MapFrom(src => src.HotSpicy))
                .ForMember(dest => dest.CookingTimeMinutes, opt => opt.MapFrom(src => src.CookingTimeMinute))
                .ForMember(dest => dest.PreparationTimeMinutes, opt => opt.MapFrom(src => src.PreparationTimeMinute))
                .ForMember(dest => dest.RecipeImageId, opt => opt.MapFrom(src => src.IDRecipeImage))
                .ForMember(dest => dest.RecipeDifficulty, opt => opt.MapFrom(src => src.RecipeDifficulties))
                .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.IDOwner));
            CreateMap<BestRecipesByLanguageOutput, BestRecipesByLanguageResult>();

            CreateMap<RecipeByIdOut, RecipeByIdOutput>()
                .ForMember(dest => dest.RecipeId, opt => opt.MapFrom(src => src.IDRecipe))
                .ForMember(dest => dest.RecipeFatherId, opt => opt.MapFrom(src => src.IdRecipeFather))
                .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.IdOwner))
                .ForMember(dest => dest.CookingTimeMinutes, opt => opt.MapFrom(src => src.CookingTimeMinute))
                .ForMember(dest => dest.PreparationTimeMinutes, opt => opt.MapFrom(src => src.PreparationTimeMinute))
                .ForMember(dest => dest.IsVegan, opt => opt.MapFrom(src => src.Vegan))
                .ForMember(dest => dest.IsVegetarian, opt => opt.MapFrom(src => src.Vegetarian))
                .ForMember(dest => dest.IsGlutenFree, opt => opt.MapFrom(src => src.GlutenFree))
                .ForMember(dest => dest.NumberOfPeople, opt => opt.MapFrom(src => src.NumberOfPerson));

            // SteepsForRecipe
            CreateMap<StepsForRecipeInput, StepsForRecipeIn>();
            CreateMap<StepsForRecipeOut, StepForRecipeOutput>()
                .ForMember(dest => dest.RecipeStepId, opt => opt.MapFrom(src => src.IdRecipeStep))
                .ForMember(dest => dest.RecipeLanguageId, opt => opt.MapFrom(src => src.IdRecipeLanguage))
                .ForMember(dest => dest.RecipeStepImageId, opt => opt.MapFrom(src => src.IDRecipeStepImage))
                .ForMember(dest => dest.StepTimeMinutes, opt => opt.MapFrom(src => src.StepTimeMinute));
            CreateMap<StepForRecipeOutput, StepsByIdRecipeAndLanguageResult>();

            CreateMap<StepsByIdRecipeAndLanguageOutput, StepsByIdRecipeAndLanguageResult>();
            CreateMap<StepsByIdRecipeAndLanguageInput, StepsByIdRecipeAndLanguageIn>();
            CreateMap<StepsByIdRecipeAndLanguageOutput, StepForRecipeOutput>();
            CreateMap<StepForRecipeOutput, StepsByIdRecipeAndLanguageResult>();

            CreateMap<RecipePropertyByIdRecipeAndLanguageInput, RecipePropertyByIdRecipeAndLanguageIn>();
            CreateMap<RecipePropertyByIdRecipeAndLanguageOut, RecipePropertyByIdRecipeAndLanguageOutput>();

            CreateMap<RecipePropertyByIdRecipeAndLanguageOut, RecipePropertyByIdRecipeAndLanguageOutput>();
            CreateMap<RecipePropertyByRecipeIdOut, RecipePropertyByRecipeIdOutput>();

            CreateMap<SearchRecipesWithEmptyFridgeOut, SearchEmptyFridgeRecipesOutput>()
                .ForMember(dest => dest.RecipeId, opt => opt.MapFrom(src => src.IdRecipe));

            CreateMap<TopRecipesByLanguageOutput, SearchRecipesOutput>();
            CreateMap<BestRecipesByLanguageOut, BestRecipesByLanguageOutput>()
                .ForMember(dest => dest.RecipeId, opt => opt.MapFrom(src => src.IDRecipe))
                .ForMember(dest => dest.RecipeImageId, opt => opt.MapFrom(src => src.IDRecipeImage))
                .ForMember(dest => dest.IsVegan, opt => opt.MapFrom(src => src.Vegan))
                .ForMember(dest => dest.IsVegetarian, opt => opt.MapFrom(src => src.Vegetarian))
                .ForMember(dest => dest.IsGlutenFree, opt => opt.MapFrom(src => src.GlutenFree))
                .ForMember(dest => dest.IsHotSpicy, opt => opt.MapFrom(src => src.HotSpicy))
                .ForMember(dest => dest.CookingTimeMinutes, opt => opt.MapFrom(src => src.CookingTimeMinute))
                .ForMember(dest => dest.PreparationTimeMinutes, opt => opt.MapFrom(src => src.PreparationTimeMinute))
                .ForMember(dest => dest.RecipeLanguageId, opt => opt.MapFrom(src => src.IDRecipeLanguage))
                .ForMember(dest => dest.RecipeDifficulty, opt => opt.MapFrom(src => src.RecipeDifficulties))
                .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.IDOwner));
            //g.CreateMap<BestRecipesByLanguageInput, BestRecipesByLanguageIn>();

            CreateMap<RecipeByIdAndLanguageOutput, RecipeByLanguageOut>()
                .ForMember(dest => dest.IDLanguage, opt => opt.MapFrom(src => src.LanguageId));

            CreateMap<StepsByIdRecipeAndLanguageOut, StepsByIdRecipeAndLanguageOutput>()
                .ForMember(dest => dest.RecipeStepId, opt => opt.MapFrom(src => src.IdRecipeStep))
                .ForMember(dest => dest.LanguageId, opt => opt.MapFrom(src => src.IdRecipeLanguage))
                .ForMember(dest => dest.RecipeStepImageId, opt => opt.MapFrom(src => src.IDRecipeStepImage));

            CreateMap<FriendlyIdByRecipeLanguageIdInput, FriendlyIdByRecipeLanguageIdIn>();
            CreateMap<FriendlyIdByRecipeLanguageIdOut, FriendlyIdByRecipeLanguageIdOutput>();

            CreateMap<RecipeByFriendlyIdInput, RecipeByFriendlyIdIn>();
            CreateMap<RecipeByFriendlyIdOut, RecipeByFriendlyIdOutput>()
                .ForMember(dest => dest.RecipeId, opt => opt.MapFrom(src => src.IDRecipe))
                .ForMember(dest => dest.RecipeFatherId, opt => opt.MapFrom(src => src.IDRecipeFather))
                .ForMember(dest => dest.CookingTimeMinutes, opt => opt.MapFrom(src => src.CookingTimeMinute))
                .ForMember(dest => dest.NumberOfPeople, opt => opt.MapFrom(src => src.NumberOfPerson))
                .ForMember(dest => dest.PreparationTimeMinutes, opt => opt.MapFrom(src => src.PreparationTimeMinute))
                .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.IdOwner));

            CreateMap<RecipeByFriendlyIdOut, RecipeByFriendlyIdOutput>()
                .ForMember(dest => dest.LanguageId, opt => opt.MapFrom(src => src.IDLanguage));
            CreateMap<RecipeByFriendlyIdInput, StepsForRecipeInput>();
            CreateMap<RecipeByFriendlyIdInput, IngredientsByIdRecipeInput>();
            CreateMap<RecipeByFriendlyIdOut, RecipeByFriendlyIdOutput>()
                .ForMember(dest => dest.RecipeImageId, opt => opt.MapFrom(src => src.IDRecipeImage))
                .ForMember(dest => dest.RecipeId, opt => opt.MapFrom(src => src.IDRecipe))
                .ForMember(dest => dest.IsVegan, opt => opt.MapFrom(src => src.Vegan))
                .ForMember(dest => dest.IsVegetarian, opt => opt.MapFrom(src => src.Vegetarian))
                .ForMember(dest => dest.IsGlutenFree, opt => opt.MapFrom(src => src.GlutenFree))
                .ForMember(dest => dest.IsHotSpicy, opt => opt.MapFrom(src => src.HotSpicy))
                .ForMember(dest => dest.CookingTimeMinutes, opt => opt.MapFrom(src => src.CookingTimeMinute))
                .ForMember(dest => dest.PreparationTimeMinutes, opt => opt.MapFrom(src => src.PreparationTimeMinute))
                .ForMember(dest => dest.RecipeImageId, opt => opt.MapFrom(src => src.IDRecipeImage))
                .ForMember(dest => dest.RecipeDifficulty, opt => opt.MapFrom(src => src.RecipeDifficulties));
            CreateMap<RecipeByFriendlyIdOutput, RecipeByFriendlyIdResult>();

            CreateMap<UserByIdOutput, RecipeOwnerOutput>();

            CreateMap<UpdateRecipeLanguageRequest, UpdateRecipeLanguageInput>();
            CreateMap<UpdateRecipeLanguageInput, UpdateRecipeLanguageIn>();
            CreateMap<UpdateRecipeLanguageOut, UpdateRecipeLanguageOutput>();
            CreateMap<UpdateRecipeLanguageOutput, UpdateRecipeLanguageResult>();

            CreateMap<UpdateRecipeLanguageStepRequest, UpdateRecipeLanguageStepInput>();
            CreateMap<UpdateRecipeLanguageStepInput, UpdateRecipeLanguageStepIn>();
            CreateMap<UpdateRecipeLanguageStepOut, UpdateRecipeLanguageStepOutput>();
            CreateMap<UpdateRecipeLanguageStepOutput, UpdateRecipeLanguageStepResult>();

            CreateMap<NewRecipeLanguageStepsRequest, NewRecipeLanguageStepsInput>();
            CreateMap<NewRecipeLanguageStepsInput, NewRecipeLanguageStepIn>();
            CreateMap<NewRecipeLanguageStepIn, NewRecipeLanguageStepOut>();
            CreateMap<NewRecipeLanguageStepOut, NewRecipeLanguageStepsOutput>();
            CreateMap<NewRecipeLanguageStepsOutput, NewRecipeLanguageStepsResult>();

            CreateMap<UpdateRecipeIngredientRequest, UpdateRecipeIngredientInput>();
            CreateMap<UpdateRecipeIngredientInput, UpdateRecipeIngredientIn>();
            CreateMap<UpdateRecipeIngredientOut, UpdateRecipeIngredientOutput>();
            CreateMap<UpdateRecipeIngredientOutput, UpdateRecipeIngredientResult>();

            CreateMap<AddNewIngredientToRecipeRequest, AddNewIngredientToRecipeInput>();
            CreateMap<AddNewIngredientToRecipeInput, AddNewIngredientToRecipeIn>();
            CreateMap<AddNewIngredientToRecipeOut, AddNewIngredientToRecipeOutput>();
            CreateMap<AddNewIngredientToRecipeOutput, AddNewIngredientToRecipeResult>();

            CreateMap<RecipeLanguageListRequest, RecipeLanguageListInput>();
            CreateMap<RecipeLanguageListInput, RecipeLanguageListIn>();
            CreateMap<RecipeLanguageListOut, RecipeLanguageListOutput>()
                .ForMember(dest => dest.RecipeId, opt => opt.MapFrom(src => src.IDRecipe))
                .ForMember(dest => dest.RecipeFatherId, opt => opt.MapFrom(src => src.IDRecipeFather))
                .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.IDOwner))
                .ForMember(dest => dest.NumberOfPeople, opt => opt.MapFrom(src => src.NumberOfPerson))
                .ForMember(dest => dest.RecipeImageId, opt => opt.MapFrom(src => src.IDRecipeImage))
                .ForMember(dest => dest.RecipeVideoId, opt => opt.MapFrom(src => src.IDRecipeVideo))
                .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.IDCity))
                .ForMember(dest => dest.IsAStarterRecipe, opt => opt.MapFrom(src => src.isStarterRecipe))
                .ForMember(dest => dest.RecipeLanguageId, opt => opt.MapFrom(src => src.IDRecipeLanguage))
                .ForMember(dest => dest.LanguageId, opt => opt.MapFrom(src => src.IDLanguage))
                .ForMember(dest => dest.GeoRegionId, opt => opt.MapFrom(src => src.GeoIDRegion));
            CreateMap<RecipeLanguageListOutput, RecipeLanguageListResult>();

            CreateMap<UpdateRecipeIngredientInput, CalculateNutritionalFactsInput>();

            CreateMap<CalculateNutritionalFactsInput, CalculateNutritionalFactsIn>();
            CreateMap<CalculateNutritionalFactsOut, CalculateNutritionalFactsOutput>();

            CreateMap<IngredientQuantityTypeListRequest, IngredientQuantityTypeListInput>();
            CreateMap<IngredientQuantityTypeListInput, IngredientQuantityTypeListIn>();
            CreateMap<IngredientQuantityTypeListOut, IngredientQuantityTypeListOutput>()
                .ForMember(dest => dest.LanguageId, opt => opt.MapFrom(src => src.IDLanguage))
                .ForMember(dest => dest.IngredientQuantityTypeId, opt => opt.MapFrom(src => src.IDIngredientQuantityType))
                .ForMember(dest => dest.IngredientQuantityTypeLanguageId, opt => opt.MapFrom(src => src.IDIngredientQuantityTypeLanguage));
            CreateMap<IngredientQuantityTypeListOutput, IngredientQuantityTypeListResult>();

            CreateMap<PropertiesListByTypeAndLanguageRequest, PropertiesListByTypeAndLanguageInput>();
            CreateMap<PropertiesListByTypeAndLanguageInput, PropertiesListByTypeAndLanguageIn>();
            CreateMap<PropertiesListByTypeAndLanguageOut, PropertiesListByTypeAndLanguageOutput>()
                .ForMember(dest => dest.LanguageId, opt => opt.MapFrom(src => src.IDLanguage))
                .ForMember(dest => dest.IsColorType, opt => opt.MapFrom(src => src.isColorType))
                .ForMember(dest => dest.IsCookingType, opt => opt.MapFrom(src => src.isCookingType))
                .ForMember(dest => dest.IsDishType, opt => opt.MapFrom(src => src.isDishType))
                .ForMember(dest => dest.IsEatType, opt => opt.MapFrom(src => src.isEatType))
                .ForMember(dest => dest.IsPeriodType, opt => opt.MapFrom(src => src.isPeriodType))
                .ForMember(dest => dest.IsUseType, opt => opt.MapFrom(src => src.isUseType))
                .ForMember(dest => dest.RecipePropertyId, opt => opt.MapFrom(src => src.IDRecipeProperty))
                .ForMember(dest => dest.RecipePropertyTypeId, opt => opt.MapFrom(src => src.IDRecipePropertyType));
            CreateMap<PropertiesListByTypeAndLanguageOutput, PropertiesListByTypeAndLanguageResult>();

            CreateMap<PropertiesListByTypeLanguageAndRecipeRequest, PropertiesListByTypeLanguageAndRecipeInput>();
            CreateMap<PropertiesListByTypeLanguageAndRecipeInput, PropertiesListByTypeLanguageAndRecipeIn>();
            CreateMap<PropertiesListByTypeLanguageAndRecipeOut, PropertiesListByTypeLanguageAndRecipeOutput>()
                .ForMember(dest => dest.LanguageId, opt => opt.MapFrom(src => src.IDLanguage))
                .ForMember(dest => dest.IsColorType, opt => opt.MapFrom(src => src.isColorType))
                .ForMember(dest => dest.IsCookingType, opt => opt.MapFrom(src => src.isCookingType))
                .ForMember(dest => dest.IsDishType, opt => opt.MapFrom(src => src.isDishType))
                .ForMember(dest => dest.IsEatType, opt => opt.MapFrom(src => src.isEatType))
                .ForMember(dest => dest.IsPeriodType, opt => opt.MapFrom(src => src.isPeriodType))
                .ForMember(dest => dest.IsUseType, opt => opt.MapFrom(src => src.isUseType))
                .ForMember(dest => dest.RecipePropertyId, opt => opt.MapFrom(src => src.IDRecipeProperty))
                .ForMember(dest => dest.RecipePropertyTypeId, opt => opt.MapFrom(src => src.IDRecipePropertyType));
            CreateMap<PropertiesListByTypeLanguageAndRecipeOutput, PropertiesListByTypeLanguageAndRecipeResult>();

            CreateMap<AddOrUpdatePropertyValueRequest, AddOrUpdatePropertyValueInput>();
            CreateMap<AddOrUpdatePropertyValueInput, AddOrUpdatePropertyValueIn>();
            CreateMap<AddOrUpdatePropertyValueOut, AddOrUpdatePropertyValueOutput>();
            CreateMap<AddOrUpdatePropertyValueOutput, AddOrUpdatePropertyValueResult>();

            CreateMap<DeletePropertyValueRequest, DeletePropertyValueInput>();
            CreateMap<DeletePropertyValueInput, DeletePropertyValueIn>();
            CreateMap<DeletePropertyValueOut, DeletePropertyValueOutput>();
            CreateMap<DeletePropertyValueOutput, DeletePropertyValueResult>();

            CreateMap<DeleteRecipeRequest, DeleteRecipeInput>();
            CreateMap<DeleteRecipeInput, DeleteRecipeIn>();
            CreateMap<DeleteRecipeOut, DeleteRecipeOutput>();
            CreateMap<DeleteRecipeOutput, DeleteRecipeResult>();

            CreateMap<TranslateBunchOfRecipesRequest, TranslateBunchOfRecipesInput>();
            //g.CreateMap< TranslateBunchOfRecipesInput,  TranslateBunchOfRecipesIn>();
            //g.CreateMap< TranslateBunchOfRecipesOut,  TranslateBunchOfRecipesOutput>();
            CreateMap<TranslateBunchOfRecipesOutput, TranslateBunchOfRecipesResult>();

            CreateMap<TranslateBunchOfRecipesRequest, TranslateBunchOfRecipesInput>();
            CreateMap<TranslateBunchOfRecipesInput, RecipesToTranslateIn>();
            CreateMap<RecipesToTranslateOut, TranslateBunchOfRecipesOutput>();
            CreateMap<TranslateBunchOfRecipesOutput, TranslateBunchOfRecipesResult>();

            //g.CreateMap<CalculateRecipeTagsRequest, CalculateRecipeTagsInput>();
            CreateMap<CalculateRecipeTagsInput, CalculateRecipeTagsIn>();
            CreateMap<CalculateRecipeTagsOut, CalculateRecipeTagsOutput>();
            //g.CreateMap<CalculateRecipeTagsOutput, CalculateRecipeTagsResult>();

            CreateMap<PropertiesByRecipeAndLanguageRequest, PropertiesByRecipeAndLanguageInput>();
            CreateMap<PropertiesByRecipeAndLanguageInput, PropertiesByRecipeAndLanguageIn>();
            CreateMap<PropertiesByRecipeAndLanguageOut, PropertiesByRecipeAndLanguageOutput>();
            CreateMap<PropertiesByRecipeAndLanguageOutput, PropertiesByRecipeAndLanguageResult>();
            CreateMap<PropertiesByRecipeAndLanguageGroupOutput, PropertiesByRecipeAndLanguageGroupResult>();

            CreateMap<RecipeByFriendlyIdOutput, SearchRecipesOutput>();
            CreateMap<RecipeByFriendlyIdOutput, BestRecipesByLanguageOutput>();
            CreateMap<RecipeByFriendlyIdOutput, TopRecipesByLanguageOutput>();
            CreateMap<TopRecipesByLanguageInput, RecipeByFriendlyIdInput>();
            CreateMap<BestRecipesByLanguageInput, RecipeByFriendlyIdInput>();

            CreateMap<RecipeByLanguageOut, RecipeByLanguageOutput>();

            CreateMap<RecipeByFriendlyIdInput, UserByIdInput>();

            /**************************************************************************/
        }
    }
}