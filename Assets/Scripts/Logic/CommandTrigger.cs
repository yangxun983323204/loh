using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YX;

public class CommandTrigger
{
    public string EventKey { get; set; }
    public string CmdType { get; set; }
    public string ActType { get; set; }
    public float FArg { get; set; }
    public string SArg { get; set; }

    public Buff Parent { get; set; }

    private Command _cmd;
    private bool _triggerOnAdd = false;
    private bool _triggerOnRemove = false;

    public void Destroy()
    {
        EventManager.Instance.RemoveListener(EventKey, OnTrigger);
        OnRemove();
    }

    public void Init(Actor actor)
    {
        _cmd = new Command();
        _cmd.Cmd = (Command.CmdType)Enum.Parse(typeof(Command.CmdType), CmdType);
        _cmd.ActType = (Command.ActionType)Enum.Parse(typeof(Command.ActionType), ActType);
        _cmd.FArg = FArg;
        _cmd.SArg = SArg;
        _cmd.SetCaller(actor);

        EventKeyPreprocess();
        if (EventKey == Buff.EVENT_SELF_Add)
            _triggerOnAdd = true;
        else if (EventKey == Buff.EVENT_SELF_REMOVE)
            _triggerOnRemove = true;

        if (!_triggerOnAdd && !_triggerOnRemove)
            EventManager.Instance.AddListener(EventKey, OnTrigger);
        
        OnAdd();
    }

    private void OnTrigger(EventDataBase evt)
    {
        InScope();
        _cmd.Execute();
        OutScope();
    }

    private void OnAdd()
    {
        InScope();
        if (_triggerOnAdd)
        {
            _cmd.Execute();
        }
        OutScope();
    }

    private void OnRemove()
    {
        InScope();
        if (_triggerOnRemove)
        {
            _cmd.Execute();
        }
        OutScope();
    }

    private void EventKeyPreprocess()
    {
        EventKey = EventKey.Replace("{self}", _cmd.Caller.Name);
    }

    public override string ToString()
    {
        return string.Format("{0}->{1}", EventKey, _cmd.ToString());
    }

    private void InScope()
    {
        GameMgr.Instance.Scope.buff = Parent;
    }

    private void OutScope()
    {
        GameMgr.Instance.Scope.buff = null;
    }
}
