using Newtonsoft.Json;
using OpenLibrary.Common.Helpers;
using OpenLibrary.Common.Models;
using OpenLibrary.Prism.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenLibrary.Prism.ViewModels
{
    public class ReviewPageViewModel : ViewModelBase
    {
        private List<ReviewResponse> _revi;

        public ReviewPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = Languages.Comments;
            LoadReviewpage();
        }

        public List<ReviewResponse> Reviews
        {
            get => _revi;
            set => SetProperty(ref _revi, value);
        }

        private void LoadReviewpage()
        {

            SearchResponse _docs = JsonConvert.DeserializeObject<SearchResponse>(Settings.DocDetail);

            List<ReviewResponse> Review = new List<ReviewResponse>();

            foreach (ReviewResponse review in _docs.Reviews)
            {
                Review.Add(review);
            }

            Reviews = Review;
        }

    }
}
