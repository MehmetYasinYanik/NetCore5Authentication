using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Shared.Configuration;
using Shared.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Extensions
{
    public static class CustomTokenAuth
    {
        public static void AddCustomTokenAuth(this IServiceCollection services, CustomTokenOption tokenOptions) 
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience[0],

                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            }
           );
        }
    }
}
