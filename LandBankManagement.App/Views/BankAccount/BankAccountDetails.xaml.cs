﻿using LandBankManagement.ViewModels;
using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace LandBankManagement.Views
{
    public sealed partial class BankAccountDetails : UserControl
    {
        public BankAccountDetails()
        {
            this.InitializeComponent();
        }
        public BankAccountDetailsViewModel ViewModel
        {
            get { return (BankAccountDetailsViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel), typeof(BankAccountDetailsViewModel), typeof(CashAccountDetails), new PropertyMetadata(null));


        public void SetFocus()
        {
            details.SetFocus();
        }

        private void ChangeCompany_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ResetCompanyOption();
        }

        private void accountNo_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            sender.Text = new String(sender.Text.Where(x => char.IsDigit(x)).ToArray());
            sender.SelectionStart = sender.Text.Length;

        }

        private void OpeningBalance_LostFocus(object sender, RoutedEventArgs e)
        {
            var testbox = (TextBox)sender;
            if (testbox.Text == "")
                return;
            var amount = testbox.Text.Replace(',', ' ').Replace(" ", "").Trim();
            testbox.Text = Convert.ToDecimal(amount).ToString("N");
        }
    }
}
