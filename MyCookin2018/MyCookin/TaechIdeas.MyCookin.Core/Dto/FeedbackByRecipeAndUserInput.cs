﻿using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class FeedbackByRecipeAndUserInput
    {
        public Guid RecipeId { get; set; }
        public Guid UserId { get; set; }
    }
}