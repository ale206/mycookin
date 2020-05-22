using System;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaechIdeas.Core.Core.Common;
using TaechIdeas.Core.Core.Common.Enums;
using TaechIdeas.Core.Core.LogAndMessage;
using TaechIdeas.Core.Core.LogAndMessage.Dto;
using TaechIdeas.Core.Core.Token;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.API.Controllers
{
    /// <summary>
    ///     Definition of API for Token
    /// </summary>
    [Route("core")]
    public class TokenController : ControllerBase
    {
        private readonly ITokenManager _tokenManager;
        private readonly IUtilsManager _utilsManager;
        private readonly ILogManager _logManager;
        private readonly IMapper _mapper;

        public TokenController(ITokenManager tokenManager, IUtilsManager utilsManager, ILogManager logManager, IMapper mapper)
        {
            _tokenManager = tokenManager;
            _utilsManager = utilsManager;
            _logManager = logManager;
            _mapper = mapper;
        }

        /// <summary>
        ///     Check if Token is still valid for the user
        /// </summary>
        /// <param name="checkTokenRequest"></param>
        /// <returns>True if valid, False if not</returns>
        [HttpPost]
        [Route("token/checktoken")]
        public CheckTokenResult CheckToken(CheckTokenRequest checkTokenRequest)
        {
            try
            {
                return _mapper.Map<CheckTokenResult>(_tokenManager.CheckToken(_mapper.Map<CheckTokenInput>(checkTokenRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "TK-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(checkTokenRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }
    }
}