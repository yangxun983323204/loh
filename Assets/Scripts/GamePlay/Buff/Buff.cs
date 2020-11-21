using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff
{
    public enum BuffType
    {

    }

    public BuffType Type;

    public abstract void Init(string str);
    public abstract void Update();
}
