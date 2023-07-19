﻿using TaechIdeas.Core.Core.Common.Dto;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class IngredientLanguageListRequest : PaginationFieldsInput
    {
        public int LanguageId { get; set; }
        public CheckTokenRequest CheckTokenRequest { get; set; }
    }
}