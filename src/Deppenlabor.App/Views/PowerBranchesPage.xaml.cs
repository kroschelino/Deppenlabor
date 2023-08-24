// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Deppenlabor.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Deppenlabor.Views;

/// <summary>
///     An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class PowerBranchesPage : Page
{
    public PowerBranchesPage()
    {
        InitializeComponent();
        DataContext = App.Current.Services.GetRequiredService<PowerBranchesViewModel>();
    }

    public PowerBranchesViewModel ViewModel => (PowerBranchesViewModel)DataContext;

    public static string GetConnectButtonText(bool isConnected) => isConnected ? "Disconnect" : "Connect";
}