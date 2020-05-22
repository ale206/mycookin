using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace TaechIdeas.MyCookin.IoC.AutoMapper
{
    /// <summary>
    ///     AutoMapper Configuration
    /// </summary>
    public static class AutoMapperConfig
    {
        /// <summary>
        ///     Extension method to configure AutoMapper in DI
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void ConfigureAutoMapper(this IServiceCollection serviceCollection)
        {
            var config = GetMapperConfiguration();

            // AutoMapper registered as Singleton
            serviceCollection.AddSingleton(sp => config.CreateMapper());
        }

        private static MapperConfiguration GetMapperConfiguration()
        {
            return new MapperConfiguration(cfg =>
            {
                //Myc
                //cfg.AddProfile<MapForIngredient>();
                cfg.AddProfile<MapForRecipe>();

                // To automatically create missing types map use: cfg.CreateMissingTypeMaps = true;
                // cfg.CreateMissingTypeMaps = true;
            });
        }
    }
}