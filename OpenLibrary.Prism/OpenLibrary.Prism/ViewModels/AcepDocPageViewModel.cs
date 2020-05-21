using Newtonsoft.Json;
using OpenLibrary.Common.Helpers;
using OpenLibrary.Common.Models;
using OpenLibrary.Common.Services;
using OpenLibrary.Prism.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenLibrary.Prism.ViewModels
{
    public class AcepDocPageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private bool _isRunning;
        private bool _isEnabled;
        private List<SearchResponse> _Doc;
        private readonly INavigationService _navigationService;
        public AcepDocPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = Languages.DocA;
            LoadDocumentsAsync();
        }
        public List<SearchResponse> UserDoc
        {
            get => _Doc;
            set => SetProperty(ref _Doc, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }
        private async void LoadDocumentsAsync()
        {
            try
            {
                IsRunning = true;
                IsEnabled = false;
                string url = App.Current.Resources["UrlAPI"].ToString();
                Response response = await _apiService.GetListAsync<SearchResponse>(url, "/api", "/Search");

                if (!response.IsSuccess)
                {
                    IsRunning = true;
                    IsEnabled = false;
                    await App.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        response.Message,
                        Languages.Accept);
                    return;
                }
                IsRunning = false;
                IsEnabled = true;

                List<SearchResponse> userDoc = (List<SearchResponse>)response.Result;
                UserResponse User = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
                List<SearchResponse> list = new List<SearchResponse>();

                foreach (SearchResponse li in userDoc)
                {
                    if (li.Accepted == true && li.User.DocumentId==User.DocumentId)
                    {
                        list.Add(li);
                    }
                }

                UserDoc = list;

            }
            catch (Exception)
            {
                Settings.User = string.Empty;
                Settings.Token = string.Empty;
                Settings.IsLogin = false;
                Settings.DocDetail = string.Empty;
            }
           
        }

    }
}
