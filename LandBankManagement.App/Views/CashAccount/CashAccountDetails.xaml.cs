using LandBankManagement.ViewModels;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace LandBankManagement.Views
{
    public sealed partial class CashAccountDetails : UserControl
    {
        public CashAccountDetails()
        {
            this.InitializeComponent();
        }
        public CashAccountDetailsViewModel ViewModel
        {
            get { return (CashAccountDetailsViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel), typeof(CashAccountDetailsViewModel), typeof(CashAccountDetails), new PropertyMetadata(null));


        public void SetFocus()
        {
            details.SetFocus();
        }

        private void ChangeCompany_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ResetCompanyOption();
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
