using System;
using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaechIdeas.Core.Core.Common;
using TaechIdeas.Core.Core.Common.Enums;
using TaechIdeas.Core.Core.LogAndMessage;
using TaechIdeas.Core.Core.LogAndMessage.Dto;

namespace TaechIdeas.Core.API.Controllers
{
    /// <summary>
    ///     Definition of API for Errors
    /// </summary>
    [Route("core")]
    public class ErrorsController : ControllerBase
    {
        private readonly IErrorManager _errorManager;
        private readonly IUtilsManager _utilsManager;
        private readonly ILogManager _logManager;
        private readonly IMapper _mapper;

        public ErrorsController(IErrorManager errorManager, IUtilsManager utilsManager, ILogManager logManager, IMapper mapper)
        {
            _errorManager = errorManager;
            _utilsManager = utilsManager;
            _logManager = logManager;
            _mapper = mapper;
        }

        /// <summary>
        ///     Get list of errors
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("errors/list")]
        public IEnumerable<GetErrorsListResult> GetErrorsList(GetErrorsListRequest getErrorsListRequest)
        {
            try
            {
                return _mapper.Map<IEnumerable<GetErrorsListResult>>(_errorManager.GetErrorsList(_mapper.Map<GetErrorsListInput>(getErrorsListRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "ER-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(getErrorsListRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        ///     Delete an error by its name
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("error/delete")]
        public DeleteErrorByErrorMessageResult DeleteErrorByErrorMessage(DeleteErrorByErrorMessageRequest deleteErrorByErrorMessageRequest)
        {
            try
            {
                return _mapper.Map<DeleteErrorByErrorMessageResult>(_errorManager.DeleteErrorByErrorMessage(_mapper.Map<DeleteErrorByErrorMessageInput>(deleteErrorByErrorMessageRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "ER-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(deleteErrorByErrorMessageRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }
    }
}