using Microsoft.Extensions.DependencyInjection;

namespace Deppenlabor.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Registers all interfaces and classes for the app core
    /// </summary>
    public static IServiceCollection AddViews(this IServiceCollection services)
    {
        services.AddSingleton<MainWindow>();
        return services;
    }

    /// <summary>
    ///     Registers all interfaces and classes for the app core
    /// </summary>
    public static IServiceCollection AddViewModels(this IServiceCollection services)
    {

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {

        return services;
    }
}