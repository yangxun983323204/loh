using System;
using System.Collections;
using System.Collections.Generic;
using YX;
using UnityEngine;

public partial class Command
{
    public static bool ExecuteEnable { get; set; } = true;
    public static ActionChain<Command,List<Actor>> g_Impls = new ActionChain<Command, List<Actor>>();

    public Actor Caller { get; private set; }
    public Actor Other { get; private set; }
    public string SArg;
    public float FArg;
    public float FArgEx;

    public CmdType Cmd = CmdType.None;
    public ActionType ActType = ActionType.Self;


    public void SetCaller(Actor caller)
    {
        Caller = caller;
        Other = (GameMgr.Instance.CurrState as BattleState).GetAnother(caller);
    }

    public override string ToString()
    {
        return string.Format("cmd:{0},type:{1},caller:{2}", Cmd.ToString(), ActType.ToString(), Caller.ToString());
    }

    List<Actor> _targets = new List<Actor>(2);
    public List<Actor> GetCmdTargets()
    {
        _targets.Clear();
        switch (ActType)
        {
            case ActionType.None:
                break;
            case ActionType.Self:
                _targets.Add(Caller);
                break;
            case ActionType.Other:
                _targets.Add(Other);
                break;
            case ActionType.Both:
                _targets.Add(Other);
                _targets.Add(Caller);
                break;
            default:
                break;
        }
        return _targets;
    }

    public bool Execute()
    {
        if (!ExecuteEnable && Cmd!= CmdType.SetExecuteTrue)
            return false;

        var targets = GetCmdTargets();
        EventManager.Instance.QueueEvent(new Evt_CmdExec()
        {
            Targets = targets,
            Cmd = this
        });

        var self = this;
        var noHandle = g_Impls.Invoke(ref self, ref targets);
        if (noHandle)
        {
            Debug.LogError("不支持的命令:" + ToString());
            return false;
        }

        return true;
    }
}
