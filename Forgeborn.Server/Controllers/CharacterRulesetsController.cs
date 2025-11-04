using Forgeborn.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace Forgeborn.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterRulesetsController : ControllerBase
    {
        // In-memory list
        private static List<CharacterRulesets> CharacterRuleset = new List<CharacterRulesets>();

        // GET: api/CharacterRulesets
        [HttpGet]
        public ActionResult<IEnumerable<CharacterRulesets>> Get()
        {
            return Ok(CharacterRuleset);
        }

        // GET: api/CharacterRulesets/{id}
        [HttpGet("{id}")]
        public ActionResult<CharacterRulesets> Get(int id)
        {
            var chruleset = CharacterRuleset.FirstOrDefault(u => u.Id == id);
            if (chruleset == null) return NotFound();
            return Ok(chruleset);
        }

        // POST: api/CharacterRulesets
        [HttpPost]
        public ActionResult<CharacterRulesets> Create(CharacterRulesets chruleset)
        {
            chruleset.Id = CharacterRuleset.Count > 0 ? CharacterRuleset.Max(u => u.Id) + 1 : 1;
            CharacterRuleset.Add(chruleset);
            return CreatedAtAction(nameof(Get), new { id = chruleset.Id }, chruleset);
        }

        // PUT: api/CharacterRulesets/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, CharacterRulesets updatedCharacterRuleset)
        {
            var chruleset = CharacterRuleset.FirstOrDefault(u => u.Id == id);
            if (chruleset == null) return NotFound();

            chruleset.IsActive = updatedCharacterRuleset.IsActive;

            return NoContent();
        }

        // DELETE: api/CharacterRulesets/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var chruleset = CharacterRuleset.FirstOrDefault(u => u.Id == id);
            if (chruleset == null) return NotFound();

            CharacterRuleset.Remove(chruleset);
            return NoContent();
        }

    }
}
