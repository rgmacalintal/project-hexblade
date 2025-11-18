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
    public class AdventuringGearsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdventuringGearsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AdventuringGears
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdventuringGears>>> GetAdventuringGears()
        {
            return await _context.AdventuringGears.ToListAsync();
        }

        // GET: api/AdventuringGears/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdventuringGears>> GetAdventuringGears(int id)
        {
            var adventuringGears = await _context.AdventuringGears.FindAsync(id);

            if (adventuringGears == null)
            {
                return NotFound();
            }

            return adventuringGears;
        }

        // PUT: api/AdventuringGears/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdventuringGears(int id, AdventuringGears adventuringGears)
        {
            if (id != adventuringGears.Id)
            {
                return BadRequest();
            }

            _context.Entry(adventuringGears).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdventuringGearsExists(id))
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

        // POST: api/AdventuringGears
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AdventuringGears>> PostAdventuringGears(AdventuringGears adventuringGears)
        {
            _context.AdventuringGears.Add(adventuringGears);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdventuringGears", new { id = adventuringGears.Id }, adventuringGears);
        }

        // DELETE: api/AdventuringGears/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdventuringGears(int id)
        {
            var adventuringGears = await _context.AdventuringGears.FindAsync(id);
            if (adventuringGears == null)
            {
                return NotFound();
            }

            _context.AdventuringGears.Remove(adventuringGears);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdventuringGearsExists(int id)
        {
            return _context.AdventuringGears.Any(e => e.Id == id);
        }
    }
}
