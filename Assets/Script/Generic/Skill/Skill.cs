using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill<TTarget, TEffect>
    where TTarget : ISkillTarget
    where TEffect : ISkillEffect
{
    public string Name { get; private set; }
    public TEffect Effect { get; private set; }

    public Skill(string name, TEffect effect)
    {
        Name = name;
        Effect = effect;
    }

    public void Use(TTarget target)
    {
        Debug.Log($"Using skill: {Name}");
        target.ApplyEffect( Effect );
    }
}
