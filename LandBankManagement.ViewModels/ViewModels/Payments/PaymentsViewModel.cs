﻿using System;
using System.Threading.Tasks;

using LandBankManagement.Models;
using LandBankManagement.Services;

namespace LandBankManagement.ViewModels
{
   public class PaymentsViewModel : ViewModelBase
    {
        IPaymentService PaymentsService { get; }
        public PaymentsListViewModel PaymentsList { get; set; }

        public PaymentsDetailsViewModel PaymentsDetails { get; set; }
        private bool _progressRingVisibility;
        public bool ProgressRingVisibility
        {
            get => _progressRingVisibility;
            set => Set(ref _progressRingVisibility, value);
        }

        private bool _progressRingActive;
        public bool ProgressRingActive
        {
            get => _progressRingActive;
            set => Set(ref _progressRingActive, value);
        }
        public PaymentsViewModel(IDropDownService dropDownService, ICommonServices commonServices, IFilePickerService filePickerService, IPaymentService paymentsService) : base(commonServices)
        {
            PaymentsService = paymentsService;
            PaymentsList = new PaymentsListViewModel(PaymentsService, commonServices,this);
            PaymentsDetails = new PaymentsDetailsViewModel(dropDownService, PaymentsService, filePickerService, commonServices,this);
        }

        public async Task LoadAsync(PaymentsListArgs args)
        {
            await PaymentsDetails.LoadAsync();
            await PaymentsList.LoadAsync(args);
        }
        public void Unload()
        {
            PaymentsList.Unload();
        }
        public void ShowProgressRing()
        {
            ProgressRingActive = true;
            ProgressRingVisibility = true;
        }
        public void HideProgressRing()
        {
            ProgressRingActive = false;
            ProgressRingVisibility = false;
        }
        public void Subscribe()
        {
            MessageService.Subscribe<PaymentsListViewModel>(this, OnMessage);
            PaymentsList.Subscribe();
        }

        public void Unsubscribe()
        {
            MessageService.Unsubscribe(this);
            PaymentsList.Unsubscribe();

        }

        private async void OnMessage(PaymentsListViewModel viewModel, string message, object args)
        {
            if (viewModel == PaymentsList && message == "ItemSelected")
            {
                await ContextService.RunAsync(() =>
                {
                    OnItemSelected();
                });
            }
        }

        private async void OnItemSelected()
        {

            var selected = PaymentsList.SelectedItem;
            if (!PaymentsList.IsMultipleSelection)
            {
                if (selected != null && !selected.IsEmpty)
                {
                    await PopulateDetails(selected);
                }
            }
        }

        private async Task PopulateDetails(PaymentModel selected)
        {
            try
            {
                ShowProgressRing();
                var model = await PaymentsService.GetPaymentAsync(selected.PaymentId);
                selected.Merge(model);
                PaymentsDetails.Item = model;
                for (int i = 0; i < model.PaymentListModel.Count; i++)
                {
                    model.PaymentListModel[i].identity = i + 1;
                }
                PaymentsDetails.PaymentList = model.PaymentListModel;
                PaymentsDetails.defaultSettings();
                SelectedPivotIndex = 1;
            }
            catch (Exception ex)
            {
                LogException("Payments", "Load Details", ex);
            }
            finally {
                HideProgressRing();
            }
        }
    }
}
