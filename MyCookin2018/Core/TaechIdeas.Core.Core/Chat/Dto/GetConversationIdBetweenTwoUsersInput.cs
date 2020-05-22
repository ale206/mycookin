using System;

namespace TaechIdeas.Core.Core.Chat.Dto
{
    public class GetConversationIdBetweenTwoUsersInput
    {
        public Guid? idUserSender { get; set; }
        public Guid? idUserRecipient { get; set; }
    }
}