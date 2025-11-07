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
    public class WeaponsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WeaponsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Weapons
        public async Task<IActionResult> Index()
        {
            return View(await _context.Weapons.ToListAsync());
        }

        // GET: Weapons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weapons = await _context.Weapons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (weapons == null)
            {
                return NotFound();
            }

            return View(weapons);
        }

        // GET: Weapons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Weapons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Attack,Damage,Id,Name,Cost,Weight,Source,Rarity,WondrousItem,Attunement,Requirements")] Weapons weapons)
        {
            if (ModelState.IsValid)
            {
                _context.Add(weapons);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(weapons);
        }

        // GET: Weapons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weapons = await _context.Weapons.FindAsync(id);
            if (weapons == null)
            {
                return NotFound();
            }
            return View(weapons);
        }

        // POST: Weapons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Attack,Damage,Id,Name,Cost,Weight,Source,Rarity,WondrousItem,Attunement,Requirements")] Weapons weapons)
        {
            if (id != weapons.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(weapons);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeaponsExists(weapons.Id))
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
            return View(weapons);
        }

        // GET: Weapons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weapons = await _context.Weapons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (weapons == null)
            {
                return NotFound();
            }

            return View(weapons);
        }

        // POST: Weapons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var weapons = await _context.Weapons.FindAsync(id);
            if (weapons != null)
            {
                _context.Weapons.Remove(weapons);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeaponsExists(int id)
        {
            return _context.Weapons.Any(e => e.Id == id);
        }
    }
}
