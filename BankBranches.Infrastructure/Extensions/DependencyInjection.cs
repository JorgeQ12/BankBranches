using System.Text;
using BankBranches.Domain.Interfaces;
using BankBranches.Infrastructure.Authentication;
using BankBranches.Infrastructure.ExternalServices;
using BankBranches.Infrastructure.Repositories.Common;
using BankBranches.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using BankBranches.Application.Interfaces;

namespace BankBranches.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Generic Repository
        services.AddSingleton<IGenericRepository, GenericRepository>();

        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IBranchRepository, BranchRepository>();
        services.AddScoped<ICityRepository, CityRepository>();

        // JWT Generator Service (Infrastructure Implementation)
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        IConfigurationSection jwtSettings = configuration.GetSection("Jwt");
        string jwtSecret = jwtSettings["Secret"]
            ?? throw new InvalidOperationException("JWT Secret no configurado.");

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
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
                RoleClaimType = System.Security.Claims.ClaimTypes.Role
            };
            options.Events = new JwtBearerEvents
            {
                OnForbidden = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                }
            };
        });

        // External API - Colombia
        services.AddHttpClient<IRegionExternalService, ColombiaApiService>(client =>
        {
            client.BaseAddress = new Uri("https://api-colombia.com/");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        return services;
    }
}
