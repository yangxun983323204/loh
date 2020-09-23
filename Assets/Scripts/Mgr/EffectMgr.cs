using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffMgr : SingletonMono<BuffMgr>
{
    private Dictionary<string, BuffData> _effects;

    public void RegBuff(BuffData effect)
    {
        _effects.Add(effect.name, effect);
    }

    public void ApplyBuff(Actor actor,string key)
    {
        var e = Instantiate(_effects[key]);
        actor.Effects.Add(e);
    }

    public void UpdateActorBuff(Actor actor)
    {
        foreach (var e in actor.Effects)
        {
            throw new System.NotImplementedException();
        }
    }
}