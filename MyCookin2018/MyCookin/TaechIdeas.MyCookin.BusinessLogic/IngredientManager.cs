using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TaechIdeas.Core.Core.Common;
using TaechIdeas.Core.Core.Common.Enums;
using TaechIdeas.Core.Core.LogAndMessage;
using TaechIdeas.Core.Core.LogAndMessage.Dto;
using TaechIdeas.Core.Core.Media;
using TaechIdeas.Core.Core.Media.Dto;
using TaechIdeas.Core.Core.MicrosoftTranslator;
using TaechIdeas.Core.Core.Network;
using TaechIdeas.Core.Core.Token;
using TaechIdeas.MyCookin.Core;
using TaechIdeas.MyCookin.Core.Dto;

namespace TaechIdeas.MyCookin.BusinessLogic
{
    public class IngredientManager : IIngredientManager
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly ILogManager _logManager;
        private readonly IMapper _mapper;
        private readonly IMediaManager _mediaManager;
        private readonly IMicrosoftTranslationManager _microsoftTranslationManager;
        private readonly INetworkManager _networkManager;
        private readonly ITokenManager _tokenManager;
        private readonly IUtilsManager _utilsManager;

        public IngredientManager(ILogManager logManager, INetworkManager networkManager, IUtilsManager utilsManager,
            IIngredientRepository ingredientRepository, ITokenManager tokenManager, IMediaManager mediaManager,
            IMicrosoftTranslationManager microsoftTranslationManager, IMapper mapper)
        {
            _logManager = logManager;
            _networkManager = networkManager;
            _utilsManager = utilsManager;
            _ingredientRepository = ingredientRepository;
            _tokenManager = tokenManager;
            _mediaManager = mediaManager;
            _microsoftTranslationManager = microsoftTranslationManager;
            _mapper = mapper;
        }

        #region IngredientLanguageList

        public IEnumerable<IngredientLanguageListOutput> IngredientLanguageList(
            IngredientLanguageListInput ingredientLanguageListInput)
        {
            try
            {
                //Check for Valid Token
                var checkTokenOutput = _tokenManager.CheckToken(ingredientLanguageListInput.CheckTokenInput);

                if (!checkTokenOutput.IsTokenValid) throw new Exception("Token not valid for the user.");

                return _mapper.Map<IEnumerable<IngredientLanguageListOutput>>(
                    _ingredientRepository.IngredientLanguageList(
                        _mapper.Map<IngredientLanguageListIn>(ingredientLanguageListInput)));
            }
            catch (Exception ex)
            {
                //ERROR LOG
                var logRow = new LogRowIn
                {
                    ErrorMessage =
                        $"Error in IngredientLanguageList: {ex.Message}. Object: {_utilsManager.GetAllPropertiesAndValues(ingredientLanguageListInput)}",
                    ErrorMessageCode = "IN-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = _networkManager.GetCurrentPageName()
                };

                _logManager.WriteLog(logRow);

                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region SearchIngredientByLanguageAndName

        public IEnumerable<SearchIngredientByLanguageAndNameOutput> SearchIngredientByLanguageAndName(
            SearchIngredientByLanguageAndNameInput searchIngredientByLanguageAndNameInput)
        {
            try
            {
                return
                    _mapper.Map<IEnumerable<SearchIngredientByLanguageAndNameOutput>>(
                        _ingredientRepository.SearchIngredientByLanguageAndName(
                            _mapper.Map<SearchIngredientByLanguageAndNameIn>(searchIngredientByLanguageAndNameInput)));
            }
            catch (Exception ex)
            {
                //ERROR LOG
                var logRow = new LogRowIn
                {
                    ErrorMessage =
                        $"Error in IngredientByIngredientName: {ex.Message}. Object: {_utilsManager.GetAllPropertiesAndValues(searchIngredientByLanguageAndNameInput)}",
                    ErrorMessageCode = "IN-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = _networkManager.GetCurrentPageName()
                };

                _logManager.WriteLog(logRow);

                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region IngredientQuantityTypeList

        public IEnumerable<IngredientQuantityTypeListOutput> IngredientQuantityTypeList(
            IngredientQuantityTypeListInput ingredientQuantityTypeListInput)
        {
            try
            {
                return
                    _mapper.Map<IEnumerable<IngredientQuantityTypeListOutput>>(
                        _ingredientRepository.IngredientQuantityTypeList(
                            _mapper.Map<IngredientQuantityTypeListIn>(ingredientQuantityTypeListInput)));
            }
            catch (Exception ex)
            {
                //ERROR LOG
                var logRow = new LogRowIn
                {
                    ErrorMessage =
                        $"Error in IngredientQuantityTypeList: {ex.Message}. Object: {_utilsManager.GetAllPropertiesAndValues(ingredientQuantityTypeListInput)}",
                    ErrorMessageCode = "IN-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = _networkManager.GetCurrentPageName()
                };

                _logManager.WriteLog(logRow);

                return null;
            }
        }

        #endregion

        #region GenerateIngredientFriendlyId

        public GenerateIngredientFriendlyIdOutput GenerateIngredientFriendlyId(
            GenerateIngredientFriendlyIdInput generateIngredientFriendlyIdInput)
        {
            try
            {
                return _mapper.Map<GenerateIngredientFriendlyIdOutput>(
                    _ingredientRepository.GenerateFriendlyId(
                        _mapper.Map<GenerateIngredientFriendlyIdIn>(generateIngredientFriendlyIdInput)));
            }
            catch (Exception ex)
            {
                //ERROR LOG
                var logRow = new LogRowIn
                {
                    ErrorMessage =
                        $"Error in GenerateIngredientFriendlyId: {ex.Message}. Object: {_utilsManager.GetAllPropertiesAndValues(generateIngredientFriendlyIdInput)}",
                    ErrorMessageCode = "IN-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = _networkManager.GetCurrentPageName()
                };

                _logManager.WriteLog(logRow);

                //Null in case of error
                return null;
            }
        }

        #endregion

        #region GenerateAllIngredientFriendlyId

        public bool GenerateAllIngredientFriendlyId()
        {
            try
            {
                _ingredientRepository.GenerateAllIngredientFriendlyId();
                return true;
            }
            catch (Exception ex)
            {
                //ERROR LOG
                var logRow = new LogRowIn
                {
                    ErrorMessage = $"Error in GenerateAllIngredientFriendlyId: {ex.Message}.",
                    ErrorMessageCode = "IN-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = _networkManager.GetCurrentPageName()
                };

                _logManager.WriteLog(logRow);

                //False in case of error
                return false;
            }
        }

        #endregion

        #region IngredientByFriendlyId

        public IngredientByFriendlyIdOutput IngredientByFriendlyId(IngredientByFriendlyIdInput ingredientByFriendlyIdIn)
        {
            try
            {
                var ingredientByFriendlyIdOutput =
                    _mapper.Map<IngredientByFriendlyIdOutput>(
                        _ingredientRepository.IngredientByFriendlyId(
                            _mapper.Map<IngredientByFriendlyIdIn>(ingredientByFriendlyIdIn)));

                //ADD QUANTITIES
                var allowedQuantitiesByIngredientIdInput = new AllowedQuantitiesByIngredientIdInput
                {
                    IngredientId = ingredientByFriendlyIdOutput.IngredientId,
                    LanguageId = ingredientByFriendlyIdOutput.LanguageId
                };
                ingredientByFriendlyIdOutput.AllowedQuantities =
                    AllowedQuantitiesByIngredientId(allowedQuantitiesByIngredientIdInput);

                //ADD CATEGORIES
                var categoriesByIngredientIdInput = new CategoriesByIngredientIdInput
                {
                    IngredientId = ingredientByFriendlyIdOutput.IngredientId,
                    LanguageId = ingredientByFriendlyIdOutput.LanguageId
                };
                ingredientByFriendlyIdOutput.Categories = CategoriesByIngredientId(categoriesByIngredientIdInput);

                return ingredientByFriendlyIdOutput;
            }
            catch (Exception ex)
            {
                //ERROR LOG
                var logRow = new LogRowIn
                {
                    ErrorMessage =
                        $"Error in IngredientByFriendlyId: {ex.Message}. Object: {_utilsManager.GetAllPropertiesAndValues(ingredientByFriendlyIdIn)}",
                    ErrorMessageCode = "IN-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = _networkManager.GetCurrentPageName()
                };

                _logManager.WriteLog(logRow);

                //Null in case of error
                return null;
            }
        }

        #endregion

        #region AllowedQuantitiesByIngredientId

        public IEnumerable<AllowedQuantitiesByIngredientIdOutput> AllowedQuantitiesByIngredientId(
            AllowedQuantitiesByIngredientIdInput allowedQuantitiesByIngredientIdInput)
        {
            try
            {
                return
                    _mapper.Map<IEnumerable<AllowedQuantitiesByIngredientIdOutput>>(
                        _ingredientRepository.AllowedQuantitiesByIngredientId(
                            _mapper.Map<AllowedQuantitiesByIngredientIdIn>(allowedQuantitiesByIngredientIdInput)));
            }
            catch (Exception ex)
            {
                //ERROR LOG
                var logRow = new LogRowIn
                {
                    ErrorMessage =
                        $"Error in AllowedQuantitiesByIngredientId: {ex.Message}. Object: {_utilsManager.GetAllPropertiesAndValues(allowedQuantitiesByIngredientIdInput)}",
                    ErrorMessageCode = "IN-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = _networkManager.GetCurrentPageName()
                };

                _logManager.WriteLog(logRow);

                //Null in case of error
                return null;
            }
        }

        #endregion

        #region CategoriesByIngredientId

        public IEnumerable<CategoriesByIngredientIdOutput> CategoriesByIngredientId(
            CategoriesByIngredientIdInput categoriesByIngredientIdInput)
        {
            try
            {
                return _mapper.Map<IEnumerable<CategoriesByIngredientIdOutput>>(
                    _ingredientRepository.CategoriesByIngredientId(
                        _mapper.Map<CategoriesByIngredientIdIn>(categoriesByIngredientIdInput)));
            }
            catch (Exception ex)
            {
                //ERROR LOG
                var logRow = new LogRowIn
                {
                    ErrorMessage =
                        $"Error in CategoriesByIngredientId: {ex.Message}. Object: {_utilsManager.GetAllPropertiesAndValues(categoriesByIngredientIdInput)}",
                    ErrorMessageCode = "IN-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = _networkManager.GetCurrentPageName()
                };

                _logManager.WriteLog(logRow);

                //Null in case of error
                return null;
            }
        }

        #endregion

        #region InsertIngredient

        public InsertIngredientOutput InsertIngredient(InsertIngredientInput insertIngredientInput)
        {
            try
            {
                //Check for Valid Token
                var checkTokenOutput = _tokenManager.CheckToken(insertIngredientInput.CheckTokenInput);

                if (!checkTokenOutput.IsTokenValid) throw new Exception("Token not valid for the user.");

                return _mapper.Map<InsertIngredientOutput>(
                    _ingredientRepository.InsertIngredient(_mapper.Map<InsertIngredientIn>(insertIngredientInput)));
            }
            catch (Exception ex)
            {
                //ERROR LOG
                var logRow = new LogRowIn
                {
                    ErrorMessage =
                        $"Error in InsertIngredient: {ex.Message}. Object: {_utilsManager.GetAllPropertiesAndValues(insertIngredientInput)}",
                    ErrorMessageCode = "IN-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = _networkManager.GetCurrentPageName()
                };

                _logManager.WriteLog(logRow);

                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region UpdateIngredient

        public UpdateIngredientOutput UpdateIngredient(UpdateIngredientInput updateIngredientInput)
        {
            try
            {
                //Check for Valid Token
                var checkTokenOutput = _tokenManager.CheckToken(updateIngredientInput.CheckTokenInput);

                if (!checkTokenOutput.IsTokenValid) throw new Exception("Token not valid for the user.");

                return _mapper.Map<UpdateIngredientOutput>(
                    _ingredientRepository.UpdateIngredient(_mapper.Map<UpdateIngredientIn>(updateIngredientInput)));
            }
            catch (Exception ex)
            {
                //ERROR LOG
                var logRow = new LogRowIn
                {
                    ErrorMessage =
                        $"Error in UpdateIngredient: {ex.Message}. Object: {_utilsManager.GetAllPropertiesAndValues(updateIngredientInput)}",
                    ErrorMessageCode = "IN-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = _networkManager.GetCurrentPageName()
                };

                _logManager.WriteLog(logRow);

                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region InsertIngredientLanguage

        public InsertIngredientLanguageOutput InsertIngredientLanguage(
            InsertIngredientLanguageInput insertIngredientLanguageInput)
        {
            try
            {
                //Check for Valid Token
                var checkTokenOutput = _tokenManager.CheckToken(insertIngredientLanguageInput.CheckTokenInput);

                if (!checkTokenOutput.IsTokenValid) throw new Exception("Token not valid for the user.");

                return _mapper.Map<InsertIngredientLanguageOutput>(
                    _ingredientRepository.InsertIngredientLanguage(
                        _mapper.Map<InsertIngredientLanguageIn>(insertIngredientLanguageInput)));
            }
            catch (Exception ex)
            {
                //ERROR LOG
                var logRow = new LogRowIn
                {
                    ErrorMessage =
                        $"Error in InsertIngredientLanguage: {ex.Message}. Object: {_utilsManager.GetAllPropertiesAndValues(insertIngredientLanguageInput)}",
                    ErrorMessageCode = "IN-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = _networkManager.GetCurrentPageName()
                };

                _logManager.WriteLog(logRow);

                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region UpdateIngredientLanguage

        public UpdateIngredientLanguageOutput UpdateIngredientLanguage(
            UpdateIngredientLanguageInput updateIngredientLanguageInput)
        {
            try
            {
                //Check for Valid Token
                var checkTokenOutput = _tokenManager.CheckToken(updateIngredientLanguageInput.CheckTokenInput);

                if (!checkTokenOutput.IsTokenValid) throw new Exception("Token not valid for the user.");

                return _mapper.Map<UpdateIngredientLanguageOutput>(
                    _ingredientRepository.UpdateIngredientLanguage(
                        _mapper.Map<UpdateIngredientLanguageIn>(updateIngredientLanguageInput)));
            }
            catch (Exception ex)
            {
                //ERROR LOG
                var logRow = new LogRowIn
                {
                    ErrorMessage =
                        $"Error in UpdateIngredientLanguage: {ex.Message}. Object: {_utilsManager.GetAllPropertiesAndValues(updateIngredientLanguageInput)}",
                    ErrorMessageCode = "IN-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = _networkManager.GetCurrentPageName()
                };

                _logManager.WriteLog(logRow);

                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region DeleteIngredient

        public DeleteIngredientOutput DeleteIngredient(DeleteIngredientInput deleteIngredientInput)
        {
            try
            {
                //Check for Valid Token
                var checkTokenOutput = _tokenManager.CheckToken(deleteIngredientInput.CheckTokenInput);

                if (!checkTokenOutput.IsTokenValid) throw new Exception("Token not valid for the user.");

                return _mapper.Map<DeleteIngredientOutput>(
                    _ingredientRepository.DeleteIngredient(_mapper.Map<DeleteIngredientIn>(deleteIngredientInput)));
            }
            catch (Exception ex)
            {
                //ERROR LOG
                var logRow = new LogRowIn
                {
                    ErrorMessage =
                        $"Error in DeleteIngredient: {ex.Message}. Object: {_utilsManager.GetAllPropertiesAndValues(deleteIngredientInput)}",
                    ErrorMessageCode = "IN-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = _networkManager.GetCurrentPageName()
                };

                _logManager.WriteLog(logRow);

                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region QuantityNotStdByIdAndLanguage

        public QuantityNotStdByIdAndLanguageOutput QuantityNotStdByIdAndLanguage(
            QuantityNotStdByIdAndLanguageInput quantityNotStdByIdAndLanguageInput)
        {
            try
            {
                return
                    _mapper.Map<QuantityNotStdByIdAndLanguageOutput>(
                        _ingredientRepository.QuantityNotStdByIdAndLanguage(
                            _mapper.Map<QuantityNotStdByIdAndLanguageIn>(quantityNotStdByIdAndLanguageInput)));
            }
            catch (Exception ex)
            {
                //ERROR LOG
                var logRow = new LogRowIn
                {
                    ErrorMessage =
                        $"Error in GetQuantityNotStdByIdAndLanguage: {ex.Message}. IdQuantityNotStd: {quantityNotStdByIdAndLanguageInput.QuantityNotStdId}, IdLanguage: {quantityNotStdByIdAndLanguageInput.LanguageId}",
                    ErrorMessageCode = "RC-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = _networkManager.GetCurrentPageName()
                };

                _logManager.WriteLog(logRow);

                //Null in case of error
                return null;
            }
        }

        #endregion

        #region IngredientQuantityTypeLanguageOutput

        public IngredientQuantityTypeLanguageOutput IngredientQuantityTypeLanguage(
            IngredientQuantityTypeLanguageInput ingredientQuantityTypeLanguageInput)
        {
            try
            {
                return
                    _mapper.Map<IngredientQuantityTypeLanguageOutput>(
                        _ingredientRepository.IngredientQuantityTypeLanguage(
                            _mapper.Map<IngredientQuantityTypeLanguageIn>(ingredientQuantityTypeLanguageInput)));
            }
            catch (Exception ex)
            {
                //ERROR LOG
                var logRow = new LogRowIn
                {
                    ErrorMessage =
                        $"Error in GetIngredientQuantityTypeLanguage: {ex.Message}. IdIngredientQuantityType: {ingredientQuantityTypeLanguageInput.IngredientQuantityTypeId}, IdLanguage: {ingredientQuantityTypeLanguageInput.LanguageId}",
                    ErrorMessageCode = "RC-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = _networkManager.GetCurrentPageName()
                };

                _logManager.WriteLog(logRow);

                //Null in case of error
                return null;
            }
        }

        #endregion

        #region IngredientsByIdRecipeAndLanguage

        public IEnumerable<IngredientsByIdRecipeAndLanguageOutput> IngredientsByIdRecipeAndLanguage(
            IngredientsByIdRecipeAndLanguageInput ingredientsByIdRecipeAndLanguageInput)
        {
            try
            {
                var ingredientsByIdRecipeAndLanguageOutput =
                    _mapper.Map<IEnumerable<IngredientsByIdRecipeAndLanguageOutput>>(
                        _ingredientRepository.IngredientsByIdRecipeAndLanguage(
                            _mapper.Map<IngredientsByIdRecipeAndLanguageIn>(ingredientsByIdRecipeAndLanguageInput)));

                ingredientsByIdRecipeAndLanguageOutput =
                    AddQuantityTypeLanguage(ingredientsByIdRecipeAndLanguageOutput);
                ingredientsByIdRecipeAndLanguageOutput =
                    AddCalculatedIngredient(ingredientsByIdRecipeAndLanguageOutput);

                //Enumerate to List
                var ingredientByIdRecipeAndLanguageOutputs =
                    ingredientsByIdRecipeAndLanguageOutput as IList<IngredientsByIdRecipeAndLanguageOutput> ??
                    ingredientsByIdRecipeAndLanguageOutput.ToList();

                //Add Steps ImageUrl
                ingredientByIdRecipeAndLanguageOutputs.ToList()
                    .ForEach(c => c.ImageUrl = c.IngredientImageId == null
                        ? null
                        : _mediaManager.MediaPathByMediaId(new MediaPathByMediaIdInput
                            {MediaId = (Guid) c.IngredientImageId}).MediaPath);

                return ingredientByIdRecipeAndLanguageOutputs;
            }
            catch (Exception ex)
            {
                //ERROR LOG
                var logRow = new LogRowIn
                {
                    ErrorMessage =
                        $"Error in GetIngredientsByIdRecipeAndLanguage: {ex.Message}. IdRecipe: {ingredientsByIdRecipeAndLanguageInput.RecipeId}, IdLanguage: {ingredientsByIdRecipeAndLanguageInput.LanguageId}",
                    ErrorMessageCode = "RC-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = _networkManager.GetCurrentPageName()
                };

                _logManager.WriteLog(logRow);

                //Null in case of error
                return null;
            }
        }

        #endregion

        #region AddQuantityTypeLanguage

        public IEnumerable<IngredientsByIdRecipeAndLanguageOutput> AddQuantityTypeLanguage(
            IEnumerable<IngredientsByIdRecipeAndLanguageOutput> recipeIngredientDetailsLanguageMapped)
        {
            var recipeIngredientsList = new List<IngredientsByIdRecipeAndLanguageOutput>();

            try
            {
                foreach (var recipeIngredientDetailsLanguage in recipeIngredientDetailsLanguageMapped)
                {
                    recipeIngredientDetailsLanguage.CalculatedQuantityTypeLanguage =
                        CalculateQuantityTypeLanguage(recipeIngredientDetailsLanguage);
                    recipeIngredientsList.Add(recipeIngredientDetailsLanguage);
                }

                return recipeIngredientsList;
            }
            catch (Exception ex)
            {
                //ERROR LOG
                var logRow = new LogRowIn
                {
                    ErrorMessage = $"Error in AddQuantityTypeLanguage: {ex.Message}.",
                    ErrorMessageCode = "RC-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = _networkManager.GetCurrentPageName()
                };

                _logManager.WriteLog(logRow);

                //Null in case of error
                return recipeIngredientsList;
            }
        }

        #endregion

        #region CalculateQuantityTypeLanguage

        public string CalculateQuantityTypeLanguage(
            IngredientsByIdRecipeAndLanguageOutput ingredientsByIdRecipeAndLanguageOutput)
        {
            //TODO: Manage here QuantityNotSpecified ??
            try
            {
                var isSingular = ingredientsByIdRecipeAndLanguageOutput.Quantity < 2;

                //Get QuantityType from IngredientsQuantityTypesLanguages

                var ingredientQuantityTypeLanguageInput = new IngredientQuantityTypeLanguageInput
                {
                    LanguageId = ingredientsByIdRecipeAndLanguageOutput.LanguageId,
                    IngredientQuantityTypeId = ingredientsByIdRecipeAndLanguageOutput.IngredientQuantityTypeId
                };

                var quantityType = IngredientQuantityTypeLanguage(ingredientQuantityTypeLanguageInput);

                if (quantityType == null) throw new Exception("Quantity type null in CalculateQuantityTypeLanguage.");

                var quantityTypeSingularOrPlural = isSingular
                    ? quantityType.IngredientQuantityTypeSingular
                    : quantityType.IngredientQuantityTypePlural;

                //TODO: CHECK!
                //if (!string.IsNullOrEmpty(IngredientsByIdRecipeAndLanguageOutput.QuantityNotStdId))
                if (ingredientsByIdRecipeAndLanguageOutput.QuantityNotStdId > 0)
                {
                    //Get Quantity Not Std

                    var quantityNotStdByIdAndLanguageInput = new QuantityNotStdByIdAndLanguageInput
                    {
                        LanguageId = ingredientsByIdRecipeAndLanguageOutput.LanguageId,
                        QuantityNotStdId = ingredientsByIdRecipeAndLanguageOutput.QuantityNotStdId
                    };
                    var quantityNotStd = QuantityNotStdByIdAndLanguage(quantityNotStdByIdAndLanguageInput);

                    //Replace prior quantityType
                    quantityTypeSingularOrPlural = isSingular
                        ? quantityNotStd.QuantityNotStdSingular
                        : quantityNotStd.QuantityNotStdPlural;
                }

                //TODO: URGENT! A VOLTE SPUNTA "QUALCHE DI" OPPURE "NUMERO DI (SALE)"   PROVA getbylanguage con languageId 2 e id = 'e0994996-6f1d-481a-99cc-8e7bf59ccd07' 

                var quantityTypeLanguage =
                    $"{quantityType.IngredientQuantityTypeWordsShowBefore} {quantityTypeSingularOrPlural} {quantityType.IngredientQuantityTypeWordsShowAfter}";

                return quantityTypeLanguage.Trim();
            }
            catch (Exception ex)
            {
                //ERROR LOG
                var logRow = new LogRowIn
                {
                    ErrorMessage =
                        $"Error in GetQuantityTypeLanguage: {ex.Message}. Object RecipeIngredientDetailsLanguage: {_utilsManager.GetAllPropertiesAndValues(ingredientsByIdRecipeAndLanguageOutput)}",
                    ErrorMessageCode = "RC-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = _networkManager.GetCurrentPageName()
                };

                _logManager.WriteLog(logRow);

                //Null in case of error
                return string.Empty;
            }
        }

        #endregion

        #region AddCalculatedIngredient

        public IEnumerable<IngredientsByIdRecipeAndLanguageOutput> AddCalculatedIngredient(
            IEnumerable<IngredientsByIdRecipeAndLanguageOutput> ingredientsByIdRecipeAndLanguageOutput)
        {
            var recipeIngredientsList = new List<IngredientsByIdRecipeAndLanguageOutput>();

            try
            {
                //TODO: TEMPORARY!! MANAGE ALL THE EXCLUSIONS FOR NOT STANDARD QUANTITIES (EX: SALT)
                var temporaryExclusions = new[] {218};

                foreach (var recipeIngredientDetailsLanguage in ingredientsByIdRecipeAndLanguageOutput)
                {
                    //TODO: TEMPORARY!!
                    if (temporaryExclusions.Contains(recipeIngredientDetailsLanguage.IngredientQuantityTypeId))
                        recipeIngredientDetailsLanguage.Quantity = 0;

                    recipeIngredientDetailsLanguage.IngredientName =
                        GetCalculatedIngredient(recipeIngredientDetailsLanguage);
                    recipeIngredientsList.Add(recipeIngredientDetailsLanguage);
                }

                return recipeIngredientsList;
            }
            catch (Exception ex)
            {
                //ERROR LOG
                var logRow = new LogRowIn
                {
                    ErrorMessage = $"Error in AddCalculatedIngredient: {ex.Message}.",
                    ErrorMessageCode = "RC-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = _networkManager.GetCurrentPageName()
                };

                _logManager.WriteLog(logRow);

                //Null in case of error
                return recipeIngredientsList;
            }
        }

        #endregion

        #region GetCalculatedIngredient

        public string GetCalculatedIngredient(
            IngredientsByIdRecipeAndLanguageOutput ingredientsByIdRecipeAndLanguageOutput)
        {
            try
            {
                var isSingular = ingredientsByIdRecipeAndLanguageOutput.Quantity < 2;

                var calculatedIngredient = isSingular
                    ? ingredientsByIdRecipeAndLanguageOutput.IngredientSingular
                    : ingredientsByIdRecipeAndLanguageOutput.IngredientPlural;

                //QuantityNotStdId
                //TODO: If !isStandardQuantityType remove Quantity (ex.: salt)

                //TODO: Manage Exceptions...

                return calculatedIngredient;
            }
            catch (Exception ex)
            {
                //ERROR LOG
                var logRow = new LogRowIn
                {
                    ErrorMessage =
                        $"Error in GetQuantityTypeLanguage: {ex.Message}. Object RecipeIngredientDetailsLanguage: {_utilsManager.GetAllPropertiesAndValues(ingredientsByIdRecipeAndLanguageOutput)}",
                    ErrorMessageCode = "RC-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = _networkManager.GetCurrentPageName()
                };

                _logManager.WriteLog(logRow);

                //Null in case of error
                return string.Empty;
            }
        }

        #endregion

        #region IngredientsByIdRecipe

        public IEnumerable<IngredientsByIdRecipeOutput> IngredientsByIdRecipe(
            IngredientsByIdRecipeInput ingredientsByIdRecipeInput)
        {
            try
            {
                return _mapper.Map<IEnumerable<IngredientsByIdRecipeOutput>>(
                    _ingredientRepository.IngredientsByIdRecipe(
                        _mapper.Map<IngredientsByIdRecipeIn>(ingredientsByIdRecipeInput)));
            }
            catch (Exception ex)
            {
                //ERROR LOG
                var logRow = new LogRowIn
                {
                    ErrorMessage =
                        $"Error in GetIngredientsByIdRecipe: {ex.Message}. IdRecipe: {ingredientsByIdRecipeInput.RecipeId}",
                    ErrorMessageCode = "MD-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = _networkManager.GetCurrentPageName()
                };

                _logManager.WriteLog(logRow);

                //Null in case of error
                return null;
            }
        }

        #endregion

        #region TranslateBunchOfIngredients

        public TranslateBunchOfIngredientsOutput TranslateBunchOfIngredients(
            TranslateBunchOfIngredientsInput translateBunchOfIngredientsInput)
        {
            try
            {
                var ingredientsToTranslateOut =
                    _ingredientRepository.IngredientsToTranslate(
                        _mapper.Map<IngredientsToTranslateIn>(translateBunchOfIngredientsInput));

                var ingredientsToTranslateOuts = ingredientsToTranslateOut as IList<IngredientsToTranslateOut> ??
                                                 ingredientsToTranslateOut.ToList();
                if (!ingredientsToTranslateOuts.Any()) return null;

                var addNewIngredientTranslationIn = new AddNewIngredientTranslationIn();

                var languageFrom = _utilsManager.GetLanguageCodeFromId(translateBunchOfIngredientsInput.LanguageIdFrom);
                var languageTo = _utilsManager.GetLanguageCodeFromId(translateBunchOfIngredientsInput.LanguageIdTo);

                foreach (var ingredientToTranslate in ingredientsToTranslateOuts)
                {
                    addNewIngredientTranslationIn.IngredientId = ingredientToTranslate.IDIngredient;
                    addNewIngredientTranslationIn.LanguageId = translateBunchOfIngredientsInput.LanguageIdTo;
                    addNewIngredientTranslationIn.IngredientSingular =
                        string.IsNullOrEmpty(ingredientToTranslate.IngredientSingular)
                            ? null
                            : _microsoftTranslationManager
                                .TranslateSentence(languageFrom, languageTo, ingredientToTranslate.IngredientSingular)
                                .TranslatedSentence;
                    addNewIngredientTranslationIn.IngredientPlural =
                        string.IsNullOrEmpty(ingredientToTranslate.IngredientPlural)
                            ? null
                            : _microsoftTranslationManager
                                .TranslateSentence(languageFrom, languageTo, ingredientToTranslate.IngredientPlural)
                                .TranslatedSentence;
                    addNewIngredientTranslationIn.IngredientDescription =
                        string.IsNullOrEmpty(ingredientToTranslate.IngredientDescription)
                            ? null
                            : _microsoftTranslationManager.TranslateSentence(languageFrom, languageTo,
                                ingredientToTranslate.IngredientDescription).TranslatedSentence;
                    addNewIngredientTranslationIn.IsAutoTranslate = true;

                    var addNewIngredientTranslationOut =
                        _ingredientRepository.AddNewIngredientTranslation(addNewIngredientTranslationIn);

                    if (!addNewIngredientTranslationOut.NewTranslationAdded)
                        throw new Exception("Error Translating Ingredient from Stored Procedure.");
                }

                return new TranslateBunchOfIngredientsOutput {BunchOfIngredientsTranslated = true};
            }
            catch (Exception ex)
            {
                //ERROR LOG
                var logRow = new LogRowIn
                {
                    ErrorMessage =
                        $"Error in TranslateBunchOfIngredients: {ex.Message}. Object: {_utilsManager.GetAllPropertiesAndValues(translateBunchOfIngredientsInput)}",
                    ErrorMessageCode = "MD-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = _networkManager.GetCurrentPageName()
                };

                _logManager.WriteLog(logRow);

                //Null in case of error
                return null;
            }
        }

        #endregion

        /****************************************************************/
        //BEVERAGES
        /****************************************************************/

        #region SearchBeverageByLanguage

        public IEnumerable<SearchBeverageByLanguageOutput> SearchBeverageByLanguage(
            SearchBeverageByLanguageInput searchBeverageByLanguageInput)
        {
            try
            {
                return _mapper.Map<IEnumerable<SearchBeverageByLanguageOutput>>(
                    _ingredientRepository.SearchBeverageByLanguage(
                        _mapper.Map<SearchBeverageByLanguageIn>(searchBeverageByLanguageInput)));
            }
            catch (Exception ex)
            {
                //ERROR LOG
                var logRow = new LogRowIn
                {
                    ErrorMessage =
                        $"Error in SearchBeverageByLanguage: {ex.Message}. Object: {_utilsManager.GetAllPropertiesAndValues(searchBeverageByLanguageInput)}",
                    ErrorMessageCode = "IN-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = _networkManager.GetCurrentPageName()
                };

                _logManager.WriteLog(logRow);

                return null;
            }
        }

        #endregion

        #region AddRecipeBeverage

        public AddRecipeBeverageOutput AddRecipeBeverage(AddRecipeBeverageInput addRecipeBeverageInput)
        {
            try
            {
                return _mapper.Map<AddRecipeBeverageOutput>(
                    _ingredientRepository.AddRecipeBeverage(_mapper.Map<AddRecipeBeverageIn>(addRecipeBeverageInput)));
            }
            catch (Exception ex)
            {
                //ERROR LOG
                var logRow = new LogRowIn
                {
                    ErrorMessage =
                        $"Error in AddRecipeBeverage: {ex.Message}. Object: {_utilsManager.GetAllPropertiesAndValues(addRecipeBeverageInput)}",
                    ErrorMessageCode = "IN-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = _networkManager.GetCurrentPageName()
                };

                _logManager.WriteLog(logRow);

                return null;
            }
        }

        #endregion

        #region DeleteRecipeBeverage

        public DeleteRecipeBeverageOutput DeleteRecipeBeverage(DeleteRecipeBeverageInput deleteRecipeBeverageInput)
        {
            try
            {
                return _mapper.Map<DeleteRecipeBeverageOutput>(
                    _ingredientRepository.DeleteRecipeBeverage(
                        _mapper.Map<DeleteRecipeBeverageIn>(deleteRecipeBeverageInput)));
            }
            catch (Exception ex)
            {
                //ERROR LOG
                var logRow = new LogRowIn
                {
                    ErrorMessage =
                        $"Error in DeleteRecipeBeverage: {ex.Message}. Object: {_utilsManager.GetAllPropertiesAndValues(deleteRecipeBeverageInput)}",
                    ErrorMessageCode = "IN-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = _networkManager.GetCurrentPageName()
                };

                _logManager.WriteLog(logRow);

                return null;
            }
        }

        #endregion

        #region SuggestedBeverageByRecipe

        public IEnumerable<SuggestedBeverageByRecipeOutput> SuggestedBeverageByRecipe(
            SuggestedBeverageByRecipeInput suggestedBeverageByRecipeInput)
        {
            try
            {
                return
                    _mapper.Map<IEnumerable<SuggestedBeverageByRecipeOutput>>(
                        _ingredientRepository.SuggestedBeverageByRecipe(
                            _mapper.Map<SuggestedBeverageByRecipeIn>(suggestedBeverageByRecipeInput)));
            }
            catch (Exception ex)
            {
                //ERROR LOG
                var logRow = new LogRowIn
                {
                    ErrorMessage =
                        $"Error in SuggestedBeverageByRecipe: {ex.Message}. Object: {_utilsManager.GetAllPropertiesAndValues(suggestedBeverageByRecipeInput)}",
                    ErrorMessageCode = "IN-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = _networkManager.GetCurrentPageName()
                };

                _logManager.WriteLog(logRow);

                return null;
            }
        }

        #endregion

        /****************************************************************/
    }
}