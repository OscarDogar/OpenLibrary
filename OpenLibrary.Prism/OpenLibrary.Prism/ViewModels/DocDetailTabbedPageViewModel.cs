using OpenLibrary.Prism.Helpers;
using Prism.Navigation;

namespace OpenLibrary.Prism.ViewModels
{
    public class DocDetailTabbedPageViewModel : ViewModelBase
    {
        public DocDetailTabbedPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = Languages.More;
        }
    }
}
