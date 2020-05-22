using System;
using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaechIdeas.Core.Core.Common;
using TaechIdeas.Core.Core.Common.Enums;
using TaechIdeas.Core.Core.LogAndMessage;
using TaechIdeas.Core.Core.LogAndMessage.Dto;
using TaechIdeas.Core.Core.Media;
using TaechIdeas.Core.Core.Media.Dto;

namespace TaechIdeas.Core.API.Controllers
{
    [Route("core")]
    public class MediaController : ControllerBase
    {
        private readonly IMediaManager _mediaManager;
        private readonly IUtilsManager _utilsManager;
        private readonly ILogManager _logManager;
        private readonly IMapper _mapper;

        public MediaController(IMediaManager mediaManager, IUtilsManager utilsManager, ILogManager logManager, IMapper mapper)
        {
            _mediaManager = mediaManager;
            _utilsManager = utilsManager;
            _logManager = logManager;
            _mapper = mapper;
        }

        /// <summary>
        ///     Search Recipes
        /// </summary>
        /// <param name="newMediaRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("media/new")]
        public NewMediaResult NewMedia(NewMediaRequest newMediaRequest)
        {
            try
            {
                return _mapper.Map<NewMediaResult>(_mediaManager.NewMedia(_mapper.Map<NewMediaInput>(newMediaRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "MD-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(newMediaRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        ///     Disable Media
        /// </summary>
        /// <param name="disableMediaRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("media/disable")]
        public DisableMediaResult DisableMedia(DisableMediaRequest disableMediaRequest)
        {
            try
            {
                return _mapper.Map<DisableMediaResult>(_mediaManager.DisableMedia(_mapper.Map<DisableMediaInput>(disableMediaRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "MD-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(disableMediaRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }

        /// <summary>
        ///     Get medias not in cdn
        /// </summary>
        /// <param name="getMediaNotInCdnRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("media/notincdn")]
        public IEnumerable<GetMediaNotInCdnResult> GetMediaNotInCdn(GetMediaNotInCdnRequest getMediaNotInCdnRequest)
        {
            try
            {
                return _mapper.Map<IEnumerable<GetMediaNotInCdnResult>>(_mediaManager.GetMediaNotInCdn(_mapper.Map<GetMediaNotInCdnInput>(getMediaNotInCdnRequest)));
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "MD-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName} | {_utilsManager.GetAllPropertiesAndValues(getMediaNotInCdnRequest)}"
                };

                _logManager.WriteLog(logRowIn);

                throw;
            }
        }
    }
}