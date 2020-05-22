using System;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class DeleteRecipeBeverageRequest
    {
        public CheckTokenRequest CheckTokenRequest { get; set; }
        public Guid BeverageRecipeId { get; set; }
    }
}