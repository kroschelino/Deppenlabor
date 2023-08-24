using Deppenlabor.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Deppenlabor.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Registers all interfaces and classes for the app core
    /// </summary>
    public static IServiceCollection AddViews(this IServiceCollection services)
    {
        services.AddScoped<MainWindow>();
        return services;
    }

    /// <summary>
    ///     Registers all interfaces and classes for the app core
    /// </summary>
    public static IServiceCollection AddViewModels(this IServiceCollection services)
    {
        services.AddScoped<PowerBranchesViewModel>();
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services) => services;
}