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
    public class ArmorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ArmorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Armors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Armors>>> GetArmors()
        {
            return await _context.Armors.ToListAsync();
        }

        // GET: api/Armors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Armors>> GetArmors(int id)
        {
            var armors = await _context.Armors.FindAsync(id);

            if (armors == null)
            {
                return NotFound();
            }

            return armors;
        }

        // PUT: api/Armors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArmors(int id, Armors armors)
        {
            if (id != armors.Id)
            {
                return BadRequest();
            }

            _context.Entry(armors).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArmorsExists(id))
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

        // POST: api/Armors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Armors>> PostArmors(Armors armors)
        {
            _context.Armors.Add(armors);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArmors", new { id = armors.Id }, armors);
        }

        // DELETE: api/Armors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArmors(int id)
        {
            var armors = await _context.Armors.FindAsync(id);
            if (armors == null)
            {
                return NotFound();
            }

            _context.Armors.Remove(armors);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArmorsExists(int id)
        {
            return _context.Armors.Any(e => e.Id == id);
        }
    }
}
