using System;
using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaechIdeas.Core.Core.Common;
using TaechIdeas.Core.Core.Common.Enums;
using TaechIdeas.Core.Core.LogAndMessage;
using TaechIdeas.Core.Core.LogAndMessage.Dto;
using TaechIdeas.Core.Core.Token.Dto;
using TaechIdeas.MyCookin.Core;
using TaechIdeas.MyCookin.Core.Dto;

namespace TaechIdeas.MyCookin.API.Controllers
{
    /// <summary>
    ///     Definition of API for Recipe
    /// </summary>
    [Route("mycookin")]
    public class RecipeController : ControllerBase
    {
        private readonly ILogManager _logManager;
        private readonly IMapper _mapper;
        private readonly IRecipeManager _recipeManager;
        private readonly IUtilsManager _utilsManager;

        public RecipeController(IRecipeManager recipeManager, IUtilsManager utilsManager, ILogManager logManager,
            IMapper mapper)
        {
            _recipeManager = recipeManager;
            _utilsManager = utilsManager;
            _logManager = logManager;
            _mapper = mapper;
        }

        /// <summary>
        ///     Search Recipes
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("recipe/search")]
        public IEnumerable<SearchRecipesResult> SearchRecipes(SearchRecipesRequest searchRecipesRequest)
        {
            try
            {
                return _mapper.Map<IEnumerable<SearchRecipesResult>>(
                    _recipeManager.SearchRecipes(_mapper.Map<SearchRecipesInput>(searchRecipesRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "RC-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin =
                        $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(searchRecipesRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        ///     Get Recipe according to language
        /// </summary>
        /// <param name="friendlyId"></param>
        /// <param name="includeIngredients"></param>
        /// <param name="includeSteps"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("recipe/getbyfriendlyid")]
        public RecipeByFriendlyIdResult RecipeByFriendlyId(string friendlyId, bool includeIngredients,
            bool includeSteps, bool includeProperties)
        {
            try
            {
                return
                    _mapper.Map<RecipeByFriendlyIdResult>(
                        _recipeManager.RecipeByFriendlyId(new RecipeByFriendlyIdInput
                        {
                            FriendlyId = friendlyId,
                            IncludeIngredients = includeIngredients,
                            IncludeProperties = includeProperties,
                            IncludeSteps = includeSteps
                        }));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "RC-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin =
                        $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | FriendlyId: {friendlyId}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        ///     Get Top Recipes according to language
        /// </summary>
        /// <param name="languageId"></param>
        /// <param name="includeIngredients"></param>
        /// <param name="includeSteps"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("recipe/gettopbylanguage")]
        public IEnumerable<TopRecipesByLanguageResult> TopRecipesByLanguage(int languageId, bool includeIngredients,
            bool includeSteps, bool includeProperties)
        {
            try
            {
                var topRecipesByLanguage = new TopRecipesByLanguageInput
                {
                    LanguageId = languageId,
                    IncludeIngredients = includeIngredients,
                    IncludeProperties = includeProperties,
                    IncludeSteps = includeSteps
                };

                return _mapper.Map<IEnumerable<TopRecipesByLanguageResult>>(
                    _recipeManager.TopRecipesByLanguage(topRecipesByLanguage));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "RC-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin =
                        $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | LanguageId: {languageId}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        ///     Get Best Recipes according to language
        /// </summary>
        /// <param name="languageId"></param>
        /// <param name="includeIngredients"></param>
        /// <param name="includeSteps"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("recipe/getbestbylanguage")]
        public IEnumerable<BestRecipesByLanguageResult> BestRecipesByLanguage(int languageId, bool includeIngredients,
            bool includeSteps, bool includeProperties)
        {
            try
            {
                var bestRecipesByLanguage = new BestRecipesByLanguageInput
                {
                    LanguageId = languageId,
                    IncludeIngredients = includeIngredients,
                    IncludeProperties = includeProperties,
                    IncludeSteps = includeSteps
                };

                return _mapper.Map<IEnumerable<BestRecipesByLanguageResult>>(
                    _recipeManager.BestRecipesByLanguage(bestRecipesByLanguage));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "RC-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin =
                        $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | LanguageId: {languageId}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        ///     Add new Recipe according to language
        /// </summary>
        /// <param name="newRecipeRequest"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("recipe/new")]
        public NewRecipeResult NewRecipe(NewRecipeRequest newRecipeRequest)
        {
            try
            {
                return _mapper.Map<NewRecipeResult>(
                    _recipeManager.NewRecipe(_mapper.Map<NewRecipeInput>(newRecipeRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "RC-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin =
                        $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(newRecipeRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        ///     Update Recipe main info
        /// </summary>
        /// <param name="updateRecipeLanguageRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("recipe/update")]
        public UpdateRecipeLanguageResult UpdateRecipeLanguage(UpdateRecipeLanguageRequest updateRecipeLanguageRequest)
        {
            try
            {
                return _mapper.Map<UpdateRecipeLanguageResult>(
                    _recipeManager.UpdateRecipeLanguage(
                        _mapper.Map<UpdateRecipeLanguageInput>(updateRecipeLanguageRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "RC-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin =
                        $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(updateRecipeLanguageRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        ///     Update a Recipe Step
        /// </summary>
        /// <param name="updateRecipeLanguageStepRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("recipe/step/update")]
        public UpdateRecipeLanguageStepResult UpdateRecipeLanguageStep(
            UpdateRecipeLanguageStepRequest updateRecipeLanguageStepRequest)
        {
            try
            {
                return _mapper.Map<UpdateRecipeLanguageStepResult>(
                    _recipeManager.UpdateRecipeLanguageStep(
                        _mapper.Map<UpdateRecipeLanguageStepInput>(updateRecipeLanguageStepRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "RC-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin =
                        $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(updateRecipeLanguageStepRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        ///     New Step for a Recipe
        /// </summary>
        /// <param name="newRecipeLanguageStepsRequest"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("recipe/step/new")]
        public IEnumerable<NewRecipeLanguageStepsResult> NewRecipeLanguageSteps(
            IEnumerable<NewRecipeLanguageStepsRequest> newRecipeLanguageStepsRequest)
        {
            try
            {
                return
                    _mapper.Map<IEnumerable<NewRecipeLanguageStepsResult>>(
                        _recipeManager.NewRecipeLanguageSteps(
                            _mapper.Map<IEnumerable<NewRecipeLanguageStepsInput>>(newRecipeLanguageStepsRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "RC-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin =
                        $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(newRecipeLanguageStepsRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        ///     Update Recipe Ingredient
        /// </summary>
        /// <param name="updateRecipeIngredientRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("recipe/ingredient/update")]
        public UpdateRecipeIngredientResult UpdateRecipeIngredient(
            UpdateRecipeIngredientRequest updateRecipeIngredientRequest)
        {
            try
            {
                return _mapper.Map<UpdateRecipeIngredientResult>(
                    _recipeManager.UpdateRecipeIngredient(
                        _mapper.Map<UpdateRecipeIngredientInput>(updateRecipeIngredientRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "RC-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin =
                        $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(updateRecipeIngredientRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        ///     Add new ingredient to the recipe
        /// </summary>
        /// <param name="addNewIngredientToRecipeRequest"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("recipe/ingredient/new")]
        public IEnumerable<AddNewIngredientToRecipeResult> AddNewIngredientToRecipe(
            IEnumerable<AddNewIngredientToRecipeRequest> addNewIngredientToRecipeRequest)
        {
            try
            {
                return
                    _mapper.Map<IEnumerable<AddNewIngredientToRecipeResult>>(
                        _recipeManager.AddNewIngredientToRecipe(
                            _mapper.Map<IEnumerable<AddNewIngredientToRecipeInput>>(addNewIngredientToRecipeRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "RC-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin =
                        $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(addNewIngredientToRecipeRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        ///     Return a list of all the recipes by language
        /// </summary>
        /// <param name="recipeLanguageListRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("recipe/list/")]
        public IEnumerable<RecipeLanguageListResult> RecipeLanguageList(
            RecipeLanguageListRequest recipeLanguageListRequest)
        {
            try
            {
                return _mapper.Map<IEnumerable<RecipeLanguageListResult>>(
                    _recipeManager.RecipeLanguageList(_mapper.Map<RecipeLanguageListInput>(recipeLanguageListRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "RC-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin =
                        $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(recipeLanguageListRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        ///     Delete a recipe
        /// </summary>
        /// <param name="deleteRecipeRequest"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("recipe/delete/")]
        public DeleteRecipeResult DeleteRecipe(DeleteRecipeRequest deleteRecipeRequest)
        {
            try
            {
                return _mapper.Map<DeleteRecipeResult>(
                    _recipeManager.DeleteRecipe(_mapper.Map<DeleteRecipeInput>(deleteRecipeRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "RC-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin =
                        $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(deleteRecipeRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        ///     Get Properties List by Type and Language
        /// </summary>
        /// <param name="languageId"></param>
        /// <param name="recipePropertyTypeId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("recipe/properties/type/all")]
        public IEnumerable<PropertiesListByTypeAndLanguageResult> PropertiesListByTypeAndLanguage(int languageId,
            int recipePropertyTypeId)
        {
            try
            {
                var propertiesListByTypeAndLanguageInput = new PropertiesListByTypeAndLanguageInput
                    {LanguageId = languageId, RecipePropertyTypeId = recipePropertyTypeId};

                return _mapper.Map<IEnumerable<PropertiesListByTypeAndLanguageResult>>(
                    _recipeManager.PropertiesListByTypeAndLanguage(propertiesListByTypeAndLanguageInput));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "RC-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin =
                        $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | LanguageId: {languageId}; RecipePropertyTypeId: {recipePropertyTypeId}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        ///     Get Properties List by Type, Language and Recipe
        /// </summary>
        /// <param name="languageId"></param>
        /// <param name="recipePropertyTypeId"></param>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("recipe/properties/type")]
        public IEnumerable<PropertiesListByTypeLanguageAndRecipeResult> PropertiesListByTypeLanguageAndRecipe(
            int languageId, int recipePropertyTypeId, Guid recipeId)
        {
            try
            {
                var propertiesListByTypeLanguageAndRecipeInput = new PropertiesListByTypeLanguageAndRecipeInput
                {
                    LanguageId = languageId,
                    RecipePropertyTypeId = recipePropertyTypeId,
                    RecipeId = recipeId
                };

                return _mapper.Map<IEnumerable<PropertiesListByTypeLanguageAndRecipeResult>>(
                    _recipeManager.PropertiesListByTypeLanguageAndRecipe(propertiesListByTypeLanguageAndRecipeInput));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "RC-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin =
                        $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | LanguageId: {languageId}; RecipePropertyTypeId: {recipePropertyTypeId}; RecipeId: {recipeId}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        ///     Add or Update Recipe Property Values
        /// </summary>
        /// <param name="checkTokenRequest"></param>
        /// <param name="addOrUpdatePropertyValueRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("recipe/properties/addorupdate")]
        public IEnumerable<AddOrUpdatePropertyValueResult> AddOrUpdatePropertyValue(CheckTokenRequest checkTokenRequest,
            IEnumerable<AddOrUpdatePropertyValueRequest> addOrUpdatePropertyValueRequest)
        {
            try
            {
                return
                    _mapper.Map<IEnumerable<AddOrUpdatePropertyValueResult>>(_recipeManager.AddOrUpdatePropertyValue(
                        _mapper.Map<CheckTokenInput>(checkTokenRequest),
                        _mapper.Map<IEnumerable<AddOrUpdatePropertyValueInput>>(addOrUpdatePropertyValueRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "RC-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin =
                        $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(addOrUpdatePropertyValueRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        ///     Delete Recipe Property Value
        /// </summary>
        /// <param name="deletePropertyValueRequest"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("recipe/property/delete")]
        public DeletePropertyValueResult DeletePropertyValue(DeletePropertyValueRequest deletePropertyValueRequest)
        {
            try
            {
                return _mapper.Map<DeletePropertyValueResult>(
                    _recipeManager.DeletePropertyValue(
                        _mapper.Map<DeletePropertyValueInput>(deletePropertyValueRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "RC-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin =
                        $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(deletePropertyValueRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        ///     Translate bunch of recipes
        /// </summary>
        /// <param name="translateBunchOfRecipesRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("recipe/translate")]
        public TranslateBunchOfRecipesResult TranslateBunchOfRecipes(
            TranslateBunchOfRecipesRequest translateBunchOfRecipesRequest)
        {
            try
            {
                return _mapper.Map<TranslateBunchOfRecipesResult>(
                    _recipeManager.TranslateBunchOfRecipes(
                        _mapper.Map<TranslateBunchOfRecipesInput>(translateBunchOfRecipesRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "RC-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin =
                        $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(translateBunchOfRecipesRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }
    }
}