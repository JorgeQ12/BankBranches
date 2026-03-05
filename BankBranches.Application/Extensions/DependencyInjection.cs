using Microsoft.Extensions.DependencyInjection;
using BankBranches.Application.Interfaces;
using BankBranches.Application.Services;

namespace BankBranches.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IBranchService, BranchService>();
        services.AddScoped<ICityService, CityService>();

        return services;
    }
}
