using System;
using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaechIdeas.Core.Core.Common;
using TaechIdeas.Core.Core.Common.Enums;
using TaechIdeas.Core.Core.LogAndMessage;
using TaechIdeas.Core.Core.LogAndMessage.Dto;
using TaechIdeas.Core.Core.UserBoard;
using TaechIdeas.Core.Core.UserBoard.Dto;

namespace TaechIdeas.Core.API.Controllers
{
    /// <summary>
    ///     Definition of API for User Board
    /// </summary>
    [Route("core")]
    public class UserBoardController : ControllerBase
    {
        private readonly IUserBoardManager _userBoardManager;
        private readonly IUtilsManager _utilsManager;
        private readonly ILogManager _logManager;
        private readonly IMapper _mapper;

        public UserBoardController(IUserBoardManager userBoardManager, IUtilsManager utilsManager, ILogManager logManager, IMapper mapper)
        {
            _userBoardManager = userBoardManager;
            _utilsManager = utilsManager;
            _logManager = logManager;
            _mapper = mapper;
        }

        /// <summary>
        /// </summary>
        /// <param name="blockElementRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("userboard/blockelement")]
        public IEnumerable<BlockElementResult> BlockElement(BlockElementRequest blockElementRequest)
        {
            try
            {
                return _mapper.Map<IEnumerable<BlockElementResult>>(_userBoardManager.BlockElement(_mapper.Map<BlockElementInput>(blockElementRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "UB-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(blockElementRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="fatherOrSonRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("userboard/fatherorson")]
        public IEnumerable<FatherOrSonResult> FatherOrSon(FatherOrSonRequest fatherOrSonRequest)
        {
            try
            {
                return _mapper.Map<IEnumerable<FatherOrSonResult>>(_userBoardManager.FatherOrSon(_mapper.Map<FatherOrSonInput>(fatherOrSonRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "UB-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(fatherOrSonRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="withPaginationRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("userboard/postswithpagination")]
        public IEnumerable<WithPaginationResult> WithPagination(WithPaginationRequest withPaginationRequest)
        {
            try
            {
                return _mapper.Map<IEnumerable<WithPaginationResult>>(_userBoardManager.WithPagination(_mapper.Map<WithPaginationInput>(withPaginationRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "UB-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(withPaginationRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="blockLoadRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("userboard/blockload")]
        public IEnumerable<BlockLoadResult> BlockLoad(BlockLoadRequest blockLoadRequest)
        {
            try
            {
                return _mapper.Map<IEnumerable<BlockLoadResult>>(_userBoardManager.BlockLoad(_mapper.Map<BlockLoadInput>(blockLoadRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "UB-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(blockLoadRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="myNewsBlockLoadRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("userboard/mynewsblockload")]
        public IEnumerable<MyNewsBlockLoadResult> MyNewsBlockLoad(MyNewsBlockLoadRequest myNewsBlockLoadRequest)
        {
            try
            {
                return _mapper.Map<IEnumerable<MyNewsBlockLoadResult>>(_userBoardManager.MyNewsBlockLoad(_mapper.Map<MyNewsBlockLoadInput>(myNewsBlockLoadRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "UB-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(myNewsBlockLoadRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="checkIfSharedRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("userboard/checkifshared")]
        public CheckIfSharedResult CheckIfShared(CheckIfSharedRequest checkIfSharedRequest)
        {
            try
            {
                return _mapper.Map<CheckIfSharedResult>(_userBoardManager.CheckIfShared(_mapper.Map<CheckIfSharedInput>(checkIfSharedRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "UB-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(checkIfSharedRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="insertActionSharedRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("userboard/insertactionshared")]
        public InsertActionSharedResult InsertActionShared(InsertActionSharedRequest insertActionSharedRequest)
        {
            try
            {
                return _mapper.Map<InsertActionSharedResult>(_userBoardManager.InsertActionShared(_mapper.Map<InsertActionSharedInput>(insertActionSharedRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "UB-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(insertActionSharedRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="insertActionRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("userboard/insertaction")]
        public InsertActionResult InsertAction(InsertActionRequest insertActionRequest)
        {
            try
            {
                return _mapper.Map<InsertActionResult>(_userBoardManager.InsertAction(_mapper.Map<InsertActionInput>(insertActionRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "UB-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(insertActionRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="checkIfSharedOnPersonalBoardRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("userboard/checkifsharedonboard")]
        public CheckIfSharedOnPersonalBoardResult CheckIfSharedOnPersonalBoard(CheckIfSharedOnPersonalBoardRequest checkIfSharedOnPersonalBoardRequest)
        {
            try
            {
                return
                    _mapper.Map<CheckIfSharedOnPersonalBoardResult>(_userBoardManager.CheckIfSharedOnPersonalBoard(_mapper.Map<CheckIfSharedOnPersonalBoardInput>(checkIfSharedOnPersonalBoardRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "UB-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(checkIfSharedOnPersonalBoardRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="countLikesOrCommentRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("userboard/countlikeorcomment")]
        public CountLikesOrCommentResult CountLikesOrComment(CountLikesOrCommentRequest countLikesOrCommentRequest)
        {
            try
            {
                return _mapper.Map<CountLikesOrCommentResult>(_userBoardManager.CountLikesOrComment(_mapper.Map<CountLikesOrCommentInput>(countLikesOrCommentRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "UB-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(countLikesOrCommentRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="countNumberOfActionsByUserAndTypeRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("userboard/actionnumbers")]
        public CountNumberOfActionsByUserAndTypeResult CountNumberOfActionsByUserAndType(CountNumberOfActionsByUserAndTypeRequest countNumberOfActionsByUserAndTypeRequest)
        {
            try
            {
                return
                    _mapper.Map<CountNumberOfActionsByUserAndTypeResult>(
                        _userBoardManager.CountNumberOfActionsByUserAndType(_mapper.Map<CountNumberOfActionsByUserAndTypeInput>(countNumberOfActionsByUserAndTypeRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "UB-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(countNumberOfActionsByUserAndTypeRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="deleteLikeRequest"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("userboard/deletelike")]
        public DeleteLikeResult DeleteLike(DeleteLikeRequest deleteLikeRequest)
        {
            try
            {
                return _mapper.Map<DeleteLikeResult>(_userBoardManager.DeleteLike(_mapper.Map<DeleteLikeInput>(deleteLikeRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "UB-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(deleteLikeRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="actionOwnerByFatherRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("userboard/actionowner")]
        public ActionOwnerByFatherResult ActionOwnerByFather(ActionOwnerByFatherRequest actionOwnerByFatherRequest)
        {
            try
            {
                return _mapper.Map<ActionOwnerByFatherResult>(_userBoardManager.ActionOwnerByFather(_mapper.Map<ActionOwnerByFatherInput>(actionOwnerByFatherRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "UB-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(actionOwnerByFatherRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="idUserFromIdUserActionRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("userboarduseraction/userid")]
        public IdUserFromIdUserActionResult IdUserFromIdUserAction(IdUserFromIdUserActionRequest idUserFromIdUserActionRequest)
        {
            try
            {
                return _mapper.Map<IdUserFromIdUserActionResult>(_userBoardManager.IdUserFromIdUserAction(_mapper.Map<IdUserFromIdUserActionInput>(idUserFromIdUserActionRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "UB-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(idUserFromIdUserActionRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="idUserByActionTypeAndActionFatherRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("userboard/actiontypebyuserid")]
        public IEnumerable<IdUserByActionTypeAndActionFatherResult> IdUserByActionTypeAndActionFather(IdUserByActionTypeAndActionFatherRequest idUserByActionTypeAndActionFatherRequest)
        {
            try
            {
                return
                    _mapper.Map<IEnumerable<IdUserByActionTypeAndActionFatherResult>>(
                        _userBoardManager.IdUserByActionTypeAndActionFather(_mapper.Map<IdUserByActionTypeAndActionFatherInput>(idUserByActionTypeAndActionFatherRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "UB-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(idUserByActionTypeAndActionFatherRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="objectYouLikeRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("userboard/objectyoulike")]
        public ObjectYouLikeResult ObjectYouLike(ObjectYouLikeRequest objectYouLikeRequest)
        {
            try
            {
                return _mapper.Map<ObjectYouLikeResult>(_userBoardManager.ObjectYouLike(_mapper.Map<ObjectYouLikeInput>(objectYouLikeRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "UB-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(objectYouLikeRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="userActionInfoByIdRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("userboard/useractioninfo")]
        public UserActionInfoByIdResult UserActionInfoById(UserActionInfoByIdRequest userActionInfoByIdRequest)
        {
            try
            {
                return _mapper.Map<UserActionInfoByIdResult>(_userBoardManager.UserActionInfoById(_mapper.Map<UserActionInfoByIdInput>(userActionInfoByIdRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "UB-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(userActionInfoByIdRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="deleteUserActionRequest"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("userboard/deleteuseraction")]
        public DeleteUserActionResult DeleteUserAction(DeleteUserActionRequest deleteUserActionRequest)
        {
            try
            {
                return _mapper.Map<DeleteUserActionResult>(_userBoardManager.DeleteUserAction(_mapper.Map<DeleteUserActionInput>(deleteUserActionRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "UB-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(deleteUserActionRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="templateByTypeAndLanguageRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("userboard/gettemplate")]
        public TemplateByTypeAndLanguageResult TemplateByTypeAndLanguage(TemplateByTypeAndLanguageRequest templateByTypeAndLanguageRequest)
        {
            try
            {
                return
                    _mapper.Map<TemplateByTypeAndLanguageResult>(
                        _userBoardManager.TemplateByTypeAndLanguage(_mapper.Map<TemplateByTypeAndLanguageInput>(templateByTypeAndLanguageRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "UB-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(templateByTypeAndLanguageRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }
    }
}