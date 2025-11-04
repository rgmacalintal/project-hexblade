using Forgeborn.Server.Models.Items;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forgeborn.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdventuringGearsController : ControllerBase
    {
        // In-memory list
        private static List<AdventuringGears> AdventuringGear = new List<AdventuringGears>();

        // GET: api/AdventuringGears
        [HttpGet]
        public ActionResult<IEnumerable<AdventuringGears>> Get()
        {
            return Ok(AdventuringGear);
        }

        // GET: api/AdventuringGears/{id}
        [HttpGet("{id}")]
        public ActionResult<AdventuringGears> Get(int id)
        {
            var agear = AdventuringGear.FirstOrDefault(u => u.Id == id);
            if (agear == null) return NotFound();
            return Ok(agear);
        }

        // POST: api/AdventuringGears
        [HttpPost]
        public ActionResult<AdventuringGears> Create(AdventuringGears agear)
        {
            agear.Id = AdventuringGear.Count > 0 ? AdventuringGear.Max(u => u.Id) + 1 : 1;
            AdventuringGear.Add(agear);
            return CreatedAtAction(nameof(Get), new { id = agear.Id }, agear);
        }

        // PUT: api/AdventuringGears/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, AdventuringGears updatedAdventuringGear)
        {
            var agear = AdventuringGear.FirstOrDefault(u => u.Id == id);
            if (agear == null) return NotFound();

            agear.Name = updatedAdventuringGear.Name;
            agear.Cost = updatedAdventuringGear.Cost;
            agear.Weight = updatedAdventuringGear.Weight;
            agear.Source = updatedAdventuringGear.Source;
            agear.Rarity = updatedAdventuringGear.Rarity;
            agear.WondrousItem = updatedAdventuringGear.WondrousItem;
            agear.Attunement = updatedAdventuringGear.Attunement;
            agear.Requirements = updatedAdventuringGear.Requirements;
            agear.Description = updatedAdventuringGear.Description;

            return NoContent();
        }

        // DELETE: api/AdventuringGears/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var agear = AdventuringGear.FirstOrDefault(u => u.Id == id);
            if (agear == null) return NotFound();

            AdventuringGear.Remove(agear);
            return NoContent();
        }
    }
}
