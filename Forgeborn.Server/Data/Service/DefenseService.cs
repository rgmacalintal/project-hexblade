using System;

namespace ProjectHexblade.Services.Gameflow
{
    public class DefenseService
    {
        private readonly DiceRoller _diceRoller;

        public DefenseService(DiceRoller diceRoller)
        {
            _diceRoller = diceRoller;
        }

        // Returns true if attack hits, but must implement attack logic
        public bool ResolveAttack(int attackerBonus, int targetArmorClass)
        {
            int attackRoll = _diceRoller.Roll("1d20") + attackerBonus;
            return attackRoll >= targetArmorClass;
        }
    }
}
