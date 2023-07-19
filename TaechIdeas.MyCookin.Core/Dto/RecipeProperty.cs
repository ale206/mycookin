﻿namespace TaechIdeas.MyCookin.Core.Dto
{
    public class RecipeProperty
    {
        public int IdRecipeProperty { get; set; }
        public RecipePropertyType RecipePropertyType { get; set; }
        public int OrderPosition { get; set; }
        public bool Enabled { get; set; }
        public int IdLanguage { get; set; }
        public string Property { get; set; }
        public string RecipePropertyToolTip { get; set; }
    }
}