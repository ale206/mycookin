using System;
using System.Collections.Generic;
using AutoMapper;
using TaechIdeas.Core.Core.Contact;
using TaechIdeas.Core.Core.Contact.Dto;
using TaechIdeas.Core.Core.LogAndMessage.Dto;
using TaechIdeas.Core.Core.Token;

namespace TaechIdeas.Core.BusinessLogic.Contact
{
    public class ContactManager : IContactManager
    {
        private readonly IContactRepository _contactRepository;
        private readonly ITokenManager _tokenManager;
        private readonly IMapper _mapper;

        public ContactManager(IContactRepository contactRepository, ITokenManager tokenManager, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _tokenManager = tokenManager;
            _mapper = mapper;
        }

        public NewMessageOutput NewMessage(NewMessageInput newMessageInput)
        {
            //Check for Valid Token
            var checkTokenOutput = _tokenManager.CheckToken(newMessageInput.CheckTokenInput);

            if (!checkTokenOutput.IsTokenValid)
            {
                throw new Exception("Token not valid for the user.");
            }

            return _mapper.Map<NewMessageOutput>(_contactRepository.NewMessage(_mapper.Map<NewMessageIn>(newMessageInput)));
        }

        public IEnumerable<RequestMessagesOutput> RequestMessages(RequestMessagesInput requestMessagesInput)
        {
            //Check for Valid Token
            var checkTokenOutput = _tokenManager.CheckToken(requestMessagesInput.CheckTokenInput);

            if (!checkTokenOutput.IsTokenValid)
            {
                throw new Exception("Token not valid for the user.");
            }

            return _mapper.Map<IEnumerable<RequestMessagesOutput>>(_contactRepository.RequestMessages(_mapper.Map<RequestMessagesIn>(requestMessagesInput)));
        }

        public NewReplyOutput NewReply(NewReplyInput newReplyInput)
        {
            //Check for Valid Token
            var checkTokenOutput = _tokenManager.CheckToken(newReplyInput.CheckTokenInput);

            if (!checkTokenOutput.IsTokenValid)
            {
                throw new Exception("Token not valid for the user.");
            }

            return _mapper.Map<NewReplyOutput>(_contactRepository.NewReply(_mapper.Map<NewReplyIn>(newReplyInput)));
        }
    }
}