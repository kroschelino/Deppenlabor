using CommunityToolkit.Mvvm.ComponentModel;

namespace Deppenlabor.ViewModels;

public partial class PowerBranchesViewModel : ObservableObject
{
    [ObservableProperty] private string _gitlabAccessToken = "";
    [ObservableProperty] private string _gitlabConnectButtonText = "Connect";
    [ObservableProperty] private bool _gitlabExpanded = true;
    [ObservableProperty] private string _gitlabUrl = "";
    [ObservableProperty] private bool _isGitlabAccessTokenEnabled = true;
    [ObservableProperty] private bool _isGitlabUrlEnabled = true;
    [ObservableProperty] private bool _isRepositoriesEnabled;
    [ObservableProperty] private bool _isRepositoriesExpanded;
}