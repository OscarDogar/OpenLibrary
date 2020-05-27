using Newtonsoft.Json;
using OpenLibrary.Common.Helpers;
using OpenLibrary.Common.Models;
using OpenLibrary.Common.Services;
using OpenLibrary.Prism.Helpers;
using Prism.Commands;
using Prism.Navigation;
using System.Threading.Tasks;

namespace OpenLibrary.Prism.ViewModels
{
    public class EditReviewPageViewModel : ViewModelBase
    {
        private ReviewResponse _revi;
        private string _type;
        private bool _isRunning;
        private bool _isEnabled;
        private readonly IApiService _apiService;
        private readonly INavigationService _navigationService;
        private DelegateCommand _registerCommand;
        public EditReviewPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            Title = "Edit Comment";
            _apiService = apiService;
            _navigationService = navigationService;
            if (string.IsNullOrEmpty(Settings.ReviewS) || Settings.ReviewS.Equals("reviewS"))
            {
                Review = new ReviewResponse();
                UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
                SearchResponse doc = JsonConvert.DeserializeObject<SearchResponse>(Settings.DocDetail);
                Type = Languages.Create;
                Title = Languages.MakeAComment;
                _revi.User= user;
                _revi.Document = doc;

            }
            else
            {
                Review = JsonConvert.DeserializeObject<ReviewResponse>(Settings.ReviewS);
                Type = Languages.Update;
                Title = Languages.EditComment;
                
            }

        }

        public DelegateCommand RegisterCommand => _registerCommand ?? (_registerCommand = new DelegateCommand(CreateDetailAsync));

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

        public ReviewResponse Review
        {
            get => _revi;
            set => SetProperty(ref _revi, value);
        }

        public string Type
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }

            private async Task<bool> ValidateDataAsync()
        {
            if (string.IsNullOrEmpty(Review.Comment))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.YouEnterAComment , Languages.Accept);
                return false;
            }
            if (Review.Rating < 0 || Review.Rating > 5)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ValidNumberValue, Languages.Accept);
                return false;
            }
            return true;
        }

        private async void CreateDetailAsync()
        {

            bool isValid = await ValidateDataAsync();
            if (!isValid)
            {
                return;
            }

            IsRunning = true;
            IsEnabled = false;
            string url = App.Current.Resources["UrlAPI"].ToString();
            bool connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }

            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            ReviewRequest reviewRequest = new ReviewRequest
            {
                User = Review.User.Id,
                Document = Review.Document.Id,
                Comment = Review.Comment,
                Rating = Review.Rating,
                Favorite = Review.Favorite,
                CultureInfo = Languages.Culture
            };


            Response response = await _apiService.CreateReviewAsync(url, "/api", "/Search/InsertReview", reviewRequest, "bearer", token.Token);
            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            await App.Current.MainPage.DisplayAlert(Languages.Ok, response.Message, Languages.Accept);


            IsRunning = false;
            IsEnabled = true;

            await _navigationService.NavigateAsync("/OpenLibraryMasterDetailPage/NavigationPage/MyCommentPage");

        }
    }
    }
