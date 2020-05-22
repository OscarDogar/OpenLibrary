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
    public class MyCommentPageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private bool _isRunning;
        private bool _isEnabled;
        private List<ReviewItmViewModel> _com;
        private readonly INavigationService _navigationService;
        public MyCommentPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = Languages.Comments;
            Settings.ReviewS = string.Empty;
            LoadDocumentsAsync();
        }
        public List<ReviewItmViewModel> Comment
        {
            get => _com;
            set => SetProperty(ref _com, value);
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
                

                List<ReviewResponse> userDoc = (List<ReviewResponse>)response.Result;
                UserResponse User = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
                List<ReviewResponse> list = new List<ReviewResponse>();

                foreach (ReviewResponse li in userDoc)
                {
                    if (li.User.Id.Equals(User.Id))
                    {
                        list.Add(li);
                    }
                }

                Comment = list.Select(t => new ReviewItmViewModel(_navigationService)
                {
                    Id = t.Id,
                    User = t.User,
                    Comment = t.Comment,
                    Rating = t.Rating,
                    Favorite = t.Favorite,
                    Document = t.Document
                }).ToList(); ;

                IsRunning = false;
                IsEnabled = true;
            }
            catch (Exception)
            {
                Settings.User = string.Empty;
                Settings.Token = string.Empty;
                Settings.IsLogin = false;
                Settings.DocDetail = string.Empty;
                Settings.ReviewS = string.Empty;
            }

        }

    }
}
