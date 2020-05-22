using System;
using TaechIdeas.Core.Core.Common.Enums;
using TaechIdeas.Core.Core.Social.Dto;

namespace TaechIdeas.Core.Core.Social
{
    public interface ISocialActionManager
    {
        //Dto.SocialAction TemplateForSharing(ActionTypes idUserActionType, Guid idActionRelatedObject);
        string FB_PostOnWall(SocialAction socialAction);
        bool FB_Like(SocialAction socialAction);
        bool FB_PostPicture(SocialAction socialAction);
        bool InsertActionShared(string idShareOnSocial, SocialNetwork idSocialNetwork, Guid idUserAction);
        bool CheckIfShared(Guid idUserAction, Guid idUser, SocialNetwork idSocialNetwork);
        bool CheckIfSharedOnPersonalBoard(Guid idActionRelatedObject, Guid idUser, ActionTypes actionType);
        void TW_tweet(SocialAction socialAction);
    }
}