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
    public sealed partial class RolePermissionView : Page
    {
        public RolePermissionViewModel ViewModel { get; }
        public INavigationService NavigationService { get; }
        public RolePermissionView()
        {
            ViewModel = ServiceLocator.Current.GetService<RolePermissionViewModel>();
            NavigationService = ServiceLocator.Current.GetService<INavigationService>();
            this.InitializeComponent();          
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.Subscribe();
            await ViewModel.RolePermissionDetials.LoadAsync();
            
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
