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
    public class FavoriteItmViewModel:ReviewResponse
    {
        private DelegateCommand _selectFavCommand;
        private readonly INavigationService _navigationService;
        public FavoriteItmViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public DelegateCommand SelectDocCommand => _selectFavCommand ?? (_selectFavCommand = new DelegateCommand(SelectDocAsync));

        private async void SelectDocAsync()
        {


            Settings.DocDetail = JsonConvert.SerializeObject(this);


            await _navigationService.NavigateAsync("DocDetailTabbedPage");
        }
    }
}
