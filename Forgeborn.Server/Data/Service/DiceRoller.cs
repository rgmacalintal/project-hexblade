using System;

namespace ProjectHexblade.Services.Gameflow
{
    public class DiceRoller
    {
        private readonly Random _random = new Random();

        // Rolls dice given an expression like "2d6+3" for common DnD jargon
        public int Roll(string expression)
        {
            // Must add more robust parsing for dice expressions
            if (expression == "1d20")
            {
                return _random.Next(1, 21);
            }

            throw new NotImplementedException("Dice expression parsing not implemented yet.");
        }
    }
}
