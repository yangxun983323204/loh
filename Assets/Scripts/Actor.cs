using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public ActorData Data;
    public ActorRenderData RenderData;
    public List<Effect> Effects;
    public Transform Center;

    private Animator _anim;

    public IEnumerator Cast(Actor target,SkillData skill)
    {
        _anim.Play(RenderData.HitAnim);
        if (skill.SpellAnimTime > 0)
        {
            yield return SkillEffectMgr.Instance.StartEffect(this,skill.SpellAnim, skill.SpellAnimTime);
            yield return new WaitForSeconds(skill.SpellAnimTime);
        }

        yield return SkillEffectMgr.Instance.StartEffect(target,skill.HitAnim, skill.HitAnimTime1 + skill.HitAnimTime2);
        yield return new WaitForSeconds(skill.HitAnimTime1);
        if (skill.Heal>0)
            target.EffectHeal(skill.Heal);

        if(skill.ATK>0)
            target.EffectDamage(skill.ATK);

        for (int i = 0; i < skill.Effects.Length; i++)
        {
            EffectMgr.Instance.ApplyEffect(target,skill.Effects[i], skill.EffectsValue[1]);
        }
        yield return new WaitForSeconds(skill.HitAnimTime2);
    }


    protected virtual IEnumerator EffectHeal(float value)
    {
        Data.CurrHp += Mathf.FloorToInt(value);
        Data.CurrHp = Mathf.Clamp(Data.CurrHp, 0, Data.MaxHp);
        yield break;
    }

    protected virtual IEnumerator EffectDamage(float value)
    {
        Data.CurrHp -= Mathf.FloorToInt(value);
        Data.CurrHp = Mathf.Clamp(Data.CurrHp, 0, Data.MaxHp);
        yield break;
    }

    protected virtual void Start()
    {
        Center = transform.Find(RenderData.CenterPath);
    }

    protected virtual void OnDestroy()
    {
        
    }
}
