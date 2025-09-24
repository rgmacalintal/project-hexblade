using Microsoft.AspNetCore.Mvc;
using ProjectHexblade.Services.Gameflow;
// Must add character model import

namespace ProjectHexblade.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly HPService _hpService;
        private readonly DefenseService _defenseService;

        public CharacterController(HPService hpService, DefenseService defenseService)
        {
            _hpService = hpService;
            _defenseService = defenseService;
        }

        // Damage template
        public class DamageRequest
        {
            public int CharacterId { get; set; }
            public int Damage { get; set; }
        }

        [HttpPost("apply-damage")]
        public IActionResult ApplyDamage([FromBody] DamageRequest request)
        {
            // Must replace this with a Tyson's db lookup
            var character = new Character
            {
                CharacterId = request.CharacterId,
                CurrentHP = 20,
                MaxHP = 30,
                ArmorClass = 15
            };

            int newHP = _hpService.ApplyHPChange(
                character.CurrentHP,
                character.MaxHP,
                request.Damage,
                isHealing: false
            );

            character.CurrentHP = newHP;

            // Check if the character is dead
            bool isDead = _hpService.IsDead(character.CurrentHP);

            return Ok(new
            {
                characterId = character.CharacterId,
                currentHP = character.CurrentHP,
                isDead = isDead
            });
        }
    }
}
