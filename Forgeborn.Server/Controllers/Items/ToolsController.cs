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
    public class ToolsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ToolsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Tools
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tools>>> GetTools()
        {
            return await _context.Tools.ToListAsync();
        }

        // GET: api/Tools/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tools>> GetTools(int id)
        {
            var tools = await _context.Tools.FindAsync(id);

            if (tools == null)
            {
                return NotFound();
            }

            return tools;
        }

        // PUT: api/Tools/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTools(int id, Tools tools)
        {
            if (id != tools.Id)
            {
                return BadRequest();
            }

            _context.Entry(tools).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToolsExists(id))
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

        // POST: api/Tools
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tools>> PostTools(Tools tools)
        {
            _context.Tools.Add(tools);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTools", new { id = tools.Id }, tools);
        }

        // DELETE: api/Tools/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTools(int id)
        {
            var tools = await _context.Tools.FindAsync(id);
            if (tools == null)
            {
                return NotFound();
            }

            _context.Tools.Remove(tools);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ToolsExists(int id)
        {
            return _context.Tools.Any(e => e.Id == id);
        }
    }
}
