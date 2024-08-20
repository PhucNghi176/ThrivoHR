using EXE201_BE_ThrivoHR.API.Services;
using EXE201_BE_ThrivoHR.Application.Common.Interfaces;
using EXE201_BE_ThrivoHR.Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace EXE201_BE_ThrivoHR.API.Configuration
{
    public static class ApplicationSecurityConfiguration
    {
        public static IServiceCollection ConfigureApplicationSecurity(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddTransient<ITokenService, TokenService>();
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            services.AddHttpContextAccessor();
            var jwtSettings = configuration.GetSection("JwtSettings");
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                 .AddJwtBearer(options =>
                 {
                     options.TokenValidationParameters = new TokenValidationParameters()
                     {
                         ValidateAudience = true,
                         ValidateIssuer = true,
                         ValidateIssuerSigningKey = true,
                         ValidateLifetime = true,
                         ValidIssuer = jwtSettings["validIssuer"],
                         ValidAudience = jwtSettings["validAudience"],
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThrivoHR API 123abc456 anh iu em....")),
                         ClockSkew = TimeSpan.Zero
                     };
                 });

            services.AddAuthorization(ConfigureAuthorization);

            return services;
        }


        private static void ConfigureAuthorization(AuthorizationOptions options)
        {
            //Configure policies and other authorization options here. For example:
            //options.AddPolicy("EmployeeOnly", policy => policy.RequireClaim("role", "employee"));
            //options.AddPolicy("AdminOnly", policy => policy.RequireClaim("role", "admin"));
        }
    }
}
