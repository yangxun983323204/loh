using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjEx
{
    public static Actor Clone(this Actor obj)
    {
        var newActor = Object.Instantiate(obj);
        newActor.Data = Object.Instantiate(obj.Data);
        return newActor;
    }
}
