using Prism;
using Prism.Ioc;
using OpenLibrary.Prism.ViewModels;
using OpenLibrary.Prism.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using OpenLibrary.Common.Services;
using Syncfusion.Licensing;
using OpenLibrary.Common.Helpers;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace OpenLibrary.Prism
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            SyncfusionLicenseProvider.RegisterLicense("MjU4MTgxQDMxMzgyZTMxMmUzMFlLdTJqTUcxTDVxU1BoVlhKemNvRmZOYStRVnR5dzBidE9CRy9tbHB0WUk9");

            InitializeComponent();

            await NavigationService.NavigateAsync("/OpenLibraryMasterDetailPage/NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.Register<IFilesHelper, FilesHelper>();
            containerRegistry.Register<IRegexHelper, RegexHelper>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<OpenLibraryMasterDetailPage, OpenLibraryMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<ModifyUserPage, ModifyUserPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<MyUploadedDocumentsPage, MyUploadedDocumentsPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
            containerRegistry.RegisterForNavigation<RememberPasswordPage, RememberPasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<ChangePasswordPage, ChangePasswordPageViewModel>();
        }
    }
}
