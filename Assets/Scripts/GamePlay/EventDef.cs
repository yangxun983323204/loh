using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YX;

public abstract class Evt_Base : EventDataBase
{
    public override EventDataBase Clone()
    {
        throw new System.NotImplementedException();
    }

    public override string GetName()
    {
        return this.GetType().Name;
    }

    protected static ulong MakeGuid()
    {
        return (ulong)Guid.NewGuid().ToString().GetHashCode();
    }
}

public class Evt_TryTakeCard:Evt_Base
{
    public static readonly ulong EvtType = MakeGuid();
    public int Count { get; set; }

    public override ulong GetEventType()
    {
        return EvtType;
    }
}

public class Evt_TakedCard:Evt_Base
{
    public static readonly ulong EvtType = MakeGuid();
    public GameCard Card { get; set; }

    public override ulong GetEventType()
    {
        return EvtType;
    }
}

public class Evt_TryPlayCard:Evt_Base
{
    public static readonly ulong EvtType = MakeGuid();
    public GameCard Card { get; set; }

    public override ulong GetEventType()
    {
        return EvtType;
    }
}

public class Evt_PlayedCard:Evt_Base
{
    public static readonly ulong EvtType = MakeGuid();
    public GameCard Card { get; set; }

    public override ulong GetEventType()
    {
        return EvtType;
    }
}

public class Evt_ActorPropsChange:Evt_Base
{
    public static readonly ulong EvtType = MakeGuid();

    public Actor Target { get; set; }
    public int Hp { get; set; }
    public int Mp { get; set; }
    public int Ap { get; set; }
    public int MaxHp { get; set; }
    public int MaxMp { get; set; }
    public int MaxAp { get; set; }

    public override ulong GetEventType()
    {
        return EvtType;
    }
}

public class Evt_AddBuff : Evt_Base
{
    public static readonly ulong EvtType = MakeGuid();

    public Buff Data { get; set; }

    public override ulong GetEventType()
    {
        return EvtType;
    }
}

public class Evt_RemoveBuff : Evt_Base
{
    public static readonly ulong EvtType = MakeGuid();

    public Buff Data { get; set; }

    public override ulong GetEventType()
    {
        return EvtType;
    }
}
