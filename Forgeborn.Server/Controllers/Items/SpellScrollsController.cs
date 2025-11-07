using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Forgeborn.Server.Data;
using Forgeborn.Server.Models.Items;

namespace Forgeborn.Server.Controllers.Items
{
    public class SpellScrollsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SpellScrollsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SpellScrolls
        public async Task<IActionResult> Index()
        {
            return View(await _context.SpellScrolls.ToListAsync());
        }

        // GET: SpellScrolls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spellScrolls = await _context.SpellScrolls
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spellScrolls == null)
            {
                return NotFound();
            }

            return View(spellScrolls);
        }

        // GET: SpellScrolls/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SpellScrolls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Level,Description,Id,Name,Cost,Weight,Source,Rarity,WondrousItem,Attunement,Requirements")] SpellScrolls spellScrolls)
        {
            if (ModelState.IsValid)
            {
                _context.Add(spellScrolls);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(spellScrolls);
        }

        // GET: SpellScrolls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spellScrolls = await _context.SpellScrolls.FindAsync(id);
            if (spellScrolls == null)
            {
                return NotFound();
            }
            return View(spellScrolls);
        }

        // POST: SpellScrolls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Level,Description,Id,Name,Cost,Weight,Source,Rarity,WondrousItem,Attunement,Requirements")] SpellScrolls spellScrolls)
        {
            if (id != spellScrolls.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(spellScrolls);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpellScrollsExists(spellScrolls.Id))
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
            return View(spellScrolls);
        }

        // GET: SpellScrolls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spellScrolls = await _context.SpellScrolls
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spellScrolls == null)
            {
                return NotFound();
            }

            return View(spellScrolls);
        }

        // POST: SpellScrolls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var spellScrolls = await _context.SpellScrolls.FindAsync(id);
            if (spellScrolls != null)
            {
                _context.SpellScrolls.Remove(spellScrolls);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpellScrollsExists(int id)
        {
            return _context.SpellScrolls.Any(e => e.Id == id);
        }
    }
}
