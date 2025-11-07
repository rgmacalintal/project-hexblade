using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Forgeborn.Server.Data;
using Forgeborn.Server.Models.Items;

namespace Forgeborn.Server.Controllers
{
    public class AmmunitionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AmmunitionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ammunitions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ammunitions.ToListAsync());
        }

        // GET: Ammunitions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ammunitions = await _context.Ammunitions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ammunitions == null)
            {
                return NotFound();
            }

            return View(ammunitions);
        }

        // GET: Ammunitions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ammunitions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Effect,Id,Name,Cost,Weight,Source,Rarity,WondrousItem,Attunement,Requirements")] Ammunitions ammunitions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ammunitions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ammunitions);
        }

        // GET: Ammunitions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ammunitions = await _context.Ammunitions.FindAsync(id);
            if (ammunitions == null)
            {
                return NotFound();
            }
            return View(ammunitions);
        }

        // POST: Ammunitions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Effect,Id,Name,Cost,Weight,Source,Rarity,WondrousItem,Attunement,Requirements")] Ammunitions ammunitions)
        {
            if (id != ammunitions.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ammunitions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AmmunitionsExists(ammunitions.Id))
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
            return View(ammunitions);
        }

        // GET: Ammunitions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ammunitions = await _context.Ammunitions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ammunitions == null)
            {
                return NotFound();
            }

            return View(ammunitions);
        }

        // POST: Ammunitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ammunitions = await _context.Ammunitions.FindAsync(id);
            if (ammunitions != null)
            {
                _context.Ammunitions.Remove(ammunitions);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AmmunitionsExists(int id)
        {
            return _context.Ammunitions.Any(e => e.Id == id);
        }
    }
}
