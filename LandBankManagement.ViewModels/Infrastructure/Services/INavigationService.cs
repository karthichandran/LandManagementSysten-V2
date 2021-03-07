using System;
using System.Threading.Tasks;

namespace LandBankManagement.Services
{
    public interface INavigationService
    {
        bool IsMainView { get; }

        bool CanGoBack { get; }

        string CurrentScreen { get; set; }
        bool CanReadWrite { get; set; }

        void Initialize(object frame);

        bool Navigate<TViewModel>(object parameter = null);
        bool Navigate(Type viewModelType, object parameter = null);

        Task<int> CreateNewViewAsync<TViewModel>(object parameter = null);
        Task<int> CreateNewViewAsync(Type viewModelType, object parameter = null);

        void GoBack();

        Task CloseViewAsync();
    }
}
