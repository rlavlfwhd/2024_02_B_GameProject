using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InventoryManager;

public class DamageEffect : ISkillEffect
{
    public int Damage {  get; private set; }

    public DamageEffect(int damage)
    {
        Damage = damage;
    }

    public void Apply(ISkillTarget target)
    {
        if(target is PlayerTarget playertarget)
        {
            playertarget.Health -= Damage;
            Debug.Log($"Player took {Damage} damage. Remaining health : {playertarget.Health}");
        }
        else if(target is EnemyTarget enemyTarget)
        {
            enemyTarget.Health -= Damage;
            Debug.Log($"Player took {Damage} damage. Remaining health : {enemyTarget.Health}");
        }
    }
}
