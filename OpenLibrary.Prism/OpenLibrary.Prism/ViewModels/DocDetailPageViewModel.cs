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
        private readonly INavigationService _navigationService;
        private DelegateCommand _openDocCommand;
        private SearchResponse _doc;
        public DocDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = Languages.Detail;
            _navigationService = navigationService;
            LoadDetailpage();
        }

        public DelegateCommand OpenDocCommand => _openDocCommand ?? (_openDocCommand = new DelegateCommand(OpenDocAsync));

        public SearchResponse Doc
        {
            get => _doc;
            set => SetProperty(ref _doc, value);
        }

        private async void OpenDocAsync()
        {
            await _navigationService.NavigateAsync("PDFPage");
        }

        private void LoadDetailpage()
        {

            SearchResponse _docs= JsonConvert.DeserializeObject<SearchResponse>(Settings.DocDetail);

            Doc = _docs;
        }
    }
}
