using Microsoft.Extensions.DependencyInjection;
using MyCookin.Business.Implementations;
using MyCookin.Business.Interfaces;
using MyCookin.Domain.Repositories;
using MyCookin.Infrastructure.Implementations;

namespace MyCookin.IoC
{
    public static class Initializer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IIngredientService, IngredientService>();
            services.AddScoped<IIngredientRepository, IngredientRepository>();
        }
    }
}