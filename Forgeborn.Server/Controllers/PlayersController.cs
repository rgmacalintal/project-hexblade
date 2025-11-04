using Forgeborn.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forgeborn.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        // In-memory list
        private static List<Players> Player = new List<Players>();

        // GET: api/Players
        [HttpGet]
        public ActionResult<IEnumerable<Players>> GetPlayer()
        {
            return Ok(Player);
        }

        // GET: api/Players/{id}
        [HttpGet("{id}")]
        public ActionResult<Players> GetPlayer(int id)
        {
            var player = Player.FirstOrDefault(u => u.Id == id);
            if (player == null) return NotFound();
            return Ok(player);
        }

        // POST: api/Players
        [HttpPost]
        public ActionResult<Players> CreatePlayer(Players player)
        {
            player.Id = Player.Count > 0 ? Player.Max(u => u.Id) + 1 : 1;
            Player.Add(player);
            return CreatedAtAction(nameof(GetPlayer), new { id = player.Id }, player);
        }

        // PUT: api/Players/5
        [HttpPut("{id}")]
        public IActionResult UpdatePlayer(int id, Players updatedPlayer)
        {
            var player = Player.FirstOrDefault(u => u.Id == id);
            if (player == null) return NotFound();

            player.IsHost = updatedPlayer.IsHost;

            return NoContent();
        }

        // DELETE: api/Players/5
        [HttpDelete("{id}")]
        public IActionResult DeletePlayer(int id)
        {
            var player = Player.FirstOrDefault(u => u.Id == id);
            if (player == null) return NotFound();

            Player.Remove(player);
            return NoContent();
        }
    }
}
