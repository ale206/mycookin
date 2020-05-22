using AutoMapper;
using TaechIdeas.Core.Core.Contact.Dto;
using TaechIdeas.Core.Core.LogAndMessage.Dto;

namespace TaechIdeas.Core.IoC.AutoMapper
{
    public class MapForContact : Profile
    {
        public MapForContact()
        {
            //CONTACT
            /**************************************************************************/
            CreateMap<NewMessageRequest, NewMessageInput>();
            CreateMap<NewMessageInput, NewMessageIn>();
            CreateMap<NewMessageOut, NewMessageOutput>();
            CreateMap<NewMessageOutput, NewMessageResult>();

            CreateMap<RequestMessagesRequest, RequestMessagesInput>();
            CreateMap<RequestMessagesInput, RequestMessagesIn>();
            CreateMap<RequestMessagesOut, RequestMessagesOutput>();
            CreateMap<RequestMessagesOutput, RequestMessagesResult>();

            CreateMap<NewReplyRequest, NewReplyInput>();
            CreateMap<NewReplyInput, NewReplyIn>();
            CreateMap<NewReplyOut, NewReplyOutput>();
            CreateMap<NewReplyOutput, NewReplyResult>();
            /**************************************************************************/
        }
    }
}