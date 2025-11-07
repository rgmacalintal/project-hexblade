using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Forgeborn.Server.Data;
using Forgeborn.Server.Models;

namespace Forgeborn.Server.Controllers
{
    public class RulesetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RulesetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rulesets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Rulesets.Include(r => r.CreatedBy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Rulesets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rulesets = await _context.Rulesets
                .Include(r => r.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rulesets == null)
            {
                return NotFound();
            }

            return View(rulesets);
        }

        // GET: Rulesets/Create
        public IActionResult Create()
        {
            ViewData["CharacterId"] = new SelectList(_context.Characters, "Id", "Class");
            return View();
        }

        // POST: Rulesets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,CreatedOn,CharacterId")] Rulesets rulesets)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rulesets);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CharacterId"] = new SelectList(_context.Characters, "Id", "Class", rulesets.CharacterId);
            return View(rulesets);
        }

        // GET: Rulesets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rulesets = await _context.Rulesets.FindAsync(id);
            if (rulesets == null)
            {
                return NotFound();
            }
            ViewData["CharacterId"] = new SelectList(_context.Characters, "Id", "Class", rulesets.CharacterId);
            return View(rulesets);
        }

        // POST: Rulesets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,CreatedOn,CharacterId")] Rulesets rulesets)
        {
            if (id != rulesets.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rulesets);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RulesetsExists(rulesets.Id))
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
            ViewData["CharacterId"] = new SelectList(_context.Characters, "Id", "Class", rulesets.CharacterId);
            return View(rulesets);
        }

        // GET: Rulesets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rulesets = await _context.Rulesets
                .Include(r => r.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rulesets == null)
            {
                return NotFound();
            }

            return View(rulesets);
        }

        // POST: Rulesets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rulesets = await _context.Rulesets.FindAsync(id);
            if (rulesets != null)
            {
                _context.Rulesets.Remove(rulesets);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RulesetsExists(int id)
        {
            return _context.Rulesets.Any(e => e.Id == id);
        }
    }
}
