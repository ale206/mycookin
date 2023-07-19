using System;
using TaechIdeas.Core.Core.Common.Enums;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class SaveFeedbackInput
    {
        public Guid? idRecipeFeedback { get; set; }
        public Guid? recipeId { get; set; }
        public Guid? userId { get; set; }
        public RecipeFeedbackType feedbackType { get; set; }
        public string feedbackText { get; set; }
    }
}