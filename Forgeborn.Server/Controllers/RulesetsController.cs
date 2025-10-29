using Forgeborn.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forgeborn.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RulesetsController : Controller
    {
        // In-memory list
        private static List<Rulesets> Ruleset = new List<Rulesets>();

        // GET: api/Rulesets
        [HttpGet]
        public ActionResult<IEnumerable<Players>> GetPlayer()
        {
            return Ok(Ruleset);
        }

        // GET: api/Rulesets/{id}
        [HttpGet("{id}")]
        public ActionResult<Rulesets> GetRuleset(int id)
        {
            var ruleset = Ruleset.FirstOrDefault(u => u.Id == id);
            if (ruleset == null) return NotFound();
            return Ok(ruleset);
        }

        // POST: api/Rulesets
        [HttpPost]
        public ActionResult<Rulesets> CreateRuleset(Rulesets ruleset)
        {
            ruleset.Id = Ruleset.Count > 0 ? Ruleset.Max(u => u.Id) + 1 : 1;
            Ruleset.Add(ruleset);
            return CreatedAtAction(nameof(GetRuleset), new { id = ruleset.Id }, ruleset);
        }

        // PUT: api/Rulesets/5
        [HttpPut("{id}")]
        public IActionResult UpdateRuleset(int id, Rulesets updatedRuleset)
        {
            var ruleset = Ruleset.FirstOrDefault(u => u.Id == id);
            if (ruleset == null) return NotFound();

            ruleset.Name = updatedRuleset.Name;
            ruleset.Description = updatedRuleset.Description;

            return NoContent();
        }

        // DELETE: api/Rulesets/5
        [HttpDelete("{id}")]
        public IActionResult DeletePlayer(int id)
        {
            var ruleset = Ruleset.FirstOrDefault(u => u.Id == id);
            if (ruleset == null) return NotFound();

            Ruleset.Remove(ruleset);
            return NoContent();
        }
    }
}
