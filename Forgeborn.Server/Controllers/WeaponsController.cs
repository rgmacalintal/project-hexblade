using Forgeborn.Server.Models.Items;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forgeborn.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeaponsController : ControllerBase
    {
        // In-memory list
        private static List<Weapons> Weapon = new List<Weapons>();

        // GET: api/Weapons
        [HttpGet]
        public ActionResult<IEnumerable<Weapons>> Get()
        {
            return Ok(Weapon);
        }

        // GET: api/Weapons/{id}
        [HttpGet("{id}")]
        public ActionResult<Weapons> Get(int id)
        {
            var weapon = Weapon.FirstOrDefault(u => u.Id == id);
            if (weapon == null) return NotFound();
            return Ok(weapon);
        }

        // POST: api/Weapons
        [HttpPost]
        public ActionResult<Weapons> Create(Weapons weapon)
        {
            weapon.Id = Weapon.Count > 0 ? Weapon.Max(u => u.Id) + 1 : 1;
            Weapon.Add(weapon);
            return CreatedAtAction(nameof(Get), new { id = weapon.Id }, weapon);
        }

        // PUT: api/Weapons/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, Weapons updatedWeapon)
        {
            var weapon = Weapon.FirstOrDefault(u => u.Id == id);
            if (weapon == null) return NotFound();

            weapon.Name = updatedWeapon.Name;
            weapon.Cost = updatedWeapon.Cost;
            weapon.Weight = updatedWeapon.Weight;
            weapon.Source = updatedWeapon.Source;
            weapon.Rarity = updatedWeapon.Rarity;
            weapon.WondrousItem = updatedWeapon.WondrousItem;
            weapon.Attunement = updatedWeapon.Attunement;
            weapon.Requirements = updatedWeapon.Requirements;
            weapon.Attack = updatedWeapon.Attack;
            weapon.Damage = updatedWeapon.Damage;
            weapon.DamageType = updatedWeapon.DamageType;
            weapon.Traits = updatedWeapon.Traits;

            return NoContent();
        }

        // DELETE: api/Weapons/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var weapon = Weapon.FirstOrDefault(u => u.Id == id);
            if (weapon == null) return NotFound();

            Weapon.Remove(weapon);
            return NoContent();
        }
    }
}
