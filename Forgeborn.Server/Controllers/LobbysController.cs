using Forgeborn.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forgeborn.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LobbysController : ControllerBase
    {
        // In-memory list
        private static List<Lobbys> Lobby = new List<Lobbys>();

        // GET: api/Lobbys
        [HttpGet]
        public ActionResult<IEnumerable<Lobbys>> Get()
        {
            return Ok(Lobby);
        }

        // GET: api/Lobbys/{id}
        [HttpGet("{id}")]
        public ActionResult<Lobbys> Get(int id)
        {
            var lobby = Lobby.FirstOrDefault(u => u.Id == id);
            if (lobby == null) return NotFound();
            return Ok(lobby);
        }

        // POST: api/Lobbys
        [HttpPost]
        public ActionResult<Lobbys> Create(Lobbys lobby)
        {
            lobby.Id = Lobby.Count > 0 ? Lobby.Max(u => u.Id) + 1 : 1;
            Lobby.Add(lobby);
            return CreatedAtAction(nameof(Get), new { id = lobby.Id }, lobby);
        }

        // PUT: api/Lobbys/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, Lobbys updatedLobby)
        {
            var lobby = Lobby.FirstOrDefault(u => u.Id == id);
            if (lobby == null) return NotFound();

            lobby.Name = updatedLobby.Name;

            return NoContent();
        }

        // DELETE: api/Lobbys/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var lobby = Lobby.FirstOrDefault(u => u.Id == id);
            if (lobby == null) return NotFound();

            Lobby.Remove(lobby);
            return NoContent();
        }
    }
}
