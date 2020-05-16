using OpenLibrary.Common.Interfaces;
using OpenLibrary.Prism.Resources;
using System.Globalization;
using Xamarin.Forms;

namespace OpenLibrary.Prism.Helpers
{
    public static class Languages
    {
        static Languages()
        {
            CultureInfo ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            Culture = ci.Name;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Culture { get; set; }
        
        public static string MyUploadedDocuments => Resource.MyUploadedDocuments;

        public static string Documents => Resource.Documents;

        public static string ModifyUser => Resource.ModifyUser;

        public static string ItWasNotFound => Resource.ItWasNotFound;

        public static string Language => Resource.Language;

        public static string TypeOfDocument => Resource.TypeOfDocument;

        public static string Title => Resource.Title;

        public static string Author => Resource.Author;

        public static string Gender => Resource.Gender;

        public static string SelectType => Resource.SelectType;

        public static string SelectLanguage => Resource.SelectLanguage;

        public static string SelectGender => Resource.SelectGender;

        public static string WriteTitle => Resource.WriteTitle;

        public static string WriteAutor => Resource.WriteAutor;

        public static string Loading => Resource.Loading;

        public static string Login => Resource.Login;

        public static string Search => Resource.Search;

        public static string Accept => Resource.Accept;

        public static string ConnectionError => Resource.ConnectionError;

        public static string Error => Resource.Error;
    }

}
