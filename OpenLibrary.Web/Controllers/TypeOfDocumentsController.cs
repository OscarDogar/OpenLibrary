using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenLibrary.Web.Data;
using OpenLibrary.Web.Data.Entities;
using System;
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

        // GET: TypeOfDocuments/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] TypeOfDocumentEntity typeOfDocumentEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeOfDocumentEntity);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There is already that type of document");
                    }
                }
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
               
                    _context.Update(typeOfDocumentEntity);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There is already that type of document");
                    }
                }
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
