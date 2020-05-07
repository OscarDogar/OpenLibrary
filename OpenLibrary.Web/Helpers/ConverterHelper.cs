using Microsoft.CodeAnalysis;
using OpenLibrary.Web.Data.Entities;
using OpenLibrary.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenLibrary.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public DocumentEntity ToDocumentEntity(DocumentViewModel model, string path, bool isNew)
        {
            return new DocumentEntity
            {
                Id = isNew ? 0 : model.Id,
                DocumentPath = path,
                Title = model.Title,
                Date = model.Date,
                PagesNumber = model.PagesNumber,
                Summary = model.Summary
            };
        }

        public DocumentViewModel ToDocumentViewModel(DocumentEntity teamEntity)
        {
            return new DocumentViewModel
            {
                Id = teamEntity.Id,
                DocumentPath = teamEntity.DocumentPath,
                Title = teamEntity.Title,
                Date = teamEntity.Date,
                PagesNumber = teamEntity.PagesNumber,
                Summary = teamEntity.Summary
            };
        }

    }
}    

