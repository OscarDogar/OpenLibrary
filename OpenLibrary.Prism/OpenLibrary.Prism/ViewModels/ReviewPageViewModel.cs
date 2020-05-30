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
        private bool _isVisible;

        public ReviewPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = Languages.Comments;
            LoadReviewpage();
        }
        public bool IsVisible
        {
            get => _isVisible;
            set => SetProperty(ref _isVisible, value);
        }
        public List<ReviewResponse> Reviews
        {
            get => _revi;
            set => SetProperty(ref _revi, value);
        }

        private void LoadReviewpage()
        {
            IsVisible = false;
            SearchResponse _docs = JsonConvert.DeserializeObject<SearchResponse>(Settings.DocDetail);

            List<ReviewResponse> Review = new List<ReviewResponse>();

            foreach (ReviewResponse review in _docs.Reviews)
            {
                Review.Add(review);
            }
            if (Review.Count == 0)
            {
                IsVisible = true;
            }
            else
            {
                IsVisible = false;
            }
            Reviews = Review;
        }

    }
}
