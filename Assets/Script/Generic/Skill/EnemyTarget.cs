using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InventoryManager;

public class EnemyTarget : MonoBehaviour, ISkillTarget
{
    public int Health { get; set; } = 50;

    public void ApplyEffect(ISkillEffect effect)
    {
        effect.Apply(this);
    }
}
