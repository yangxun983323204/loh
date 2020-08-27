using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectMgr : SingletonMono<EffectMgr>
{
    private Dictionary<int, Effect> _effects;

    public void RegEffect(Effect effect)
    {
        _effects.Add(effect.Code, effect);
    }

    public void ApplyEffect(Actor actor,int code)
    {
        var e = Instantiate(_effects[code]);
        actor.Effects.Add(e);
    }

    public void UpdateActorEffects(Actor actor)
    {
        foreach (var e in actor.Effects)
        {
            throw new System.NotImplementedException();
        }
    }
}