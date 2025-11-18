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
    public class MountsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Mounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mounts>>> GetMounts()
        {
            return await _context.Mounts.ToListAsync();
        }

        // GET: api/Mounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mounts>> GetMounts(int id)
        {
            var mounts = await _context.Mounts.FindAsync(id);

            if (mounts == null)
            {
                return NotFound();
            }

            return mounts;
        }

        // PUT: api/Mounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMounts(int id, Mounts mounts)
        {
            if (id != mounts.Id)
            {
                return BadRequest();
            }

            _context.Entry(mounts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MountsExists(id))
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

        // POST: api/Mounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Mounts>> PostMounts(Mounts mounts)
        {
            _context.Mounts.Add(mounts);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMounts", new { id = mounts.Id }, mounts);
        }

        // DELETE: api/Mounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMounts(int id)
        {
            var mounts = await _context.Mounts.FindAsync(id);
            if (mounts == null)
            {
                return NotFound();
            }

            _context.Mounts.Remove(mounts);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MountsExists(int id)
        {
            return _context.Mounts.Any(e => e.Id == id);
        }
    }
}
