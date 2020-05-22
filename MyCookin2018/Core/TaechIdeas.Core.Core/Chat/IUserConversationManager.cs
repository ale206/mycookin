using System;
using System.Collections.Generic;
using TaechIdeas.Core.Core.Chat.Dto;

namespace TaechIdeas.Core.Core.Chat
{
    public interface IUserConversationManager
    {
        IEnumerable<NewConversationOutput> NewConversation(NewConversationInput nwNewConversationInput);
        GetConversationIdBetweenTwoUsersOutput GetConversationIdBetweenTwoUsers(GetConversationIdBetweenTwoUsersInput getConversationIdBetweenTwoUsersInput);
        SetUserConversationAsArchivedOutput SetUserConversationAsArchived(SetUserConversationAsArchivedInput setUserConversationAsArchivedInput);
        SetUserConversationAsActiveOutput SetUserConversationAsActive(SetUserConversationAsActiveInput setUserConversationAsActiveInput);
        IEnumerable<UsersConversationsOutput> UsersConversations(UsersConversationsInput usersConversationsInput);
        IEnumerable<MyConversationsOutput> MyConversations(MyConversationsInput myConversationsInput);
        IEnumerable<IsUserPartOfAConversationOutput> IsUserPartOfAConversation(IsUserPartOfAConversationInput isUserPartOfAConversationInput);
        NumberOfMessagesToReadOutput NumberOfMessagesToRead(NumberOfMessagesToReadInput numberOfMessagesToReadInput, Guid? idUserConversation);
    }
}