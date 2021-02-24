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

public class Evt_Back : Evt_Base
{
    public static readonly ulong EvtType = MakeGuid();

    public GameMgr.GameState ActiveState { get;private set; }
    public override ulong GetEventType()
    {
        return EvtType;
    }

    public Evt_Back()
    {
        ActiveState = GameMgr.Instance.CurrState;
    }

    public bool Self(GameMgr.GameState state)
    {
        return state == ActiveState;
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

public class Evt_EnterArea : Evt_Base
{
    public static readonly ulong EvtType = MakeGuid();

    public int Id { get; private set; }

    public override ulong GetEventType()
    {
        return EvtType;
    }

    public Evt_EnterArea(int id)
    {
        Id = id;
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

public class Evt_ActorDie : Evt_Base
{
    public static readonly ulong EvtType = MakeGuid();

    public Actor Target { get; set; }

    public override ulong GetEventType()
    {
        return EvtType;
    }
}

public class Evt_ActorActionDone : Evt_Base
{
    public static readonly ulong EvtType = MakeGuid();

    public Actor Caller { get; set; }

    public override ulong GetEventType()
    {
        return EvtType;
    }
}
/// <summary>
/// 一方死亡，退出回合时也使用这个事件，Current为null
/// </summary>
public class Evt_InRound : Evt_Base
{
    public static readonly ulong EvtType = MakeGuid();

    public Actor Current { get; set; }

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
    public Actor Owner { get; set; }
    public Actor Target { get; set; }
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

public class Evt_ActorPropChange:Evt_Base
{
    public static readonly ulong EvtType = MakeGuid();

    public Actor Target { get; set; }
    public string PropName { get; set; }

    public override ulong GetEventType()
    {
        return EvtType;
    }
}

public class Evt_CmdExec : Evt_Base
{
    public static readonly ulong EvtType = MakeGuid();

    public Actor Target { get; set; }
    public Command Cmd { get; set; }

    public override ulong GetEventType()
    {
        return EvtType;
    }
}


public class Evt_AddBuff : Evt_Base
{
    public static readonly ulong EvtType = MakeGuid();

    public Actor Target { get; set; }
    public Buff Data { get; set; }

    public override ulong GetEventType()
    {
        return EvtType;
    }
}

public class Evt_RemoveBuff : Evt_Base
{
    public static readonly ulong EvtType = MakeGuid();

    public Actor Target { get; set; }
    public Buff Data { get; set; }

    public override ulong GetEventType()
    {
        return EvtType;
    }
}

public class Evt_UpdateBuff : Evt_Base
{
    public static readonly ulong EvtType = MakeGuid();

    public Actor Target { get; set; }
    public Buff Data { get; set; }

    public override ulong GetEventType()
    {
        return EvtType;
    }
}
