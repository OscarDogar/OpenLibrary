using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenLibrary.Web.Data;
using OpenLibrary.Web.Data.Entities;
using OpenLibrary.Web.Helpers;
using OpenLibrary.Web.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OpenLibrary.Web.Controllers
{
    [Authorize]
    public class DocumentsController : Controller
    {
        private readonly DataContext _context;
        private readonly IDocumentHelper _documentHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IUserHelper _userHelper;

        public DocumentsController(DataContext context,
                                   IDocumentHelper documentHelper,
                                   IConverterHelper converterHelper, 
                                   ICombosHelper combosHelper,
                                   IUserHelper userHelper)
        {
            _context = context;
            _documentHelper = documentHelper;
            _converterHelper = converterHelper;
            _combosHelper = combosHelper;
            _userHelper = userHelper;
        }

        // GET: Documents
        public async Task<IActionResult> Index()
        {
            return View(await _context.Documents.Include(g => g.Gender)
                                                .Include(g => g.TypeOfDocument)
                                                .Include(g => g.Author)
                                                .Include(g => g.DocumentLanguage)
                                                .Include(g => g.User)
                                                .ToListAsync());
        }
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Details(int? id)
        {
            return View(await _context.Documents.Include(tt => tt.Reviews).ThenInclude(u => u.User).FirstOrDefaultAsync(td => td.Id == id));
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddReview(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentEntity = await _context.Documents.FindAsync(id);
            var user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (documentEntity == null || user == null)
            {
                return NotFound();
            }

            var model = new ReviewViewModel
            {
                User = user,
                Document = documentEntity,
                DocumentId = documentEntity.Id
            };

            return View(model);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReview(ReviewViewModel model)
        {
            model.User = _context.Users.FirstOrDefault(t => t.Email == User.Identity.Name);
            if (ModelState.IsValid)
            {
                var reviewEntity = await _converterHelper.ToReviewEntityAsync(model, true);
                _context.Reviews.Add(reviewEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction($"{nameof(Details)}/{model.DocumentId}");
            }

            return View(model);
        }


        [Authorize(Roles = "User")]
        public IActionResult Create()
        {
            var model = new DocumentViewModel
            {
                Genders = _combosHelper.GetComboGenders(),
                Authors = _combosHelper.GetComboAuthors(),
                TypeOfDocuments = _combosHelper.GetComboTypeOfDocuments(),
                DocumentLanguages = _combosHelper.GetComboDocumentLanguages(),
                Date = DateTime.Now
            };
            return View(model);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DocumentViewModel documentViewModel)
        {
            if (ModelState.IsValid)
            {
                string path = string.Empty;

                if (documentViewModel.DocumentFile != null)
                {
                    path = await _documentHelper.UploadDocumentAsync(documentViewModel.DocumentFile,"document");
                }
                DocumentEntity document = await _converterHelper.ToDocumentEntity(documentViewModel, path, true);
                _context.Add(document);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            documentViewModel.Genders = _combosHelper.GetComboGenders();
            documentViewModel.Authors = _combosHelper.GetComboAuthors();
            documentViewModel.TypeOfDocuments = _combosHelper.GetComboTypeOfDocuments();
            documentViewModel.DocumentLanguages = _combosHelper.GetComboDocumentLanguages();

            return View(documentViewModel);
        }

        [Authorize(Roles = "BookAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DocumentEntity documentEntity = await _context.Documents.Include(d => d.Gender)
                                                                    .Include(d => d.DocumentLanguage)
                                                                    .Include(d => d.TypeOfDocument)
                                                                    .Include(d => d.Author)
                                                                    .Where(d => d.Id ==id )
                                                                    .FirstOrDefaultAsync();
            if (documentEntity == null)
            {
                return NotFound();
            }
            DocumentViewModel documentViewModel = _converterHelper.ToDocumentViewModel(documentEntity);
            return View(documentViewModel);
        }

        [Authorize(Roles = "BookAdmin")]
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
                        path = await _documentHelper.UploadDocumentAsync(documentViewModel.DocumentFile,"document");
                    }
                    DocumentEntity document = await _converterHelper.ToDocumentEntity(documentViewModel, path, false);
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

        [Authorize(Roles = "BookAdmin")]
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
