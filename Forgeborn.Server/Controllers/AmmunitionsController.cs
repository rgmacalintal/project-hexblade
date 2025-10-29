using Forgeborn.Server.Models.Items;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forgeborn.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmmunitionsController : ControllerBase
    {
        // In-memory list
        private static List<Ammunitions> Ammunition = new List<Ammunitions>();

        // GET: api/Ammunitions
        [HttpGet]
        public ActionResult<IEnumerable<Ammunitions>> GetAmmunitions()
        {
            return Ok(Ammunition);
        }

        // GET: api/Ammunitions/{id}
        [HttpGet("{id}")]
        public ActionResult<Ammunitions> GetAmmunition(int id)
        {
            var ammunition = Ammunition.FirstOrDefault(u => u.Id == id);
            if (ammunition == null) return NotFound();
            return Ok(ammunition);
        }

        // POST: api/Ammunitions
        [HttpPost]
        public ActionResult<Ammunitions> CreateAmmunition(Ammunitions ammunition)
        {
            ammunition.Id = Ammunition.Count > 0 ? Ammunition.Max(u => u.Id) + 1 : 1;
            Ammunition.Add(ammunition);
            return CreatedAtAction(nameof(GetAmmunition), new { id = ammunition.Id }, ammunition);
        }

        // PUT: api/Ammunitions/5
        [HttpPut("{id}")]
        public IActionResult UpdateAmmunition(int id, Ammunitions updatedAmmunition)
        {
            var ammunition = Ammunition.FirstOrDefault(u => u.Id == id);
            if (ammunition == null) return NotFound();

            ammunition.Name = updatedAmmunition.Name;
            ammunition.Cost = updatedAmmunition.Cost;
            ammunition.Weight = updatedAmmunition.Weight;
            ammunition.Source = updatedAmmunition.Source;
            ammunition.Rarity = updatedAmmunition.Rarity;
            ammunition.WondrousItem = updatedAmmunition.WondrousItem;
            ammunition.Attunement = updatedAmmunition.Attunement;
            ammunition.Requirements = updatedAmmunition.Requirements;
            ammunition.Effect = updatedAmmunition.Effect;

            return NoContent();
        }

        // DELETE: api/Ammunitions/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAmmunition(int id)
        {
            var ammunition = Ammunition.FirstOrDefault(u => u.Id == id);
            if (ammunition == null) return NotFound();

            Ammunition.Remove(ammunition);
            return NoContent();
        }
    }
}
