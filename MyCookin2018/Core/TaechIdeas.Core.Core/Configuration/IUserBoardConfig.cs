namespace TaechIdeas.Core.Core.Configuration
{
    public interface IUserBoardConfig
    {
        int? NotificationsRead { get; }
        int MaxNotificationsNumber { get; }
        int UserLikesResultsNumber { get; }
        string OtherIdActionsToShowOnUserBoard { get; }
    }
}