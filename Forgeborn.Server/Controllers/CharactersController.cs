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
    public class CharactersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CharacterService _characterService;

        public CharactersController(ApplicationDbContext context, CharacterService characterService)
        {
            _context = context;
            _characterService = characterService;
        }

        // GET: Characters
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Characters.Include(c => c.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Characters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var characters = await _context.Characters
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (characters == null)
            {
                return NotFound();
            }

            return View(characters);
        }

        // GET: Characters/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
            return View();
        }

        // POST: Characters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Class,Race,Strength,Dexterity,Constitution,Intelligence,Wisdom,Charisma,Inventory,Background,Journal,CreatedOn,UserId")] Characters characters)
        {
            if (ModelState.IsValid)
            {
                _context.Add(characters);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", characters.UserId);
            return View(characters);
        }

        // GET: Characters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var characters = await _context.Characters.FindAsync(id);
            if (characters == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", characters.UserId);
            return View(characters);
        }

        // POST: Characters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Class,Race,Strength,Dexterity,Constitution,Intelligence,Wisdom,Charisma,Inventory,Background,Journal,CreatedOn,UserId")] Characters characters)
        {
            if (id != characters.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(characters);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CharactersExists(characters.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", characters.UserId);
            return View(characters);
        }

        // GET: Characters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var characters = await _context.Characters
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (characters == null)
            {
                return NotFound();
            }

            return View(characters);
        }

        // POST: Characters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var characters = await _context.Characters.FindAsync(id);
            if (characters != null)
            {
                _context.Characters.Remove(characters);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CharactersExists(int id)
        {
            return _context.Characters.Any(e => e.Id == id);
        }

        [HttpPost("{id}/damage")]
        public async Task<IActionResult> ApplyDamage(int id, [FromBody] string dice)
        {
            var newHP = await _characterService.ApplyDamageAsync(id, dice);
            return Ok(new { currentHP = newHP });
        }

        [HttpPost("{id}/heal")]
        public async Task<IActionResult> Heal(int id, [FromBody] string dice)
        {
            var newHP = await _characterService.HealAsync(id, dice);
            return Ok(new { currentHP = newHP });
        }

    }
}
