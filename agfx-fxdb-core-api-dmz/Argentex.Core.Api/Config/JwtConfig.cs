using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Primitives;

namespace Argentex.Core.Api.Config
{
    public static class JwtConfig
    {
        public static void ConfigureJwt(IServiceCollection services, IConfiguration config)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                //options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.Audience = "resource_server";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    RequireExpirationTime = false,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:SecurityKey"])),
                    ClockSkew = TimeSpan.FromSeconds(Convert.ToInt32(config["Jwt:Expires"])),
                    NameClaimType = OpenIdConnectConstants.Claims.Subject,
                    RoleClaimType = OpenIdConnectConstants.Claims.Role
            };
            });
        }
    }
}
