﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using LandBankManagement.Models;
using LandBankManagement.Services;

namespace LandBankManagement.ViewModels
{
    #region AppLogDetailsArgs
    public class AppLogDetailsArgs
    {
        static public AppLogDetailsArgs CreateDefault() => new AppLogDetailsArgs();

        public long AppLogID { get; set; }
    }
    #endregion

    public class AppLogDetailsViewModel : GenericDetailsViewModel<AppLogModel>
    {
        public AppLogDetailsViewModel(ICommonServices commonServices) : base(commonServices)
        {
        }

        override public string Title => "Activity Logs";

        public override bool ItemIsNew => false;

        public AppLogDetailsArgs ViewModelArgs { get; private set; }

        public async Task LoadAsync(AppLogDetailsArgs args)
        {
            ViewModelArgs = args ?? AppLogDetailsArgs.CreateDefault();

            try
            {
                var item = await LogService.GetLogAsync(ViewModelArgs.AppLogID);
                Item = item ?? new AppLogModel { Id = 0, IsEmpty = true };
            }
            catch (Exception ex)
            {
                LogException("AppLog", "Load", ex);
            }
        }
        public void Unload()
        {
            ViewModelArgs.AppLogID = Item?.Id ?? 0;
        }

        public void Subscribe()
        {
            MessageService.Subscribe<AppLogDetailsViewModel, AppLogModel>(this, OnDetailsMessage);
            MessageService.Subscribe<AppLogListViewModel>(this, OnListMessage);
        }
        public void Unsubscribe()
        {
            MessageService.Unsubscribe(this);
        }

        public AppLogDetailsArgs CreateArgs()
        {
            return new AppLogDetailsArgs
            {
                AppLogID = Item?.Id ?? 0
            };
        }

        protected override Task<bool> SaveItemAsync(AppLogModel model)
        {
            throw new NotImplementedException();
        }
        protected override void ClearItem()
        {
           
        }
        protected override async Task<bool> DeleteItemAsync(AppLogModel model)
        {
            try
            {
                StartStatusMessage("Deleting log...");
                
                await LogService.DeleteLogAsync(model);
                EndStatusMessage("Log deleted");
                return true;
            }
            catch (Exception ex)
            {
                StatusError($"Error deleting log: {ex.Message}");
                LogException("AppLog", "Delete", ex);
                return false;
            }
        }

        protected override async Task<bool> ConfirmDeleteAsync()
        {
            return await DialogService.ShowAsync("Confirm Delete", "Are you sure to delete current log?", "Ok", "Cancel");
        }

        /*
         *  Handle external messages
         ****************************************************************/
        private async void OnDetailsMessage(AppLogDetailsViewModel sender, string message, AppLogModel changed)
        {
            var current = Item;
            if (current != null)
            {
                if (changed != null && changed.Id == current?.Id)
                {
                    switch (message)
                    {
                        case "ItemDeleted":
                            await OnItemDeletedExternally();
                            break;
                    }
                }
            }
        }

        private async void OnListMessage(AppLogListViewModel sender, string message, object args)
        {
            var current = Item;
            if (current != null)
            {
                switch (message)
                {
                    case "ItemsDeleted":
                        if (args is IList<AppLogModel> deletedModels)
                        {
                            if (deletedModels.Any(r => r.Id == current.Id))
                            {
                                await OnItemDeletedExternally();
                            }
                        }
                        break;
                    case "ItemRangesDeleted":
                        var model = await LogService.GetLogAsync(current.Id);
                        if (model == null)
                        {
                            await OnItemDeletedExternally();
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
                StatusMessage("WARNING: This log has been deleted externally");
            });
        }
    }
}
