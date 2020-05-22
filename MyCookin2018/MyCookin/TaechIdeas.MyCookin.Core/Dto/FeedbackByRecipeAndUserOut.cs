using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class FeedbackByRecipeAndUserOut
    {
        public Guid IdRecipeFeedback { get; set; }
        public Guid Recipe { get; set; }
        public Guid User { get; set; }
        public int FeedbackType { get; set; }
        public string FeedbackText { get; set; }
        public DateTime FeedbackDate { get; set; }
    }
}