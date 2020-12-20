using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActionChain<T>
{
    List<Func<T, bool>> _chain = new List<Func<T, bool>>(2);

    public void Add(Func<T,bool> action)
    {
        _chain.Add(action);
    }

    public void Remove(Func<T, bool> action)
    {
        _chain.Remove(action);
    }

    public void SetAsFirst(Func<T, bool> action)
    {
        if (_chain.Count>0 && _chain[0] == action)
        {
            return;
        }

        Remove(action);
        _chain.Insert(0, action);
    }

    public void SetAsLast(Func<T, bool> action)
    {
        Remove(action);
        Add(action);
    }

    public bool Invoke(T arg)
    {
        for (int i = 0; i < _chain.Count; i++)
        {
            var node = _chain[i];
            var s = node.Invoke(arg);
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
    List<Func<T1, T2, bool>> _chain = new List<Func<T1, T2, bool>>(2);

    public void Add(Func<T1, T2, bool> action)
    {
        _chain.Add(action);
    }

    public void Remove(Func<T1, T2, bool> action)
    {
        _chain.Remove(action);
    }

    public void SetAsFirst(Func<T1, T2, bool> action)
    {
        if (_chain.Count > 0 && _chain[0] == action)
        {
            return;
        }

        Remove(action);
        _chain.Insert(0, action);
    }

    public void SetAsLast(Func<T1, T2, bool> action)
    {
        Remove(action);
        Add(action);
    }

    public bool Invoke(T1 arg1,T2 arg2)
    {
        for (int i = 0; i < _chain.Count; i++)
        {
            var node = _chain[i];
            var s = node.Invoke(arg1,arg2);
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