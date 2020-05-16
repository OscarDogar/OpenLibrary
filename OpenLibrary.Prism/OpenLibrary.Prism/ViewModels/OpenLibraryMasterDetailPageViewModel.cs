using OpenLibrary.Common.Models;
using OpenLibrary.Prism.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OpenLibrary.Prism.ViewModels
{
    public class OpenLibraryMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public OpenLibraryMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            LoadMenus();
        }

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        private void LoadMenus()
        {
            List<Menu> menus = new List<Menu>
            {
                new Menu
                {
                    Icon = "home",
                    PageName = "MainPage",
                    Title = Languages.Documents
                },
                new Menu
                {
                    Icon = "user",
                    PageName = "ModifyUserPage",
                    Title = Languages.ModifyUser
                },
                new Menu
                {
                    Icon = "Document",
                    PageName = "MyUploadedDocumentsPage",
                    Title = Languages.MyUploadedDocuments
                },
                new Menu
                {
                    Icon = "login",
                    PageName = "LoginPage",
                    Title = Languages.Login
                }
            };

            Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel(_navigationService)
                {
                    Icon = m.Icon,
                    PageName = m.PageName,
                    Title = m.Title
                }).ToList());
        }
    }

}
