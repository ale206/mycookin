﻿using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class LikesForRecipeInput
    {
        public Guid RecipeId { get; set; }
        public int rowOffset { get; set; }
        public int fetchRows { get; set; }
    }
}