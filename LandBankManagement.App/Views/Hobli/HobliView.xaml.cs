﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using LandBankManagement.ViewModels;
using LandBankManagement.Services;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LandBankManagement.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HobliView : Page
    {
        public HobliViewModel ViewModel { get; }
        public INavigationService NavigationService { get; }
        public HobliView()
        {
            ViewModel = ServiceLocator.Current.GetService<HobliViewModel>();
            NavigationService = ServiceLocator.Current.GetService<INavigationService>();
            this.InitializeComponent();
            ViewModel.HobliDetials.IsEditMode = true;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.Subscribe();
            await ViewModel.LoadAsync(e.Parameter as HobliListArgs);
            ViewModel.HobliDetials.Load();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            ViewModel.Unload();
            ViewModel.Unsubscribe();
        }

        private async void OpenInNewView(object sender, RoutedEventArgs e)
        {
            //await NavigationService.CreateNewViewAsync<PartiesViewModel>(ViewModel.PartyList.CreateArgs());
        }

        private async void OpenDetailsInNewView(object sender, RoutedEventArgs e)
        {
            //ViewModel.PartyDetails.CancelEdit();

            //await NavigationService.CreateNewViewAsync<VendorDetailsViewModel>(ViewModel.PartyDetails.CreateArgs());

        }

        public int GetRowSpan(bool isMultipleSelection)
        {
            return isMultipleSelection ? 2 : 1;
        }

    }
}
