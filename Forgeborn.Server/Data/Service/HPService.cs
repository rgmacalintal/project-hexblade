using System;

namespace ProjectHexblade.Services.Gameflow
{
    public class HPService
    {
        // Apply damage or healing to current HP from spell or item
        public int ApplyHPChange(int currentHP, int maxHP, int amount, bool isHealing, string[] resistances = null, string[] vulnerabilities = null, string[] immunities = null)
        {
            // Must check for resistances/vulnerabilities/immunities here through DMsGuild
            int adjustedAmount = amount;

            if (isHealing)
            {
                currentHP += adjustedAmount;
                if (currentHP > maxHP)
                    currentHP = maxHP;
            }
            else
            {
                currentHP -= adjustedAmount;
                if (currentHP < 0)
                    currentHP = 0;
            }

            return currentHP;
        }

        // Check if HP is 0 to trigger death saves
        public bool IsDead(int currentHP)
        {
            return currentHP <= 0;
        }

        // Must implement death save tracking from a separate dice roll
    }
}
