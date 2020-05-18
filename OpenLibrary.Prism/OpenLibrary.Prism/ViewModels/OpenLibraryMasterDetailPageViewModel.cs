using Newtonsoft.Json;
using OpenLibrary.Common.Helpers;
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
        private UserResponse _user;
        public OpenLibraryMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            LoadUser();
            LoadMenus();
        }
        public UserResponse User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        private void LoadUser()
        {
            if (Settings.IsLogin)
            {
                User = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            }
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
                   Title = Settings.IsLogin ? Languages.Logout : Languages.Login
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
