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
    public class LobbysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LobbysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Lobbys
        public async Task<IActionResult> Index()
        {
            return View(await _context.Lobbys.ToListAsync());
        }

        // GET: Lobbys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lobbys = await _context.Lobbys
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lobbys == null)
            {
                return NotFound();
            }

            return View(lobbys);
        }

        // GET: Lobbys/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lobbys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreatedOn")] Lobbys lobbys)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lobbys);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lobbys);
        }

        // GET: Lobbys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lobbys = await _context.Lobbys.FindAsync(id);
            if (lobbys == null)
            {
                return NotFound();
            }
            return View(lobbys);
        }

        // POST: Lobbys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CreatedOn")] Lobbys lobbys)
        {
            if (id != lobbys.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lobbys);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LobbysExists(lobbys.Id))
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
            return View(lobbys);
        }

        // GET: Lobbys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lobbys = await _context.Lobbys
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lobbys == null)
            {
                return NotFound();
            }

            return View(lobbys);
        }

        // POST: Lobbys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lobbys = await _context.Lobbys.FindAsync(id);
            if (lobbys != null)
            {
                _context.Lobbys.Remove(lobbys);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LobbysExists(int id)
        {
            return _context.Lobbys.Any(e => e.Id == id);
        }
    }
}
