namespace Forgeborn.Server.Models
{
    public static class DiceRoller
    {
        private static readonly Random _random = new();

        // Supports formats like "1d20", "2d6+3", "4d8-1"
        // Example Usage: int rollResult = DiceRoller.Roll("2d6+3");
        public static int Roll(string expression)
        {
            var match = System.Text.RegularExpressions.Regex.Match(expression, @"(\d*)d(\d+)([+-]\d+)?");

            if (!match.Success)
                throw new ArgumentException("Invalid dice expression.");

            int count = string.IsNullOrEmpty(match.Groups[1].Value) ? 1 : int.Parse(match.Groups[1].Value);
            int sides = int.Parse(match.Groups[2].Value);
            int modifier = string.IsNullOrEmpty(match.Groups[3].Value) ? 0 : int.Parse(match.Groups[3].Value);

            int total = 0;
            for (int i = 0; i < count; i++)
                total += _random.Next(1, sides + 1);

            return total + modifier;
        }
    }

}


