﻿using System;
using System.Threading.Tasks;

using Windows.UI.Xaml.Controls;
using Windows.Storage;

using LandBankManagement.Views;
using System.Collections.Generic;
using LandBankManagement.ViewModels;

namespace LandBankManagement.Services
{
    public class SettingsService : ISettingsService
    {
        public SettingsService(IDialogService dialogService)
        {
            DialogService = dialogService;
        }

        public IDialogService DialogService { get; }

        public string Version => AppSettings.Current.Version;

        public string DbVersion => AppSettings.Current.DbVersion;

        public string UserName
        {
            get => AppSettings.Current.UserName;
            set => AppSettings.Current.UserName = value;
        }

        public DataProviderType DataProvider
        {
            get => DataProviderType.SQLServer;
            set => AppSettings.Current.DataProvider = value;
        }

        public string PatternConnectionString => $"Data Source={AppSettings.DatabasePatternFileName}";

        public string SQLServerConnectionString
        {
            get => AppSettings.Current.SQLServerConnectionString;
            set => AppSettings.Current.SQLServerConnectionString = value;
        }

        public bool IsRandomErrorsEnabled
        {
            get => AppSettings.Current.IsRandomErrorsEnabled;
            set => AppSettings.Current.IsRandomErrorsEnabled = value;
        }

        public async Task<Result> ResetLocalDataProviderAsync()
        {
            Result result = null;
            string errorMessage = null;
            try
            {
                var localFolder = ApplicationData.Current.LocalFolder;
                var databaseFolder = await localFolder.CreateFolderAsync(AppSettings.DatabasePath, CreationCollisionOption.OpenIfExists);
                var sourceFile = await databaseFolder.GetFileAsync(AppSettings.DatabasePattern);
                var targetFile = await databaseFolder.CreateFileAsync(AppSettings.DatabaseName, CreationCollisionOption.ReplaceExisting);
                await sourceFile.CopyAndReplaceAsync(targetFile);
                await DialogService.ShowAsync("Reset Local Data Provider", "Local Data Provider restore successfully.");
                result = Result.Ok();
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                result = Result.Error(ex);
            }
            if (errorMessage != null)
            {
                await DialogService.ShowAsync("Reset Local Data Provider", errorMessage);
            }
            return result;
        }

        public async Task<Result> ValidateConnectionAsync(string connectionString)
        {
            var dialog = new ValidateConnectionView(connectionString);
            var res = await dialog.ShowAsync();
            switch (res)
            {
                case ContentDialogResult.Secondary:
                    return Result.Ok("Operation canceled by user");
                default:
                    break;
            }
            return dialog.Result;
        }

        public async Task<Result> CreateDabaseAsync(string connectionString)
        {
            var dialog = new CreateDatabaseView(connectionString);
            var res = await dialog.ShowAsync();
            switch (res)
            {
                case ContentDialogResult.Secondary:
                    return Result.Ok("Operation canceled by user");
                default:
                    break;
            }
            return dialog.Result;
        }

        public List<SettingsDictionary> FetchAllLocalAppSettings()
        {
            var allSettings = ApplicationData.Current.LocalSettings.Values;

            var result = new List<SettingsDictionary>();

            foreach (KeyValuePair<string, object> settings in allSettings)
            {
                result.Add(new SettingsDictionary { Key = settings.Key, Value = settings.Value.ToString() });
            }

            return result;
        }

        public void ClearAllLocalAppSettings()
        {
            var allSettings = ApplicationData.Current.LocalSettings.Values;


            foreach (KeyValuePair<string, object> settings in allSettings)
            {
                ApplicationData.Current.LocalSettings.Values[settings.Key] = "";
            }
        }
    }
}
