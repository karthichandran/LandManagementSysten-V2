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
    
    public class DealDetailsViewModel : GenericDetailsViewModel<DealModel>
    {
        public IDropDownService DropDownService { get; }
        public IDealService DealService { get; }
        public IFilePickerService FilePickerService { get; }

        private ObservableCollection<ComboBoxOptions> _companyOptions = null;
        public ObservableCollection<ComboBoxOptions> CompanyOptions
        {
            get => _companyOptions;
            set => Set(ref _companyOptions, value);
        }

        private ObservableCollection<ComboBoxOptions> _propertyMergeOptions = null;
        public ObservableCollection<ComboBoxOptions> PropertyMergeOptions
        {
            get => _propertyMergeOptions;
            set => Set(ref _propertyMergeOptions, value);
        }


        private ObservableCollection<ComboBoxOptions> _partyOptions = null;
        public ObservableCollection<ComboBoxOptions> PartyOptions
        {
            get => _partyOptions;
            set => Set(ref _partyOptions, value);
        }

        public string _partySearchQuery = null;
        public string PartySearchQuery
        {
            get => _partySearchQuery;
            set => Set(ref _partySearchQuery, value);
        }

        private ObservableCollection<DealPartiesModel> _dealPartiesOptions = null;
        public ObservableCollection<DealPartiesModel> DealPartyList
        {
            get => _dealPartiesOptions;
            set => Set(ref _dealPartiesOptions, value);
        }

        private ObservableCollection<DealPayScheduleModel> _ScheduleList = null;
        public ObservableCollection<DealPayScheduleModel> ScheduleList
        {
            get => _ScheduleList;
            set => Set(ref _ScheduleList, value);
        }

        private DealPayScheduleModel _currentScheduleOptions = null;
        public DealPayScheduleModel CurrentSchedule
        {
            get => _currentScheduleOptions;
            set => Set(ref _currentScheduleOptions, value);
        }


        private int _companyId;
        public int selectedCompany
        {
            get => _companyId;
            set => Set(ref _companyId, value);
        }

        private int _propertyId;
        public int selectedProperty
        {
            get => _propertyId;
            set => Set(ref _propertyId, value);
        }

        private string _finalAmount = null;
        public string FinalAmount
        {
            get => _finalAmount;
            set => Set(ref _finalAmount, value);
        }
        private string _totalAmount1 = null;
        public string TotalAmount1
        {
            get => _totalAmount1;
            set => Set(ref _totalAmount1, value);
        }
        private string _totalAmount2 = null;
        public string TotalAmount2
        {
            get => _totalAmount2;
            set => Set(ref _totalAmount2, value);
        }

        private string _sale1 = null;
        public string Sale1
        {
            get => _sale1;
            set => Set(ref _sale1, value);
        }
        private string _sale2 = null;
        public string Sale2
        {
            get => _sale2;
            set => Set(ref _sale2, value);
        }
        private string _saleTotal = null;
        public string SaleTotal
        {
            get => _saleTotal;
            set => Set(ref _saleTotal, value);
        }
        private bool _popupOpened = false;
        public bool PopupOpened
        {
            get => _popupOpened;
            set => Set(ref _popupOpened, value);
        }

        private bool _noRecords = true;
        public bool NoRecords
        {
            get => _noRecords;
            set => Set(ref _noRecords, value);
        }

        private bool _showParties = false;
        public bool ShowParties
        {
            get => _showParties;
            set => Set(ref _showParties, value);
        }

        private double _setPopupWidth;
        public double SetPopupWidth
        {
            get => _setPopupWidth;
            set => Set(ref _setPopupWidth, value);
        }

        private DealViewModel DealsViewModel { get; set; }
        private bool IsProcessing = false;
        public DealDetailsViewModel(IDropDownService dropDownService, IDealService dealService, IFilePickerService filePickerService, ICommonServices commonServices, DealViewModel dealViewModel) : base(commonServices)
        {
            DropDownService = dropDownService;
            FilePickerService = filePickerService;
            DealService = dealService;
            DealsViewModel = dealViewModel;
        }

        override public string Title => (Item?.IsNew ?? true) ? "New Deals" : TitleEdit;
        public string TitleEdit = "Deals";

        public override bool ItemIsNew => Item?.IsNew ?? true;

        // public ExpenseHeadDetailsArgs ViewModelArgs { get; private set; }

        public async Task LoadAsync()
        {
            Item = new DealModel { PropertyMergeId="0",CompanyId="0"};
            GetDropdowns();
            IsEditMode = true;
            CurrentSchedule = new DealPayScheduleModel { ScheduleDate=DateTimeOffset.Now};
        }

        private async void GetDropdowns()
        {
            DealsViewModel.ShowProgressRing();
            CompanyOptions = await DropDownService.GetCompanyOptions();
            PropertyMergeOptions =await DropDownService.GetPropertyMergeOptions();
            DealsViewModel.HideProgressRing();
        }
        public async void GetParties()
        {
            DealsViewModel.ShowProgressRing();

            PartyOptions =await DropDownService.GetPartyOptions(PartySearchQuery);
            if (PartyOptions == null || PartyOptions.Count == 0)
            {
                ShowParties = false;
                NoRecords = true;
            }
            else
            {
                ShowParties = true;
                NoRecords = false;
            }
            PopupOpened = true;
            DealsViewModel.HideProgressRing();
        }

        public void PreparePartyList()
        {
            PopupOpened = false;
            if (PartyOptions == null)
                return;

            foreach (var item in PartyOptions)
            {
                if (item.IsSelected)
                {
                    if (DealPartyList == null)
                        DealPartyList = new ObservableCollection<DealPartiesModel>();
                    DealPartyList.Add(new DealPartiesModel
                    {
                        PartyId = Convert.ToInt32(item.Id),
                        PartyName = item.Description,
                        IsGroup = item.Description.Contains("(G)") ? true : false
                    }) ;
                }
            }
            PartySearchQuery = "";
        }

        public async void RemoveParty(int id)
        {
            var model = DealPartyList.First(x => x.PartyId == id);
            if (model.DealPartyId > 0)
            {
                DealsViewModel.ShowProgressRing();
                await DealService.DeleteDealPartiesAsync(model.DealPartyId);
                DealPartyList = await DealService.GetDealParties(Item.DealId);
                DealsViewModel.HideProgressRing();
            }
            else
                DealPartyList.Remove(model);
        }

        public void CalculateSaleValue() {
            try
            {
                Sale1 = string.IsNullOrEmpty(Sale1) ? "0" : Sale1;
                Sale2 = string.IsNullOrEmpty(Sale2) ? "0" : Sale2;
                Item.SaleValue1 = Convert.ToDecimal(Sale1).ToString("N");
                Item.SaleValue2 =  Convert.ToDecimal(Sale2).ToString("N");
                SaleTotal = (Convert.ToDecimal(Sale1) + Convert.ToDecimal(Sale2)).ToString("N");
            }
            catch (Exception ex) {
                DialogService.ShowAsync("", "Please enter the correct sale value","OK");
            }
        }
        private void CalculateTotalAMounts()
        {
            decimal totalAmt1 = 0;
            decimal totalAmt2 = 0;
            foreach (var model in ScheduleList)
            {
                totalAmt1 += Convert.ToDecimal(model.Amount1);
                totalAmt2 += Convert.ToDecimal(model.Amount2);
            }
            FinalAmount = (totalAmt1 + totalAmt2).ToString("N");
            TotalAmount1 = totalAmt1.ToString("N");
            TotalAmount2 = totalAmt2.ToString("N");
        }
        public void AddPaymentToList()
        {
            if (CurrentSchedule == null)
                return;

            if (ScheduleList == null)
                ScheduleList = new ObservableCollection<DealPayScheduleModel>();


            CurrentSchedule.Amount1 = string.IsNullOrEmpty(CurrentSchedule.Amount1) ? "0" : Convert.ToDecimal(CurrentSchedule.Amount1).ToString("N");
            CurrentSchedule.Amount2 = string.IsNullOrEmpty(CurrentSchedule.Amount2) ? "0" : Convert.ToDecimal(CurrentSchedule.Amount2).ToString("N");
           var total = Convert.ToDecimal(CurrentSchedule.Amount1) + Convert.ToDecimal(CurrentSchedule.Amount2);
            if (total <= 0)
                return;

            if (CurrentSchedule.Identity <= 0)
            {
                CurrentSchedule.Total = total.ToString("N");
                ScheduleList.Add(CurrentSchedule);              
               
                for (int i = 0; i < ScheduleList.Count; i++)
                {
                    ScheduleList[i].Identity = i + 1;
                }
                ScheduleList = new ObservableCollection<DealPayScheduleModel>(ScheduleList);
            }
            else {

                var existItem = ScheduleList.Where(x => x.Identity == CurrentSchedule.Identity).FirstOrDefault();
                existItem.Description = CurrentSchedule.Description;
                existItem.Amount1 = CurrentSchedule.Amount1;
                existItem.Amount2 = CurrentSchedule.Amount2;
                existItem.Total = total.ToString("N");
                existItem.ScheduleDate = CurrentSchedule.ScheduleDate;
                ScheduleList = new ObservableCollection<DealPayScheduleModel>(ScheduleList);
            }
            CurrentSchedule = new DealPayScheduleModel() { ScheduleDate = DateTimeOffset.Now };
            CalculateTotalAMounts();
        }

        public async void DeletePaySchedule(int inx) {
          
            StartStatusMessage("Deleteing Payment...");
            DealsViewModel.ShowProgressRing();
            var item = ScheduleList.Where(x => x.Identity == inx).FirstOrDefault();
            if (item == null)
                return;

            if (item.DealPayScheduleId > 0)
            {
                await DealService.DeleteDealPayScheduleAsync(item.DealPayScheduleId);
            }
            
            ScheduleList.Remove(item);
            var i = 1;
            foreach (var obj in ScheduleList)
            {
                obj.Identity = i;
                i++;
            }

            DealsViewModel.HideProgressRing();
            CalculateTotalAMounts();
            CurrentSchedule = new DealPayScheduleModel() { ScheduleDate = DateTimeOffset.Now };
            EndStatusMessage("Payment deleted");
        }

        public void Subscribe()
        {
            MessageService.Subscribe<DealDetailsViewModel, DealModel>(this, OnDetailsMessage);
            MessageService.Subscribe<DealListViewModel>(this, OnListMessage);
        }
        public void Unsubscribe()
        {
            MessageService.Unsubscribe(this);
        }


        protected override async Task<bool> SaveItemAsync(DealModel model)
        {
            try
            {
                if (IsProcessing)
                    return false;
                IsProcessing = true;

                if (ScheduleList!=null && ScheduleList.Count>0)
                model.DealPaySchedules = ScheduleList;

                if (DealPartyList != null && DealPartyList.Count > 0)
                    model.DealParties = DealPartyList;

                StartStatusMessage("Saving Deals...");
                DealsViewModel.ShowProgressRing();
                int mergeId = 0;
                if (model.DealId <= 0)
                    mergeId = await DealService.AddDealAsync(model);
                else
                    await DealService.UpdateDealAsync(model);
                IsProcessing = false;
                await loadDeal(mergeId == 0 ? model.DealId : mergeId);
                //var item = await DealService.GetDealAsync(mergeId == 0 ? model.DealId : mergeId);
                //Item = item;
                //PropertyList = item.propertyMergeLists;
                ShowPopup("success", "Deal details is Saved");
                EndStatusMessage("Deals saved");
                LogInformation("Deals", "Save", "Deals saved successfully", $"Deals {model.DealId}  was saved successfully.");
                return true;
            }
            catch (Exception ex)
            {
                IsProcessing = false;
                ShowPopup("error", "Deal details is not Saved");
                StatusError($"Error saving Deals: {ex.Message}");
                LogException("Deals", "Save", ex);
                return false;
            }
            finally { 
                DealsViewModel.HideProgressRing(); }
        }

        public async Task loadDeal(int dealId) {
            var model = await DealService.GetDealAsync(dealId);
            Item = model;
            Sale1 = model.SaleValue1;
            Sale2 = model.SaleValue2;
            SaleTotal = (Convert.ToDecimal(model.SaleValue1) + Convert.ToDecimal(model.SaleValue2)).ToString("N");
            DealPartyList = model.DealParties;
            for (int i = 0; i < model.DealPaySchedules.Count; i++)
            {
                model.DealPaySchedules[i].Identity = i + 1;
            }
            decimal amt1 = 0;
            decimal amt2 = 0;
            foreach (var obj in model.DealPaySchedules)
            {
                amt1 += Convert.ToDecimal(obj.Amount1);
                amt2 += Convert.ToDecimal(obj.Amount2);
                obj.Total =( Convert.ToDecimal(obj.Amount1) + Convert.ToDecimal(obj.Amount2)).ToString("N");
            }
           TotalAmount1 = amt1.ToString("N");
           TotalAmount2 = amt2.ToString("N");
           FinalAmount = (amt1 + amt2).ToString("N");
           ScheduleList = model.DealPaySchedules;
        }

        protected override void ClearItem()
        {
            Item = new DealModel();
            DealPartyList = new ObservableCollection<DealPartiesModel>();
            ScheduleList = new ObservableCollection<DealPayScheduleModel>();
            PartyOptions = new ObservableCollection<ComboBoxOptions>();
            TotalAmount1 = "";
            TotalAmount2 = "";
            FinalAmount = "";
            Sale1 = "0";
            Sale2 = "0";
            SaleTotal = "0";
            PartySearchQuery = "";
        }
        protected override async Task<bool> DeleteItemAsync(DealModel model)
        {
            try
            {
                StartStatusMessage("Deleting Deals...");
                DealsViewModel.ShowProgressRing();
               int status= await DealService.DeleteDealAsync(model);              
                ShowPopup("success", "Deal details is deleted");
                if (status == -1)
                {
                    ShowPopup("error", "Receipts are exist for this deal");
                    return true;
                }
                ClearItem();
                EndStatusMessage("Deals deleted");
                LogWarning("Deals", "Delete", "Deals deleted", $"Taluk {model.DealId}  was deleted.");
                return true;
            }
            catch (Exception ex)
            {
                ShowPopup("error", "Deal details is not deleted");
                StatusError($"Error deleting Deals: {ex.Message}");
                LogException("Deals", "Delete", ex);
                return false;
            }
            finally { DealsViewModel.HideProgressRing(); }
        }

        protected override async Task<bool> ConfirmDeleteAsync()
        {
            if (Item.DealId == 0)
                return false;
            return await DialogService.ShowAsync("Confirm Delete", "Are you sure to delete current Deals?", "Ok", "Cancel");
        }

        override protected IEnumerable<IValidationConstraint<DealModel>> GetValidationConstraints(DealModel model)
        {
            yield return new ValidationConstraint<DealModel>("Deal Name must be selected", m => Convert.ToInt32(m.PropertyMergeId) > 0);
            yield return new ValidationConstraint<DealModel>("Company must be selected", m => Convert.ToInt32(m.CompanyId) > 0);
            yield return new ValidationConstraint<DealModel>("Sale value 1 must be entered", m => Convert.ToDecimal(Sale1) > 0);
            yield return new ValidationConstraint<DealModel>("Sale value 2 must be entered", m => Convert.ToDecimal(Sale2) > 0);
            yield return new ValidationConstraint<DealModel>("Total of Amount 1 must be equal to Sale value 1", m => Convert.ToDecimal(Sale1)== Convert.ToDecimal(TotalAmount1??"0"));
            yield return new ValidationConstraint<DealModel>("Total of Amount 2 must be equal to Sale value 2", m => Convert.ToDecimal(Sale2)== Convert.ToDecimal(TotalAmount2??"0"));         

        }

        /*
         *  Handle external messages
         ****************************************************************/
        private async void OnDetailsMessage(DealDetailsViewModel sender, string message, DealModel changed)
        {
            var current = Item;
            if (current != null)
            {
                if (changed != null && changed.DealId == current?.DealId)
                {
                    switch (message)
                    {
                        case "ItemChanged":
                            await ContextService.RunAsync(async () =>
                            {
                                try
                                {
                                    var item = await DealService.GetDealAsync(current.DealId);
                                    item = item ?? new DealModel { DealId = current.DealId, IsEmpty = true };
                                    current.Merge(item);
                                    current.NotifyChanges();
                                    NotifyPropertyChanged(nameof(Title));
                                    if (IsEditMode)
                                    {
                                        StatusMessage("WARNING: This Deals has been modified externally");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    LogException("Deals", "Handle Changes", ex);
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

        private async void OnListMessage(DealListViewModel sender, string message, object args)
        {
            var current = Item;
            if (current != null)
            {
                switch (message)
                {
                    case "ItemsDeleted":
                        if (args is IList<DealModel> deletedModels)
                        {
                            if (deletedModels.Any(r => r.DealId == current.DealId))
                            {
                                await OnItemDeletedExternally();
                            }
                        }
                        break;
                    case "ItemRangesDeleted":
                        try
                        {
                            var model = await DealService.GetDealAsync(current.DealId);
                            if (model == null)
                            {
                                await OnItemDeletedExternally();
                            }
                        }
                        catch (Exception ex)
                        {
                            LogException("Deals", "Handle Ranges Deleted", ex);
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
                StatusMessage("WARNING: This Taluk has been deleted externally");
            });
        }
    }
}
