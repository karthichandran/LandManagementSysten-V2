using LandBankManagement.ViewModels;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace LandBankManagement.Views
{
    public sealed partial class FundTransferDetails : UserControl
    {
        public FundTransferDetails()
        {
            this.InitializeComponent();
        }
        public FundTransferDetailsViewModel ViewModel
        {
            get { return (FundTransferDetailsViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel), typeof(FundTransferDetailsViewModel), typeof(FundTransferDetails), new PropertyMetadata(null));


        public void SetFocus()
        {
            details.SetFocus();
        }

        private async void FromCompanyDDL_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var val = ((ComboBox)sender).SelectedValue;
            if (val == null|| val=="0")
                return;
           
            await ViewModel.LoadFromBankAndCompany();

        }

        private async void ToCompanyDDl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var val = ((ComboBox)sender).SelectedValue;
            if (val == null || val == "0")
                return;
            await ViewModel.LoadToBankAndCompany();
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
