using System;
using System.Collections.Generic;
using System.Linq;
using Windows.ApplicationModel;
using Windows.System;
using Windows.UI.Core;
using Deppenlabor.Views;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Deppenlabor;

public sealed partial class MainWindow : Window
{
    // List of ValueTuple holding the Navigation Tag and the relative Navigation Page
    private readonly List<(string Tag, Type Page)> _pages = new()
    {
        ("powerBranches", typeof(PowerBranchesPage)),
    };

    public MainWindow()
    {
        InitializeComponent();

        ExtendsContentIntoTitleBar = true;
        AppTitleTextBlock.Text = AppInfo.Current.DisplayInfo.DisplayName;
        SetTitleBar(AppTitleBar);
    }

    private void ContentFrame_NavigationFailed(object? sender, NavigationFailedEventArgs e)
    {
        throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
    }

    private void NavigationViewControl_Loaded(object? sender, RoutedEventArgs e)
    {
        // Add handler for ContentFrame navigation.
        ContentFrame.Navigated += On_Navigated;

        // NavigationViewControl doesn't load any page by default, so load home page.
        NavigationViewControl.SelectedItem = NavigationViewControl.MenuItems[0];
        // If navigation occurs on SelectionChanged, this isn't needed.
        // Because we use ItemInvoked to navigate, we need to call Navigate
        // here to load the home page.
        NavigationViewControl_Navigate(_pages.First().Tag);

        // Listen to the window directly so the app responds
        // to accelerator keys regardless of which element has focus.
        //Current.CoreWindow.Dispatcher.AcceleratorKeyActivated +=
        //    CoreDispatcher_AcceleratorKeyActivated;

        //Current.CoreWindow.PointerPressed += CoreWindow_PointerPressed;

        //SystemNavigationManager.GetForCurrentView().BackRequested += System_BackRequested;
    }


    private void NavigationViewControl_ItemInvoked(NavigationView sender,
        NavigationViewItemInvokedEventArgs args)
    {
        if (args.IsSettingsInvoked)
        {
            NavigationViewControl_Navigate("settings");
        }
        else if (args.InvokedItemContainer != null)
        {
            var navItemTag = args.InvokedItemContainer.Tag.ToString();
            NavigationViewControl_Navigate(navItemTag!);
        }
    }

    // NavigationViewControl_SelectionChanged is not used in this example, but is shown for completeness.
    // You will typically handle either ItemInvoked or SelectionChanged to perform navigation,
    // but not both.
    //private void NavigationViewControl_SelectionChanged(NavigationView sender,
    //    NavigationViewSelectionChangedEventArgs args)
    //{
    //    if (args.IsSettingsSelected)
    //    {
    //        NavigationViewControl_Navigate("settings");
    //    }
    //    else if (args.SelectedItemContainer != null)
    //    {
    //        var navItemTag = args.SelectedItemContainer.Tag.ToString();
    //        NavigationViewControl_Navigate(navItemTag!);
    //    }
    //}

    private void NavigationViewControl_Navigate(
        string navItemTag)
    {
        Type? page = null;
        if (navItemTag == "settings")
        {
            //_page = typeof(SettingsPage);
        }
        else
        {
            var item = _pages.FirstOrDefault(p => p.Tag.Equals(navItemTag));
            page = item.Page;
        }

        // Get the page type before navigation so you can prevent duplicate
        // entries in the backstack.
        var preNavPageType = ContentFrame.CurrentSourcePageType;

        // Only navigate if the selected page isn't currently loaded.
        if (page is not null && preNavPageType != page) ContentFrame.Navigate(page, null);
    }

    private void NavigationViewControl_BackRequested(NavigationView sender,
        NavigationViewBackRequestedEventArgs args)
    {
        TryGoBack();
    }

    private void CoreDispatcher_AcceleratorKeyActivated(CoreDispatcher sender, AcceleratorKeyEventArgs e)
    {
        // When Alt+Left are pressed navigate back
        if (e is
            {
                EventType: CoreAcceleratorKeyEventType.SystemKeyDown, VirtualKey: VirtualKey.Left,
                KeyStatus.IsMenuKeyDown: true, Handled: false,
            })
            e.Handled = TryGoBack();
    }

    private void System_BackRequested(object? sender, BackRequestedEventArgs e)
    {
        if (!e.Handled) e.Handled = TryGoBack();
    }

    private void CoreWindow_PointerPressed(CoreWindow sender, PointerEventArgs e)
    {
        // Handle mouse back button.
        if (e.CurrentPoint.Properties.IsXButton1Pressed) e.Handled = TryGoBack();
    }

    private bool TryGoBack()
    {
        if (!ContentFrame.CanGoBack)
            return false;

        // Don't go back if the nav pane is overlayed.
        if (NavigationViewControl.IsPaneOpen &&
            NavigationViewControl.DisplayMode is NavigationViewDisplayMode.Compact or NavigationViewDisplayMode.Minimal)
            return false;
        ContentFrame.GoBack();
        return true;
    }

    private void On_Navigated(object sender, NavigationEventArgs e)
    {
        NavigationViewControl.IsBackEnabled = ContentFrame.CanGoBack;

        //if (ContentFrame.SourcePageType == typeof(SettingsPage))
        //{
        //    // SettingsItem is not part of NavigationViewControl.MenuItems, and doesn't have a Tag.
        //    NavigationViewControl.SelectedItem = (NavigationViewItem)NavigationViewControl.SettingsItem;
        //    NavigationViewControl.Header = "Settings";
        //}
        //else
        if (ContentFrame.SourcePageType != null)
        {
            var item = _pages.FirstOrDefault(p => p.Page == e.SourcePageType);
            NavigationViewControl.SelectedItem = NavigationViewControl.MenuItems
                .OfType<NavigationViewItem>()
                .First(n => n.Tag.Equals(item.Tag));

            NavigationViewControl.Header =
                ((NavigationViewItem)NavigationViewControl.SelectedItem)?.Content?.ToString();
        }
    }
}