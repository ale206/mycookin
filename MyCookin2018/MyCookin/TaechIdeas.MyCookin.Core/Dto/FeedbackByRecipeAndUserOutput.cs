using System;
using TaechIdeas.Core.Core.Common.Enums;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class FeedbackByRecipeAndUserOutput
    {
        public Guid IdRecipeFeedback { get; set; }
        public Guid Recipe { get; set; }
        public Guid User { get; set; }
        public RecipeFeedbackType FeedbackType { get; set; }
        public string FeedbackText { get; set; }
        public DateTime FeedbackDate { get; set; }
    }
}