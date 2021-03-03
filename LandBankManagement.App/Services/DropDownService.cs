﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using IronSnappy;
using LandBankManagement.Data;
using LandBankManagement.Data.Services;
using LandBankManagement.Models;

namespace LandBankManagement.Services
{
    public class DropDownService:IDropDownService
    {
        public IDataServiceFactory DataServiceFactory { get; }
        public ILogService LogService { get; }

        public DropDownService(IDataServiceFactory dataServiceFactory, ILogService logService)
        {
            DataServiceFactory = dataServiceFactory;
            LogService = logService;
        }
        public async Task<ObservableCollection<ComboBoxOptions>> GetHobliOptions()
        {
            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models =await dataService.GetHobliOptions();
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id =obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id="0", Description = "" });
                return list;
            }
        }

        public async Task<ObservableCollection<ComboBoxOptions>> GetAllHobliOptions()
        {
            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models = await dataService.GetAllHobliOptions();
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id = obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id = "0", Description = "" });
                return list;
            }
        }

        public async Task<ObservableCollection<ComboBoxOptions>> GetHobliOptionsByTaluk(int talukId)
        {
            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models =await dataService.GetHobliOptionsByTaluk(talukId);
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id =obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id="0", Description = "" });
                return list;
            }
        }

        public async Task<ObservableCollection<ComboBoxOptions>> GetTalukOptions()
        {
            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models =await dataService.GetTalukOptions();
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id =obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id="0", Description = "" });
                return list;
            }
        }
        public async Task<ObservableCollection<ComboBoxOptions>> GetAllTalukOptions()
        {
            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models =await dataService.GetAllTalukOptions();
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id =obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id="0", Description = "" });
                return list;
            }
        }
        public async Task<ObservableCollection<ComboBoxOptions>> GetVillageOptions()
        {
            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models =await dataService.GetVillageOptions();
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id =obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id="0", Description = "" });
                return list;
            }

        }
        public async Task<ObservableCollection<ComboBoxOptions>> GetAllVillageOptions()
        {
            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models = await dataService.GetAllVillageOptions();
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id = obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id = "0", Description = "" });
                return list;
            }

        }
        public async Task<ObservableCollection<ComboBoxOptions>> GetVillageOptionsByHobli(int hobliId)
        {
            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models =await dataService.GetVillageOptionsByHobli(hobliId);
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id =obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id="0", Description = "" });
                return list;
            }

        }
        public async Task<ObservableCollection<ComboBoxOptions>> GetCompanyOptions()
        {
            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models =await dataService.GetCompanyOptions();
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id =obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id="0", Description = "" });
                return list;
            }
        }
         public async Task<ObservableCollection<ComboBoxOptions>> GetAllCompanyOptions()
        {
            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models =await dataService.GetAllCompanyOptions();
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id =obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id="0", Description = "" });
                return list;
            }
        }

        public async Task<ObservableCollection<ComboBoxOptions>> GetAccountTypeOptions()
        {
            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models =await dataService.GetAccountTypeOptions();
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id =obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id="0", Description = "" });
                return list;
            }
        }

        public async Task<ObservableCollection<ComboBoxOptions>> GetExpenseHeadOptions()
        {
            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models =await dataService.GetExpenseHeadOptions();
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id =obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id="0", Description = "" });
                return list;
            }
        }
        public async Task<ObservableCollection<ComboBoxOptions>> GetPartyOptions()
        {
            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models =await dataService.GetPartyOptions();
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id =obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id="0", Description = "" });
                return list;
            }
        }

        public async Task<ObservableCollection<ComboBoxOptions>> GetDocumentTypeOptions()
        {
            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models =await dataService.GetDocumentTypeOptions();
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id =obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id="0", Description = "" });
                return list;
            }
        }

        public async Task<ObservableCollection<ComboBoxOptions>> GetAllDocumentTypeOptions()
        {
            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models = await dataService.GetAllDocumentTypeOptions();
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id = obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id = "0", Description = "" });
                return list;
            }
        }

        public async Task<ObservableCollection<ComboBoxOptions>> GetPropertyOptions()
        {
            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models =await dataService.GetPropertyOptions();
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id =obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id="0", Description = "" });
                return list;
            }
        }

        public async Task<ObservableCollection<ComboBoxOptions>> GetUnSoldPropertyOptions()
        {
            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models = await dataService.GetUnSoldPropertyOptions();
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id =obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id="0", Description = "" });
                return list;
            }
        }

        public async Task<ObservableCollection<ComboBoxOptions>> GetCashOptions()
        {
            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models =await dataService.GetCashOptions();
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id =obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id="0", Description = "" });
                return list;
            }
        }
        public async Task<ObservableCollection<ComboBoxOptions>> GetCashOptionsByCompany(int companyId)
        {
            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models =await dataService.GetCashOptionsByCompany(companyId);
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id =obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id="0", Description = "" });
                return list;
            }
        }

        public async Task<ObservableCollection<ComboBoxOptions>> GetBankOptions()
        {
            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models =await dataService.GetBankOptions();
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id =obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id="0", Description = "" });
                return list;
            }
        } 
        public async Task<ObservableCollection<ComboBoxOptions>> GetBankOptionsByCompany(int companyId)
        {
            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models =await dataService.GetBankOptionsByCompany(companyId);
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id =obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id="0", Description = "" });
                return list;
            }
        }

        public async Task<ObservableCollection<ComboBoxOptions>> GetVendorOptions()
        {
            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models =await dataService.GetVendorOptions();
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id =obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id="0", Description = "" });
                return list;
            }
        }
        public ObservableCollection<ComboBoxOptions> GetReportingToOptions()
        {
            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models = dataService.GetReportingToOptions();
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id =obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id="0", Description = "" });
                return list;
            }
        }
        public ObservableCollection<ComboBoxOptions> GetGenderOptions()
        {
            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models = dataService.GetGenderOptions();
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id =obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id="0", Description = "" });
                return list;
            }
        }

        public async Task<ObservableCollection<ComboBoxOptions>> GetPartyOptions(string party) {

            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models =await dataService.GetPartyOptions(party);
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id =obj.Id.ToString(),
                        Description = obj.Description
                    });
                }
                return list;
            }
        }

        public async Task<ObservableCollection<ComboBoxOptions>> GetPartyOptionsByProperty(int propertyId)
        {

            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models = await dataService.GetPartyOptionsByProperty(propertyId);
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id = obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id = "0", Description = "" });
                return list;
            }
        }
        public async Task<ObservableCollection<ComboBoxOptions>> GetPartyOptionsByGroup(int groupId)
        {

            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models = await dataService.GetPartyOptionsByGroup(groupId);
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id = obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id = "0", Description = "" });
                return list;
            }
        }

        
        public async Task<ObservableCollection<ComboBoxOptions>> GetPropertyTypeOptions()
        {

            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models =await dataService.GetPropertyTypeOptions();
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id =obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id="0", Description = "" });
                return list;
            }
        }

        public async Task<ObservableCollection<ComboBoxOptions>> GetRoleOptions() {
            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models =await dataService.GetRoleOptions();
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id =obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id="0", Description = "" });
                return list;
            }
        }

        public async Task<ObservableCollection<ComboBoxOptions>> GetVendorOptions(string vendor)
        {

            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models =await dataService.GetVendorOptions(vendor);
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id =obj.Id.ToString(),
                        Description = obj.Description
                    });
                }
                return list;
            }
        }

        public async Task<ObservableCollection<ComboBoxOptions>> GetCheckListOptions()
        {
            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models =await dataService.GetCheckListOptions();
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id =obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                return list;
            }
        }

        public async Task<ObservableCollection<ComboBoxOptions>> GetPropertyCheckListOptions()
        {
            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models = await dataService.GetPropertyCheckListOptions();
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id =obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                return list;
            }
        }

        public async Task<ObservableCollection<ComboBoxOptions>> GetPropertyMergeOptions() {
            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models =await dataService.GetPropertyMergeOptions();
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id =obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id="0", Description = "" });
                return list;
            }
        }

        public async Task<ObservableCollection<ComboBoxOptions>> GetPropertyOptionsByCompanyID(int companyId) {
            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models = await dataService.GetPropertyOptionsByCompanyID(companyId);
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id =obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id="0", Description = "" });
                return list;
            }
        }

        public async Task<ObservableCollection<ComboBoxOptions>> GetDealOptions()
        {
            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models = await dataService.GetDealOptions();
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id =obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id="0", Description = "" });
                return list;
            }
        }
        public async Task<ObservableCollection<ComboBoxOptions>> GetDocumentTypesByPropertyID(int propertyId)
        {
            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models = await dataService.GetDocumentTypesByPropertyID(propertyId);
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id =obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id="0", Description = "" });
                return list;
            }
        }
        public async Task<ObservableCollection<ComboBoxOptions>> GetDealPartiesOptions(int dealId)
        {
            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models = await dataService.GetDealPartiesOptions(dealId);
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id =obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id="0", Description = "" });
                return list;
            }
        }

        public  ObservableCollection<ComboBoxOptions> GetSalutationOptions()
        {
            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models =  dataService.GetSalutationOptions();
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id =obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id="0", Description = "" });
                return list;
            }
        }

        public ObservableCollection<ComboBoxOptions> GetGroupsTypeOptions()
        {
            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models = dataService.GetGroupsTypeOptions();
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id = obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id = "0", Description = "" });
                return list;
            }
        }
        public async Task<ObservableCollection<ComboBoxOptions>> GetGroupsOptions()
        {
            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models = await dataService.GetGroupsOptions();
                if (models == null)
                    return list;
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id = obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id = "0", Description = "" });
                return list;
            }
        }
        public async Task<ObservableCollection<ComboBoxOptions>> GetGroupsOptionsForParty()
        {
            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models = await dataService.GetGroupsOptionsForParty();
                if (models == null)
                    return list;
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id = obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id = "0", Description = "" });
                return list;
            }
        } 
         public async Task<ObservableCollection<ComboBoxOptions>> GetGroupsOptionsByProperty(int propertyId)
        {
            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models = await dataService.GetGroupsOptionsByProperty(propertyId);
                if (models == null)
                    return list;
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id = obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id = "0", Description = "" });
                return list;
            }
        } 
        
        public async Task<ObservableCollection<ComboBoxOptions>> GetGroupsOptionsForVendor()
        {
            ObservableCollection<ComboBoxOptions> list = new ObservableCollection<ComboBoxOptions>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var models = await dataService.GetGroupsOptionsForVendor();
                if (models == null)
                    return list;
                foreach (var obj in models)
                {
                    list.Add(new ComboBoxOptions
                    {
                        Id = obj.Key.ToString(),
                        Description = obj.Value
                    });
                }
                list.Insert(0, new ComboBoxOptions { Id = "0", Description = "" });
                return list;
            }
        }
    }
}
