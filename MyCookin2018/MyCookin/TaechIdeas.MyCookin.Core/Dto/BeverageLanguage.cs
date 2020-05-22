using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class BeverageLanguage : IngredientLanguage
    {
        private Guid IdBeverageLanguage { get; set; }
        private Beverage IdBeverage { get; set; }
        private string BeverageInfoLanguage { get; set; }
    }
}