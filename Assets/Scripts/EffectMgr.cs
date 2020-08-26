using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect
{
    public int Code;

    public Effect()
    { }

    public abstract void SetValue(int value);
    public abstract void Update();
    public abstract Effect Clone();
}

public class EffectMgr : SingletonMono<EffectMgr>
{
    private Dictionary<int, Effect> _effects;

    public void RegEffect(Effect effect)
    {
        _effects.Add(effect.Code, effect);
    }

    public void ApplyEffect(Actor actor,int code,int value)
    {
        var e = _effects[code].Clone();
        e.SetValue(value);
        actor.Effects.Add(e);
    }

    public void UpdateActorEffects(Actor actor)
    {
        foreach (var e in actor.Effects)
        {
            e.Update();
        }
    }
}