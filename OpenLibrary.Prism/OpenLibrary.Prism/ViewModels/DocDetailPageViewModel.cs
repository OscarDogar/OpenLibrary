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
    public class DocDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private DelegateCommand _openDocCommand;
        private DelegateCommand _makeCommand;
        private bool _isRunning;
        private bool _isEnabled;
        private bool _isVisible;
        private SearchResponse _doc;
        public DocDetailPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            Title = Languages.Detail;
            _navigationService = navigationService;
            _apiService = apiService;
            IsVisible = Settings.IsLogin;
            IsEnabled = false;
            LoadDetailpage();
            LoadDocumentsAsync();
            IsEnabled = true;
        }
        
        public DelegateCommand OpenDocCommand => _openDocCommand ?? (_openDocCommand = new DelegateCommand(OpenDocAsync));

        public DelegateCommand MakeCommentCommand => _makeCommand ?? (_makeCommand = new DelegateCommand(MakeAsync));

        public SearchResponse Doc
        {
            get => _doc;
            set => SetProperty(ref _doc, value);
        }
        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsVisible
        {
            get => _isVisible;
            set => SetProperty(ref _isVisible, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        private async void MakeAsync()
        {
            IsEnabled = false;
            IsRunning = true;
            if (Settings.IsLogin)
            {  
                await _navigationService.NavigateAsync("EditReviewPage");
            }
            else
            {
                await _navigationService.NavigateAsync("LoginPage");
            }
            IsRunning = false;
            IsEnabled = true;

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

                List<ReviewResponse> userDoc = (List<ReviewResponse>)response.Result;
                UserResponse User = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
                ReviewResponse list = new ReviewResponse();
                bool ready=false;

                foreach (ReviewResponse li in userDoc)
                {
                    if (li.User.Id.Equals(User.Id) && Doc.Id==li.Document.Id)
                    {
                        list =li;
                        ready = true;
                    }
                }

                if (ready)
                {
                    
                    Settings.ReviewS= JsonConvert.SerializeObject(list);
                }
                else
                {
                    Settings.ReviewS = string.Empty;
                }

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

        private async void OpenDocAsync()
        {
            IsEnabled = false;
            IsRunning = true;
            await _navigationService.NavigateAsync("PDFPage");
            IsRunning = false;
            IsEnabled = true;
        }

        private void LoadDetailpage()
        {

            SearchResponse _docs= JsonConvert.DeserializeObject<SearchResponse>(Settings.DocDetail);

            Doc = _docs;
        }
    }
}
