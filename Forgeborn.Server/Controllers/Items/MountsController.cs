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
    public class MountsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Mounts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Mounts.ToListAsync());
        }

        // GET: Mounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mounts = await _context.Mounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mounts == null)
            {
                return NotFound();
            }

            return View(mounts);
        }

        // GET: Mounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Mounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Cost,Weight,Source,Rarity,WondrousItem,Attunement,Requirements")] Mounts mounts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mounts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mounts);
        }

        // GET: Mounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mounts = await _context.Mounts.FindAsync(id);
            if (mounts == null)
            {
                return NotFound();
            }
            return View(mounts);
        }

        // POST: Mounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Cost,Weight,Source,Rarity,WondrousItem,Attunement,Requirements")] Mounts mounts)
        {
            if (id != mounts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mounts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MountsExists(mounts.Id))
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
            return View(mounts);
        }

        // GET: Mounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mounts = await _context.Mounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mounts == null)
            {
                return NotFound();
            }

            return View(mounts);
        }

        // POST: Mounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mounts = await _context.Mounts.FindAsync(id);
            if (mounts != null)
            {
                _context.Mounts.Remove(mounts);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MountsExists(int id)
        {
            return _context.Mounts.Any(e => e.Id == id);
        }
    }
}
