using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Forgeborn.Server.Data;
using Forgeborn.Server.Models;

namespace Forgeborn.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterRulesetsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CharacterRulesetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CharacterRulesets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterRulesets>>> GetCharacterRulesets()
        {
            return await _context.CharacterRulesets.ToListAsync();
        }

        // GET: api/CharacterRulesets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterRulesets>> GetCharacterRulesets(int id)
        {
            var characterRulesets = await _context.CharacterRulesets.FindAsync(id);

            if (characterRulesets == null)
            {
                return NotFound();
            }

            return characterRulesets;
        }

        // PUT: api/CharacterRulesets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacterRulesets(int id, CharacterRulesets characterRulesets)
        {
            if (id != characterRulesets.Id)
            {
                return BadRequest();
            }

            _context.Entry(characterRulesets).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterRulesetsExists(id))
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

        // POST: api/CharacterRulesets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CharacterRulesets>> PostCharacterRulesets(CharacterRulesets characterRulesets)
        {
            _context.CharacterRulesets.Add(characterRulesets);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCharacterRulesets", new { id = characterRulesets.Id }, characterRulesets);
        }

        // DELETE: api/CharacterRulesets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacterRulesets(int id)
        {
            var characterRulesets = await _context.CharacterRulesets.FindAsync(id);
            if (characterRulesets == null)
            {
                return NotFound();
            }

            _context.CharacterRulesets.Remove(characterRulesets);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CharacterRulesetsExists(int id)
        {
            return _context.CharacterRulesets.Any(e => e.Id == id);
        }
    }
}
