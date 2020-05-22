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
    public class MyFavoritesPageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private bool _isRunning;
        private bool _isEnabled;
        private List<DocumentItemViewModel> _favor;
        private readonly INavigationService _navigationService;
        public MyFavoritesPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = Languages.MyFavorite;
            LoadDocumentsAsync();
        }

        public List<DocumentItemViewModel> Favorite
        {
            get => _favor;
            set => SetProperty(ref _favor, value);
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
                Response response = await _apiService.GetListAsync<ReviewResponse>(url, "/api", "/Search/GetReview");

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

                List<ReviewResponse> userRev = (List<ReviewResponse>)response.Result;

                IsRunning = true;
                IsEnabled = false;
                string url2 = App.Current.Resources["UrlAPI"].ToString();
                Response response2 = await _apiService.GetListAsync<SearchResponse>(url2, "/api", "/Search");

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

                List<SearchResponse> doc = (List<SearchResponse>)response2.Result;


                UserResponse User = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
                List<SearchResponse> list2 = new List<SearchResponse>();

                foreach (ReviewResponse li in userRev)
                {
                    if (li.Favorite==true && li.User.Id == User.Id)
                    {
                        foreach (SearchResponse le in doc)
                        {
                            if (li.Document.Id == le.Id)
                            {
                                list2.Add(le);
                            }
                        }
                    }
                }


                Favorite = list2.Select(t => new DocumentItemViewModel(_navigationService)
                {
                    Id = t.Id,
                    Title = t.Title,
                    DocumentPath = t.DocumentPath,
                    Date = t.Date,
                    User = t.User,
                    Summary = t.Summary,
                    PagesNumber = t.PagesNumber,
                    Accepted = t.Accepted,
                    Gender = t.Gender,
                    Author = t.Author,
                    DocumentLanguage = t.DocumentLanguage,
                    TypeOfDocument = t.TypeOfDocument,
                    Reviews = t.Reviews
                }).ToList(); ;

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
