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
    public class DocumentItemViewModel : SearchResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectDocCommand;

        public DocumentItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectDocCommand => _selectDocCommand ?? (_selectDocCommand = new DelegateCommand(SelectDocAsync));

        private async void SelectDocAsync()
        {


            Settings.DocDetail = JsonConvert.SerializeObject(this);


            await _navigationService.NavigateAsync("DocDetailTabbedPage");
        }
    }
}
