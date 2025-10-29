using Forgeborn.Server.Models.Items;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forgeborn.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        // In-memory list
        private static List<Vehicles> Vehicle = new List<Vehicles>();

        // GET: api/Vehicles
        [HttpGet]
        public ActionResult<IEnumerable<Vehicles>> GetVehicle()
        {
            return Ok(Vehicle);
        }

        // GET: api/Vehicles/{id}
        [HttpGet("{id}")]
        public ActionResult<Vehicles> GetVehicle(int id)
        {
            var vehicle = Vehicle.FirstOrDefault(u => u.Id == id);
            if (vehicle == null) return NotFound();
            return Ok(vehicle);
        }

        // POST: api/Vehicles
        [HttpPost]
        public ActionResult<Vehicles> CreateVehicle(Vehicles vehicle)
        {
            vehicle.Id = Vehicle.Count > 0 ? Vehicle.Max(u => u.Id) + 1 : 1;
            Vehicle.Add(vehicle);
            return CreatedAtAction(nameof(GetVehicle), new { id = vehicle.Id }, vehicle);
        }

        // PUT: api/Vehicles/5
        [HttpPut("{id}")]
        public IActionResult UpdateVehicle(int id, Vehicles updatedVehicle)
        {
            var vehicle = Vehicle.FirstOrDefault(u => u.Id == id);
            if (vehicle == null) return NotFound();

            vehicle.Name = updatedVehicle.Name;
            vehicle.Cost = updatedVehicle.Cost;
            vehicle.Weight = updatedVehicle.Weight;
            vehicle.Source = updatedVehicle.Source;
            vehicle.Rarity = updatedVehicle.Rarity;
            vehicle.WondrousItem = updatedVehicle.WondrousItem;
            vehicle.Attunement = updatedVehicle.Attunement;
            vehicle.Requirements = updatedVehicle.Requirements;
            vehicle.VehicleType = updatedVehicle.VehicleType;

            return NoContent();
        }

        // DELETE: api/Vehicles/5
        [HttpDelete("{id}")]
        public IActionResult DeleteVehicle(int id)
        {
            var vehicle = Vehicle.FirstOrDefault(u => u.Id == id);
            if (vehicle == null) return NotFound();

            Vehicle.Remove(vehicle);
            return NoContent();
        }
    }
}
