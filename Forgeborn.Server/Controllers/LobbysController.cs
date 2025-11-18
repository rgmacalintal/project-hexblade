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
    public class LobbysController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LobbysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Lobbys
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lobbys>>> GetLobbys()
        {
            return await _context.Lobbys
            .Include(l => l.Players)
                .ThenInclude(p => p.Character)
            .ToListAsync();
        }

        // GET: api/Lobbys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lobbys>> GetLobbys(int id)
        {
            //var lobbys = await _context.Lobbys.FindAsync(id);
            var lobbys = await _context.Lobbys
            .Include(l => l.Players)
                .ThenInclude(p => p.Character)
            .FirstOrDefaultAsync(l => l.Id == id);

            if (lobbys == null)
            {
                return NotFound();
            }

            return lobbys;
        }

        // PUT: api/Lobbys/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLobbys(int id, Lobbys lobbys)
        {
            if (id != lobbys.Id)
            {
                return BadRequest();
            }

            _context.Entry(lobbys).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LobbysExists(id))
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

        // POST: api/Lobbys
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Lobbys>> PostLobbys(Lobbys lobbys)
        {
            _context.Lobbys.Add(lobbys);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLobbys", new { id = lobbys.Id }, lobbys);
        }

        // DELETE: api/Lobbys/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLobbys(int id)
        {
            var lobbys = await _context.Lobbys.FindAsync(id);
            if (lobbys == null)
            {
                return NotFound();
            }

            _context.Lobbys.Remove(lobbys);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LobbysExists(int id)
        {
            return _context.Lobbys.Any(e => e.Id == id);
        }
    }
}
