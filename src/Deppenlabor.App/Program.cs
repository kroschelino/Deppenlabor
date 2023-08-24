using System;
using System.IO;
using System.Runtime.Versioning;
using Windows.Storage;
using CommunityToolkit.Extensions.Hosting;
using Deppenlabor.Extensions;
using Deppenlabor.GitLabConnector.Extensions;
using Deppenlabor.UserSettings.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace Deppenlabor;

[SupportedOSPlatform("windows10.0.19041.0")]
public static class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        var builder = new WindowsAppSdkHostBuilder<App>();

        builder.ConfigureServices(ConfigureServices);
        builder.ConfigureLogging(ConfigureLogging);

        var app = builder.Build();

        app.StartAsync().GetAwaiter().GetResult();
    }


    private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        services.AddViews();
        services.AddViewModels();
        services.AddServices();
        services.AddGitlab();
        services.AddUserSettings();
    }

    private static void ConfigureLogging(ILoggingBuilder loggingBuilder)
    {
        loggingBuilder.ClearProviders();
        var logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Debug()
            .WriteTo.File(Path.Combine(ApplicationData.Current.LocalFolder.Path,
                $"{typeof(Program).Assembly.GetName().Name}.log"))
            .CreateLogger();
        loggingBuilder.AddSerilog(logger);
    }
}