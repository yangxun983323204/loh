using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewJsonDB : IDB
{
    private const string DIR = "json";
    private Dictionary<Type, string> _fileDict = new Dictionary<Type, string>();
    private Dictionary<Type, IList> _caches = new Dictionary<Type, IList>();

    public NewJsonDB()
    {
        _fileDict.Add(typeof(GameCard), "carddb");
        _fileDict.Add(typeof(ActorRecord), "actordb");
        _fileDict.Add(typeof(ActorDeckRecord), "deckdb");
    }

    public override void Dispose()
    {
    }

    public override T Find<T>(Predicate<T> func)
    {
        return GetCache<T>().Find(func);
    }

    public override List<T> Finds<T>(Predicate<T> func)
    {
        return GetCache<T>().FindAll(func);
    }

    private List<T> GetCache<T>()
    {
        var t = typeof(T);
        if (!_fileDict.ContainsKey(t))
            return null;
        if (!_caches.ContainsKey(t))
        {
            var list = JsonConvert.DeserializeObject<List<T>>(Resources.Load<TextAsset>($"DB/{DIR}/{_fileDict[t]}").text);
            _caches.Add(t, list);
            return list;
        }
        else
            return _caches[t] as List<T>;
    }
}
