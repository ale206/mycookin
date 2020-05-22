using System;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaechIdeas.Core.Core.Common;
using TaechIdeas.Core.Core.Common.Enums;
using TaechIdeas.Core.Core.LogAndMessage;
using TaechIdeas.Core.Core.LogAndMessage.Dto;
using TaechIdeas.Core.Core.User.Dto;
using TaechIdeas.Core.Core.Verification;
using TaechIdeas.Core.Core.Verification.Dto;

namespace TaechIdeas.Core.API.Controllers
{
    /// <summary>
    ///     Definition of API for Verifications
    /// </summary>
    [Route("core")]
    public class VerificationController : ControllerBase
    {
        private readonly IVerificationManager _verificationManager;
        private readonly IUtilsManager _utilsManager;
        private readonly ILogManager _logManager;
        private readonly IMapper _mapper;

        public VerificationController(IVerificationManager verificationManager, IUtilsManager utilsManager, ILogManager logManager, IMapper mapper)
        {
            _verificationManager = verificationManager;
            _utilsManager = utilsManager;
            _logManager = logManager;
            _mapper = mapper;
        }

        /// <summary>
        ///     Check if username already exists in database
        /// </summary>
        /// <param name="usernameAlreadyExistsRequest"></param>
        /// <returns>Return True or False</returns>
        [HttpPost]
        [Route("verification/usernamealreadyexist")]
        public UsernameAlreadyExistsResult UsernameAlreadyExist(UsernameAlreadyExistsRequest usernameAlreadyExistsRequest)
        {
            try
            {
                return _mapper.Map<UsernameAlreadyExistsResult>(_verificationManager.UsernameAlreadyExists(_mapper.Map<UsernameAlreadyExistsInput>(usernameAlreadyExistsRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "VE-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(usernameAlreadyExistsRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        ///     Check if the email already exists in database
        /// </summary>
        /// <param name="emailAlreadyExistsRequest"></param>
        /// <returns>Return True or False</returns>
        [HttpPost]
        [Route("verification/emailalreadyexist")]
        //public NewUserResult LoginUser([FromUri] LoginUserRequest myLogin)
        public EmailAlreadyExistsResult EmailAlreadyExist(EmailAlreadyExistsRequest emailAlreadyExistsRequest)
        {
            try
            {
                return _mapper.Map<EmailAlreadyExistsResult>(_verificationManager.EmailAlreadyExists(_mapper.Map<EmailAlreadyExistsInput>(emailAlreadyExistsRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "VE-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(emailAlreadyExistsRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }
    }
}