using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IdObj
{
    int Id { get; }
}

public class MatchId<T> where T:IdObj
{
    private int _id;

    public MatchId(int id)
    {
        _id = id;
    }

    public MatchId<T> SetId(int id)
    {
        _id = id;
        return this;
    }

    public bool Check(T r)
    {
        return r.Id == _id;
    }
}
