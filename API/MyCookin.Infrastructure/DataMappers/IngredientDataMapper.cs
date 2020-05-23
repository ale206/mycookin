using System;
using System.ComponentModel.DataAnnotations.Schema;
using MyCookin.Domain.Entities;

namespace MyCookin.Infrastructure.DataMappers
{
    public class IngredientDataMapper
    {
        [Column("id")]
        public long Id { get; set; }
        
        [Column("unitary_average_weight")]
        public double UnitaryAverageWeight { get; set; }
        
        [Column("kcal_100_gr")]
        public double Kcal100Gr { get; set; }
        
        [Column("gr_proteins")]
        public double GrProteins { get; set; }
        
        [Column("gr_fats")]
        public double GrFats { get; set; }

        [Column("gr_carbohydrates")]
        public double GrCarbohydrates { get; set; }
        
        [Column("gr_alcohol")]
        public double GrAlcohol { get; set; }
        
        [Column("is_for_baby")]
        public bool IsForBaby { get; set; }
        
        [Column("is_meat")]
        public bool IsMeat { get; set; }
        
        [Column("is_fish")]
        public bool IsFish { get; set; }
        
        [Column("is_vegetarian")]
        public bool IsVegetarian { get; set; }
        
        [Column("is_vegan")]
        public bool IsVegan { get; set; }
        
        [Column("is_gluten_free")]
        public bool IsGlutenFree { get; set; }
        
        [Column("is_spicy")]
        public bool IsSpicy { get; set; }
        
        [Column("has_been_verified")]
        public bool HasBeenVerified { get; set; }
        
        [Column("created_on")]
        public DateTime CreatedOn { get; set; }
        
        [Column("modified_on")]
        public DateTime? ModifiedOn { get; set; }
        
        [Column("is_enabled")]
        public bool IsEnabled { get; set; }
        
        [Column("legacy_id")]
        public string LegacyId { get; set; }
        
        internal Ingredient CovertToEntity()
        {
            return new Ingredient()
            {
                Id = Id,
                UnitaryAverageWeight = UnitaryAverageWeight,
                Kcal100Gr = Kcal100Gr,
                GrProteins = GrProteins,
                GrFats = GrFats,
                GrCarbohydrates = GrCarbohydrates,
                GrAlcohol = GrAlcohol,
                IsForBaby = IsForBaby,
                IsMeat = IsMeat,
                IsFish = IsFish,
                IsVegetarian = IsVegetarian,
                IsVegan = IsVegan,
                IsGlutenFree = IsGlutenFree,
                IsSpicy = IsSpicy,
                HasBeenVerified = HasBeenVerified,
                CreatedOn = CreatedOn,
                ModifiedOn = ModifiedOn
            };
        }
    }
}