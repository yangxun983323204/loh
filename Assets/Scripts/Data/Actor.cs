using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewActor", menuName = "LOH/Actor")]
public class Actor : ScriptableObject
{
    public string Name;
    public ActorData Data;
    public ActorRenderData RenderData;
    [System.NonSerialized]
    public List<BuffData> Effects;
    [System.NonSerialized]
    public ActorRenderer Renderer;

    public delegate void RefDel<T>(ref T val);

    public Action<AnimInfo> onAppear;
    public Action<AnimInfo> onHit;
    public Action<Skill,AnimInfo> onSkillSpell;
    public Action<Skill, AnimInfo> onSkillHit;
    public RefDel<float> onWillHeal,onHeal;
    public RefDel<float> onWillDamage,onDamage;
    public Action<Actor> onWillDead,onDead;

    public void SetRenderer(ActorRenderer renderer)
    {
        Renderer = renderer;
        Renderer.Self = this;
    }

    public IEnumerator Appear()
    {
        onAppear?.Invoke(RenderData.CreateAnim);
        yield return new WaitForSeconds(RenderData.CreateAnim.Time);
    }

    public IEnumerator Cast(Actor target,Skill skill)
    {
        onHit?.Invoke(RenderData.HitAnim);
        yield return skill.Cast(this, target);
    }

    public void Heal(float val)
    {
        onWillHeal?.Invoke(ref val);
        onHeal?.Invoke(ref val);
        Debug.LogFormat("{0}受到治疗:{1}", this, val);
        Data.AddHp((int)val);
    }

    public void Damage(float val)
    {
        onWillDamage?.Invoke(ref val);
        onDamage?.Invoke(ref val);
        Debug.LogFormat("{0}受到伤害:{1}", this, val);
        Data.AddHp(-(int)val);
        if (Data.CurrHp <=0)
        {
            onWillDead?.Invoke(this);
            if (Data.CurrHp <= 0)
            {
                onDead?.Invoke(this);
                Debug.LogFormat("{0}阵亡", this);
            }
        }
    }

    public override string ToString()
    {
        return Name;
    }
}