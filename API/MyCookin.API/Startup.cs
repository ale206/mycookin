using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyCookin.IoC;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace MyCookin.API
{
    
    public class Startup
    {
        private const string ApiVersion = "v1";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            Initializer.RegisterServices(services);

            services.AddAuthentication(c =>
                {
                    c.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    c.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    c.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddOpenIdConnect(c =>
                {
                    c.ResponseType = Configuration["Authentication:Cognito:ResponseType"];
                    c.MetadataAddress = Configuration["Authentication:Cognito:MetadataAddress"];
                    c.ClientId = Configuration["Authentication:Cognito:ClientId"];
                    c.Authority = "https://auth.mycookin.com";
                });
            
            // Configure named auth policies that map directly to OAuth2.0 scopes
            services.AddAuthorization(c =>
            {
                c.AddPolicy("readAccess", p => p.RequireClaim("scope", "mycookin/api"));
            });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(ApiVersion, new OpenApiInfo
                {
                    Title = "MyCookin", Version = ApiVersion
                });
                c.OperationFilter<AddAuthHeaderOperationFilter>();

                c.AddSecurityDefinition("bearer", //Name the security scheme
                    new OpenApiSecurityScheme{
                        Flows = new OpenApiOAuthFlows()
                        {
                            ClientCredentials = new OpenApiOAuthFlow()
                            {
                                TokenUrl = new Uri("https://auth.mycookin.com/oauth2/token"),
                                Scopes = new Dictionary<string, string>(){ {"mycookin/api", "Access API"}},
                                AuthorizationUrl = new Uri("https://auth.mycookin.com/oauth2/authorize")
                            }
                        }, 
                        Type = SecuritySchemeType.OAuth2,
                        OpenIdConnectUrl = new Uri("https://cognito-idp.eu-west-1.amazonaws.com/eu-west-1_Zrq7io2kN/.well-known/openid-configuration"),
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Scheme = "bearer"
                    });
            });

           
            services.AddOptions();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseAuthentication();

           
            
            app.UseSwagger();
            app.UseSwaggerUI(c => { 
                c.SwaggerEndpoint($"{ApiVersion}/swagger.json", "MyCookin v1");
                c.OAuth2RedirectUrl("https://auth.mycookin.com/signin-oidc");
                
            });
        }
    }
}