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

        public static string More => Resource.More;

        public static string Create => Resource.Create;

        public static string Update => Resource.Update;

        public static string Favorite => Resource.Favorite;

        public static string MakeAComment => Resource.MakeAComment;

        public static string EditComment => Resource.EditComment;

        public static string YouEnterAComment => Resource.YouEnterAComment;

        public static string ValidNumberValue => Resource.ValidNumberValue;

        public static string Edit => Resource.Edit;

        public static string Delete => Resource.Delete;

        public static string MyFavorite => Resource.MyFavorite;

        public static string MyComment => Resource.MyComment;

        public static string DocP => Resource.DocP;

        public static string DocA => Resource.DocA;

        public static string Detail => Resource.Detail;

        public static string Rating => Resource.Rating;

        public static string Comment => Resource.Comment;

        public static string Comments => Resource.Comments;

        public static string MakeComment => Resource.MakeComment;

        public static string Viewpdf => Resource.Viewpdf;

        public static string PagesNumber => Resource.PagesNumber;

        public static string ErrorLanguages => Resource.ErrorLanguages;

        public static string ErrorTypeOfDocument => Resource.ErrorTypeOfDocument;

        public static string ErrorGender => Resource.ErrorGender;

        public static string ErrorConnection => Resource.ErrorConnection;

        public static string ConfirmNewPassword => Resource.ConfirmNewPassword;

        public static string ConfirmNewPasswordError => Resource.ConfirmNewPasswordError;

        public static string ConfirmNewPasswordError2 => Resource.ConfirmNewPasswordError2;

        public static string ConfirmNewPasswordPlaceHolder => Resource.ConfirmNewPasswordPlaceHolder;

        public static string CurrentPassword => Resource.CurrentPassword;

        public static string CurrentPasswordError => Resource.CurrentPasswordError;

        public static string CurrentPasswordPlaceHolder => Resource.CurrentPasswordPlaceHolder;

        public static string NewPassword => Resource.NewPassword;

        public static string NewPasswordError => Resource.NewPasswordError;

        public static string NewPasswordPlaceHolder => Resource.NewPasswordPlaceHolder;

        public static string UserUpdated => Resource.UserUpdated;

        public static string PasswordRecover => Resource.PasswordRecover;

        public static string Save => Resource.Save;

        public static string ChangePassword => Resource.ChangePassword;

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
