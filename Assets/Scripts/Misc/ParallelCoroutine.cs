using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ParallelCoroutine
{
    public static IEnumerable<TR> SelcetParallel<TS,TR>(this IEnumerable<TS> source,System.Func<TS,TR> selector)
    {
        List<TR> list = new List<TR>();
        foreach (var obj in source)
        {
            list.Add(selector(obj));
        }
        return list;
    }
}
