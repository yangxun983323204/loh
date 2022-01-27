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
}

public class Evt_StartGame : Evt_Base
{
    public static readonly string EvtType = "Evt_StartGame";

    public override string GetEventType()
    {
        return EvtType;
    }
}

public class Evt_Back : Evt_Base
{
    public static readonly string EvtType = "Evt_Back";

    public GameMgr.GameState ActiveState { get;private set; }
    public override string GetEventType()
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
    public static readonly string EvtType = "Evt_Quit";

    public override string GetEventType()
    {
        return EvtType;
    }
}

public class Evt_EnterArea : Evt_Base
{
    public static readonly string EvtType = "Evt_EnterArea";

    public int Id { get; private set; }

    public override string GetEventType()
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
    public static readonly string EvtType = "Evt_InitBattle";

    public Actor Player { get; set; }
    public Actor Enemy { get; set; }

    public override string GetEventType()
    {
        return EvtType;
    }
}

public class Evt_ActorDie : Evt_Base
{
    public static readonly string EvtType = "Evt_ActorDie";

    public Actor Target { get; set; }

    public override string GetEventType()
    {
        return EvtType;
    }
}

public class Evt_ActorActionDone : Evt_Base
{
    public static readonly string EvtType = "Evt_ActorActionDone";

    public Actor Caller { get; set; }

    public override string GetEventType()
    {
        return EvtType;
    }
}

/// <summary>
/// 一方死亡，退出回合时也使用这个事件，Current为null
/// </summary>
public class Evt_InRound : Evt_Base
{
    public static readonly string EvtType = "Evt_InRound";

    public Actor Current { get; set; }

    public override string GetEventType()
    {
        return EvtType;
    }
}

public class Evt_TakedCard:Evt_Base
{
    public static readonly string EvtType = "Evt_TakedCard";
    public Actor Owner { get; set; }
    public Card Card { get; set; }

    public override string GetEventType()
    {
        return EvtType;
    }
}

public class Evt_TakeCardFailed : Evt_Base
{
    public static readonly string EvtType = "Evt_TakeCardFailed";
    public Actor Owner { get; set; }

    public override string GetEventType()
    {
        return EvtType;
    }
}

public class Evt_TryPlayCard:Evt_Base
{
    public static readonly string EvtType = "Evt_TryPlayCard";
    public Actor Owner { get; set; }
    public Actor Target { get; set; }
    public Card Card { get; set; }

    public override string GetEventType()
    {
        return EvtType;
    }
}

public class Evt_PlayedCard:Evt_Base
{
    public static readonly string EvtType = "Evt_PlayedCard";
    public Actor Owner { get; set; }
    public Card Card { get; set; }

    public override string GetEventType()
    {
        return EvtType;
    }
}

public class Evt_DiscardCard : Evt_Base
{
    public static readonly string EvtType = "Evt_DiscardCard";
    public Actor Owner { get; set; }
    public Card Card { get; set; }

    public override string GetEventType()
    {
        return EvtType;
    }
}


public class Evt_ActorPropWillChange : Evt_Base
{
    public static readonly string EvtType = "Evt_ActorPropWillChange";

    public Actor Target { get; set; }

    public string PropName { get; set; }

    public float Value { get; set; }

    public override string GetEventType()
    {
        return EvtType;
    }
}

public class Evt_ActorPropChange:Evt_Base
{
    public static readonly string EvtType = "Evt_ActorPropChange";

    public Actor Target { get; set; }
    public string PropName { get; set; }

    public override string GetEventType()
    {
        return EvtType;
    }
}

public class Evt_CmdExec : Evt_Base
{
    public static readonly string EvtType = "Evt_CmdExec";

    public List<Actor> Targets { get; set; }
    public Command Cmd { get; set; }

    public override string GetEventType()
    {
        return EvtType;
    }
}


public class Evt_AddBuff : Evt_Base
{
    public static readonly string EvtType = "Evt_AddBuff";

    public Actor Target { get; set; }
    public Buff Data { get; set; }

    public override string GetEventType()
    {
        return EvtType;
    }
}

public class Evt_RemoveBuff : Evt_Base
{
    public static readonly string EvtType = "Evt_RemoveBuff";

    public Actor Target { get; set; }
    public Buff Data { get; set; }

    public override string GetEventType()
    {
        return EvtType;
    }
}

public class Evt_UpdateBuff : Evt_Base
{
    public static readonly string EvtType = "Evt_UpdateBuff";

    public Actor Target { get; set; }
    public Buff Data { get; set; }

    public override string GetEventType()
    {
        return EvtType;
    }
}

public class Evt_Dynamic : Evt_Base
{
    public override string GetEventType()
    {
        return _t;
    }

    private string _t;

    public Evt_Dynamic(string t)
    {
        _t = t;
    }
}