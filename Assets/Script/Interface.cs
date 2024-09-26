using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillTarget
{
    void ApplyEffect(ISkillEffect effect);
}

public interface ISkillEffect
{
    void Apply(ISkillTarget target);
}
