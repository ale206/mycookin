using System;

namespace TaechIdeas.Core.Core.UserBoard.Dto
{
    public class ActionOwnerByFatherOut
    {
        public Guid IDUserAction { get; set; }

        public Guid IDUser { get; set; }

        public Guid? IDUserActionFather { get; set; }

        public int IDUserActionType { get; set; }

        public Guid? IDActionRelatedObject { get; set; }

        public string UserActionMessage { get; set; }

        public int? IDVisibility { get; set; }

        public DateTime UserActionDate { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}