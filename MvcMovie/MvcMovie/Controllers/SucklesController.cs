using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class SucklesController : Controller
    {
        private readonly MvcMovieContext _context;

        public SucklesController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Suckles
        public async Task<IActionResult> Index(string suckleSide, string searchString)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> sideQuery = from m in _context.Suckle
                                            orderby m.Side
                                            select m.Side;

            var suckles = from m in _context.Suckle
                         select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                suckles = suckles.Where(s => s.Side.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(suckleSide))
            {
                suckles = suckles.Where(x => x.Side == suckleSide);
            }

            var suckleSideVM = new SuckleSideViewModel
            {
                Side = new SelectList(await sideQuery.Distinct().ToListAsync()),
                Suckles = await suckles.ToListAsync()
            };

            return View(suckleSideVM);
        }

        // GET: Suckles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suckle = await _context.Suckle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suckle == null)
            {
                return NotFound();
            }

            return View(suckle);
        }

        // GET: Suckles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suckles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SuckleTime,Side")] Suckle suckle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(suckle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(suckle);
        }

        // GET: Suckles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suckle = await _context.Suckle.FindAsync(id);
            if (suckle == null)
            {
                return NotFound();
            }
            return View(suckle);
        }

        // POST: Suckles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SuckleTime,Side")] Suckle suckle)
        {
            if (id != suckle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(suckle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuckleExists(suckle.Id))
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
            return View(suckle);
        }

        // GET: Suckles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suckle = await _context.Suckle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suckle == null)
            {
                return NotFound();
            }

            return View(suckle);
        }

        // POST: Suckles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var suckle = await _context.Suckle.FindAsync(id);
            _context.Suckle.Remove(suckle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SuckleExists(int id)
        {
            return _context.Suckle.Any(e => e.Id == id);
        }
    }
}
