using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IDB:IDisposable
{
    public abstract T Find<T>(Predicate<T> func) where T : new();
    public abstract List<T> Finds<T>(Predicate<T> func) where T : new();
    public virtual void Dispose()
    {

    }
}
