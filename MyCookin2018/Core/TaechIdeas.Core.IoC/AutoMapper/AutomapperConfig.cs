using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace TaechIdeas.Core.IoC.AutoMapper
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
                //cfg.Map
                cfg.AddProfile<MapForAudit>();
                cfg.AddProfile<MapForCommon>();
                cfg.AddProfile<MapForUser>();
                cfg.AddProfile<MapForMedia>();
                cfg.AddProfile<MapForStatistics>();
                cfg.AddProfile<MapForVerification>();
                cfg.AddProfile<MapForLogsErrorsMessages>();
                cfg.AddProfile<MapForUserBoard>();
                cfg.AddProfile<MapForContact>();
                cfg.AddProfile<MapForLogsErrorsMessages>();
                cfg.AddProfile<MapForNetwork>();

                // To automatically create missing types map use: cfg.CreateMissingTypeMaps = true;
                // cfg.CreateMissingTypeMaps = true;
            });
        }
    }
}