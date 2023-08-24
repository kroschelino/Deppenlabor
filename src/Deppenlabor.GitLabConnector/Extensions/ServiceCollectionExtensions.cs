using Deppenlabor.GitLabConnector.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Deppenlabor.GitLabConnector.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGitlab(this IServiceCollection services)
    {
        services.AddScoped<GitLabService>();
        return services;
    }
}