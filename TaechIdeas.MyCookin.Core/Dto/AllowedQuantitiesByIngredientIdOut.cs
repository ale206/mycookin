using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class AllowedQuantitiesByIngredientIdOut
    {
        public Guid IDIngredientAllowedQuantityType { get; set; }

        public Guid? IDingredient { get; set; }

        public int? IDIngredientQuantityType { get; set; }

        //FROM quantity types

        public bool isWeight { get; set; }

        public bool isLiquid { get; set; }

        public bool isPiece { get; set; }

        public bool isStandardQuantityType { get; set; }

        public double? NoStdAvgWeight { get; set; }

        public bool EnabledToUser { get; set; }

        public bool ShowInIngredientList { get; set; }

        //FROM Quantity Types Language
        public int IDIngredientQuantityTypeLanguage { get; set; }

        public int IDLanguage { get; set; }

        public string IngredientQuantityTypeSingular { get; set; }

        public string IngredientQuantityTypePlural { get; set; }

        public float ConvertionRatio { get; set; }

        public string IngredientQuantityTypeX1000Singular { get; set; }

        public string IngredientQuantityTypeX1000Plural { get; set; }

        public string IngredientQuantityTypeWordsShowBefore { get; set; }

        public string IngredientQuantityTypeWordsShowAfter { get; set; }
    }
}