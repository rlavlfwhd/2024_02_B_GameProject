using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InventoryManager;

public class HealEffect : ISkillEffect
{
    public int HealAmount { get; private set; }

    public HealEffect(int damage)
    {
        HealAmount = damage;
    }

    public void Apply(ISkillTarget target)
    {
        if (target is PlayerTarget playertarget)
        {
            playertarget.Health += HealAmount;
            Debug.Log($"Player healed for {HealAmount}. Remaining health : {playertarget.Health}");
        }
        else if (target is EnemyTarget enemyTarget)
        {
            enemyTarget.Health += HealAmount;
            Debug.Log($"Player healed for {HealAmount}. Remaining health : {enemyTarget.Health}");
        }
    }
}
