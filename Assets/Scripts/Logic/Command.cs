using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using YX;

public class Command
{
    public enum CmdType
    {
        None,

        HpChange,
        MpChange,
        ApChange,

        HpSet,
        MpSet,
        ApSet,

        AddBuff,
        RemoveBuff,
    }

    public enum ActionType
    {
        None,
        Self,
        Other,
        Both,
    }

    public Actor Caller { get;private set; }
    public Actor Other { get;private set; }

    public CmdType Cmd;
    public ActionType ActType;
    public float[] FArgs;
    public string[] SArgs;

    public void SetCaller(Actor caller)
    {
        Caller = caller;
        Other = (GameMgr.Instance.CurrState as BattleState).GetAnother(caller);
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
        if (Cmd == CmdType.None)
            return true;

        EventManager.Instance.QueueEvent(new Evt_CmdExec() {
            Target = Other,
            Cmd = this
        });

        var targets = GetCmdTargets();

        switch (Cmd)
        {
            // 血、蓝改变
            case CmdType.HpChange:
                foreach (var tar in targets)
                    tar.ChangeHp(FArgs[0]);
                break;
            case CmdType.MpChange:
                foreach (var tar in targets)
                    tar.ChangeMp(FArgs[0]);
                break;
            case CmdType.ApChange:
                foreach (var tar in targets)
                    tar.ChangeAp(FArgs[0]);
                break;
            // buff增加和移除
            case CmdType.AddBuff:
                foreach (var tar in targets)
                    tar.AddBuff((int)FArgs[0]);
                break;
            case CmdType.RemoveBuff:
                foreach (var tar in targets)
                    tar.RemoveBuff((int)FArgs[0]);
                break;
            //
            default:
                break;
        }

        return true;
    }
}
