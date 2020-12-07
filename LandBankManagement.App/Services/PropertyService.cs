﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LandBankManagement.Data;
using LandBankManagement.Data.Services;
using LandBankManagement.Models;
using LandBankManagement.Services.VirtualCollections;

namespace LandBankManagement.Services
{
    public class PropertyService : IPropertyService
    {

        public IDataServiceFactory DataServiceFactory { get; }
        public ILogService LogService { get; }

        public PropertyService(IDataServiceFactory dataServiceFactory, ILogService logService)
        {
            DataServiceFactory = dataServiceFactory;
            LogService = logService;
        }

        public async Task<PropertyModel> AddPropertyAsync(PropertyModel model)
        {
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var property = new Property();
                if (property != null)
                {
                    UpdatePropertyFromModel(property, model);
                    property.PropertyGuid = Guid.NewGuid();
                    await dataService.AddPropertyAsync(property);
                    model.Merge(await GetPropertyAsync(dataService, property.PropertyId));
                }
                return model;
            }
        }

        static private async Task<PropertyModel> GetPropertyAsync(IDataService dataService, long id)
        {
            var item = await dataService.GetPropertyAsync(id);
            if (item != null)
            {
                return await CreatePropertyModelAsync(item, includeAllFields: true);
            }
            return null;
        }

        public async Task<PropertyModel> GetPropertyAsync(long id)
        {
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                return await GetPropertyAsync(dataService, id);
            }
        }

        public async Task<IList<PropertyModel>> GetPropertiesAsync(DataRequest<Property> request)
        {
            var collection = new PropertyCollection(this, LogService);
            await collection.LoadAsync(request);
            return collection;
        }

        public async Task<IList<PropertyModel>> GetPropertiesAsync(int skip, int take, DataRequest<Property> request)
        {
            var models = new List<PropertyModel>();
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var items = await dataService.GetPropertiesAsync(skip, take, request);
                foreach (var item in items)
                {
                    models.Add(await CreatePropertyModelAsync(item, includeAllFields: false));
                }
                return models;
            }
        }

        public async Task<int> GetPropertiesCountAsync(DataRequest<Property> request)
        {
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                return await dataService.GetPropertiesCountAsync(request);
            }
        }

        public async Task<PropertyModel> UpdatePropertyAsync(PropertyModel model)
        {
            long id = model.PropertyId;
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                var property = id > 0 ? await dataService.GetPropertyAsync(model.PropertyId) : new Property();
                if (property != null)
                {
                    UpdatePropertyFromModel(property, model);
                    await dataService.UpdatePropertyAsync(property);
                    model.Merge(await GetPropertyAsync(dataService, property.PropertyId));
                }
                return model;
            }
        }

        public async Task<int> DeletePropertyAsync(PropertyModel model)
        {
            var property = new Property { PropertyId = model.PropertyId };
            using (var dataService = DataServiceFactory.CreateDataService())
            {
                return await dataService.DeletePropertyAsync(property);
            }
        }

        static public async Task<PropertyModel> CreatePropertyModelAsync(Property source, bool includeAllFields)
        {
            var model = new PropertyModel()
            {
                PropertyId = source.PropertyId,
                PropertyGuid = source.PropertyGuid,
                PropertyName = source.PropertyName,
                PartyId = source.PartyId,
                TalukId = source.TalukId,
                HobliId = source.HobliId,
                VillageId = source.VillageId,
                DocumentTypeId = source.DocumentTypeId,
                DateOfExecution = source.DateOfExecution,
                DocumentNo = source.DocumentNo,
                PropertyTypeId = source.PropertyTypeId,
                SurveyNo = source.SurveyNo,
                PropertyGMapLink = source.PropertyGMapLink,
                LandAreaInputAcres = source.LandAreaInputAcres,
                LandAreaInputGuntas = source.LandAreaInputGuntas,
                LandAreaInAcres = source.LandAreaInAcres,
                LandAreaInGuntas = source.LandAreaInGuntas,
                LandAreaInSqMts = source.LandAreaInSqMts,
                LandAreaInSqft = source.LandAreaInSqft,
                AKarabAreaInputAcres = source.AKarabAreaInputAcres,
                AKarabAreaInputGuntas = source.AKarabAreaInputGuntas,
                AKarabAreaInAcres = source.AKarabAreaInAcres,
                AKarabAreaInGuntas = source.AKarabAreaInGuntas,
                AKarabAreaInSqMts = source.AKarabAreaInSqMts,
                AKarabAreaInSqft = source.AKarabAreaInSqft,
                BKarabAreaInputAcres = source.BKarabAreaInputAcres,
                BKarabAreaInputGuntas = source.BKarabAreaInputGuntas,
                BKarabAreaInAcres = source.BKarabAreaInAcres,
                BKarabAreaInGuntas = source.BKarabAreaInGuntas,
                BKarabAreaInSqMts = source.BKarabAreaInSqMts,
                BKarabAreaInSqft = source.BKarabAreaInSqft,
                SaleValue1 = source.SaleValue1,
                SaleValue2 = source.SaleValue2
            };
            return model;
        }

        private void UpdatePropertyFromModel(Property target, PropertyModel source)
        {
            target.PropertyId = source.PropertyId;
            target.PropertyGuid = source.PropertyGuid;
            target.PropertyName = source.PropertyName;
            target.PartyId = source.PartyId;
            target.TalukId = source.TalukId;
            target.HobliId = source.HobliId;
            target.VillageId = source.VillageId;
            target.DocumentTypeId = source.DocumentTypeId;
            target.DateOfExecution = source.DateOfExecution;
            target.DocumentNo = source.DocumentNo;
            target.PropertyTypeId = source.PropertyTypeId;
            target.SurveyNo = source.SurveyNo;
            target.PropertyGMapLink = source.PropertyGMapLink;
            target.LandAreaInputAcres = source.LandAreaInputAcres;
            target.LandAreaInputGuntas = source.LandAreaInputGuntas;
            target.LandAreaInAcres = source.LandAreaInAcres;
            target.LandAreaInGuntas = source.LandAreaInGuntas;
            target.LandAreaInSqMts = source.LandAreaInSqMts;
            target.LandAreaInSqft = source.LandAreaInSqft;
            target.AKarabAreaInputAcres = source.AKarabAreaInputAcres;
            target.AKarabAreaInputGuntas = source.AKarabAreaInputGuntas;
            target.AKarabAreaInAcres = source.AKarabAreaInAcres;
            target.AKarabAreaInGuntas = source.AKarabAreaInGuntas;
            target.AKarabAreaInSqMts = source.AKarabAreaInSqMts;
            target.AKarabAreaInSqft = source.AKarabAreaInSqft;
            target.BKarabAreaInputAcres = source.BKarabAreaInputAcres;
            target.BKarabAreaInputGuntas = source.BKarabAreaInputGuntas;
            target.BKarabAreaInAcres = source.BKarabAreaInAcres;
            target.BKarabAreaInGuntas = source.BKarabAreaInGuntas;
            target.BKarabAreaInSqMts = source.BKarabAreaInSqMts;
            target.BKarabAreaInSqft = source.BKarabAreaInSqft;
            target.SaleValue1 = source.SaleValue1;
            target.SaleValue2 = source.SaleValue2;
        }
    }
}
