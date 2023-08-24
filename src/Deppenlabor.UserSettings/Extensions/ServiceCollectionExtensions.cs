using Deppenlabor.UserSettings.Context;
using Deppenlabor.UserSettings.Context.Interfaces;
using Deppenlabor.UserSettings.Services;
using Deppenlabor.UserSettings.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Deppenlabor.UserSettings.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUserSettings(this IServiceCollection services)
    {
        services.AddScoped<IUserSettingsService, UserSettingsService>();
        services.AddScoped<IUserSettingsContext, UserSettingsContext>();
        services.AddSingleton<IApplicationDataContainer, ApplicationDataContainer>();
        return services;
    }
}