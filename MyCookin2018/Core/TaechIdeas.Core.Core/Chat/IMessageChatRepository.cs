using System.Collections.Generic;
using TaechIdeas.Core.Core.Chat.Dto;

namespace TaechIdeas.Core.Core.Chat
{
    public interface IMessageChatRepository
    {
        //IUserConversationManager
        IEnumerable<NewConversationOut> NewConversation(NewConversationIn nwNewConversationIn);
        GetConversationIdBetweenTwoUsersOut GetConversationIdBetweenTwoUsers(GetConversationIdBetweenTwoUsersIn getConversationIdBetweenTwoUsersIn);
        SetUserConversationAsArchivedOut SetUserConversationAsArchived(SetUserConversationAsArchivedIn setUserConversationAsArchivedIn);
        SetUserConversationAsActiveOut SetUserConversationAsActive(SetUserConversationAsActiveIn setUserConversationAsActiveIn);
        IEnumerable<UsersConversationsOut> UsersConversations(UsersConversationsIn usersConversationsIn);
        IEnumerable<MyConversationsOut> MyConversations(MyConversationsIn myConversationsIn);
        IEnumerable<IsUserPartOfAConversationOut> IsUserPartOfAConversation(IsUserPartOfAConversationIn isUserPartOfAConversationIn);
        NumberOfMessagesToReadOut NumberOfMessagesToRead(NumberOfMessagesToReadIn numberOfMessagesToReadIn);

        //IMessageTypeManager
        IEnumerable<TypeOfMessageInfoByIdOut> TypeOfMessageInfoById(TypeOfMessageInfoByIdIn typeOfMessageInfoByIdIn);

        //IMessageRecipientManager
        IEnumerable<NewMessageRecipientOut> NewMessageRecipient(NewMessageRecipientIn newMessageRecipientIn);
        SetMessageAsViewedOut SetMessageAsViewed(SetMessageAsViewedIn setMessageAsViewedIn);
        SetAllConversationMessagesAsViewedOut SetAllConversationMessagesAsViewed(SetAllConversationMessagesAsViewedIn setAllConversationMessagesAsViewedInpu);
        SetMessageAsDeletedOut SetMessageAsDeleted(SetMessageAsDeletedIn setMessageAsDeletedIn);

        //IMessageManager
        InsertNewMessageOut InsertNewMessage(InsertNewMessageIn insertNewMessageIn);
        IEnumerable<MessageByIdOut> MessageById(MessageByIdIn messageInfoByIdIn);
    }
}