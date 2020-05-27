using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MyCookin.API
{
    public class AddAuthHeaderOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var isAuthorized = (context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>()
                                    .Any()
                                || context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any())
                               && !context.MethodInfo.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>()
                                   .Any(); // this excludes methods with AllowAnonymous attribute

            if (!isAuthorized) return;

            operation.Responses.TryAdd("401", new OpenApiResponse {Description = "Unauthorized"});
            operation.Responses.TryAdd("403", new OpenApiResponse {Description = "Forbidden"});

            var jwtBearerScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "bearer"}
            };

            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement {[jwtBearerScheme] = new string[] { }}
            };
        }
    }
}