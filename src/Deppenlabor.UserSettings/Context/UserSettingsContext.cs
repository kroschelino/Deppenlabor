using Deppenlabor.UserSettings.Context.Interfaces;
using Deppenlabor.UserSettings.Models;
using Deppenlabor.UserSettings.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Deppenlabor.UserSettings.Context;

public class UserSettingsContext : IUserSettingsContext
{
    private readonly ILogger<UserSettingsContext> _logger;
    private readonly IUserSettingsService _userSettingsService;

    public UserSettingsContext(IUserSettingsService userSettingsService, IServiceScopeFactory serviceScopeFactory,
        ILogger<UserSettingsContext> logger)
    {
        _userSettingsService = userSettingsService;
        _logger = logger;

        using var scope = serviceScopeFactory.CreateScope();
        var propertyInfos = GetType().GetProperties().ToList();
        _logger.LogTrace("Load user settings from storage...");
        propertyInfos.ForEach(p =>
        {
            var ctor = p.PropertyType.GetConstructors().FirstOrDefault();
            var parameterValue = ctor!.GetParameters().Length > 0
                ? ctor!.Invoke(ctor!.GetParameters()
                    .Select(cp => scope.ServiceProvider.GetRequiredService(cp.ParameterType))
                    .ToArray())
                : Activator.CreateInstance(p.PropertyType);
            p.SetValue(this, parameterValue);

            _userSettingsService.LoadSetting(p.PropertyType, p.GetValue(this)!);
        });
    }

    public GitLabAccounts GitLabAccounts { get; set; } = null!;

    public Task SaveChanges()
    {
        _logger.LogInformation("Saving user settings");

        var propertyInfos = GetType().GetProperties().ToList();
        propertyInfos.ForEach(p => { _userSettingsService.SaveSetting(p.PropertyType, p.GetValue(this)!); });

        return Task.CompletedTask;
    }
}