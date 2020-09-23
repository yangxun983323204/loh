using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YX;

using GoPool = YX.Pool<UnityEngine.GameObject>;

public class AssetsMgr : SingletonMono<AssetsMgr>
{
    Dictionary<string, Object> _sharePools = new Dictionary<string, Object>();
    Dictionary<string, GoPool> _instPools = new Dictionary<string, GoPool>();
    Transform _goPoolRoot;
    DictAccessor<string, bool> _loading = new DictAccessor<string, bool>();

    public IEnumerator Get<T>(string key) where T:UnityEngine.Object
    {
        if (_loading.Get(key, false))
            yield return new WaitUntil(() => _loading.Get(key, false));

        bool isGo = typeof(T) == typeof(GameObject);
        if (isGo && _instPools.ContainsKey(key))
        {
            yield return _instPools[key];
            yield break;
        }
        else if (!isGo && _sharePools.ContainsKey(key))
        {
            yield return _sharePools[key];
            yield break;
        }

        _loading.Set(key, true);
        var req = Resources.LoadAsync<T>(key);
        yield return req;
        _loading.Set(key, false);
        var origin = req.asset;
        if (isGo)
        {
            Debug.Assert(origin is GameObject);
            var pool = new GoPool();
            pool.SetTemplate(Instantiate(origin as GameObject), new GameObjectAllocator());
            _instPools.Add(key, pool);
            yield return pool;
        }
        else
        {
            _sharePools.Add(key, origin);
            yield return origin;
        }
    }

    public IEnumerator Prepare<T>(string key,int count) where T : UnityEngine.Object
    {
        if (_loading.Get(key, false))
            yield return new WaitUntil(() => _loading.Get(key, false));

        bool isGo = typeof(T) == typeof(GameObject);
        if (isGo && _instPools.ContainsKey(key))
            yield break;
        else if (!isGo && _sharePools.ContainsKey(key))
            yield break;

        _loading.Set(key, true);
        var req = Resources.LoadAsync<T>(key);
        yield return req;
        _loading.Set(key, false);
        var origin = req.asset;
        if (isGo)
        {
            Debug.Assert(origin is GameObject);
            var pool = new GoPool();
            pool.SetTemplate(Instantiate(origin as GameObject), new GameObjectAllocator());
            _instPools.Add(key, pool);
            pool.Reserve((uint)count);
        }
        else
        {
            _sharePools.Add(key, origin);
        }
    }

    public void Recycle(string key,Object asset)
    {
        if (asset is GameObject)
        {
            if (!_instPools.ContainsKey(key))
                return;

            _instPools[key].Recycle(asset as GameObject);
        }
    }
}

public static class IEnumeratorEx
{
    public static T Asset<T>(this IEnumerator inst) where T : UnityEngine.Object
    {
        if (typeof(T) == typeof(GameObject))
        {
            return (inst.Current as GoPool).Spawn() as T;
        }
        else 
            return inst.Current as T;
    }
}
