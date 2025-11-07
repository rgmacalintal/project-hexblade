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
    public class ArmorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArmorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Armors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Armors.ToListAsync());
        }

        // GET: Armors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var armors = await _context.Armors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (armors == null)
            {
                return NotFound();
            }

            return View(armors);
        }

        // GET: Armors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Armors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Effect,ArmorClass,Id,Name,Cost,Weight,Source,Rarity,WondrousItem,Attunement,Requirements")] Armors armors)
        {
            if (ModelState.IsValid)
            {
                _context.Add(armors);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(armors);
        }

        // GET: Armors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var armors = await _context.Armors.FindAsync(id);
            if (armors == null)
            {
                return NotFound();
            }
            return View(armors);
        }

        // POST: Armors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Effect,ArmorClass,Id,Name,Cost,Weight,Source,Rarity,WondrousItem,Attunement,Requirements")] Armors armors)
        {
            if (id != armors.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(armors);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArmorsExists(armors.Id))
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
            return View(armors);
        }

        // GET: Armors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var armors = await _context.Armors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (armors == null)
            {
                return NotFound();
            }

            return View(armors);
        }

        // POST: Armors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var armors = await _context.Armors.FindAsync(id);
            if (armors != null)
            {
                _context.Armors.Remove(armors);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArmorsExists(int id)
        {
            return _context.Armors.Any(e => e.Id == id);
        }
    }
}
