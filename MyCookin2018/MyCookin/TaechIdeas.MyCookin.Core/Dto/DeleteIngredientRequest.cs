using System;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class DeleteIngredientRequest
    {
        public CheckTokenRequest CheckTokenRequest { get; set; }
        public Guid IngredientId { get; set; }
    }
}