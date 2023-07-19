﻿using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class RecipePropertyByRecipeIdOutput
    {
        public bool Value { get; set; }
        public Guid IdRecipePropertyValue { get; set; }
        public Guid IdRecipe { get; set; }
        public int IdRecipeProperty { get; set; }
        public int IdRecipePropertyType { get; set; }
        public bool IsDishType { get; set; }
        public bool IsCookingType { get; set; }
        public bool IsColorType { get; set; }
        public bool IsEatType { get; set; }
        public bool IsUseType { get; set; }
        public bool IsPeriodType { get; set; }
    }
}