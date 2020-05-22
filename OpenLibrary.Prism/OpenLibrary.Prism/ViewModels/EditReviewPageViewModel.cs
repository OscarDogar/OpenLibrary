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
using System.Threading.Tasks;

namespace OpenLibrary.Prism.ViewModels
{
    public class EditReviewPageViewModel : ViewModelBase
    {
        private ReviewResponse _revi;
        private string _type;
        private bool isCreate;
        public EditReviewPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            Title = "Edit Comment";

            if (string.IsNullOrEmpty(Settings.ReviewS) || Settings.ReviewS.Equals("reviewS"))
            {
                Review = new ReviewResponse();
                Type = "Create";
                isCreate = true;
            }
            else
            {
                Review = JsonConvert.DeserializeObject<ReviewResponse>(Settings.ReviewS);
                Type = "Update";
                isCreate = false;
            }
            
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
                await App.Current.MainPage.DisplayAlert(Languages.Error, "You must enter a Comment", Languages.Accept);
                return false;
            }
            return true;
        }
        }
    }
