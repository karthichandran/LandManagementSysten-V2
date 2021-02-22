using LandBankManagement.Common;
using LandBankManagement.Controls;
using LandBankManagement.Models;
using LandBankManagement.ViewModels;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace LandBankManagement.Views
{
    public sealed partial class CostDetails : UserControl
    {
        public CostDetails()
        {
            this.InitializeComponent();
        }

        public CostDetailsViewModel ViewModel
        {
            get { return (CostDetailsViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register("ViewModel", typeof(CostDetailsViewModel), typeof(CostDetails), new PropertyMetadata(null));

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PropertyListViewModel.PopupOpened = false;
           
        }

        private void AddPayment_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AddPaymentToList();
            TotalAmountTxt1.Text = ViewModel.TotalAmount1;
            TotalAmountTxt2.Text = ViewModel.TotalAmount2;
        }

        private void ClearPayment_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ClearPayment();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SavePaymentSequence();
        }

        private void SaleValue_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ViewModel.Item.SaleValue1) && !string.IsNullOrEmpty(ViewModel.Item.SaleValue1)) {

                var valu = Convert.ToDecimal(ViewModel.Item.SaleValue1) + Convert.ToDecimal(ViewModel.Item.SaleValue2);
                if (valu > 0) {
                    TotalSales.Text = valu.ToString();
                }
            }

        }

        private void treeGrid_SelectionChanged(object sender, Syncfusion.UI.Xaml.Grid.GridSelectionChangedEventArgs e)
        {
            var model = (PaymentScheduleModel)treeGrid.SelectedItem;
            if (model != null)
                ViewModel.SelectedPayment(model.Identity);
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var identity = Convert.ToInt32(((Button)sender).Tag.ToString());
            ViewModel.DeletePayment(identity);
        }
    }
}
