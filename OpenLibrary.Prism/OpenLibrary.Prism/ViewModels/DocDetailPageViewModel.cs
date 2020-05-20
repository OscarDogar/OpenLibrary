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
    public class DocDetailPageViewModel : ViewModelBase
    {
        private SearchResponse _doc;
        public DocDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = Languages.Detail;
            LoadDetailpage();
        }

        public SearchResponse Doc
        {
            get => _doc;
            set => SetProperty(ref _doc, value);
        }

        private void LoadDetailpage()
        {

            SearchResponse _docs= JsonConvert.DeserializeObject<SearchResponse>(Settings.DocDetail);

            Doc = _docs;
        }
    }
}
