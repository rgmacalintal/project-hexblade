using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Linq;

namespace Forgeborn.Server.Models.Items
{
    public class Weapons : Items
    {
        public struct DmgTypeStruct
        {
            public bool isSlashing = true;
            public bool isPiercing = false;
            public bool isBludgeoning = false;
            public bool isForce = true;
            public bool isCold = false;
            public bool isFire = false;
            public bool isLightning = false;
            public bool isNecrotic = false;
            public bool isPoison = false;
            public bool isPsychic = false;
            public bool isRadiant = false;
            public bool isThunder = false;
            public bool isAcid = false;

            public DmgTypeStruct() { }

        }

        public struct WeaponTraits
        {
            public bool simple = true;
            public bool martial = false;
            public bool melee = true;
            public bool ranged = false;
            public bool staff = false;
            public int reach = 5;
            public int range = 0;
            public bool canBeThrown = false;
            public bool light = true;
            public bool heavy = false;
            public bool finesse = true;
            public bool ammunition = false;
            public bool loading = false;
            public WeaponTraits() { }
        }

        [Required]
        public int Attack { get; set; } = 0;
        [Required]
        public int Damage { get; set; } = 0;
        [Required]
        public DmgTypeStruct DamageType { get; set; }
        public WeaponTraits Traits { get; set; }

    }
}
