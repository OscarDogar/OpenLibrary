using Microsoft.CodeAnalysis;
using OpenLibrary.Web.Data;
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
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(DataContext context,ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
        }
        public async Task<DocumentEntity> ToDocumentEntity(DocumentViewModel model, string path, bool isNew)
        {
            return new DocumentEntity
            {
                Id = isNew ? 0 : model.Id,
                DocumentPath = path,
                Title = model.Title,
                Date = model.Date.ToUniversalTime(),
                PagesNumber = model.PagesNumber,
                Summary = model.Summary,
                Gender = await _context.Genders.FindAsync(model.GenderId),
                Author = await _context.Authors.FindAsync(model.AuthorId),
                TypeOfDocument = await _context.TypeOfDocuments.FindAsync(model.TypeOfDocumentId),
                DocumentLanguage = await _context.DocumentLanguages.FindAsync(model.DocumentLanguageId),
            };
        }

        public DocumentViewModel ToDocumentViewModel(DocumentEntity documentEntity)
        {
            return new DocumentViewModel
            {
                Id = documentEntity.Id,
                DocumentPath = documentEntity.DocumentPath,
                Title = documentEntity.Title,
                Date = documentEntity.Date.ToLocalTime(),
                PagesNumber = documentEntity.PagesNumber,
                Summary = documentEntity.Summary,
                Gender = documentEntity.Gender,
                GenderId = documentEntity.Gender.Id,
                Genders = _combosHelper.GetComboGenders(),
                Author = documentEntity.Author,
                AuthorId = documentEntity.Author.Id,
                Authors = _combosHelper.GetComboAuthors(),
                TypeOfDocument = documentEntity.TypeOfDocument,
                TypeOfDocumentId = documentEntity.TypeOfDocument.Id,
                TypeOfDocuments = _combosHelper.GetComboTypeOfDocuments(),
                DocumentLanguage = documentEntity.DocumentLanguage,
                DocumentLanguageId = documentEntity.DocumentLanguage.Id,
                DocumentLanguages = _combosHelper.GetComboDocumentLanguages(),
            };
        }

    }
}    

