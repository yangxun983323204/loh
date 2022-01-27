using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff
{
    public BuffRecord Data;
    
    public static Buff Create(int id)
    {
        var rec = GameMgr.Instance.DB.Find<BuffRecord>(BuffRecord.Checker.SetId(id).Check);
        var inst = new Buff() { Data = rec };
        return inst;
    }

    public Actor Owner { get; private set; }

    public void SetOwner(Actor owner)
    {
        Owner = owner;
    }
    public void Destroy()
    {
        Owner = null;
    }

    public void Overlay(Buff buff)
    {
        if (buff.Data.Id == Data.Id && Data.CanMerge)
        {
            Data.Count += buff.Data.Count;
        }
    }

    public override string ToString()
    {
        return Data.Name;
    }
}
