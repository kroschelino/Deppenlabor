using Windows.ApplicationModel;
using Microsoft.UI.Xaml;

namespace Deppenlabor;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        ExtendsContentIntoTitleBar = true;
        AppTitleTextBlock.Text = AppInfo.Current.DisplayInfo.DisplayName;
        SetTitleBar(AppTitleBar);
    }
}