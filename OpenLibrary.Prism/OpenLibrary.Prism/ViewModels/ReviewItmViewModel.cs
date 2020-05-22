using Newtonsoft.Json;
using OpenLibrary.Common.Helpers;
using OpenLibrary.Common.Models;
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

        public ReviewItmViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectRevCommand => _selectRevCommand ?? (_selectRevCommand = new DelegateCommand(SelectDocAsync));

        private async void SelectDocAsync()
        {


            Settings.ReviewS = JsonConvert.SerializeObject(this);


            await _navigationService.NavigateAsync("EditReviewPage");
        }
    }
}
