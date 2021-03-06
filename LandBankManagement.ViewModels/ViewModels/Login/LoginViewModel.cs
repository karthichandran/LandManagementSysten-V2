﻿using System;
using System.Windows.Input;
using System.Threading.Tasks;

using LandBankManagement.Services;

namespace LandBankManagement.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private bool _progressRingVisibility;
        public bool ProgressRingVisibility
        {
            get => _progressRingVisibility;
            set => Set(ref _progressRingVisibility, value);
        }

        private bool _progressRingActive;
        public bool ProgressRingActive
        {
            get => _progressRingActive;
            set => Set(ref _progressRingActive, value);
        }
        public LoginViewModel(ILoginService loginService, ISettingsService settingsService, ICommonServices commonServices) : base(commonServices)
        {
            LoginService = loginService;
            SettingsService = settingsService;
        }

        public ILoginService LoginService { get; }
        public ISettingsService SettingsService { get; }

        private ShellArgs ViewModelArgs { get; set; }

        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { Set(ref _isBusy, value); }
        }

        private bool _isLoginWithPassword = false;
        public bool IsLoginWithPassword
        {
            get { return _isLoginWithPassword; }
            set { Set(ref _isLoginWithPassword, value); }
        }

        private bool _isLoginWithWindowsHello = false;
        public bool IsLoginWithWindowsHello
        {
            get { return _isLoginWithWindowsHello; }
            set { Set(ref _isLoginWithWindowsHello, value); }
        }

        private string _userName = null;
        public string UserName
        {
            get { return _userName; }
            set { Set(ref _userName, value); }
        }

        private string _password = "";
        public string Password
        {
            get { return _password; }
            set { Set(ref _password, value); }
        }

        public void ShowProgressRing()
        {
            ProgressRingActive = true;
            ProgressRingVisibility = true;
        }
        public void HideProgressRing()
        {
            ProgressRingActive = false;
            ProgressRingVisibility = false;
        }

        public ICommand ShowLoginWithPasswordCommand => new RelayCommand(ShowLoginWithPassword);
        public ICommand LoginWithPasswordCommand => new RelayCommand(LoginWithPassword);
        public ICommand LoginWithWindowHelloCommand => new RelayCommand(LoginWithWindowHello);
        private bool loginProcessStarted = false;
        public Task LoadAsync(ShellArgs args)
        {
            ViewModelArgs = args;

           // UserName = args.UserInfo.UserName;
            UserName = args.UserInfo.loginName;
            IsLoginWithWindowsHello = LoginService.IsWindowsHelloEnabled(UserName);
            IsLoginWithPassword = !IsLoginWithWindowsHello;
            IsBusy = false;

            return Task.CompletedTask;
        }

        public void Login()
        {
            
            if (IsLoginWithPassword)
            {
                LoginWithPassword();
            }
            else
            {
                LoginWithWindowHello();
            }
        }

        private void ShowLoginWithPassword()
        {
            IsLoginWithWindowsHello = false;
            IsLoginWithPassword = true;
        }

        public async void LoginWithPassword()
        {
            try
            {
                if (loginProcessStarted)
                    return;
                ShowProgressRing();
                loginProcessStarted = true;
                IsBusy = true;

                var result = ValidateInput();
                if (result.IsOk)
                {
                    result = await LoginService.SignInWithPasswordAsync(UserName, Password);
                    if (result.IsOk)
                    {
                        EnterApplication();
                        loginProcessStarted = false;
                        return;
                    }

                }
                loginProcessStarted = false;
                await DialogService.ShowAsync(result.Message, result.Description);
                IsBusy = false;
                HideProgressRing();
            }
            catch (Exception ex) {
                await DialogService.ShowAsync("Error", ex.Message +" --- stack trace :"+ex.StackTrace);
            }
        }

        public async void LoginWithWindowHello()
        {
            IsBusy = true;
            ShowProgressRing();
            var result = await LoginService.SignInWithWindowsHelloAsync();
            if (result.IsOk)
            {
                EnterApplication();
                return;
            }
            await DialogService.ShowAsync(result.Message, result.Description);
            IsBusy = false;
            HideProgressRing();
        }

        private void EnterApplication()
        {
            ViewModelArgs.UserInfo = LoginService.UserInfo;
            NavigationService.Navigate<MainShellViewModel>(ViewModelArgs);
        }

        private Result ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
            {
                return Result.Error("Login error", "Please, enter valid credentials.");
            }

            return Result.Ok();
        }
    }
}
