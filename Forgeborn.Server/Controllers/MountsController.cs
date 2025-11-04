using Forgeborn.Server.Models.Items;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forgeborn.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MountsController : ControllerBase
    {
        // In-memory list
        private static List<Mounts> Mount = new List<Mounts>();

        // GET: api/Mounts
        [HttpGet]
        public ActionResult<IEnumerable<Mounts>> Get()
        {
            return Ok(Mount);
        }

        // GET: api/Mounts/{id}
        [HttpGet("{id}")]
        public ActionResult<Mounts> Get(int id)
        {
            var mount = Mount.FirstOrDefault(u => u.Id == id);
            if (mount == null) return NotFound();
            return Ok(mount);
        }

        // POST: api/Mounts
        [HttpPost]
        public ActionResult<Mounts> Create(Mounts mount)
        {
            mount.Id = Mount.Count > 0 ? Mount.Max(u => u.Id) + 1 : 1;
            Mount.Add(mount);
            return CreatedAtAction(nameof(Get), new { id = mount.Id }, mount);
        }

        // PUT: api/Mounts/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, Mounts updatedMount)
        {
            var mount = Mount.FirstOrDefault(u => u.Id == id);
            if (mount == null) return NotFound();

            mount.Name = updatedMount.Name;
            mount.Cost = updatedMount.Cost;
            mount.Weight = updatedMount.Weight;
            mount.Source = updatedMount.Source;
            mount.Rarity = updatedMount.Rarity;
            mount.WondrousItem = updatedMount.WondrousItem;
            mount.Attunement = updatedMount.Attunement;
            mount.Requirements = updatedMount.Requirements;

            return NoContent();
        }

        // DELETE: api/Mounts/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var mount = Mount.FirstOrDefault(u => u.Id == id);
            if (mount == null) return NotFound();

            Mount.Remove(mount);
            return NoContent();
        }
    }
}
