using Newtonsoft.Json.Linq;

namespace Forgeborn.Server.Models.Items
{
    public class Armors : Items
    {
        public struct ArmorTraits
        {
            public ArmorTraits() { }

            public bool Shield { get; set; } = false;
            public bool Light { get; set; } = true;
            public bool Medium { get; set; } = false;
            public bool Heavy { get; set; } = false;
        }

        public string Effect { get; set; } = string.Empty;
        public int ArmorClass { get; set; } = 0;
        public ArmorTraits Traits { get; set; }

    }
}
