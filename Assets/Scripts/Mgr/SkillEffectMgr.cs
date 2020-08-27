using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffectMgr : SingletonMono<SkillEffectMgr>
{
    public IEnumerator StartEffect(ActorRenderer actor, string key, float time)
    {
        StopEffect(actor,key);
        var iter = AssetsMgr.Instance.Get<GameObject>(key);
        yield return iter;
        var obj = iter.Asset<GameObject>();
        StartCoroutine(ApplyEffect(actor,key, obj, time));
    }

    public IEnumerator WaitEffect(ActorRenderer actor, string key, float time)
    {
        if (time > 20)
        {
            Debug.LogError("试图等待一个大于20秒的效果");
            yield break;
        }
        StopEffect(actor, key);
        var iter = AssetsMgr.Instance.Get<GameObject>(key);
        yield return iter;
        var obj = iter.Asset<GameObject>();
        yield return ApplyEffect(actor, key, obj, time);
    }

    private IEnumerator ApplyEffect(ActorRenderer actor,string key, GameObject effect,float time)
    {
        effect.transform.SetParent(actor.Center);
        effect.transform.localPosition = Vector3.zero;
        effect.name = key;
        effect.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(time);
        AssetsMgr.Instance.Recycle(key, effect);
    }

    public void StopEffect(ActorRenderer actor,string key)
    {
        for (int i = 0; i < actor.Center.childCount; i++)
        {
            var c = actor.Center.GetChild(i);
            if (c.name == key)
            {
                AssetsMgr.Instance.Recycle(key, c.gameObject);
                break;
            }
        }
    }
}
