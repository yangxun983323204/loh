using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsMgr : SingletonMono<AssetsMgr>
{
    Dictionary<string, Object> _pool = new Dictionary<string, Object>();
    Dictionary<string, List<Object>> _instPool = new Dictionary<string, List<Object>>();
    Transform _goPoolRoot;

    protected override void OnInit()
    {
        base.OnInit();
        var poolRoot = new GameObject("pool");
        _goPoolRoot = poolRoot.transform;
        _goPoolRoot.SetParent(transform);
        _goPoolRoot.gameObject.SetActive(false);
    }

    public IEnumerator Get(string key)
    {
        Object inst = null;
        if (_instPool.ContainsKey(key))
        {
            var list = _instPool[key];
            inst = list[list.Count - 1];
            if(list.Count <= 1)
            {
                _instPool.Remove(key);
            }
            yield return inst;
            yield break;
        }

        Object origin = null;
        if (_pool.ContainsKey(key))
        {
            origin = _pool[key];
            goto tag_instant;
        }

        var req = Resources.LoadAsync(key);
        yield return req;
        var a = req.asset;
        if (a != null)
            _pool.Add(key, a);

        yield return a;
    tag_instant:
        if (origin is GameObject)
        {
            inst = Instantiate(origin);
            var nlist = new List<Object>(1);
            nlist.Add(inst);
            _instPool.Add(key, nlist);
            yield return inst;
        }
        else
            yield return origin;
    }

    public void Recycle(string key,Object asset)
    {
        if (asset is GameObject)
        {
            (asset as GameObject).transform.SetParent(_goPoolRoot);
            if (!_instPool.ContainsKey(key))
                _instPool.Add(key, new List<Object>(1));

            _instPool[key].Add(asset);
        }
    }
}

public static class IEnumeratorEx
{
    public static T Asset<T>(this IEnumerator inst) where T : UnityEngine.Object
    {
        return inst.Current as T;
    }
}
