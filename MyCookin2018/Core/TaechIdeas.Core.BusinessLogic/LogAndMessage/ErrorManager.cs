using System.Collections.Generic;
using AutoMapper;
using TaechIdeas.Core.Core.LogAndMessage;
using TaechIdeas.Core.Core.LogAndMessage.Dto;

namespace TaechIdeas.Core.BusinessLogic.LogAndMessage
{
    public class ErrorManager : IErrorManager
    {
        private readonly IErrorAndMessageRepository _errorAndMessageRepository;
        private readonly IMapper _mapper;

        public ErrorManager(IErrorAndMessageRepository errorAndMessageRepository, IMapper mapper)
        {
            _errorAndMessageRepository = errorAndMessageRepository;
            _mapper = mapper;
        }

        public GetErrorOrMessageOutput GetErrorOrMessage(GetErrorOrMessageInput getErrorOrMessageInput)
        {
            return _mapper.Map<GetErrorOrMessageOutput>(_errorAndMessageRepository.GetErrorOrMessage(_mapper.Map<GetErrorOrMessageIn>(getErrorOrMessageInput)));
        }

        public InsertErrorLogOutput InsertErrorLog(InsertErrorLogInput insertErrorLogInput)
        {
            return _mapper.Map<InsertErrorLogOutput>(_errorAndMessageRepository.InsertErrorLog(_mapper.Map<InsertErrorLogIn>(insertErrorLogInput)));
        }

        public GetLastErrorLogDateOutput GetLastErrorLogDate(GetLastErrorLogDateInput getLastErrorLogDateInput)
        {
            return _mapper.Map<GetLastErrorLogDateOutput>(_errorAndMessageRepository.GetLastErrorLogDate(_mapper.Map<GetLastErrorLogDateIn>(getLastErrorLogDateInput)));
        }

        public DeleteErrorByErrorMessageOutput DeleteErrorByErrorMessage(DeleteErrorByErrorMessageInput deleteErrorByErrorMessageInput)
        {
            return _mapper.Map<DeleteErrorByErrorMessageOutput>(_errorAndMessageRepository.DeleteErrorByErrorMessage(_mapper.Map<DeleteErrorByErrorMessageIn>(deleteErrorByErrorMessageInput)));
        }

        public IEnumerable<GetErrorsListOutput> GetErrorsList(GetErrorsListInput getErrorsListInput)
        {
            return _mapper.Map<IEnumerable<GetErrorsListOutput>>(_errorAndMessageRepository.GetErrorsList(_mapper.Map<GetErrorsListIn>(getErrorsListInput)));
        }
    }
}