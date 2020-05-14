using OpenLibrary.Common.Models;
using OpenLibrary.Common.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace OpenLibrary.Prism.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private List<SearchResponse> _Doc;

        public MainPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _apiService = apiService;
            Title = "Open Library";
            LoadTournamentsAsync();
        }

        public List<SearchResponse> UserDoc
        {
            get => _Doc;
            set => SetProperty(ref _Doc, value);
        }

        private async void LoadTournamentsAsync()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetListAsync<UserResponse>(
                url,
                "/api",
                "/Search");

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

          

   

        }
    }

}

