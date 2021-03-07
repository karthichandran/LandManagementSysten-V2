﻿using System.Linq;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;
using Windows.UI.Core;
using Windows.ApplicationModel.Core;

using LandBankManagement.ViewModels;
using LandBankManagement.Services;

using muxc = Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using BoldReports.UI.Xaml;

namespace LandBankManagement.Views
{
    public sealed partial class MainShellView : Page
    {
        private INavigationService _navigationService = null;
        private readonly ILogService LogService;

        public MainShellView()
        {
            ViewModel = ServiceLocator.Current.GetService<MainShellViewModel>();
            LogService = ServiceLocator.Current.GetService<ILogService>();
            InitializeContext();
            InitializeComponent();
            InitializeNavigation();
        }

        public MainShellViewModel ViewModel { get; }

        private SystemNavigationManager CurrentView => SystemNavigationManager.GetForCurrentView();

        private void InitializeContext()
        {
            var context = ServiceLocator.Current.GetService<IContextService>();
            context.Initialize(Dispatcher, ApplicationView.GetForCurrentView().Id, CoreApplication.GetCurrentView().IsMain);
        }

        private void InitializeNavigation()
        {
            _navigationService = ServiceLocator.Current.GetService<INavigationService>();
            _navigationService.Initialize(frame);
            frame.Navigated += OnFrameNavigated;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.LoadAsync(e.Parameter as ShellArgs);
            ViewModel.Subscribe();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            ViewModel.Unload();
            ViewModel.Unsubscribe();
        }

        private void OnSelectionChanged(muxc.NavigationView sender, muxc.NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItem is NavigationItem item)
            {
                ViewModel.NavigateTo(item.ViewModel, item.Screen.ToString());
            }
            else if (args.IsSettingsSelected)
            {
                ViewModel.NavigateTo(typeof(SettingsViewModel));
            }
            UpdateBackButton();
        }

        private void OnNavigationViewBackButton(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (_navigationService.CanGoBack)
            {
                _navigationService.GoBack();
            }
        }

        private void OnFrameNavigated(object sender, NavigationEventArgs e)
        {
            var targetType = NavigationService.GetViewModel(e.SourcePageType);
            switch (targetType.Name)
            {
                case "SettingsViewModel":
                    ViewModel.SelectedItem = navigationView.SettingsItem;
                    break;
                default:
                    var selectedItem = ViewModel.Items.Where(r => r.ViewModel == targetType).FirstOrDefault();
                    if (selectedItem == null)
                    {
                        foreach (var item in ViewModel.Items)
                        {
                            if (item.Children != null)
                                selectedItem = item.Children.Where(r => r.ViewModel == targetType).FirstOrDefault();

                            if (selectedItem != null) break;
                        }

                    }
                    ViewModel.SelectedItem = selectedItem;
                    break;
            }
            UpdateBackButton();
        }

        private void UpdateBackButton()
        {
            //  NavigationViewBackButton.IsEnabled = _navigationService.CanGoBack;
        }

        private async void OnLogoff(object sender, RoutedEventArgs e)
        {
            var dialogService = ServiceLocator.Current.GetService<IDialogService>();
            if (await dialogService.ShowAsync("Confirm logoff", "Are you sure to logoff?", "Ok", "Cancel"))
            {
                var loginService = ServiceLocator.Current.GetService<ILoginService>();
                loginService.Logoff();
                if (Frame.CanGoBack)
                {
                    Frame.GoBack();
                }
            }
        }

        private void CtrlF_Invoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            // controlsSearchBox.Focus(FocusState.Programmatic);
        }

        private void ReportViewer_ReportError(object sender, ReportErrorEventArgs e)
        {
            string errorLog;

            if (e.Exception != null)
            {
                errorLog = (string.Format("Error Message: {0} \n Stack Trace: {1}", e.Message, e.Exception.StackTrace));
            }
            else
            {
                errorLog = e.Message;
            }

            LogService.WriteAsync(Data.LogType.Error, errorLog, "Bold Reports", e.Exception);
        }
        private void Popup_closeBtn_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ClosePopup();
        }
    }
}
