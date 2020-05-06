using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenLibrary.Web.Data;
using OpenLibrary.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace OpenLibrary.Web.Controllers
{
    public class DocumentLanguagesController : Controller
    {
        private readonly DataContext _context;

        public DocumentLanguagesController(DataContext context)
        {
            _context = context;
        }

        // GET: DocumentLanguages
        public async Task<IActionResult> Index()
        {
            return View(await _context.DocumentLanguages.ToListAsync());
        }

        // GET: DocumentLanguages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DocumentLanguageEntity documentLanguageEntity = await _context.DocumentLanguages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (documentLanguageEntity == null)
            {
                return NotFound();
            }

            return View(documentLanguageEntity);
        }

        // GET: DocumentLanguages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DocumentLanguages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] DocumentLanguageEntity documentLanguageEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(documentLanguageEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(documentLanguageEntity);
        }

        // GET: DocumentLanguages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DocumentLanguageEntity documentLanguageEntity = await _context.DocumentLanguages.FindAsync(id);
            if (documentLanguageEntity == null)
            {
                return NotFound();
            }
            return View(documentLanguageEntity);
        }

        // POST: DocumentLanguages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] DocumentLanguageEntity documentLanguageEntity)
        {
            if (id != documentLanguageEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(documentLanguageEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentLanguageEntityExists(documentLanguageEntity.Id))
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
            return View(documentLanguageEntity);
        }

        // GET: DocumentLanguages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DocumentLanguageEntity documentLanguageEntity = await _context.DocumentLanguages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (documentLanguageEntity == null)
            {
                return NotFound();
            }

            return View(documentLanguageEntity);
        }

        // POST: DocumentLanguages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            DocumentLanguageEntity documentLanguageEntity = await _context.DocumentLanguages.FindAsync(id);
            _context.DocumentLanguages.Remove(documentLanguageEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentLanguageEntityExists(int id)
        {
            return _context.DocumentLanguages.Any(e => e.Id == id);
        }
    }
}
