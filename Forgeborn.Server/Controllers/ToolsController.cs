using Forgeborn.Server.Models.Items;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forgeborn.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToolsController : ControllerBase
    {
        // In-memory list
        private static List<Tools> Tool = new List<Tools>();

        // GET: api/Tools
        [HttpGet]
        public ActionResult<IEnumerable<Tools>> Get()
        {
            return Ok(Tool);
        }

        // GET: api/Tools/{id}
        [HttpGet("{id}")]
        public ActionResult<Tools> Get(int id)
        {
            var tool = Tool.FirstOrDefault(u => u.Id == id);
            if (tool == null) return NotFound();
            return Ok(tool);
        }

        // POST: api/Tools
        [HttpPost]
        public ActionResult<Tools> Create(Tools tool)
        {
            tool.Id = Tool.Count > 0 ? Tool.Max(u => u.Id) + 1 : 1;
            Tool.Add(tool);
            return CreatedAtAction(nameof(Get), new { id = tool.Id }, tool);
        }

        // PUT: api/Tools/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, Tools updatedTool)
        {
            var tool = Tool.FirstOrDefault(u => u.Id == id);
            if (tool == null) return NotFound();

            tool.Name = updatedTool.Name;
            tool.Cost = updatedTool.Cost;
            tool.Weight = updatedTool.Weight;
            tool.Source = updatedTool.Source;
            tool.Rarity = updatedTool.Rarity;
            tool.WondrousItem = updatedTool.WondrousItem;
            tool.Attunement = updatedTool.Attunement;
            tool.Requirements = updatedTool.Requirements;
            tool.Description = updatedTool.Description;

            return NoContent();
        }

        // DELETE: api/Tools/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var tool = Tool.FirstOrDefault(u => u.Id == id);
            if (tool == null) return NotFound();

            Tool.Remove(tool);
            return NoContent();
        }
    }
}
