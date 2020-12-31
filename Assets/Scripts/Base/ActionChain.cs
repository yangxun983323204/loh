using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionChain<T>
{
    public delegate bool Func(ref T val);

    List<Func> _chain = new List<Func>(2);

    public void Add(Func action)
    {
        _chain.Add(action);
    }

    public void Remove(Func action)
    {
        _chain.Remove(action);
    }

    public void SetAsFirst(Func action)
    {
        if (_chain.Count>0 && _chain[0] == action)
        {
            return;
        }

        Remove(action);
        _chain.Insert(0, action);
    }

    public void SetAsLast(Func action)
    {
        Remove(action);
        Add(action);
    }

    public bool Invoke(ref T arg)
    {
        for (int i = 0; i < _chain.Count; i++)
        {
            var node = _chain[i];
            var s = node.Invoke(ref arg);
            if (!s)
            {
                return false;
            }
        }

        return true;
    }

    public void Clear()
    {
        _chain.Clear();
    }
}

public class ActionChain<T1,T2>
{
    public delegate bool Func(ref T1 val1,ref T2 val2);

    List<Func> _chain = new List<Func>(2);

    public void Add(Func action)
    {
        _chain.Add(action);
    }

    public void Remove(Func action)
    {
        _chain.Remove(action);
    }

    public void SetAsFirst(Func action)
    {
        if (_chain.Count > 0 && _chain[0] == action)
        {
            return;
        }

        Remove(action);
        _chain.Insert(0, action);
    }

    public void SetAsLast(Func action)
    {
        Remove(action);
        Add(action);
    }

    public bool Invoke(ref T1 arg1,ref T2 arg2)
    {
        for (int i = 0; i < _chain.Count; i++)
        {
            var node = _chain[i];
            var s = node.Invoke(ref arg1,ref arg2);
            if (!s)
            {
                return false;
            }
        }

        return true;
    }

    public void Clear()
    {
        _chain.Clear();
    }
}