using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using TaechIdeas.MyCookin.BusinessLogic;
using TaechIdeas.MyCookin.DataAccessLayer;
using TaechIdeas.MyCookin.IoC.AutoMapper;
using TaechIdeas.MyCookin.Services;

namespace TaechIdeas.MyCookin.IoC
{
    public static class Setup
    {
        private static IServiceCollection _serviceCollection;
        private static IServiceProvider _serviceProvider;

        /// <summary>
        ///     Extension method to Setup IoC.
        ///     At this stage everything in given Assemblies are registered with Scoped Lifetime.
        ///     Must be considered to provide more flexibility filtering by namespaces,
        ///     allowing to register for different scopes
        /// </summary>
        /// <param name="serviceCollection">IServiceCollection</param>
        /// <param name="configuration"></param>
        /// <returns>IServiceCollection with registered interfaces</returns>
        public static void SetupIoC(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            // Register Types
            serviceCollection.RegisterAllTypes();

            // RestSharp
            serviceCollection.AddScoped<IRestClient, RestClient>();

            serviceCollection.AddSingleton(configuration);

            // Automatically register all the configuration parameters classes as singletons
            //serviceCollection.RegisterConfigurationParameters(configuration);
        }

        /// <summary>
        ///     Register Types in Channel Manager, filtered by Assembly
        /// </summary>
        /// <param name="serviceCollection">Service Collection</param>
        private static void RegisterAllTypes(this IServiceCollection serviceCollection)
        {
            // Assemblies to be registered with Scoped Lifetime
            var scopedLifetimeAssemblies = new[]
            {
                // Business Logic
                typeof(RecipeManager).GetTypeInfo().Assembly,

                // Data Layer
                typeof(RecipeRepository).GetTypeInfo().Assembly,

                // Services
                typeof(AuthService).GetTypeInfo().Assembly
            };

            var typesCollection = scopedLifetimeAssemblies
                .SelectMany(assembly => assembly.GetTypes())
                // Not Abstract
                .Where(type => !type.GetTypeInfo().IsAbstract)
                // At least one interface
                .Where(p => p.GetInterfaces().FirstOrDefault() != null);

            // Service Types already registered
            var serviceTypes = serviceCollection.Select(x => x.ServiceType);

            // Register only not-already-registered types
            var toBeRegistered = typesCollection
                .Where(p => serviceTypes.All(x => x.FullName != p.GetInterfaces().First().FullName)
                ).ToList();

            serviceCollection.RegisterAsScoped(toBeRegistered);
        }

        /// <summary>
        ///     Register types as Scoped
        /// </summary>
        /// <param name="serviceCollection">ServiceCollection</param>
        /// <param name="implementationTypes">Types to be registered</param>
        private static void RegisterAsScoped(this IServiceCollection serviceCollection, IEnumerable<Type> implementationTypes)
        {
            foreach (var implementationType in implementationTypes)
            {
                foreach (var interfaceType in implementationType.GetInterfaces())
                {
                    serviceCollection.AddScoped(interfaceType, implementationType);
                }
            }
        }

        /// <summary>
        ///     Retrieve the Service from where the IoC can't be initialized at Startup (e.g.: WebApplication, Integration Tests)
        /// </summary>
        /// <typeparam name="T">Desired Service</typeparam>
        /// <param name="appSettingToBeUsed">Name of the app settings that must be used</param>
        /// <returns>Implementation of the service</returns>
        public static T GetService<T>(string appSettingToBeUsed = null)
        {
            // Temporary fix to avoid "Collection was modified" error
            var serviceCollection = _serviceCollection;

            if (serviceCollection != null)
            {
                return _serviceProvider.GetService<T>();
            }

            var contentRoot = AppDomain.CurrentDomain.BaseDirectory;

            var builder = new ConfigurationBuilder()
                .SetBasePath(contentRoot)
                .AddJsonFile("appsettings.json");

            if (!string.IsNullOrEmpty(appSettingToBeUsed))
            {
                builder.AddJsonFile(appSettingToBeUsed, true);
            }

            builder.AddEnvironmentVariables();
            var configuration = builder.Build();

            serviceCollection = new ServiceCollection();
            serviceCollection.ConfigureAutoMapper();
            serviceCollection.SetupIoC(configuration);

            _serviceProvider = serviceCollection.BuildServiceProvider();

            _serviceCollection = serviceCollection;

            return _serviceProvider.GetService<T>();
        }

        /// <summary>
        ///     Reset Services
        /// </summary>
        public static void ResetServices()
        {
            _serviceProvider = null;
            _serviceCollection = null;
        }
    }
}