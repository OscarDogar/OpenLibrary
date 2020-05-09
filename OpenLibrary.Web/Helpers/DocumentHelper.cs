using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OpenLibrary.Web.Helpers
{
    public class DocumentHelper : IDocumentHelper
    {
        public async Task<string> UploadDocumentAsync(IFormFile documentFile, string tipo)
        {
            string guid = Guid.NewGuid().ToString();
            string path = "";
            string file = "";
            string retornar = $"~/images/Users/{file}";
            if (tipo == "document") {
                 file = $"{guid}.pdf";
                 path = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    $"wwwroot\\Documents",
                    file);
                retornar = $"~/Documents/{file}";
            }
            else
            {
                 file = $"{guid}.jpg";
                 path = Path.Combine(
                   Directory.GetCurrentDirectory(),
                   $"wwwroot\\images\\Users",
                   file);
                retornar = $"~/images/Users/{file}";
            }
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await documentFile.CopyToAsync(stream);
            }

            return retornar;
        }

    }
}
