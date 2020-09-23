using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    Self,
    Other,
    Any
}

[CreateAssetMenu(fileName = "NewSkill", menuName = "LOH/Skill")]
public class Skill:ScriptableObject
{
    public enum SkillAnimType
    {
        AtOnce,
        Fly,
        Continue,
    }

    public int Id;
    public string Name;
    public SkillType Type;
    public SkillAnimType AnimType;
    public float Damage;
    public float Heal;
    public string[] Buffs;
    // render
    public AnimInfo SpellAnim = new AnimInfo();
    public AnimInfo HitAnim = new AnimInfo();
    public float HitApplyTime;

    public Action<Actor, Actor, AnimInfo> onSpell;
    public Action<Actor, Actor, AnimInfo> onHit;

    public IEnumerator Cast(Actor caller,Actor target)
    {
        Debug.LogFormat("{0}向{1}施放技能：{2}",caller,target,this);
        onSpell?.Invoke(caller,target,SpellAnim);
        yield return new WaitForSeconds(SpellAnim.Time);
        onHit?.Invoke(caller, target, HitAnim);
        yield return new WaitForSeconds(HitApplyTime);
        if (Heal > 0)
            target.Heal(Heal);

        if (Damage > 0)
            target.Damage(Damage);

        for (int i = 0; i < Buffs.Length; i++)
        {
            BuffMgr.Instance.ApplyBuff(target, Buffs[i]);
        }

        yield return new WaitForSeconds(HitAnim.Time - HitApplyTime);
    }

    public override string ToString()
    {
        return Name;
    }
}