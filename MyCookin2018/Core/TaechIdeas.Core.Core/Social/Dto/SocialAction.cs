using System;
using TaechIdeas.Core.Core.Common.Enums;

namespace TaechIdeas.Core.Core.Social.Dto
{
    public class SocialAction
    {
        public string IdUserSocial { get; set; }
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }
        public string Message { get; set; }
        public string LinkUrl { get; set; }
        public string PictureUrl { get; set; }
        public string SourcePathUrl { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string Actions { get; set; }
        public string Privacy { get; set; }
        public string IdPostOnWall { get; set; }
        public string ImagePath { get; set; }
        public Guid? IdActionRelatedObject { get; set; }
        public ActionTypes IdUserActionType { get; set; }
        public Guid? IdUserAction { get; set; }
    }
}