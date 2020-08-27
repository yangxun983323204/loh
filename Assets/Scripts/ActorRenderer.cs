using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorRenderer : MonoBehaviour
{
    public Actor Target;
    public Transform Center;

    private Animator _anim;
    private ActorCanvas _ui;

    public IEnumerator Appear()
    {
        if (Target.RenderData.CreateAnimTime > 0)
        {
            _anim.Play(Target.RenderData.CreateAnim);
            yield return new WaitForSeconds(Target.RenderData.CreateAnimTime);
        }
        var iter = AssetsMgr.Instance.Get<GameObject>("UI/ActorCanvas");
        yield return iter;
        var obj = iter.Asset<GameObject>();
        obj.transform.SetParent(Center,false);
        obj.transform.localPosition = Vector3.zero;
        _ui = obj.GetComponent<ActorCanvas>();
        _ui.SetHp(Target.Data.CurrHp, Target.Data.MaxHp);
    }

    public IEnumerator Cast(ActorRenderer target,SkillData skill)
    {
        _anim.Play(Target.RenderData.HitAnim);
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
            EffectMgr.Instance.ApplyEffect(target.Target,skill.Effects[i]);
        }
        yield return new WaitForSeconds(skill.HitAnimTime2);
    }

    public IEnumerator Dispose()
    {
        for (int i = Center.childCount - 1; i >= 0; i--)
        {
            var obj = Center.GetChild(i).gameObject;
            AssetsMgr.Instance.Recycle(obj.name, obj);
        }
        yield break;
    }

    protected virtual IEnumerator EffectHeal(float value)
    {
        Target.Data.CurrHp += Mathf.FloorToInt(value);
        Target.Data.CurrHp = Mathf.Clamp(Target.Data.CurrHp, 0, Target.Data.MaxHp);
        yield break;
    }

    protected virtual IEnumerator EffectDamage(float value)
    {
        Target.Data.CurrHp -= Mathf.FloorToInt(value);
        Target.Data.CurrHp = Mathf.Clamp(Target.Data.CurrHp, 0, Target.Data.MaxHp);
        yield break;
    }

    protected virtual void Start()
    {
        Target.Renderer = this;
        _anim = GetComponent<Animator>();
        Center = transform.Find(Target.RenderData.CenterPath);
    }

    protected virtual void OnDestroy()
    {
        Target.Renderer = null;
    }
}
