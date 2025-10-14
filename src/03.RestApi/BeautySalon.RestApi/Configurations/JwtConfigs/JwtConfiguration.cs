using BeautySalon.Common.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BeautySalon.RestApi.Configurations.JwtConfigs;

public static class JwtConfiguration
{

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
        string environment, string contentRootPath)
    {
        var jwtSettings = JwtSettingLoader.Load(environment, contentRootPath);
        services.AddSingleton(jwtSettings);
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
       .AddJwtBearer(options =>
       {
           options.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuer = true,
               ValidIssuer = jwtSettings.Issuer,

               ValidateAudience = true,
               ValidAudience = jwtSettings.Audience,

               ValidateIssuerSigningKey = true,
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),

               ValidateLifetime = true,
               ClockSkew = TimeSpan.Zero
           };
       });
        return services;
    }
}
