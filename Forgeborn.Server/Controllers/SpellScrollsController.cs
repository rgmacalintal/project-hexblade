using Forgeborn.Server.Models.Items;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forgeborn.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpellScrollsController : ControllerBase
    {
        // In-memory list
        private static List<SpellScrolls> SpellScroll = new List<SpellScrolls>();

        // GET: api/SpellScrolls
        [HttpGet]
        public ActionResult<IEnumerable<SpellScrolls>> Get()
        {
            return Ok(SpellScroll);
        }

        // GET: api/SpellScrolls/{id}
        [HttpGet("{id}")]
        public ActionResult<SpellScrolls> Get(int id)
        {
            var scroll = SpellScroll.FirstOrDefault(u => u.Id == id);
            if (scroll == null) return NotFound();
            return Ok(scroll);
        }

        // POST: api/SpellScrolls
        [HttpPost]
        public ActionResult<SpellScrolls> Create(SpellScrolls scroll)
        {
            scroll.Id = SpellScroll.Count > 0 ? SpellScroll.Max(u => u.Id) + 1 : 1;
            SpellScroll.Add(scroll);
            return CreatedAtAction(nameof(Get), new { id = scroll.Id }, scroll);
        }

        // PUT: api/SpellScrolls/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, SpellScrolls updatedSpellScroll)
        {
            var scroll = SpellScroll.FirstOrDefault(u => u.Id == id);
            if (scroll == null) return NotFound();

            scroll.Name = updatedSpellScroll.Name;
            scroll.Cost = updatedSpellScroll.Cost;
            scroll.Weight = updatedSpellScroll.Weight;
            scroll.Source = updatedSpellScroll.Source;
            scroll.Rarity = updatedSpellScroll.Rarity;
            scroll.WondrousItem = updatedSpellScroll.WondrousItem;
            scroll.Attunement = updatedSpellScroll.Attunement;
            scroll.Requirements = updatedSpellScroll.Requirements;
            scroll.Level = updatedSpellScroll.Level;
            scroll.Description = updatedSpellScroll.Description;

            return NoContent();
        }

        // DELETE: api/SpellScrolls/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var scroll = SpellScroll.FirstOrDefault(u => u.Id == id);
            if (scroll == null) return NotFound();

            SpellScroll.Remove(scroll);
            return NoContent();
        }
    }
}
