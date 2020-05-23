using System;
using System.Collections.Generic;
using MyCookin.Domain.Entities;

namespace MyCookin.UnitTests.Helpers
{
    public static class IngredientsHelper
    {
        public static IEnumerable<Ingredient> GetIngredients()
        {
            return new List<Ingredient>
            {
                GetIngredient()
            };
        }

        public static Ingredient GetIngredient()
        {
            var rnd = new Random();


            return new Ingredient
            {
                Id = 1,
                CreatedOn = DateTime.UtcNow,
                GrAlcohol = rnd.Next(1, 65),
                GrCarbohydrates = rnd.Next(1, 65),
                GrFats = rnd.Next(1, 65),
                GrProteins = rnd.Next(1, 65),
                IsFish = false,
                IsMeat = false,
                IsSpicy = false,
                IsVegan = false,
                IsVegetarian = false,
                Kcal100Gr = rnd.Next(1, 250),
                ModifiedOn = null,
                HasBeenVerified = false,
                IsForBaby = false,
                IsGlutenFree = false,
                UnitaryAverageWeight = rnd.Next(1, 150)
            };
        }
    }
}