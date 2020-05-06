using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenLibrary.Web.Data;
using OpenLibrary.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace OpenLibrary.Web.Controllers
{
    public class GendersController : Controller
    {
        private readonly DataContext _context;

        public GendersController(DataContext context)
        {
            _context = context;
        }

        // GET: Genders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Genders.ToListAsync());
        }

        // GET: Genders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GenderEntity genderEntity = await _context.Genders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genderEntity == null)
            {
                return NotFound();
            }

            return View(genderEntity);
        }

        // GET: Genders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Genders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] GenderEntity genderEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(genderEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(genderEntity);
        }

        // GET: Genders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GenderEntity genderEntity = await _context.Genders.FindAsync(id);
            if (genderEntity == null)
            {
                return NotFound();
            }
            return View(genderEntity);
        }

        // POST: Genders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] GenderEntity genderEntity)
        {
            if (id != genderEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(genderEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenderEntityExists(genderEntity.Id))
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
            return View(genderEntity);
        }

        // GET: Genders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GenderEntity genderEntity = await _context.Genders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genderEntity == null)
            {
                return NotFound();
            }

            return View(genderEntity);
        }

        // POST: Genders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            GenderEntity genderEntity = await _context.Genders.FindAsync(id);
            _context.Genders.Remove(genderEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GenderEntityExists(int id)
        {
            return _context.Genders.Any(e => e.Id == id);
        }
    }
}
