using UnityEngine;
using System;
using System.Collections.Generic;

public static class PowerUpFactory
{
    private static readonly Dictionary<PowerUp.Abilities, Type> AbilityToTypeMap = new Dictionary<PowerUp.Abilities, Type>
    {
        { PowerUp.Abilities.Health, typeof(MaxHealth) },
        { PowerUp.Abilities.Boost, typeof(MaxBoosts) },
        { PowerUp.Abilities.Invincibility, typeof(Invulnerability) },
        { PowerUp.Abilities.Nuclear, typeof(Nuclear) },
        { PowerUp.Abilities.TimeSlow, typeof(TimeSlow) },
        { PowerUp.Abilities.Laser, typeof(Laser) },
        { PowerUp.Abilities.x2, typeof(DoublePoints) },
        { PowerUp.Abilities.DeathMachine, typeof(DeathMachine) }
    };

    public static PowerUps CreatePowerUp(PowerUp.Abilities ability)
    {
        if (AbilityToTypeMap.TryGetValue(ability, out Type powerUpType))
        {
            GameObject powerUpObject = new GameObject("PowerUp");
            PowerUps powerUpsInstance = (PowerUps)powerUpObject.AddComponent(powerUpType);

            if (powerUpsInstance == null)
            {
                Debug.LogError("Failed to create valid power-up instance for ability: " + ability);
                GameObject.Destroy(powerUpObject);
            }

            return powerUpsInstance;
        }
        else
        {
            Debug.LogError("Unsupported power-up ability: " + ability);
            return null;
        }
    }
}
