namespace TaechIdeas.Core.Core.Configuration
{
    public interface IUserConfig
    {
        string CookieName { get; }
        int? UserFindResultsNumber { get; }
        string UserProfilePropertiesId { get; }

        string EmailFromProfileUser { get; }
        int? NumberOfPeopleYouMayKnow { get; }
        int DaysOfLastRetrieveSocialFriends { get; }
        int MinPasswordLength { get; }
        int YearsAfterAccountExpire { get; }
    }
}