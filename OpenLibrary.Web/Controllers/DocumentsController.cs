using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenLibrary.Web.Data;
using OpenLibrary.Web.Data.Entities;
using OpenLibrary.Web.Helpers;
using OpenLibrary.Web.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OpenLibrary.Web.Controllers
{
    public class DocumentsController : Controller
    {
        private readonly DataContext _context;
        private readonly IDocumentHelper _documentHelper;
        private readonly IConverterHelper _converterHelper;

        public DocumentsController(DataContext context, IDocumentHelper documentHelper, IConverterHelper converterHelper)
        {
            _context = context;
            _documentHelper = documentHelper;
            _converterHelper = converterHelper;
        }

        // GET: Documents
        public async Task<IActionResult> Index()
        {
            return View(await _context.Documents.ToListAsync());
        }

        // GET: Documents/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DocumentViewModel documentViewModel)
        {
            if (ModelState.IsValid)
            {
                string path = string.Empty;

                if (documentViewModel.DocumentFile != null)
                {
                    path = await _documentHelper.UploadDocumentAsync(documentViewModel.DocumentFile);
                }

                DocumentEntity document = _converterHelper.ToDocumentEntity(documentViewModel, path, true);
                _context.Add(document);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(documentViewModel);
        }

        // GET: Documents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DocumentEntity documentEntity = await _context.Documents.FindAsync(id);
            if (documentEntity == null)
            {
                return NotFound();
            }
            DocumentViewModel documentViewModel = _converterHelper.ToDocumentViewModel(documentEntity);
            return View(documentViewModel);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DocumentViewModel documentViewModel)
        {
            if (id != documentViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string path = documentViewModel.DocumentPath;

                    if (documentViewModel.DocumentFile != null)
                    {
                        path = await _documentHelper.UploadDocumentAsync(documentViewModel.DocumentFile);
                    }
                    DocumentEntity document = _converterHelper.ToDocumentEntity(documentViewModel, path, false);
                    _context.Update(document);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentEntityExists(documentViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(documentViewModel);
        }

        // GET: Documents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DocumentEntity documentEntity = await _context.Documents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (documentEntity == null)
            {
                return NotFound();
            }

            _context.Documents.Remove(documentEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentEntityExists(int id)
        {
            return _context.Documents.Any(e => e.Id == id);
        }
    }
}
