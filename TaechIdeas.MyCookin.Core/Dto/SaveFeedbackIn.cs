using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class SaveFeedbackIn
    {
        public Guid? idRecipeFeedback { get; set; }
        public Guid? recipeId { get; set; }
        public Guid? userId { get; set; }
        public int feedbackType { get; set; }
        public string feedbackText { get; set; }
    }
}