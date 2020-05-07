using Microsoft.AspNetCore.Mvc.Rendering;
using OpenLibrary.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenLibrary.Web.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;

        public CombosHelper(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetComboGenders()
        {
            List<SelectListItem> list = _context.Genders.Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = $"{t.Id}"
            })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a gender...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboAuthors()
        {
            List<SelectListItem> list = _context.Authors.Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = $"{t.Id}"
            })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select an author...]",
                Value = "0"
            });

            return list;
        }
        public IEnumerable<SelectListItem> GetComboTypeOfDocuments()
        {
            List<SelectListItem> list = _context.TypeOfDocuments.Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = $"{t.Id}"
            })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a Type of document...]",
                Value = "0"
            });

            return list;
        }
        
            public IEnumerable<SelectListItem> GetComboDocumentLanguages()
        {
            List<SelectListItem> list = _context.DocumentLanguages.Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = $"{t.Id}"
            })
                .OrderBy(t => t.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a Language for the document...]",
                Value = "0"
            });

            return list;
        }
    }

}
