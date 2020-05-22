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
        private readonly bool isCreate;
        private DelegateCommand _validCommand;
        public EditReviewPageViewModel(INavigationService navigationService,
                                       IApiService apiService) : base(navigationService)
        {
            if (string.IsNullOrEmpty(Settings.ReviewS) || Settings.ReviewS.Equals("reviewS"))
            {
                Review = new ReviewResponse();
                Type = Languages.Create;
                Title = Languages.MakeAComment;
                isCreate = true;
            }
            else
            {
                Review = JsonConvert.DeserializeObject<ReviewResponse>(Settings.ReviewS);
                Type = Languages.Update;
                Title = Languages.EditComment;
                isCreate = false;
            }

        }
        public DelegateCommand ValidCommand => _validCommand ?? (_validCommand = new DelegateCommand(ReviewAsync));
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
        private async void ReviewAsync()
        {
            bool isValid = await ValidateDataAsync();
            if (!isValid)
            {
                return;
            }
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
    }
}
