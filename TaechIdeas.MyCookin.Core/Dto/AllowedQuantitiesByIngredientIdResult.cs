using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class AllowedQuantitiesByIngredientIdResult
    {
        public Guid IngredientAllowedQuantityTypeId { get; set; }

        public Guid? IngredientId { get; set; }

        public int? IngredientQuantityTypeId { get; set; }

        //FROM quantity types

        public bool IsWeight { get; set; }

        public bool IsLiquid { get; set; }

        public bool IsPiece { get; set; }

        public bool IsStandardQuantityType { get; set; }

        public double? NoStdAvgWeight { get; set; }

        public bool EnabledToUser { get; set; }

        public bool ShowInIngredientList { get; set; }

        //FROM Quantity Types Language
        public int IngredientQuantityTypeLanguageId { get; set; }

        public int LanguageId { get; set; }

        public string IngredientQuantityTypeSingular { get; set; }

        public string IngredientQuantityTypePlural { get; set; }

        public float ConvertionRatio { get; set; }

        public string IngredientQuantityTypeX1000Singular { get; set; }

        public string IngredientQuantityTypeX1000Plural { get; set; }

        public string IngredientQuantityTypeWordsShowBefore { get; set; }

        public string IngredientQuantityTypeWordsShowAfter { get; set; }
    }
}