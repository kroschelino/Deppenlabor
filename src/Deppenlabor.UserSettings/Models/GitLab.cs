using CommunityToolkit.Mvvm.ComponentModel;

namespace Deppenlabor.UserSettings.Models;

public partial class GitLab : ObservableObject
{
    [ObservableProperty] private string _serverUrl;

    [ObservableProperty] private string _accessToken;

    public GitLab()
    {
        ServerUrl = string.Empty;
        AccessToken = string.Empty;
    }
}