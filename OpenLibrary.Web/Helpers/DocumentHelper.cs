using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OpenLibrary.Web.Helpers
{
    public class DocumentHelper : IDocumentHelper
    {
        public async Task<string> UploadDocumentAsync(IFormFile documentFile)
        {
            string guid = Guid.NewGuid().ToString();
            string file = $"{guid}.pdf";
            string path = Path.Combine(
                Directory.GetCurrentDirectory(),
                $"wwwroot\\Documents",
                file);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await documentFile.CopyToAsync(stream);
            }

            return $"~/Documents/{file}";
        }

    }
}
