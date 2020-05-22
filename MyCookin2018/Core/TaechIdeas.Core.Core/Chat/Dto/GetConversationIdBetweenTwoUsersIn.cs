using System;

namespace TaechIdeas.Core.Core.Chat.Dto
{
    public class GetConversationIdBetweenTwoUsersIn
    {
        public Guid? IDUserSender { get; set; }
        public Guid? IDUserRecipient { get; set; }
    }
}