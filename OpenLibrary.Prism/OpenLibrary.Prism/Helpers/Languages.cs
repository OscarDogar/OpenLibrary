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
            Culture = ci.ToString();
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Culture { get; set; }

        public static string PasswordRecover => Resource.PasswordRecover;

        public static string ForgotPassword => Resource.ForgotPassword;

        public static string PictureSource => Resource.PictureSource;

        public static string Cancel => Resource.Cancel;

        public static string FromCamera => Resource.FromCamera;

        public static string FromGallery => Resource.FromGallery;

        public static string Ok => Resource.Ok;

        public static string DocumentError => Resource.DocumentError;

        public static string FirstNameError => Resource.FirstNameError;

        public static string LastNameError => Resource.LastNameError;

        public static string Address => Resource.Address;

        public static string AddressError => Resource.AddressError;

        public static string AddressPlaceHolder => Resource.AddressPlaceHolder;

        public static string Phone => Resource.Phone;

        public static string PhoneError => Resource.PhoneError;

        public static string PhonePlaceHolder => Resource.PhonePlaceHolder;

        public static string PasswordConfirm => Resource.PasswordConfirm;

        public static string PasswordConfirmError1 => Resource.PasswordConfirmError1;

        public static string PasswordConfirmError2 => Resource.PasswordConfirmError2;

        public static string PasswordConfirmPlaceHolder => Resource.PasswordConfirmPlaceHolder;

        public static string PasswordLengthError => Resource.PasswordLengthError;

        public static string Email => Resource.Email;

        public static string Logout => Resource.Logout;

        public static string LoginError => Resource.LoginError;

        public static string EmailPlaceHolder => Resource.EmailPlaceHolder;

        public static string EmailError => Resource.EmailError;

        public static string Password => Resource.Password;

        public static string PasswordError => Resource.PasswordError;

        public static string PasswordPlaceHolder => Resource.PasswordPlaceHolder;

        public static string Register => Resource.Register;

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
