using LandBankManagement.Data;
using LandBankManagement.Models;
using LandBankManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LandBankManagement.ViewModels
{
    public class CompanyReportArgs
    {
        static public CompanyReportArgs CreateEmpty() => new CompanyReportArgs { IsEmpty = true };

        public CompanyReportArgs()
        {
            OrderBy = r => r.Name;
        }

        public bool IsEmpty { get; set; }

        public string Query { get; set; }

        public Expression<Func<Data.Company, object>> OrderBy { get; set; }
        public Expression<Func<Data.Company, object>> OrderByDesc { get; set; }
    }
    public class CompanyReportViewModel : GenericListViewModel<CompanyModel>
    {
        public ICompanyService CompanyService { get; }
        public CompanyReportArgs ViewModelArgs { get; private set; }
        public CompanyReportViewModel(ICommonServices commonServices, ICompanyService companyService) : base(commonServices)
        {
            CompanyService = companyService;
        }

        public List<CompanyModel> ReportItems { get; set; }

        protected override void OnDeleteSelection()
        {
            
        }

        protected override void OnNew()
        {
            
        }

        protected override void OnRefresh()
        {
            
        }

        public void Unload()
        {
            ViewModelArgs.Query = Query;
        }

        public void Subscribe()
        {
            MessageService.Subscribe<CompanyReportViewModel>(this, OnMessage);

        }
        public void Unsubscribe()
        {
            MessageService.Unsubscribe(this);
        }

        public CompanyReportArgs CreateArgs()
        {
            return new CompanyReportArgs
            {
                Query = Query,
                OrderBy = ViewModelArgs.OrderBy,
                OrderByDesc = ViewModelArgs.OrderByDesc
            };
        }

        private DataRequest<Company> BuildDataRequest()
        {
            return new DataRequest<Company>()
            {
                Query = Query,
                OrderBy = ViewModelArgs.OrderBy,
                OrderByDesc = ViewModelArgs.OrderByDesc
            };
        }

        public IList<CompanyModel> GetCompanies()
        {
            ViewModelArgs = new CompanyReportArgs();
            IList<CompanyModel> result = GetItemsAsync().Result;
            ReportItems = (List<CompanyModel>)result;
            return result;
        }
        public async Task LoadAsync(CompanyReportArgs args)
        {
            ViewModelArgs = args ?? CompanyReportArgs.CreateEmpty();
            Query = ViewModelArgs.Query;

            StartStatusMessage("Loading Company...");
            if (await RefreshAsync())
            {
                EndStatusMessage("Company loaded");
            }
        }
        private async Task<IList<CompanyModel>> GetItemsAsync()
        {
            if (!ViewModelArgs.IsEmpty)
            {
                DataRequest<Company> request = BuildDataRequest();
                return await CompanyService.GetCompaniesAsync(request);
            }
            return new List<CompanyModel>();
        }

        public async Task<bool> RefreshAsync()
        {
            bool isOk = true;

            Items = null;
            ItemsCount = 0;
            SelectedItem = null;

            try
            {
                Items = await GetItemsAsync();
            }
            catch (Exception ex)
            {
                Items = new List<CompanyModel>();
                StatusError($"Error loading Company: {ex.Message}");
                LogException("Company", "Refresh", ex);
                isOk = false;
            }

            ItemsCount = Items.Count;
            
            NotifyPropertyChanged(nameof(Title));

            return isOk;
        }

        private async void OnMessage(ViewModelBase sender, string message, object args)
        {
            switch (message)
            {
                case "NewItemSaved":
                case "ItemDeleted":
                case "ItemsDeleted":
                case "ItemRangesDeleted":
                    await ContextService.RunAsync(async () =>
                    {
                        await RefreshAsync();
                    });
                    break;
            }
        }

    }
}
