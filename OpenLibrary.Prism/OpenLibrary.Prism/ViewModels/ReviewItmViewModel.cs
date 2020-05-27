using ImTools;
using Newtonsoft.Json;
using OpenLibrary.Common.Helpers;
using OpenLibrary.Common.Models;
using OpenLibrary.Common.Services;
using OpenLibrary.Prism.Helpers;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenLibrary.Prism.ViewModels
{
    public class ReviewItmViewModel: ReviewResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectRevCommand;
        private DelegateCommand _delCommand;
        private readonly IApiService _apiService;

        public ReviewItmViewModel(INavigationService navigationService, IApiService apiService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
        }
        public DelegateCommand DelCommand => _delCommand ?? (_delCommand = new DelegateCommand(CreateDetailAsync));

        public DelegateCommand SelectRevCommand => _selectRevCommand ?? (_selectRevCommand = new DelegateCommand(SelectDocAsync));

        private async void CreateDetailAsync()
        {

            string url = App.Current.Resources["UrlAPI"].ToString();
            bool connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
               
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }

            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            ReviewDeleteRequest reviewRequest = new ReviewDeleteRequest
            {
                User=this.User.Id,
                Document=this.Document.Id,
                CultureInfo = Languages.Culture
            };


            Response response = await _apiService.DeleteReviewAsync(url, "/api", "/Search/DeleteReview", reviewRequest, "bearer", token.Token);
           

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            await App.Current.MainPage.DisplayAlert(Languages.Ok, response.Message, Languages.Accept);

            await _navigationService.NavigateAsync("/OpenLibraryMasterDetailPage/NavigationPage/MyCommentPage");

        }

        private async void SelectDocAsync()
        {


            Settings.ReviewS = JsonConvert.SerializeObject(this);


            await _navigationService.NavigateAsync("EditReviewPage");
        }
    }
}
