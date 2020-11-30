using LandBankManagement.ReportViewers;
using LandBankManagement.Services;
using LandBankManagement.ViewModels;
using Syncfusion.UI.Xaml.Reports;
using System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LandBankManagement.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CompanyReportView : Page
    {
        public CompanyReportViewModel ViewModel { get; }
        public INavigationService NavigationService { get; }
        public CompanyReportViewer CompanyReportViewer { get; set; }

        public CompanyReportView()
        {
            ViewModel = ServiceLocator.Current.GetService<CompanyReportViewModel>();
            NavigationService = ServiceLocator.Current.GetService<INavigationService>();
            ICompanyService CompanyService = ServiceLocator.Current.GetService<ICompanyService>();
            this.InitializeComponent();

            CompanyReportViewer = new CompanyReportViewer(reportViewer, CompanyService);

            Loaded += ReportParameters_Loaded;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.Subscribe();

            //  await ViewModel.LoadAsync(e.Parameter as CompanyReportArgs);

        }

        async void ReportParameters_Loaded(object sender, RoutedEventArgs e)
        {

            reportViewer.ReportLoaded += reportViewer_ReportLoaded;
            reportViewer.ViewButtonClick += reportViewer_ViewButtonClick;

            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, new DispatchedHandler(() =>
            {
                if (CompanyReportViewer != null)
                {
                    CompanyReportViewer.LoadReport();
                }
            }));

        }

        void reportViewer_ViewButtonClick(object sender, CancelEventArgs args)
        {
            this.CompanyReportViewer.UpdateDataSet();
        }

        void reportViewer_ReportLoaded(object sender, EventArgs e)
        {
            CompanyReportViewer.SetParameter();
            CompanyReportViewer.UpdateDataSet();
        }
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            ViewModel.Unload();
            ViewModel.Unsubscribe();
        }
    }
}
