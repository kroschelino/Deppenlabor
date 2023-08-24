using Deppenlabor.UserSettings.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Deppenlabor.UserSettings.Services;

public class UserSettingsService : IUserSettingsService
{
    private readonly IApplicationDataContainer _applicationDataContainer;
    private readonly ILogger<UserSettingsService> _logger;

    public UserSettingsService(IApplicationDataContainer applicationDataContainer, ILogger<UserSettingsService> logger)
    {
        _applicationDataContainer = applicationDataContainer;
        _logger = logger;
    }

    public bool LoadSetting(Type type, object defaultSetting)
    {
        if (defaultSetting == null) throw new ArgumentNullException(nameof(defaultSetting));

        _logger.LogTrace("{methodName}()", nameof(LoadSetting));
        if (!_applicationDataContainer.Values.TryGetValue(type.Name, out var value))
        {
            _logger.LogDebug("Setting {name} not in storage.", type.Name);
            return false;
        }

        try
        {
            _logger.LogDebug("Setting {name} restored from local storage. Content was\n{content}",
                type.Name, value as string);
            JsonConvert.PopulateObject((value as string)!, defaultSetting);
            return true;
        }
        catch (Exception e)
        {
            _logger.LogDebug(e, "Could not deserialize setting {name}", type.Name);
            return false;
        }
    }

    public void SaveSetting(Type type, object value)
    {
        var json = JsonConvert.SerializeObject(value);
        _logger.LogDebug("{methodName} for setting {name} with value:\n{value}", nameof(SaveSetting),
            type.Name, json);
        _applicationDataContainer.Values[type.Name] = json;
    }
}