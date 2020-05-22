using System;
using System.Collections.Generic;
using AutoMapper;
using TaechIdeas.Core.Core.Audit;
using TaechIdeas.Core.Core.Audit.Dto;
using TaechIdeas.Core.Core.Common.Enums;
using TaechIdeas.Core.Core.Token;

namespace TaechIdeas.Core.BusinessLogic.Audit
{
    public class AuditManager : IAuditManager
    {
        private readonly IAuditRepository _auditRepository;
        private readonly ITokenManager _tokenManager;
        private readonly IMapper _mapper;

        public AuditManager(IAuditRepository auditRepository, ITokenManager tokenManager, IMapper mapper)
        {
            _auditRepository = auditRepository;
            _tokenManager = tokenManager;
            _mapper = mapper;
        }

        #region AutoAuditConfigInfo

        public AutoAuditConfigInfoOutput AutoAuditConfigInfo(AutoAuditConfigInfoInput autoAuditConfigInfoInput)
        {
            return _mapper.Map<AutoAuditConfigInfoOutput>(_auditRepository.AutoAuditConfigInfo(_mapper.Map<AutoAuditConfigInfoIn>(autoAuditConfigInfoInput)));
        }

        #endregion

        #region NewAuditEvent

        /// <summary>
        ///     Insert Event to DB Audit
        /// </summary>
        /// <returns></returns>
        public NewAuditEventOutput NewAuditEvent(NewAuditEventInput newAuditEventInput)
        {
            return _mapper.Map<NewAuditEventOutput>(_auditRepository.NewAuditEvent(_mapper.Map<NewAuditEventIn>(newAuditEventInput)));
        }

        #endregion

        #region UpdateAuditEvent

        public UpdateAuditEventOutput UpdateAuditEvent(UpdateAuditEventInput updateAuditEventInput)
        {
            return _mapper.Map<UpdateAuditEventOutput>(_auditRepository.UpdateAuditEvent(_mapper.Map<UpdateAuditEventIn>(updateAuditEventInput)));
        }

        #endregion

        #region CheckUserSpamReported

        public CheckUserSpamReportedOutput CheckUserSpamReported(CheckUserSpamReportedInput checkUserSpamReportedInput)
        {
            return _mapper.Map<CheckUserSpamReportedOutput>(_auditRepository.CheckUserSpamReported(_mapper.Map<CheckUserSpamReportedIn>(checkUserSpamReportedInput)));
        }

        #endregion

        #region DeleteByObjectId

        public DeleteByObjectIdOutput DeleteByObjectId(DeleteByObjectIdInput deleteByObjectIdInput)
        {
            return _mapper.Map<DeleteByObjectIdOutput>(_auditRepository.DeleteByObjectId(_mapper.Map<DeleteByObjectIdIn>(deleteByObjectIdInput)));
        }

        #endregion

        #region GetAuditEventById

        public GetAuditEventByIdOutput GetAuditEventById(GetAuditEventByIdInput getAuditEventByIdInput)
        {
            return _mapper.Map<GetAuditEventByIdOutput>(_auditRepository.GetAuditEventById(_mapper.Map<GetAuditEventByIdIn>(getAuditEventByIdInput)));
        }

        #endregion

        #region GetAuditEventToCheck

        public IEnumerable<GetAuditEventToCheckOutput> GetAuditEventToCheck(GetAuditEventToCheckInput getAuditEventToCheckInput)
        {
            return _mapper.Map<IEnumerable<GetAuditEventToCheckOutput>>(_auditRepository.GetAuditEventToCheck(_mapper.Map<GetAuditEventToCheckIn>(getAuditEventToCheckInput)));
        }

        #endregion

        #region GetNumberOfEventToCheck

        public GetNumberOfEventToCheckOutput GetNumberOfEventToCheck(GetNumberOfEventToCheckInput getNumberOfEventToCheckInput)
        {
            return _mapper.Map<GetNumberOfEventToCheckOutput>(_auditRepository.GetNumberOfEventToCheck(_mapper.Map<GetNumberOfEventToCheckIn>(getNumberOfEventToCheckInput)));
        }

        #endregion

        #region GetObjectIdNumberOfEveniences

        public IEnumerable<GetObjectIdNumberOfEveniencesOutput> GetObjectIdNumberOfEveniences(GetObjectIdNumberOfEveniencesInput getObjectIdNumberOfEveniencesInput)
        {
            return
                _mapper.Map<IEnumerable<GetObjectIdNumberOfEveniencesOutput>>(
                    _auditRepository.GetObjectIdNumberOfEveniences(_mapper.Map<GetObjectIdNumberOfEveniencesIn>(getObjectIdNumberOfEveniencesInput)));
        }

        #endregion

        #region GetPicturesToCheck

        public IEnumerable<GetPicturesToCheckOutput> GetPicturesToCheck(GetPicturesToCheckInput getPicturesToCheckInput)
        {
            //Check for Valid Token
            var checkTokenOutput = _tokenManager.CheckToken(getPicturesToCheckInput.CheckTokenInput);

            if (!checkTokenOutput.IsTokenValid)
            {
                throw new Exception("Token not valid for the user.");
            }

            var getAuditEventToCheckInput = new GetAuditEventToCheckInput
            {
                NumberOfResults = getPicturesToCheckInput.NumberOfResults,
                ObjectType = ObjectType.Photo
            };

            var getAuditEventToCheckOutput = GetAuditEventToCheck(getAuditEventToCheckInput);

            var getPicturesToCheckOutput = _mapper.Map<IEnumerable<GetPicturesToCheckOutput>>(getAuditEventToCheckOutput);

            return getPicturesToCheckOutput;
        }

        #endregion
    }
}