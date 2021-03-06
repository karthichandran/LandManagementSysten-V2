﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using LandBankManagement.Models;
using LandBankManagement.Services;

namespace LandBankManagement.ViewModels
{
    public class CompanyViewModel : ViewModelBase
    {
       
        ICompanyService CompanyService { get; }
        public CompanyListViewModel CompanyList { get; set; }

        public CompanyDetailsViewModel CompanyDetials { get; set; }

        private bool _progressRingVisibility;
        public bool NewProgressRingVisibility
        {
            get => _progressRingVisibility;
            set => Set(ref _progressRingVisibility, value);
        }

        private bool _progressRingActive;
        public bool NewProgressRingActive
        {
            get => _progressRingActive;
            set => Set(ref _progressRingActive, value);
        }

        public CompanyViewModel(ICommonServices commonServices, IFilePickerService filePickerService, ICompanyService companyService) : base(commonServices)
        {
            CompanyService = companyService;
            CompanyList = new CompanyListViewModel(companyService, commonServices,this);
            CompanyDetials = new CompanyDetailsViewModel(companyService, filePickerService, commonServices,this);
        }

        public async Task LoadAsync(CompanyListArgs args)
        {
            ShowProgressRing();
            await CompanyList.LoadAsync(args);
            HideProgressRing();
        }
        public void Unload()
        {
            CompanyList.Unload();
        }

        int noOfApiCalls = 0;
        public void ShowProgressRing()
        {
            noOfApiCalls++;
               NewProgressRingActive = true;
            NewProgressRingVisibility = true;
        }
        public void HideProgressRing()
        {
            if (noOfApiCalls > 1)
            {
                noOfApiCalls--;
                return;
            }
            else
                noOfApiCalls--;
            NewProgressRingActive = false;
            NewProgressRingVisibility = false;
        }

        public void Subscribe()
        {
            MessageService.Subscribe<CompanyListViewModel>(this, OnMessage);
            CompanyList.Subscribe();
        }

        public void Unsubscribe()
        {
            MessageService.Unsubscribe(this);
            CompanyList.Unsubscribe();

        }

        private async void OnMessage(CompanyListViewModel viewModel, string message, object args)
        {
            if (viewModel == CompanyList && message == "ItemSelected")
            {
                await ContextService.RunAsync(() =>
                {
                    OnItemSelected();
                });
            }
        }

        private async void OnItemSelected()
        {

            var selected = CompanyList.SelectedItem;
            if (!CompanyList.IsMultipleSelection)
            {
                if (selected != null && !selected.IsEmpty)
                {
                    await PopulateDetails(selected);
                }
            }
        }

        public async Task PopulateDetails(CompanyModel selected)
        {
            try
            {
                ShowProgressRing();
                var model = await CompanyService.GetCompanyAsync(selected.CompanyID);

                selected.Merge(model);
                CompanyDetials.Item = CompanyModel.CreateEmpty();
                CompanyDetials.Item = model;
                if (model.CompanyDocuments != null)
                {
                    CompanyDetials.DocList = model.CompanyDocuments;
                    for (int i = 0; i < CompanyDetials.DocList.Count; i++)
                    {
                        CompanyDetials.DocList[i].Identity = i + 1;
                    }
                }
                SelectedPivotIndex = 1;
            }
            catch (Exception ex)
            {
                LogException("Company", "Load Details", ex);
            }
            finally {
                HideProgressRing();
            }
        }
    }
}
