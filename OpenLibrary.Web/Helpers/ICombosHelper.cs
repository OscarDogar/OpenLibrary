using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenLibrary.Web.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboGenders();

        IEnumerable<SelectListItem> GetComboAuthors();

        IEnumerable<SelectListItem> GetComboTypeOfDocuments();

        IEnumerable<SelectListItem> GetComboDocumentLanguages();
    }

}
