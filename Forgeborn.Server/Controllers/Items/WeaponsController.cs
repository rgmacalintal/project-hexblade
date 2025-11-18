using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Forgeborn.Server.Data;
using Forgeborn.Server.Models.Items;

namespace Forgeborn.Server.Controllers.Items
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeaponsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WeaponsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Weapons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Weapons>>> GetWeapons()
        {
            return await _context.Weapons.ToListAsync();
        }

        // GET: api/Weapons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Weapons>> GetWeapons(int id)
        {
            var weapons = await _context.Weapons.FindAsync(id);

            if (weapons == null)
            {
                return NotFound();
            }

            return weapons;
        }

        // PUT: api/Weapons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeapons(int id, Weapons weapons)
        {
            if (id != weapons.Id)
            {
                return BadRequest();
            }

            _context.Entry(weapons).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeaponsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Weapons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Weapons>> PostWeapons(Weapons weapons)
        {
            _context.Weapons.Add(weapons);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWeapons", new { id = weapons.Id }, weapons);
        }

        // DELETE: api/Weapons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeapons(int id)
        {
            var weapons = await _context.Weapons.FindAsync(id);
            if (weapons == null)
            {
                return NotFound();
            }

            _context.Weapons.Remove(weapons);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WeaponsExists(int id)
        {
            return _context.Weapons.Any(e => e.Id == id);
        }
    }
}
