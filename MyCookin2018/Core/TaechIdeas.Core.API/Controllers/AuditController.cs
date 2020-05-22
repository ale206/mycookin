using System;
using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaechIdeas.Core.Core.Audit;
using TaechIdeas.Core.Core.Audit.Dto;
using TaechIdeas.Core.Core.Common;
using TaechIdeas.Core.Core.Common.Enums;
using TaechIdeas.Core.Core.LogAndMessage;
using TaechIdeas.Core.Core.LogAndMessage.Dto;

namespace TaechIdeas.Core.API.Controllers
{
    /// <summary>
    ///     Definition of API for Audit
    /// </summary>
    [Route("core")]
    public class AuditController : ControllerBase
    {
        private readonly IAuditManager _auditManager;
        private readonly IUtilsManager _utilsManager;
        private readonly ILogManager _logManager;
        private readonly IMapper _mapper;

        /// <summary>
        ///     API for Audit
        /// </summary>
        public AuditController(IAuditManager auditManager, IUtilsManager utilsManager, ILogManager logManager, IMapper mapper)
        {
            _auditManager = auditManager;
            _utilsManager = utilsManager;
            _logManager = logManager;
            _mapper = mapper;
        }

        /// <summary>
        ///     Get Pictures to check
        /// </summary>
        /// <param name="getPicturesToCheckRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("audit/picturestocheck")]
        public IEnumerable<GetPicturesToCheckResult> GetPicturesToCheck(GetPicturesToCheckRequest getPicturesToCheckRequest)
        {
            try
            {
                return _mapper.Map<IEnumerable<GetPicturesToCheckResult>>(_auditManager.GetPicturesToCheck(_mapper.Map<GetPicturesToCheckInput>(getPicturesToCheckRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "AU-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(getPicturesToCheckRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }
    }
}