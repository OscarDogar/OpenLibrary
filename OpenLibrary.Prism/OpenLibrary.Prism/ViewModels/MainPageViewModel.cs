using OpenLibrary.Common.Models;
using OpenLibrary.Common.Services;
using OpenLibrary.Prism.Helpers;
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
        private string _titleOfDocument;
        private bool _isRunning;
        private bool _isEnabled;



        public MainPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _apiService = apiService;
            Title = "Open Library";
            LoadLanguagesAsync();
            LoadGendersAsync();
            LoadTypesAsync();
            LoadDocumentsAsync();
        }

        public DelegateCommand SearchCommand => _searchCommand ?? (_searchCommand = new DelegateCommand(SearchAsync));

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }
        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        public string Author
        {
            get => _author;
            set => SetProperty(ref _author, value);
        }

        public string TitleOfDocument
        {
            get => _titleOfDocument;
            set => SetProperty(ref _titleOfDocument, value);
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

        public ObservableCollection<DocumentLanguageResponse> LanguageDocument
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
            IsRunning = true;
            UserDoc = UserDoc2;
            List<SearchResponse> list = UserDoc;
            
            if (Type != null)
            {
                if (Type.Id != 0)
                {
                    list = list.Where(u => u.TypeOfDocument.Id == Type.Id).ToList();
                }
            }

            if (Gender != null)
            {
                if (Gender.Id != 0)
                {
                    list = list.Where(u => u.Gender.Id == Gender.Id).ToList();
                }
            }

            if (Language != null)
            {
                if (Language.Id != 0)
                {
                    list = list.Where(u => u.DocumentLanguage.Id == Language.Id).ToList();
                }
            }

            if (!string.IsNullOrEmpty(Author))
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Author.Name.ToLower().Contains(Author.ToLower()))
                    {
                        list = list.Where(u => u.Author.Name.Equals(list[i].Author.Name)).ToList();
                    }
                }
            }
            if (!string.IsNullOrEmpty(TitleOfDocument))
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Title.ToLower().Contains(TitleOfDocument.ToLower()))
                    {
                        list = list.Where(u => u.Title.Equals(list[i].Title)).ToList();
                    }
                }
            }

            if (list.Count == 0)
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ItWasNotFound , Languages.Accept);
            }
            else
            {
                IsRunning = false;
                UserDoc = list;
            }

        }

        private async void LoadDocumentsAsync()
        {
            IsRunning = true;
            IsEnabled = false;
            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetListAsync<SearchResponse>(url,"/api","/Search");

            if (!response.IsSuccess)
            {
                IsRunning = true;
                IsEnabled = false;
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);
                return;
            }
            IsRunning = false;
            IsEnabled = true;

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
               await App.Current.MainPage.DisplayAlert(Languages.ConnectionError, Languages.ErrorLanguages, Languages.Accept);
                return;
            }

            Response response = await _apiService.GetListAsync<DocumentLanguageResponse>(url, "/api", "/Language");

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }
            List<DocumentLanguageResponse> list = (List<DocumentLanguageResponse>)response.Result;
            list.Insert(0, new DocumentLanguageResponse
            {
                Name = "No Filter"
            });
            LanguageDocument = new ObservableCollection<DocumentLanguageResponse>(list.OrderBy(t => t.Id));
        }

        private async void LoadTypesAsync()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();
            bool connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                await App.Current.MainPage.DisplayAlert(Languages.ConnectionError, Languages.ErrorTypeOfDocument , Languages.Accept);
                return;
            }

            Response response = await _apiService.GetListAsync<TypeOfDocumentResponse>(url, "/api", "/Type");

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }
            List<TypeOfDocumentResponse> list = (List<TypeOfDocumentResponse>)response.Result;
            list.Insert(0, new TypeOfDocumentResponse
            {
                Name = "No Filter"
            });

            Types = new ObservableCollection<TypeOfDocumentResponse>(list.OrderBy(t => t.Id));
        }

        private async void LoadGendersAsync()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();
            bool connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                await App.Current.MainPage.DisplayAlert(Languages.ConnectionError, Languages.ErrorGender, Languages.Accept);
                return;
            }

            Response response = await _apiService.GetListAsync<GenderResponse>(url, "/api", "/Gender");

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }
            List<GenderResponse> list = (List<GenderResponse>)response.Result;
            list.Insert(0, new GenderResponse
            {
                Name = "No Filter"
            });
            Genders = new ObservableCollection<GenderResponse>(list.OrderBy(t => t.Id));
        }
    }

}

