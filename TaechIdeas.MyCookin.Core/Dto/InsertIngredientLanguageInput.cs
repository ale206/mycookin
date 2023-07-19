﻿using System;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class InsertIngredientLanguageInput : TokenRequiredInput
    {
        public Guid IngredientId { get; set; }

        public int LanguageId { get; set; }

        public string IngredientSingular { get; set; }

        public string IngredientPlural { get; set; }

        public string IngredientDescription { get; set; }

        public bool IsAutoTranslate { get; set; }

        public int? GeoRegionId { get; set; }

        public string FriendlyId { get; set; }
    }
}