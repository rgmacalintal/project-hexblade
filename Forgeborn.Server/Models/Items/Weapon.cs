using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Linq;

namespace Forgeborn.Server.Models.Items
{
    public class Weapon : Item
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

        private static string defaultTraits = "{" +
            "'simple' : true," +
            "'martial' : false," +
            "'melee' : true," +
            "'ranged' : false," +
            "'staff' : false," +
            "'reach' : 5," +
            "'range' : 0," +
            "'canBeThrown' : false," +
            "'light' : true," +
            "'heavy' : false," +
            "'finesse' : true," +
            "'ammunition' : false," +
            "'loading' : false" +
        "}";

        [Required]
        int Attack { get; set; } = 0;
        [Required]
        public int Damage { get; set; } = 0;
        [Required]
        public DmgTypeStruct DamageType { get; set; }
        public JObject Traits { get; set; } = JObject.Parse(defaultTraits);

    }
}
