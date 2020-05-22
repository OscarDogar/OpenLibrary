using OpenLibrary.Prism.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenLibrary.Prism.ViewModels
{
    public class MyDocTabbedPageViewModel : ViewModelBase
    {
        public MyDocTabbedPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = Languages.MyUploadedDocuments;
        }
    }
}
