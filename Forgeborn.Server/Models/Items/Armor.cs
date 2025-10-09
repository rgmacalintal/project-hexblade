using Newtonsoft.Json.Linq;

namespace Forgeborn.Server.Models.Items
{
    public class Armor : Item
    {
        private static string defaultTraits = "{" +
            "'shield' : false," +
            "'light' : true" +
            "'medium' : false" +
            "'heavy' : false" +
        "}";

        public string Effect { get; set; } = null!;
        public int ArmorClass { get; set; } = 0;
        public JObject Traits { get; set; } = JObject.Parse(defaultTraits);

    }
}
