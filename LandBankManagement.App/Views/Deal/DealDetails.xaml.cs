﻿using LandBankManagement.Models;
using LandBankManagement.ViewModels;
using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
namespace LandBankManagement.Views
{
    public sealed partial class DealDetails : UserControl
    {
        public DealDetails()
        {
            this.InitializeComponent();
        }

        public DealDetailsViewModel ViewModel
        {
            get { return (DealDetailsViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel), typeof(DealDetailsViewModel), typeof(DealDetails), new PropertyMetadata(null));



        private void partySearch_Click(object sender, RoutedEventArgs e)
        {
            var stackpanel = (StackPanel)((Button)sender).Parent; 
            var grid = (Grid)stackpanel.Parent;
            ViewModel.SetPopupWidth = grid.ActualWidth;
            ViewModel.GetParties();
        }

        private void AddParty_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PreparePartyList();
        }

        private void Party_Delete_Click(object sender, RoutedEventArgs e)
        {
            var identity = Convert.ToInt32(((Button)sender).Tag.ToString());
            ViewModel.RemoveParty(identity);
        }


        private void Amount_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void AddPayment_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AddPaymentToList();
        }

        private void Delete_payment_Click(object sender, RoutedEventArgs e)
        {
            var identity = Convert.ToInt32(((Button)sender).Tag.ToString());
            ViewModel.DeletePaySchedule(identity);
        }

        private void saleValue_LostFocus(object sender, RoutedEventArgs e)
        {
            ViewModel.CalculateSaleValue();
        }

        private void salevalue2_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            sender.Text = new String(sender.Text.Where(x=>char.IsDigit(x)||x=='.' ).ToArray());
            sender.SelectionStart = sender.Text.Length;
        }

        private void PaymentListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item =(DealPayScheduleModel) ((ListView)sender).SelectedItem;
            ViewModel.CurrentSchedule = item;
        }
    }
}
