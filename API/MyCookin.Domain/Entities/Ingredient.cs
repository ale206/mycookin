using System;

namespace MyCookin.Domain.Entities
{
    public class Ingredient
    {
        public long Id { get; set; }
        public double UnitaryAverageWeight { get; set; }
        public double Kcal100Gr { get; set; }
        public double GrProteins { get; set; }
        public double GrFats { get; set; }
        public double GrCarbohydrates { get; set; }
        public double GrAlcohol { get; set; }
        public bool IsForBaby { get; set; }
        public bool IsMeat { get; set; }
        public bool IsFish { get; set; }
        public bool IsVegetarian { get; set; }
        public bool IsVegan { get; set; }
        public bool IsGlutenFree { get; set; }
        public bool IsSpicy { get; set; }
        public bool HasBeenVerified { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}