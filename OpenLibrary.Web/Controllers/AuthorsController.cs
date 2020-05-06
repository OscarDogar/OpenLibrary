using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenLibrary.Web.Data;
using OpenLibrary.Web.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OpenLibrary.Web.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly DataContext _context;

        public AuthorsController(DataContext context)
        {
            _context = context;
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Authors.ToListAsync());
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuthorEntity authorEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(authorEntity);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty,"There is already an author with that name");
                    }
                }
            }
            return View(authorEntity);
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AuthorEntity authorEntity = await _context.Authors.FindAsync(id);
            if (authorEntity == null)
            {
                return NotFound();
            }
            return View(authorEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AuthorEntity authorEntity)
        {
            if (id != authorEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                _context.Update(authorEntity);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There is already an author with that name");
                    }
                }
            }
            return View(authorEntity);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AuthorEntity authorEntity = await _context.Authors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (authorEntity == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(authorEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool AuthorEntityExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}
