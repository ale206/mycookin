using System;
using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaechIdeas.Core.Core.Common;
using TaechIdeas.Core.Core.Common.Enums;
using TaechIdeas.Core.Core.Contact;
using TaechIdeas.Core.Core.Contact.Dto;
using TaechIdeas.Core.Core.LogAndMessage;
using TaechIdeas.Core.Core.LogAndMessage.Dto;

namespace TaechIdeas.Core.API.Controllers
{
    /// <summary>
    ///     Definition of API for Contact
    /// </summary>
    [Route("core")]
    public class ContactController : ControllerBase
    {
        private readonly IContactManager _contactManager;
        private readonly IUtilsManager _utilsManager;
        private readonly ILogManager _logManager;
        private readonly IMapper _mapper;

        public ContactController(IContactManager contactManager, IUtilsManager utilsManager, ILogManager logManager, IMapper mapper)
        {
            _contactManager = contactManager;
            _utilsManager = utilsManager;
            _logManager = logManager;
            _mapper = mapper;
        }

        /// <summary>
        ///     New Message
        /// </summary>
        /// <param name="newMessageRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("contact/newmessage")]
        public NewMessageResult NewMessage(NewMessageRequest newMessageRequest)
        {
            try
            {
                return _mapper.Map<NewMessageResult>(_contactManager.NewMessage(_mapper.Map<NewMessageInput>(newMessageRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "CO-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(newMessageRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        ///     Get Messages
        /// </summary>
        /// <param name="requestMessagesRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("contact/getmessages")]
        public IEnumerable<RequestMessagesResult> RequestMessages(RequestMessagesRequest requestMessagesRequest)
        {
            try
            {
                return _mapper.Map<IEnumerable<RequestMessagesResult>>(_contactManager.RequestMessages(_mapper.Map<RequestMessagesInput>(requestMessagesRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "CO-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(requestMessagesRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        ///     New Reply
        /// </summary>
        /// <param name="newReplyRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("contact/newreply")]
        public NewReplyResult NewReply(NewReplyRequest newReplyRequest)
        {
            try
            {
                return _mapper.Map<NewReplyResult>(_contactManager.NewReply(_mapper.Map<NewReplyInput>(newReplyRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "CO-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(newReplyRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }
    }
}