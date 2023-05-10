using CommunityToolkit.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;

namespace Deppenlabor;

/// <summary>
///     Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : CancelableApplication
{
    private Window? _mainWindow;

    /// <summary>
    ///     Initializes the singleton application object.  This is the first line of authored code
    ///     executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        InitializeComponent();
    }

    public static FrameworkElement? MainRoot { get; private set; }

    public new static App Current => (App)CancelableApplication.Current;

    /// <summary>
    ///     Invoked when the application is launched.
    /// </summary>
    /// <param name="args">Details about the launch request and process.</param>
    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        _mainWindow = Services.GetRequiredService<MainWindow>();
        _mainWindow.Activate();
        MainRoot = _mainWindow.Content as FrameworkElement;
    }
}