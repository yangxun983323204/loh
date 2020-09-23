using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorRenderer : MonoBehaviour
{
    public Actor Self;
    public Transform Center;

    private Animator _anim;
    private ActorCanvas _ui;

    private void Bind()
    {
        Self.onHeal += (ref float val) =>
        {
            _ui.SetHp(Self.Data.CurrHp, Self.Data.MaxHp);
        };

        Self.onDamage += (ref float val) =>
        {
            _ui.SetHp(Self.Data.CurrHp, Self.Data.MaxHp);
        };
    }

    public IEnumerator Appear()
    {
        Bind();
        var iter = AssetsMgr.Instance.Get<GameObject>("Prefab/UI/ActorCanvas");
        yield return iter;
        var obj = iter.Asset<GameObject>();
        obj.transform.SetParent(Center,false);
        obj.transform.localPosition = Vector3.zero;
        _ui = obj.GetComponent<ActorCanvas>();
        _ui.SetHp(Self.Data.CurrHp, Self.Data.MaxHp);
        if (Self.RenderData.CreateAnim.Time > 0)
        {
            _anim.Play(Self.RenderData.CreateAnim.Name);
            yield return new WaitForSeconds(Self.RenderData.CreateAnim.Time);
        }
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

    protected virtual void Start()
    {
        Self.Renderer = this;
        _anim = GetComponent<Animator>();
        Center = transform.Find(Self.RenderData.CenterPath);
    }

    protected virtual void OnDestroy()
    {
        Self.Renderer = null;
    }
}
