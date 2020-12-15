﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using LandBankManagement.Models;
using LandBankManagement.Services;

namespace LandBankManagement.ViewModels
{
    public class PartyDetailsViewModel : GenericDetailsViewModel<PartyModel>
    {
        private ObservableCollection<ImagePickerResult> _docList = null;
        public ObservableCollection<ImagePickerResult> DocList
        {
            get => _docList;
            set => Set(ref _docList, value);
        }
        private ObservableCollection<ComboBoxOptions> _vendorOptions = null;
        public ObservableCollection<ComboBoxOptions> VendorOptions

        {
            get => _vendorOptions;
            set => Set(ref _vendorOptions, value);
        }

        public IPartyService PartyService { get; }
        public IDropDownService DropDownService { get; }
        public IFilePickerService FilePickerService { get; }
        public PartyListViewModel PartyListViewModel { get; }
        public IVendorService VendorService { get; }
        public PartyDetailsViewModel(IPartyService partyService, IFilePickerService filePickerService, ICommonServices commonServices, PartyListViewModel partyListViewModel,IDropDownService dropDownService,IVendorService vendorService) : base(commonServices)
        {
            PartyService = partyService;
            PartyListViewModel = partyListViewModel;
            FilePickerService = filePickerService;
            DropDownService = dropDownService;
            VendorService = vendorService;
        }

        override public string Title => (Item?.IsNew ?? true) ? "New Party" : TitleEdit;
        public string TitleEdit => Item == null ? "Party" : $"{Item.PartyName}";

        public override bool ItemIsNew => Item?.IsNew ?? true;

        public async Task LoadAsync()
        {
            Item = new PartyModel();
            Item.IsPartyActive = true;
            IsEditMode = true;
            getVendors();
        }

        private void getVendors() {
            VendorOptions = DropDownService.GetVendorOptions();
        }
        public void Unload()
        {
        }

        public async void ClodeVendorDetails(int id) {

            var model =await VendorService.GetVendorAsync(id);
            Item = new PartyModel
            {
                PartyName = model.VendorName,
                PartyAlias = model.VendorAlias,
                PartySalutation = model.VendorSalutation,
                AadharNo = model.AadharNo,
                ContactPerson = model.ContactPerson,
                PAN = model.PAN,
                GSTIN = model.GSTIN,
                email = model.email,
                IsPartyActive = model.IsVendorActive,
                PhoneNo = model.PhoneNo,
                AddressLine1 = model.AddressLine1,
                AddressLine2 = model.AddressLine2,
                City = model.City,
                PinCode = model.PinCode
            };
               
            DocList = model.VendorDocuments;
            if (model.VendorDocuments != null)
            {
                for (int i = 0; i < DocList.Count; i++)
                {
                    DocList[i].blobId = 0;
                    DocList[i].Identity = i + 1;
                }
            }
        }

        public void Subscribe()
        {
            MessageService.Subscribe<PartyDetailsViewModel, PartyModel>(this, OnDetailsMessage);
            MessageService.Subscribe<PartyListViewModel>(this, OnListMessage);
        }
        public void Unsubscribe()
        {
            MessageService.Unsubscribe(this);
        }


        public override void BeginEdit()
        {
            base.BeginEdit();
        }

        public ICommand EditPictureCommand => new RelayCommand(OnEditFile);
        private async void OnEditFile()
        {
            var result = await FilePickerService.OpenImagePickerAsync();
            if (result != null)
            {
                if (DocList == null)
                    DocList = new ObservableCollection<ImagePickerResult>();

                foreach (var file in result)
                {
                    DocList.Add(file);
                }
                for (int i = 0; i < DocList.Count; i++)
                {
                    DocList[i].Identity = i + 1;
                }
            }

        }

        public void DeleteDocument(int id)
        {
            if (id > 0)
            {
                if (DocList[id - 1].blobId > 0)
                {
                    PartyService.DeletePartyDocumentAsync(DocList[id - 1]);
                }
                DocList.RemoveAt(id - 1);
                var newlist = DocList;
                for (int i = 0; i < newlist.Count; i++)
                {
                    newlist[i].Identity = i + 1;
                }
                DocList = null;
                DocList = newlist;
            }
        }

        protected override async Task<bool> SaveItemAsync(PartyModel model)
        {
            try
            {
                StartStatusMessage("Saving Party...");
                
                if (model.PartyId <= 0)
                    await PartyService.AddPartyAsync(model, DocList);
                else
                    await PartyService.UpdatePartyAsync(model, DocList);

                DocList = model.partyDocuments;
                EndStatusMessage("Party saved");
                LogInformation("Party", "Save", "Party saved successfully", $"Party {model.PartyId} '{model.PartyName}' was saved successfully.");
                return true;
            }
            catch (Exception ex)
            {
                StatusError($"Error saving Party: {ex.Message}");
                LogException("Party", "Save", ex);
                return false;
            }
        }
        protected override void ClearItem()
        {
            Item = new PartyModel();
            if (DocList != null)
                DocList.Clear();
        }
        protected override async Task<bool> DeleteItemAsync(PartyModel model)
        {
            try
            {
                StartStatusMessage("Deleting Party...");
                
                await PartyService.DeletePartyAsync(model);
                EndStatusMessage("Party deleted");
                ClearItem();                
                LogWarning("Party", "Delete", "Party deleted", $"Party {model.PartyId} '{model.PartyName}' was deleted.");
                return true;
            }
            catch (Exception ex)
            {
                StatusError($"Error deleting Party: {ex.Message}");
                LogException("Party", "Delete", ex);
                return false;
            }
        }

        protected override async Task<bool> ConfirmDeleteAsync()
        {
            return await DialogService.ShowAsync("Confirm Delete", "Are you sure to delete this Party?", "Ok", "Cancel");
        }

        override protected IEnumerable<IValidationConstraint<PartyModel>> GetValidationConstraints(PartyModel model)
        {
            yield return new RequiredConstraint<PartyModel>("Name", m => m.PartyName);
            //yield return new RequiredConstraint<CompanyModel>("Email", m => m.Email);
            //yield return new RequiredConstraint<CompanyModel>("Phone Number", m => m.PhoneNo);

        }

        /*
         *  Handle external messages
         ****************************************************************/
        private async void OnDetailsMessage(PartyDetailsViewModel sender, string message, PartyModel changed)
        {
            var current = Item;
            if (current != null)
            {
                if (changed != null && changed.PartyId == current?.PartyId)
                {
                    switch (message)
                    {
                        case "ItemChanged":
                            await ContextService.RunAsync(async () =>
                            {
                                try
                                {
                                    var item = await PartyService.GetPartyAsync(current.PartyId);
                                    item = item ?? new PartyModel { PartyId = current.PartyId, IsEmpty = true };
                                    current.Merge(item);
                                    current.NotifyChanges();
                                    NotifyPropertyChanged(nameof(Title));
                                    if (IsEditMode)
                                    {
                                        StatusMessage("WARNING: This Party has been modified externally");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    LogException("Party", "Handle Changes", ex);
                                }
                            });
                            break;
                        case "ItemDeleted":
                            await OnItemDeletedExternally();
                            break;
                    }
                }
            }
        }

        private async void OnListMessage(PartyListViewModel sender, string message, object args)
        {
            var current = Item;
            if (current != null)
            {
                switch (message)
                {
                    case "ItemsDeleted":
                        if (args is IList<PartyModel> deletedModels)
                        {
                            if (deletedModels.Any(r => r.PartyId == current.PartyId))
                            {
                                await OnItemDeletedExternally();
                            }
                        }
                        break;
                    case "ItemRangesDeleted":
                        try
                        {
                            var model = await PartyService.GetPartyAsync(current.PartyId);
                            if (model == null)
                            {
                                await OnItemDeletedExternally();
                            }
                        }
                        catch (Exception ex)
                        {
                            LogException("Party", "Handle Ranges Deleted", ex);
                        }
                        break;
                }
            }
        }

        private async Task OnItemDeletedExternally()
        {
            await ContextService.RunAsync(() =>
            {
                CancelEdit();
                IsEnabled = false;
                StatusMessage("WARNING: This Party has been deleted externally");
            });
        }
    }
}
