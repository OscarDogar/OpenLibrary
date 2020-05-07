using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace OpenLibrary.Web.Helpers
{
    public interface IDocumentHelper
    {
        Task<string> UploadDocumentAsync(IFormFile documentFile);
    }
}
