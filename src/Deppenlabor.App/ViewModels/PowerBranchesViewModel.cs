using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Deppenlabor.GitLabConnector.Dtos;
using Deppenlabor.GitLabConnector.Services;
using Deppenlabor.UserSettings.Context.Interfaces;
using Microsoft.Extensions.Logging;

namespace Deppenlabor.ViewModels;

public partial class PowerBranchesViewModel : ObservableObject
{
    private readonly GitLabService _gitLabService;
    private readonly IUserSettingsContext _appSettingsService;
    private readonly ILogger<PowerBranchesViewModel> _logger;

    [ObservableProperty] private string? _gitlabAccessToken;
    [ObservableProperty] private bool _isGitlabConnected;
    [ObservableProperty] private string? _gitlabUrl;
    [ObservableProperty] private bool _isLoading;

    public PowerBranchesViewModel(GitLabService gitLabService,
        IUserSettingsContext appSettingsService, ILogger<PowerBranchesViewModel> logger)
    {
        _gitLabService = gitLabService;
        _appSettingsService = appSettingsService;
        _logger = logger;

        GitlabUrl = _appSettingsService.GitLabAccounts.Accounts.First().ServerUrl;
        GitlabAccessToken = _appSettingsService.GitLabAccounts.Accounts.First().AccessToken;
    }

    public ObservableCollection<Repository> Repositories { get; set; } = new();


    [RelayCommand]
    private async Task ConnectToGitlab()
    {
        if (IsGitlabConnected)
        {
            _logger.LogWarning("Disconnecting is not yet supported");
            return;
        }

        IsGitlabConnected = false;

        if (!string.IsNullOrWhiteSpace(GitlabUrl) && !string.IsNullOrWhiteSpace(GitlabAccessToken))
        {
            _appSettingsService.GitLabAccounts.Accounts.First().AccessToken = GitlabAccessToken;
            _appSettingsService.GitLabAccounts.Accounts.First().ServerUrl = GitlabUrl;
            await _appSettingsService.SaveChanges();

            if (await _gitLabService.Connect(GitlabUrl, GitlabAccessToken))
            {
                IsGitlabConnected = true;
                IsLoading = true;
                Repositories = new ObservableCollection<Repository>(
                    (await _gitLabService.GetRepositories()).OrderBy(_ => _.Name));
                IsLoading = false;
                OnPropertyChanged(nameof(Repositories));
            }
        }
    }
}