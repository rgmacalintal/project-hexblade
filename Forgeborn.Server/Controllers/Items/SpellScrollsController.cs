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
    public class SpellScrollsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SpellScrollsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SpellScrolls
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpellScrolls>>> GetSpellScrolls()
        {
            return await _context.SpellScrolls.ToListAsync();
        }

        // GET: api/SpellScrolls/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SpellScrolls>> GetSpellScrolls(int id)
        {
            var spellScrolls = await _context.SpellScrolls.FindAsync(id);

            if (spellScrolls == null)
            {
                return NotFound();
            }

            return spellScrolls;
        }

        // PUT: api/SpellScrolls/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpellScrolls(int id, SpellScrolls spellScrolls)
        {
            if (id != spellScrolls.Id)
            {
                return BadRequest();
            }

            _context.Entry(spellScrolls).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpellScrollsExists(id))
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

        // POST: api/SpellScrolls
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SpellScrolls>> PostSpellScrolls(SpellScrolls spellScrolls)
        {
            _context.SpellScrolls.Add(spellScrolls);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpellScrolls", new { id = spellScrolls.Id }, spellScrolls);
        }

        // DELETE: api/SpellScrolls/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpellScrolls(int id)
        {
            var spellScrolls = await _context.SpellScrolls.FindAsync(id);
            if (spellScrolls == null)
            {
                return NotFound();
            }

            _context.SpellScrolls.Remove(spellScrolls);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpellScrollsExists(int id)
        {
            return _context.SpellScrolls.Any(e => e.Id == id);
        }
    }
}
