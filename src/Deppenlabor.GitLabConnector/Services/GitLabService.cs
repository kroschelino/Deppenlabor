using Deppenlabor.GitLabConnector.Dtos;
using GitLabApiClient;
using Microsoft.Extensions.Logging;

namespace Deppenlabor.GitLabConnector.Services;

public class GitLabService
{
    private readonly ILogger<GitLabService> _logger;
    private GitLabClient? _gitLabClient;
    private List<Repository>? _repositoryList;

    public GitLabService(ILogger<GitLabService> logger)
    {
        _logger = logger;
    }

    public bool IsConnected => _gitLabClient != null;

    public Task<bool> Connect(string url, string accessToken)
    {
        _logger.LogDebug($"Connect to to Gitlab at {url}");
        try
        {
            _gitLabClient = new GitLabClient(url, accessToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to connect to GitLab at {url}");
            _gitLabClient = null;
        }

        return Task.FromResult(IsConnected);
    }

    public async Task<List<Repository>> GetRepositories(bool forceUpdate = false)
    {
        if (_gitLabClient == null) throw new InvalidOperationException();
        if (_repositoryList != null && !forceUpdate) return _repositoryList;

        var projects = await _gitLabClient.Projects.GetAsync(options =>
        {
            options.IsMemberOf = true;
            options.Simple = true;
        });
        _repositoryList = projects.Select(_ =>
            new Repository
            {
                Name = _.Name,
                SshUrl = _.SshUrlToRepo,
                HttpUrl = _.HttpUrlToRepo,
            }).ToList();

        return _repositoryList;
    }
}