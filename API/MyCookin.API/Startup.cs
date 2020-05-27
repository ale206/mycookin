using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyCookin.IoC;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerUI;

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
            IdentityModelEventSource.ShowPII = true;

            services.AddControllers();

            Initializer.RegisterServices(services);

            services.AddAuthentication(o =>
                {
                    o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKeyResolver = (s, securityToken, identifier, parameters) =>
                        {
                            var json = new WebClient().DownloadString(
                                parameters.ValidIssuer + "/.well-known/jwks.json");
                            var keys = JsonConvert.DeserializeObject<JsonWebKeySet>(json).Keys;
                            return (IEnumerable<SecurityKey>) keys;
                        },

                        ValidIssuer = Configuration["Authentication:Cognito:Authority"],
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = false,
                        ValidateLifetime = true,
                        ValidateAudience = false,
                    };
                    options.IncludeErrorDetails = true;
                    options.SaveToken = true;
                    options.Authority = Configuration["Authentication:Cognito:Authority"];
                    options.RequireHttpsMetadata = true;
                })
                .AddOpenIdConnect(options =>
                {
                    options.Authority = Configuration["Authentication:Cognito:Authority"];
                    options.RequireHttpsMetadata = false;
                    options.ClientId = Configuration["Authentication:Cognito:ClientId"];
                    options.Scope.Add("mycookin/api");
                });;

            services.AddAuthorization(options => 
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(ApiVersion, new OpenApiInfo
                {
                    Title = "MyCookin", Version = ApiVersion
                });
                c.OperationFilter<AddAuthHeaderOperationFilter>();

                c.AddSecurityDefinition("bearer", //Name the security scheme
                    new OpenApiSecurityScheme
                    {
                        Flows = new OpenApiOAuthFlows
                        {
                            ClientCredentials = new OpenApiOAuthFlow
                            {
                                TokenUrl = new Uri("https://auth.mycookin.com/oauth2/token"),
                                Scopes = new Dictionary<string, string> {{"mycookin/api", "Access API"}},
                                AuthorizationUrl = new Uri("https://auth.mycookin.com/oauth2/authorize")
                            }
                        },
                        Type = SecuritySchemeType.OAuth2,
                        OpenIdConnectUrl =
                            new Uri(
                                "https://cognito-idp.eu-west-1.amazonaws.com/eu-west-1_Zrq7io2kN/.well-known/openid-configuration"),
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
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"{ApiVersion}/swagger.json", "MyCookin v1");
                c.OAuth2RedirectUrl("https://auth.mycookin.com/signin-oidc");
                c.OAuthConfigObject = new OAuthConfigObject
                {
                    ClientId = Configuration["Authentication:Cognito:ClientId"],
                    UsePkceWithAuthorizationCodeGrant = true
                };
            });
        }
    }
}