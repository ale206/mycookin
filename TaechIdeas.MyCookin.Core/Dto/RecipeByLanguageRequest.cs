﻿using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class RecipeByLanguageRequest
    {
        [ApiMember(Name = "RecipeId", DataType = "guid", IsRequired = true)]
        [Required(ErrorMessage = "RecipeId Required")]
        public Guid RecipeId { get; set; }

        [ApiMember(Name = "LanguageId", DataType = "int", IsRequired = true)]
        [Required(ErrorMessage = "LanguageId Required")]
        [Range(1, 3, ErrorMessage = "LanguageId must be between 1 and 3")]
        public int LanguageId { get; set; }
    }
}