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

public class Evt_StartGame : Evt_Base
{
    public static readonly ulong EvtType = MakeGuid();

    public override ulong GetEventType()
    {
        return EvtType;
    }
}

public class Evt_Quit : Evt_Base
{
    public static readonly ulong EvtType = MakeGuid();

    public override ulong GetEventType()
    {
        return EvtType;
    }
}

public class Evt_InitBattle : Evt_Base
{
    public static readonly ulong EvtType = MakeGuid();

    public Actor Player { get; set; }
    public Actor Enemy { get; set; }

    public override ulong GetEventType()
    {
        return EvtType;
    }
}

public class Evt_TakedCard:Evt_Base
{
    public static readonly ulong EvtType = MakeGuid();
    public Actor Owner { get; set; }
    public GameCard Card { get; set; }

    public override ulong GetEventType()
    {
        return EvtType;
    }
}

public class Evt_TakeCardFailed : Evt_Base
{
    public static readonly ulong EvtType = MakeGuid();
    public Actor Owner { get; set; }

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
    public Actor Owner { get; set; }
    public GameCard Card { get; set; }

    public override ulong GetEventType()
    {
        return EvtType;
    }
}

public class Evt_DiscardCard : Evt_Base
{
    public static readonly ulong EvtType = MakeGuid();
    public Actor Owner { get; set; }
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
