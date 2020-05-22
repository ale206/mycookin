using System;
using TaechIdeas.Core.Core.User.Dto;

namespace TaechIdeas.Core.Core.Chat.Dto
{
    public class NewConversationOutput
    {
        public Guid IdUserConversation { get; set; }
        public Guid? IdConversation { get; set; }
        public Guid? IdUser { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ArchivedOn { get; set; }

        public MyUser[] UsersRecipient { get; set; }

        public Guid IdUserSender { get; set; }
        public Guid IdUserRecipient { get; set; }
        public DateTime? LastMessageViewedOn { get; set; } //Date of the last message viewed in the conversation
        public Guid IdUserConversationSender { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public bool UserIsOnLine { get; set; }
    }
}