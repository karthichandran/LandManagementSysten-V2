﻿using LandBankManagement.Services;
using LandBankManagement.ViewModels;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Concurrent;

using Windows.UI.ViewManagement;

namespace LandBankManagement
{
    public class ServiceLocator : IDisposable
    {
        static private readonly ConcurrentDictionary<int, ServiceLocator> _serviceLocators = new ConcurrentDictionary<int, ServiceLocator>();

        static private ServiceProvider _rootServiceProvider = null;

        /// <summary>
        /// Configure all necessary dependencies
        /// </summary>
        /// <param name="serviceCollection"></param>
        static public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ISettingsService, SettingsService>();
            serviceCollection.AddSingleton<IDataServiceFactory, DataServiceFactory>();
            serviceCollection.AddSingleton<ICompanyService, CompanyService>();
            serviceCollection.AddSingleton<IVendorService, VendorService>();
            serviceCollection.AddSingleton<IPartyService, PartyService>();
            serviceCollection.AddSingleton<IExpenseHeadService, ExpenseHeadService>();
            serviceCollection.AddSingleton<ITalukService, TalukService>();
            serviceCollection.AddSingleton<IHobliService, HobliService>();
            serviceCollection.AddSingleton<IVillageService, VillageService>();
            serviceCollection.AddSingleton<ICashAccountService, CashAccountService>();
            serviceCollection.AddSingleton<IDocumentTypeService, DocumentTypeService>();
            serviceCollection.AddSingleton<ICheckListService, CheckListService>();
            serviceCollection.AddSingleton<IPropertyTypeService, PropertyTypeService>();
            serviceCollection.AddSingleton<IBankAccountService, BankAccountService>();
            serviceCollection.AddSingleton<IMessageService, MessageService>();
            serviceCollection.AddSingleton<ILogService, LogService>();
            serviceCollection.AddSingleton<IDialogService, DialogService>();
            serviceCollection.AddSingleton<IFilePickerService, FilePickerService>();
            serviceCollection.AddSingleton<ILoginService, LoginService>();
            serviceCollection.AddSingleton<IDropDownService, DropDownService>();
            serviceCollection.AddSingleton<IPaymentService, PaymentService>();
            serviceCollection.AddSingleton<IFundTransferService, FundTransferService>();
            serviceCollection.AddSingleton<IReceiptService, ReceiptService>();
            serviceCollection.AddSingleton<IRoleService, RoleService>();
            serviceCollection.AddSingleton<IUserService, UserService>();
            serviceCollection.AddSingleton<IUserRoleService, UserRoleService>();
            serviceCollection.AddSingleton<IPropertyService, PropertyService>();
            serviceCollection.AddSingleton<IRolePermissionService, RolePermissionService>();
            serviceCollection.AddSingleton<IPropertyCheckListService, PropertyCheckListService>();
            serviceCollection.AddScoped<IContextService, ContextService>();
            serviceCollection.AddScoped<INavigationService, NavigationService>();
            serviceCollection.AddScoped<ICommonServices, CommonServices>();
            serviceCollection.AddScoped<IPropertyMergeService, PropertyMergeService>();
            serviceCollection.AddScoped<IDealService, DealService>();
            serviceCollection.AddScoped<IGroupsService, GroupsService>();

            serviceCollection.AddTransient<LoginViewModel>();
            serviceCollection.AddTransient<ShellViewModel>();
            serviceCollection.AddTransient<MainShellViewModel>();
            serviceCollection.AddTransient<DashboardViewModel>();

            serviceCollection.AddTransient<CompanyViewModel>();
            serviceCollection.AddTransient<CompanyDetailsViewModel>();

            serviceCollection.AddTransient<VendorViewModel>();
            serviceCollection.AddTransient<VendorDetailsViewModel>();
            serviceCollection.AddTransient<PartyViewModel>();
            serviceCollection.AddTransient<PartyDetailsViewModel>();
            serviceCollection.AddTransient<ExpenseHeadViewModel>();
            serviceCollection.AddTransient<PaymentsViewModel>();

            serviceCollection.AddTransient<TalukViewModel>();
            serviceCollection.AddTransient<TalukDetailsViewModel>();
            serviceCollection.AddTransient<HobliViewModel>();
            serviceCollection.AddTransient<HobliDetailsViewModel>();
            serviceCollection.AddTransient<VillageViewModel>();
            serviceCollection.AddTransient<VillageDetailsViewModel>();
            serviceCollection.AddTransient<CashAccountViewModel>();
            serviceCollection.AddTransient<CashAccountDetailsViewModel>();
            serviceCollection.AddTransient<DocumentTypeViewModel>();
            serviceCollection.AddTransient<DocumentTypeDetailsViewModel>();
            serviceCollection.AddTransient<CheckListViewModel>();
            serviceCollection.AddTransient<CheckListDetailsViewModel>();
            serviceCollection.AddTransient<PropertyTypeViewModel>();
            serviceCollection.AddTransient<PropertyTypeDetailsViewModel>();
            serviceCollection.AddTransient<CompanyReportViewModel>();
            serviceCollection.AddTransient<BankAccountViewModel>();
            serviceCollection.AddTransient<BankAccountDetailsViewModel>();
            serviceCollection.AddTransient<FundTransferViewModel>();
            serviceCollection.AddTransient<FundTransferDetailsViewModel>();
            serviceCollection.AddTransient<ReceiptsViewModel>();
            serviceCollection.AddTransient<ReceiptsDetailsViewModel>();
            serviceCollection.AddTransient<RoleViewModel>();
            serviceCollection.AddTransient<RoleDetailsViewModel>();
            serviceCollection.AddTransient<UserViewModel>();
            serviceCollection.AddTransient<UserDetailsViewModel>();
            serviceCollection.AddTransient<PropertyViewModel>();
            serviceCollection.AddTransient<PropertyDetailsViewModel>();
            serviceCollection.AddTransient<CostDetailsViewModel>();
            serviceCollection.AddTransient<RolePermissionViewModel>();
            serviceCollection.AddTransient<RolePermissionDetailsViewModel>();
            serviceCollection.AddTransient<PropertyCheckListViewModel>();
            serviceCollection.AddTransient<PropertyCheckListListViewModel>();
            serviceCollection.AddTransient<PropertyCheckListDetailsViewModel>();
            serviceCollection.AddTransient<AppLogsViewModel>();
            serviceCollection.AddTransient<PropertyMergeViewModel>();
            serviceCollection.AddTransient<PropertyMergeDetailsViewModel>();
            serviceCollection.AddTransient<DealViewModel>();
            serviceCollection.AddTransient<DealDetailsViewModel>();
            serviceCollection.AddTransient<DealReportViewModel>();
            serviceCollection.AddTransient<SettingsViewModel>();
            serviceCollection.AddTransient<ValidateConnectionViewModel>();
            serviceCollection.AddTransient<CreateDatabaseViewModel>();
            serviceCollection.AddTransient<PropertyCheckListReportViewModel>();
            serviceCollection.AddTransient<GroupsViewModel>();
            serviceCollection.AddTransient<GroupsDetailsViewModel>();

            _rootServiceProvider = serviceCollection.BuildServiceProvider();
        }

        static public ServiceLocator Current
        {
            get
            {
                int currentViewId = ApplicationView.GetForCurrentView().Id;
                return _serviceLocators.GetOrAdd(currentViewId, key => new ServiceLocator());
            }
        }

        static public void DisposeCurrent()
        {
            int currentViewId = ApplicationView.GetForCurrentView().Id;
            if (_serviceLocators.TryRemove(currentViewId, out ServiceLocator current))
            {
                current.Dispose();
            }
        }

        private IServiceScope _serviceScope = null;

        private ServiceLocator()
        {
            _serviceScope = _rootServiceProvider.CreateScope();
        }

        public T GetService<T>()
        {
            return GetService<T>(true);
        }

        public T GetService<T>(bool isRequired)
        {
            if (isRequired)
            {
                return _serviceScope.ServiceProvider.GetRequiredService<T>();
            }
            return _serviceScope.ServiceProvider.GetService<T>();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_serviceScope != null)
                {
                    _serviceScope.Dispose();
                }
            }
        }
    }
}
