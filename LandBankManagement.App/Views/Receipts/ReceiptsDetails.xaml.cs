﻿using LandBankManagement.ViewModels;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace LandBankManagement.Views
{
    public sealed partial class ReceiptsDetails : UserControl
    {
        public ReceiptsDetails()
        {
            this.InitializeComponent();
        }
        public ReceiptsDetailsViewModel ViewModel
        {
            get { return (ReceiptsDetailsViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel), typeof(ReceiptsDetailsViewModel), typeof(ReceiptsDetails), new PropertyMetadata(null));


        public void SetFocus()
        {
            details.SetFocus();
        }

        private async void DealPartiesDDl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var val = ((ComboBox)sender).SelectedValue;
            if (val == null)
            {
                if (ViewModel.currentDealId >0) {
                    ((ComboBox)sender).SelectedValue = ViewModel.currentDealId.ToString();
                }
                return;
            }


           await ViewModel.LoadDealParties(Convert.ToInt32(val));
        }

        private async void CompanyDDl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var val = ((ComboBox)sender).SelectedValue;
            if (val == null)
                return;
            await ViewModel.LoadDealsByCompany();
            await ViewModel.LoadBankAndCompany();
        }

        private void Amount_LostFocus(object sender, RoutedEventArgs e)
        {
            var testbox = (TextBox)sender;
            if (testbox.Text == "")
                return;
            var amount = testbox.Text.Replace(',', ' ').Replace(" ", "").Trim();
            testbox.Text = Convert.ToDecimal(amount).ToString("N");
        }
    }
}
