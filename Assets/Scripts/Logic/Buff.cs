using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff:IdObj
{
    private static MatchId _checker = new MatchId(typeof(Buff), 0);
    public static Buff Create(int id)
    {
        var buff = GameMgr.Instance.DB.Find<Buff>(_checker.SetId(id).Check);
        foreach (var t in buff.Triggers)
        {
            t.Init();
        }
        return buff;
    }

    public Actor Owner { get; private set; }
    public int Count { get; set; }

    public string Name { get; set; }
    public string Desc { get; set; }
    public string View { get; set; }

    public CommandTrigger[] Triggers;

    public void SetOwner(Actor owner)
    {
        Owner = owner;
        foreach (var t in Triggers)
        {
            t.SetOwner(owner);
        }
    }
    public void Destroy()
    {
        Owner = null;
        foreach (var t in Triggers)
        {
            t.Destroy();
        }
    }
    public void Overlay(Buff buff)
    {
        if (buff.Id == Id)
        {
            Count += buff.Count;
        }
    }
}
