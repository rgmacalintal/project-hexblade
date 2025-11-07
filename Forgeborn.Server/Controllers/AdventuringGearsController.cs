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
    public class AdventuringGearsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdventuringGearsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdventuringGears
        public async Task<IActionResult> Index()
        {
            return View(await _context.AdventuringGears.ToListAsync());
        }

        // GET: AdventuringGears/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adventuringGears = await _context.AdventuringGears
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adventuringGears == null)
            {
                return NotFound();
            }

            return View(adventuringGears);
        }

        // GET: AdventuringGears/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdventuringGears/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,Id,Name,Cost,Weight,Source,Rarity,WondrousItem,Attunement,Requirements")] AdventuringGears adventuringGears)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adventuringGears);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adventuringGears);
        }

        // GET: AdventuringGears/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adventuringGears = await _context.AdventuringGears.FindAsync(id);
            if (adventuringGears == null)
            {
                return NotFound();
            }
            return View(adventuringGears);
        }

        // POST: AdventuringGears/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Description,Id,Name,Cost,Weight,Source,Rarity,WondrousItem,Attunement,Requirements")] AdventuringGears adventuringGears)
        {
            if (id != adventuringGears.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adventuringGears);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdventuringGearsExists(adventuringGears.Id))
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
            return View(adventuringGears);
        }

        // GET: AdventuringGears/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adventuringGears = await _context.AdventuringGears
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adventuringGears == null)
            {
                return NotFound();
            }

            return View(adventuringGears);
        }

        // POST: AdventuringGears/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adventuringGears = await _context.AdventuringGears.FindAsync(id);
            if (adventuringGears != null)
            {
                _context.AdventuringGears.Remove(adventuringGears);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdventuringGearsExists(int id)
        {
            return _context.AdventuringGears.Any(e => e.Id == id);
        }
    }
}
