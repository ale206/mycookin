using System;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class ObjectYouLikeOut
    {
        public Guid IDUserAction { get; set; }

        public Guid IDUserOwner { get; set; }

        public Guid? IDUserActionFather { get; set; }

        public int IDUserActionType { get; set; }

        public Guid? IDActionRelatedObject { get; set; }

        public string UserActionMessage { get; set; }

        public int? IDVisibility { get; set; }

        public DateTime UserActionDate { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}