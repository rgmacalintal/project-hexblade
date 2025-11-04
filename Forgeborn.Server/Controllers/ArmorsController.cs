using Forgeborn.Server.Models.Items;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forgeborn.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArmorsController : ControllerBase
    {
        // In-memory list
        private static List<Armors> Armor = new List<Armors>();

        // GET: api/Armors
        [HttpGet]
        public ActionResult<IEnumerable<Armors>> Get()
        {
            return Ok(Armor);
        }

        // GET: api/Armors/{id}
        [HttpGet("{id}")]
        public ActionResult<Armors> Get(int id)
        {
            var armor = Armor.FirstOrDefault(u => u.Id == id);
            if (armor == null) return NotFound();
            return Ok(armor);
        }

        // POST: api/Armors
        [HttpPost]
        public ActionResult<Armors> Create(Armors armor)
        {
            armor.Id = Armor.Count > 0 ? Armor.Max(u => u.Id) + 1 : 1;
            Armor.Add(armor);
            return CreatedAtAction(nameof(Get), new { id = armor.Id }, armor);
        }

        // PUT: api/Armors/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, Armors updatedArmor)
        {
            var armor = Armor.FirstOrDefault(u => u.Id == id);
            if (armor == null) return NotFound();

            armor.Name = updatedArmor.Name;
            armor.Cost = updatedArmor.Cost;
            armor.Weight = updatedArmor.Weight;
            armor.Source = updatedArmor.Source;
            armor.Rarity = updatedArmor.Rarity;
            armor.WondrousItem = updatedArmor.WondrousItem;
            armor.Attunement = updatedArmor.Attunement;
            armor.Requirements = updatedArmor.Requirements;
            armor.Effect = updatedArmor.Effect;
            armor.ArmorClass = updatedArmor.ArmorClass;
            armor.Traits = updatedArmor.Traits;

            return NoContent();
        }

        // DELETE: api/Armors/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var armor = Armor.FirstOrDefault(u => u.Id == id);
            if (armor == null) return NotFound();

            Armor.Remove(armor);
            return NoContent();
        }
    }
}
