using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaechIdeas.Core.Core.Common;
using TaechIdeas.MyCookin.Core;
using TaechIdeas.MyCookin.Core.Dto;

namespace TaechIdeas.MyCookin.API.Controllers
{
    /// <summary>
    ///     Definition of API for Recipe
    /// </summary>
    [Route("mycookin")]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientManager _ingredientManager;
        private readonly IUtilsManager _utilsManager;
        private readonly IMapper _mapper;

        public IngredientController(IIngredientManager ingredientManager, IUtilsManager utilsManager, IMapper mapper)
        {
            _ingredientManager = ingredientManager;
            _utilsManager = utilsManager;
            _mapper = mapper;
        }

        /// <summary>
        ///     Return a list of all the ingredients by language
        /// </summary>
        /// <param name="ingredientLanguageListRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ingredient/list/")]
        public IEnumerable<IngredientLanguageListResult> IngredientLanguageList(IngredientLanguageListRequest ingredientLanguageListRequest)
        {
            return _mapper.Map<IEnumerable<IngredientLanguageListResult>>(_ingredientManager.IngredientLanguageList(_mapper.Map<IngredientLanguageListInput>(ingredientLanguageListRequest)));
        }

        /// <summary>
        ///     Return a list of all the ingredients by language
        /// </summary>
        /// <param name="ingredientName"></param>
        /// <param name="languageId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ingredient/searchbyname/")]
        public IEnumerable<SearchIngredientByLanguageAndNameResult> IngredientByIngredientName(string ingredientName, int languageId)
        {
            var searchIngredientByLanguageAndNameInput = new SearchIngredientByLanguageAndNameInput
            {
                LanguageId = languageId,
                IngredientName = ingredientName
            };
            return _mapper.Map<IEnumerable<SearchIngredientByLanguageAndNameResult>>(_ingredientManager.SearchIngredientByLanguageAndName(searchIngredientByLanguageAndNameInput));
        }

        /// <summary>
        ///     Return a list of all the ingredients by language
        /// </summary>
        /// <param name="languageId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ingredient/quantitytype/list")]
        public IEnumerable<IngredientQuantityTypeListResult> IngredientQuantityTypeList(int languageId)
        {
            var ingredientQuantityTypeListInput = new IngredientQuantityTypeListInput {LanguageId = languageId};
            return _mapper.Map<IEnumerable<IngredientQuantityTypeListResult>>(_ingredientManager.IngredientQuantityTypeList(ingredientQuantityTypeListInput));
        }

        /// <summary>
        ///     Get ingredient by friendly id
        /// </summary>
        /// <param name="friendlyId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ingredient/{friendlyid}")]
        public IngredientByFriendlyIdResult IngredientByFriendlyId(string friendlyId)
        {
            var ingredientByFriendlyIdInput = new IngredientByFriendlyIdInput {FriendlyId = friendlyId};
            return _mapper.Map<IngredientByFriendlyIdResult>(_ingredientManager.IngredientByFriendlyId(ingredientByFriendlyIdInput));
        }

        /// <summary>
        ///     Quantities for an Ingredient
        /// </summary>
        /// <param name="ingredientId"></param>
        /// <param name="languageId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ingredient/quantities")]
        public IEnumerable<AllowedQuantitiesByIngredientIdResult> AllowedQuantitiesByIngredientId(Guid ingredientId, int languageId)
        {
            var allowedQuantitiesByIngredientIdInput = new AllowedQuantitiesByIngredientIdInput {IngredientId = ingredientId, LanguageId = languageId};
            return _mapper.Map<IEnumerable<AllowedQuantitiesByIngredientIdResult>>(_ingredientManager.AllowedQuantitiesByIngredientId(allowedQuantitiesByIngredientIdInput));
        }

        /// <summary>
        ///     Categories for an Ingredient
        /// </summary>
        /// <param name="ingredientId"></param>
        /// <param name="languageId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ingredient/categories")]
        public IEnumerable<CategoriesByIngredientIdResult> CategoriesByIngredientId(Guid ingredientId, int languageId)
        {
            var categoriesByIngredientIdInput = new CategoriesByIngredientIdInput {IngredientId = ingredientId, LanguageId = languageId};
            return _mapper.Map<IEnumerable<CategoriesByIngredientIdResult>>(_ingredientManager.CategoriesByIngredientId(categoriesByIngredientIdInput));
        }

        /// <summary>
        ///     Insert new ingredient
        /// </summary>
        /// <param name="insertIngredientRequest"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("ingredient/new")]
        public InsertIngredientResult InsertIngredient(InsertIngredientRequest insertIngredientRequest)
        {
            return _mapper.Map<InsertIngredientResult>(_ingredientManager.InsertIngredient(_mapper.Map<InsertIngredientInput>(insertIngredientRequest)));
        }

        /// <summary>
        ///     Update Ingredient
        /// </summary>
        /// <param name="updateIngredientRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ingredient/update")]
        public UpdateIngredientResult UpdateIngredient(UpdateIngredientRequest updateIngredientRequest)
        {
            return _mapper.Map<UpdateIngredientResult>(_ingredientManager.UpdateIngredient(_mapper.Map<UpdateIngredientInput>(updateIngredientRequest)));
        }

        /// <summary>
        ///     Insert new Ingredient Language
        /// </summary>
        /// <param name="insertIngredientLanguageRequest"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("ingredientlanguage/new")]
        public InsertIngredientLanguageResult InsertIngredientLanguage(InsertIngredientLanguageRequest insertIngredientLanguageRequest)
        {
            return _mapper.Map<InsertIngredientLanguageResult>(_ingredientManager.InsertIngredientLanguage(_mapper.Map<InsertIngredientLanguageInput>(insertIngredientLanguageRequest)));
        }

        /// <summary>
        ///     Update Ingredient Language
        /// </summary>
        /// <param name="updateIngredientLanguageRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ingredientlanguage/update")]
        public UpdateIngredientLanguageResult UpdateIngredientLanguage(UpdateIngredientLanguageRequest updateIngredientLanguageRequest)
        {
            return _mapper.Map<UpdateIngredientLanguageResult>(_ingredientManager.UpdateIngredientLanguage(_mapper.Map<UpdateIngredientLanguageInput>(updateIngredientLanguageRequest)));
        }

        /// <summary>
        ///     Delete an Ingredient
        /// </summary>
        /// <param name="deleteIngredientRequest"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("ingredient/delete")]
        public DeleteIngredientResult DeleteIngredient(DeleteIngredientRequest deleteIngredientRequest)
        {
            return _mapper.Map<DeleteIngredientResult>(_ingredientManager.DeleteIngredient(_mapper.Map<DeleteIngredientInput>(deleteIngredientRequest)));
        }

        /// <summary>
        ///     Translate Bunch of Ingredients
        /// </summary>
        /// <param name="translateBunchOfIngredientsRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ingredient/translate")]
        public TranslateBunchOfIngredientsResult TranslateBunchOfIngredients(TranslateBunchOfIngredientsRequest translateBunchOfIngredientsRequest)
        {
            return _mapper.Map<TranslateBunchOfIngredientsResult>(_ingredientManager.TranslateBunchOfIngredients(_mapper.Map<TranslateBunchOfIngredientsInput>(translateBunchOfIngredientsRequest)));
        }

        /****************************************************************/
        //BEVERAGES
        /****************************************************************/

        /// <summary>
        ///     Search Beverage by Name and Language
        /// </summary>
        /// <param name="beverageName"></param>
        /// <param name="languageId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ingredient/beverage/search")]
        public IEnumerable<SearchBeverageByLanguageResult> SearchBeverageByLanguage(string beverageName, int languageId)
        {
            return
                _mapper.Map<IEnumerable<SearchBeverageByLanguageResult>>(
                    _ingredientManager.SearchBeverageByLanguage(new SearchBeverageByLanguageInput {LanguageId = languageId, BeverageName = beverageName}));
        }

        /// <summary>
        ///     Add a suggested Beverage in a Recipe
        /// </summary>
        /// <param name="addRecipeBeverageRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ingredient/beverageforrecipe/add")]
        public AddRecipeBeverageResult AddRecipeBeverage(AddRecipeBeverageRequest addRecipeBeverageRequest)
        {
            return _mapper.Map<AddRecipeBeverageResult>(_ingredientManager.AddRecipeBeverage(_mapper.Map<AddRecipeBeverageInput>(addRecipeBeverageRequest)));
        }

        /// <summary>
        ///     Delete a suggested beverage for a Recipe
        /// </summary>
        /// <param name="deleteRecipeBeverageRequest"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("ingredient/beverageforrecipe/delete")]
        public DeleteRecipeBeverageResult DeleteRecipeBeverage(DeleteRecipeBeverageRequest deleteRecipeBeverageRequest)
        {
            return _mapper.Map<DeleteRecipeBeverageResult>(_ingredientManager.DeleteRecipeBeverage(_mapper.Map<DeleteRecipeBeverageInput>(deleteRecipeBeverageRequest)));
        }

        /// <summary>
        ///     Get list of suggested beverages for a recipe
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ingredient/beverageforrecipe/suggested")]
        public IEnumerable<SuggestedBeverageByRecipeResult> SuggestedBeverageByRecipe(Guid recipeId)
        {
            return _mapper.Map<IEnumerable<SuggestedBeverageByRecipeResult>>(_ingredientManager.SuggestedBeverageByRecipe(new SuggestedBeverageByRecipeInput {RecipeId = recipeId}));
        }
    }
}