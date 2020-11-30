using LandBankManagement.Infrastructure;
using LandBankManagement.Models;
using LandBankManagement.Services;
using Syncfusion.UI.Xaml.Reports;
using System.Collections.Generic;

namespace LandBankManagement.ReportViewers
{
    public class CompanyReportViewer : ReportViewerHelper
    {
        private readonly ICompanyService _companyService;
        public CompanyReportViewer(SfReportViewer reportViewerControl, ICompanyService companyService)
        {
            ReportViewer = reportViewerControl;
            ReportName = "CompanyReport";
            _companyService = companyService;
        }

        public override void UpdateDataSet()
        {
            var companyModels = _companyService.GetCompaniesAsync().Result;
            ReportViewer.DataSources.Clear();
            ReportViewer.DataSources.Add(new ReportDataSource { Name = "DataSet1", Value = companyModels });
        }
    }
}
