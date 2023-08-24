using Windows.Foundation.Collections;
using Windows.Storage;
using Deppenlabor.UserSettings.Services.Interfaces;
using WindowsApplicationDataContainer = Windows.Storage.ApplicationDataContainer;

namespace Deppenlabor.UserSettings.Services;

public class ApplicationDataContainer : IApplicationDataContainer
{
    private readonly WindowsApplicationDataContainer _localSettings =
        ApplicationData.Current.LocalSettings;

    public IPropertySet Values => _localSettings.Values;
}