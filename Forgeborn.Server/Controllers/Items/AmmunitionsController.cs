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
    public class AmmunitionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AmmunitionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Ammunitions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ammunitions>>> GetAmmunitions()
        {
            return await _context.Ammunitions.ToListAsync();
        }

        // GET: api/Ammunitions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ammunitions>> GetAmmunitions(int id)
        {
            var ammunitions = await _context.Ammunitions.FindAsync(id);

            if (ammunitions == null)
            {
                return NotFound();
            }

            return ammunitions;
        }

        // PUT: api/Ammunitions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAmmunitions(int id, Ammunitions ammunitions)
        {
            if (id != ammunitions.Id)
            {
                return BadRequest();
            }

            _context.Entry(ammunitions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AmmunitionsExists(id))
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

        // POST: api/Ammunitions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ammunitions>> PostAmmunitions(Ammunitions ammunitions)
        {
            _context.Ammunitions.Add(ammunitions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAmmunitions", new { id = ammunitions.Id }, ammunitions);
        }

        // DELETE: api/Ammunitions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAmmunitions(int id)
        {
            var ammunitions = await _context.Ammunitions.FindAsync(id);
            if (ammunitions == null)
            {
                return NotFound();
            }

            _context.Ammunitions.Remove(ammunitions);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AmmunitionsExists(int id)
        {
            return _context.Ammunitions.Any(e => e.Id == id);
        }
    }
}
