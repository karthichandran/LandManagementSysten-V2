﻿using System;
using System.Threading.Tasks;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;
using Windows.ApplicationModel.Core;

using LandBankManagement.Animations;
using LandBankManagement.ViewModels;
using LandBankManagement.Services;

namespace LandBankManagement.Views
{
    public sealed partial class LoginView : Page
    {
        public LoginView()
        {
            ViewModel = ServiceLocator.Current.GetService<LoginViewModel>();
            InitializeContext();
            InitializeComponent();
            this.Loaded += LoginView_Loaded;

        }

        private void LoginView_Loaded(object sender, RoutedEventArgs e)
        {
            this.Frame.Loaded += Frame_Loaded;
        }

        private void Frame_Loaded(object sender, RoutedEventArgs e)
        {
            passwordView.Focus();
        }

        public LoginViewModel ViewModel { get; }

        private void InitializeContext()
        {
            var context = ServiceLocator.Current.GetService<IContextService>();
            context.Initialize(Dispatcher, ApplicationView.GetForCurrentView().Id, CoreApplication.GetCurrentView().IsMain);
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            _currentEffectMode = EffectMode.None;
            await ViewModel.LoadAsync(e.Parameter as ShellArgs);
            InitializeNavigation();
        }

        private void InitializeNavigation()
        {
            var navigationService = ServiceLocator.Current.GetService<INavigationService>();
            navigationService.Initialize(Frame);
        }

        protected override async void OnKeyDown(KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                ViewModel.ShowProgressRing();
                DoEffectOut();

                ViewModel.Login();
            }
            base.OnKeyDown(e);
        }

        private async void OnShowLoginWithPassword(object sender, RoutedEventArgs e)
        {

            passwordView.Focus();
        }

        private void OnBackgroundFocus(object sender, RoutedEventArgs e)
        {
            DoEffectIn();
        }

        private void OnForegroundFocus(object sender, RoutedEventArgs e)
        {
            DoEffectOut();
        }

        private EffectMode _currentEffectMode = EffectMode.None;

        private void DoEffectIn(double milliseconds = 1000)
        {
            if (_currentEffectMode == EffectMode.Foreground || _currentEffectMode == EffectMode.None)
            {
                _currentEffectMode = EffectMode.Background;
                background.Scale(milliseconds, 1.0, 1.1);
                background.Blur(milliseconds, 6.0, 0.0);
                foreground.Scale(500, 1.0, 0.95);
                foreground.Fade(milliseconds, 1.0, 0.75);
            }
        }

        private void DoEffectOut(double milliseconds = 1000)
        {
            if (_currentEffectMode == EffectMode.Background || _currentEffectMode == EffectMode.None)
            {
                _currentEffectMode = EffectMode.Foreground;
                background.Scale(milliseconds, 1.1, 1.0);
                background.Blur(milliseconds, 0.0, 6.0);
                foreground.Scale(500, 0.95, 1.0);
                foreground.Fade(milliseconds, 0.75, 1.0);
            }
        }

        public enum EffectMode
        {
            None,
            Background,
            Foreground,
            Disabled
        }
    }
}
