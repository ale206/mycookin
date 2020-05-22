using System;
using System.ComponentModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using TaechIdeas.MyCookin.API.Middleware;
using TaechIdeas.MyCookin.API.Utils;
using TaechIdeas.MyCookin.IoC;
using TaechIdeas.MyCookin.IoC.AutoMapper;

namespace TaechIdeas.MyCookin.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _environment;
        private readonly ILoggerFactory _loggerFactory;

        public Startup(IConfiguration configuration, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // TODO https://github.com/shieldfy/API-Security-Checklist

            _configuration = configuration;
            _environment = env;
            _loggerFactory = loggerFactory;

            //Convert all coming dates to UTC
            // Due to a limitation to this converter, if the client passes a ts without timezone information, this server defaults the DateTime to server's Local tz.
            // Therefore, the server TZ must always be set to UTC!!!
            TypeDescriptor.AddAttributes(typeof(DateTime),
                new TypeConverterAttribute(typeof(DateUtils.UtcDateTimeConverter)));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

            // Load configuration from appsettings.json
            services.AddOptions();

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            // .AddJsonOptions(o => o.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat)
            // .AddJsonOptions(o => o.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc)
            // .AddJsonOptions(o => o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

            // Routing Options
            services.AddRouting(options => { options.LowercaseUrls = true; });

            // AutoMapper
            services.ConfigureAutoMapper();

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MyCookin", Version = "v1"
                });
                //c.DescribeAllEnumsAsStrings();

                // Fix for: Swashbuckler tries to just use the class name as a simple schemaId, 
                // however if you have two classes in different namespaces with the same name this will not work.
                //c.CustomSchemaIds(x => x.FullName);

                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"; // Keep this in sync with csproj's <DocumentationFile>
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);

                //xmlPath = Path.Combine(AppContext.BaseDirectory, "TaechIdeas.MyCookin.Core.xml"); // Keep this in sync with csproj's <DocumentationFile>
                //c.IncludeXmlComments(xmlPath);
            });

            // WebAPI specific injections
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Dependency Injection
            services.SetupIoC(_configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            // should be the first middleware to run, sets a "request id"
            app.Use(async (context, next) =>
            {
                context.TraceIdentifier = Guid.NewGuid().ToString();
                await next.Invoke();
            });

            // Add StopWatch Middleware //TODO: Move to Foundation
            app.UseMiddleware<StopWatchMiddleware>();

            // This is what UseMvc() adds: https://github.com/aspnet/Mvc/blob/dev/src/Microsoft.AspNetCore.Mvc/MvcServiceCollectionExtensions.cs#L25
            //app.UseMvc();

            // Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("v1/swagger.json", "MyCookin"); });
        }
    }
}