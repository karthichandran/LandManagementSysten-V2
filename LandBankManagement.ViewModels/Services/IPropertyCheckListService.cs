﻿
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using LandBankManagement.Data;
using LandBankManagement.Models;

namespace LandBankManagement.Services
{
   public interface IPropertyCheckListService
    {
        Task<int> AddPropertyCheckListAsync(PropertyCheckListModel model);
        Task<PropertyCheckListModel> GetPropertyCheckListAsync(long id);
        Task<IList<PropertyCheckListModel>> GetPropertyCheckListAsync(DataRequest<PropertyCheckList> request);
        Task<IList<PropertyCheckListModel>> GetPropertyCheckListAsync(int skip, int take, DataRequest<PropertyCheckList> request);
        Task<int> GetPropertyCheckListCountAsync(DataRequest<PropertyCheckList> request);
        Task<int> UpdatePropertyCheckListAsync(PropertyCheckListModel model);
        Task<int> DeletePropertyCheckListAsync(PropertyCheckListModel model);
        Task<int> DeletePropertyVendorAsync(int propertyVenderId);
        Task<int> DeletePropertyDocumentAsync(PropertyCheckListDocumentsModel documents);
        List<PropertyCheckListVendorModel> GetPropertyCheckListVendors(int id);
        Task<int> UpdatePropertyCheckListStatusAsync(int id, int status, string remarks);
        Task<int> SaveDocuments(ICollection<PropertyCheckListDocumentsModel> docs, Guid propertyGuid);
        Task<ObservableCollection<PropertyCheckListDocumentsModel>> GetDocuments(Guid propertyGuid);
        Task<ObservableCollection<CheckListOfPropertyModel>> GetCheckListOfProperty(int id);
        Task<int> DeleteCheckListOfPropertyAsync(int checkListPropertyId);
        void StoreItems(PropertyCheckListContainer data);
        PropertyCheckListContainer GetStoredItems();
        void AddVendor(PropertyCheckListVendorModel propertyVendorModel);
    }
}
