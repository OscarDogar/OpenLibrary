using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenLibrary.Web.Data;
using OpenLibrary.Web.Data.Entities;

namespace OpenLibrary.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly DataContext _context;

        public LanguageController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Language
        [HttpGet]
        public IEnumerable<DocumentLanguageEntity> GetDocumentLanguages()
        {
            return _context.DocumentLanguages;
        }

        
    }
}