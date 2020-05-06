using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenLibrary.Web.Data;
using OpenLibrary.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace OpenLibrary.Web.Controllers
{
    public class TypeOfDocumentsController : Controller
    {
        private readonly DataContext _context;

        public TypeOfDocumentsController(DataContext context)
        {
            _context = context;
        }

        // GET: TypeOfDocuments
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypeOfDocuments.ToListAsync());
        }

        // GET: TypeOfDocuments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TypeOfDocumentEntity typeOfDocumentEntity = await _context.TypeOfDocuments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeOfDocumentEntity == null)
            {
                return NotFound();
            }

            return View(typeOfDocumentEntity);
        }

        // GET: TypeOfDocuments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeOfDocuments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] TypeOfDocumentEntity typeOfDocumentEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeOfDocumentEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeOfDocumentEntity);
        }

        // GET: TypeOfDocuments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TypeOfDocumentEntity typeOfDocumentEntity = await _context.TypeOfDocuments.FindAsync(id);
            if (typeOfDocumentEntity == null)
            {
                return NotFound();
            }
            return View(typeOfDocumentEntity);
        }

        // POST: TypeOfDocuments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] TypeOfDocumentEntity typeOfDocumentEntity)
        {
            if (id != typeOfDocumentEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeOfDocumentEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeOfDocumentEntityExists(typeOfDocumentEntity.Id))
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
            return View(typeOfDocumentEntity);
        }

        // GET: TypeOfDocuments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TypeOfDocumentEntity typeOfDocumentEntity = await _context.TypeOfDocuments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeOfDocumentEntity == null)
            {
                return NotFound();
            }

            return View(typeOfDocumentEntity);
        }

        // POST: TypeOfDocuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            TypeOfDocumentEntity typeOfDocumentEntity = await _context.TypeOfDocuments.FindAsync(id);
            _context.TypeOfDocuments.Remove(typeOfDocumentEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeOfDocumentEntityExists(int id)
        {
            return _context.TypeOfDocuments.Any(e => e.Id == id);
        }
    }
}
