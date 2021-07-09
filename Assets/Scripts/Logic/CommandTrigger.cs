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

    public void Init()
    {
        _cmd = new Command();
        _cmd.Cmd = (Command.CmdType)Enum.Parse(typeof(Command.CmdType),CmdType);
        _cmd.ActType = (Command.ActionType)Enum.Parse(typeof(Command.ActionType), ActType);
        _cmd.FArg = FArg;
        _cmd.SArg = SArg;
        EventManager.Instance.AddListener(EventKey, OnTrigger);
    }

    public void Destroy()
    {
        EventManager.Instance.RemoveListener(EventKey, OnTrigger);
    }

    public void SetOwner(Actor actor)
    {
        _cmd.SetCaller(actor);
    }

    private void OnTrigger(EventDataBase evt)
    {
        _cmd.Execute();
    }
}
