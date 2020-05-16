using OpenLibrary.Common.Models;
using OpenLibrary.Common.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace OpenLibrary.Prism.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private List<SearchResponse> _Doc;
        private List<SearchResponse> _Doc2;
        private ObservableCollection<DocumentLanguageResponse> _laguage;
        private DocumentLanguageResponse _laguages;
        private ObservableCollection<GenderResponse> _gender;
        private GenderResponse _genders;
        private ObservableCollection<TypeOfDocumentResponse> _type;
        private TypeOfDocumentResponse _types;
        private DelegateCommand _searchCommand;
        private string _author;

        public MainPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _apiService = apiService;
            Title = "Open Library";
            LoadLanguagesAsync();
            LoadGendersAsync();
            LoadTypesAsync();
            LoadTournamentsAsync();
        }

        public DelegateCommand SearchCommand => _searchCommand ?? (_searchCommand = new DelegateCommand(SearchAsync));

        public string Author
        {
            get => _author;
            set => SetProperty(ref _author, value);
        }

        public List<SearchResponse> UserDoc
        {
            get => _Doc;
            set => SetProperty(ref _Doc, value);
        }

        public List<SearchResponse> UserDoc2
        {
            get => _Doc2;
            set => SetProperty(ref _Doc2, value);
        }

        public ObservableCollection<DocumentLanguageResponse> Languages
        {
            get => _laguage;
            set => SetProperty(ref _laguage, value);
        }

        public DocumentLanguageResponse Language
        {
            get => _laguages;
            set => SetProperty(ref _laguages, value);
        }

        public ObservableCollection<GenderResponse> Genders
        {
            get => _gender;
            set => SetProperty(ref _gender, value);
        }

        public GenderResponse Gender
        {
            get => _genders;
            set => SetProperty(ref _genders, value);
        }

        public ObservableCollection<TypeOfDocumentResponse> Types
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }

        public TypeOfDocumentResponse Type
        {
            get => _types;
            set => SetProperty(ref _types, value);
        }

        private async void SearchAsync()
        {
            UserDoc = UserDoc2;
            List<SearchResponse> list = UserDoc;

            if (Type != null)
            {

                list = list.Where(u => u.TypeOfDocument.Id == Type.Id).ToList();
            }

            if (Gender != null)
            {
                list = list.Where(u => u.Gender.Id== Gender.Id).ToList();

            }

            if (Language != null)
            {
                list = list.Where(u => u.DocumentLanguage.Id == Language.Id).ToList();
            }

            if (!string.IsNullOrEmpty(Author))
            {
               
                list = list.Where(u => u.Author.Name.Equals(Author)).ToList();
            }

            if (list.Count == 0)
            {
                await App.Current.MainPage.DisplayAlert("Error", "It was not found", "Accept");

            }
            else
            {
                UserDoc = list;
            }


        }

        private async void LoadTournamentsAsync()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetListAsync<SearchResponse>(
                url,
                "/api",
                "/Search");

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            UserDoc = (List<SearchResponse>)response.Result;

            List<SearchResponse> list = new List<SearchResponse>();
           
            foreach(SearchResponse li in UserDoc)
            {
                if (li.Accepted==true)
                {
                    list.Add(li);
                }
            }

            UserDoc = list;
            UserDoc2 = UserDoc;
        }

        private async void LoadLanguagesAsync()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();
            bool connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Conexion error", "Accept");
                return;
            }

            Response response = await _apiService.GetListAsync<DocumentLanguageResponse>(url, "/api", "/Language");

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            List<DocumentLanguageResponse> list = (List<DocumentLanguageResponse>)response.Result;
            Languages = new ObservableCollection<DocumentLanguageResponse>(list.OrderBy(t => t.Name));
        }

        private async void LoadTypesAsync()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();
            bool connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Conexion error", "Accept");
                return;
            }

            Response response = await _apiService.GetListAsync<TypeOfDocumentResponse>(url, "/api", "/Type");

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            List<TypeOfDocumentResponse> list = (List<TypeOfDocumentResponse>)response.Result;
            Types = new ObservableCollection<TypeOfDocumentResponse>(list.OrderBy(t => t.Name));
        }

        private async void LoadGendersAsync()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();
            bool connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Conexion error", "Accept");
                return;
            }

            Response response = await _apiService.GetListAsync<GenderResponse>(url, "/api", "/Gender");

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            List<GenderResponse> list = (List<GenderResponse>)response.Result;
            Genders = new ObservableCollection<GenderResponse>(list.OrderBy(t => t.Name));
        }
    }

}

