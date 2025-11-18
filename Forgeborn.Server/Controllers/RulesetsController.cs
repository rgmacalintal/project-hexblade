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
    public class RulesetsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RulesetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Rulesets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rulesets>>> GetRulesets()
        {
            return await _context.Rulesets.ToListAsync();
        }

        // GET: api/Rulesets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rulesets>> GetRulesets(int id)
        {
            var rulesets = await _context.Rulesets.FindAsync(id);

            if (rulesets == null)
            {
                return NotFound();
            }

            return rulesets;
        }

        // PUT: api/Rulesets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRulesets(int id, Rulesets rulesets)
        {
            if (id != rulesets.Id)
            {
                return BadRequest();
            }

            _context.Entry(rulesets).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RulesetsExists(id))
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

        // POST: api/Rulesets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rulesets>> PostRulesets(Rulesets rulesets)
        {
            _context.Rulesets.Add(rulesets);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRulesets", new { id = rulesets.Id }, rulesets);
        }

        // DELETE: api/Rulesets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRulesets(int id)
        {
            var rulesets = await _context.Rulesets.FindAsync(id);
            if (rulesets == null)
            {
                return NotFound();
            }

            _context.Rulesets.Remove(rulesets);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RulesetsExists(int id)
        {
            return _context.Rulesets.Any(e => e.Id == id);
        }
    }
}
