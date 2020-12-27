using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using YX;

public class Command
{
    public enum CommandType
    {
        None,

        Weapon,

        Fire,
        Ice,
        Light,
        Gas,

        Divinity,
        Dark,
    }

    public enum CommandKey
    {
        None,

        HpChange,
        MpChange,
        ApChange,

        HpSet,
        MpSet,
        ApSet,
    }

    public Actor Caller;
    public Actor Target;

    public string Key;
    public string Type;
    public float NumArg;
    public string StrArg;

    private CommandKey? _key;
    private CommandType? _type;

    public CommandKey GetCmdKey()
    {
        if (!_key.HasValue)
        {
            try
            {
                _key = (CommandKey)Enum.Parse(typeof(CommandKey), Key);
            }
            catch (Exception)
            {
                Debug.LogError("解析CommandKey失败");
                _key = CommandKey.None;
            }
        }

        return _key.Value;
    }

    public CommandType GetCmdType()
    {
        if (!_type.HasValue)
        {
            try
            {
                _type = (CommandType)Enum.Parse(typeof(CommandType), Type);
            }
            catch (Exception)
            {
                Debug.LogWarning("解析CommandType失败");
                _type = CommandType.None;
            }
        }

        return _type.Value;
    }

    public void Execute()
    {
        if (GetCmdKey() == CommandKey.None)
            return;

        EventManager.Instance.QueueEvent(new Evt_CmdExec() {
            Target = Target,
            Cmd = this
        });

        switch (GetCmdKey())
        {
            case CommandKey.HpChange:
                Target.SetHp(Target.Hp + NumArg);
                break;
            case CommandKey.MpChange:
                Target.SetMp(Target.Mp + NumArg);
                break;
            case CommandKey.ApChange:
                Target.SetAp(Target.Ap + NumArg);
                break;
            case CommandKey.HpSet:
                Target.SetHp(NumArg);
                break;
            case CommandKey.MpSet:
                Target.SetMp(NumArg);
                break;
            case CommandKey.ApSet:
                Target.SetAp(NumArg);
                break;
            default:
                break;
        }
    }

    public static Command[] Load(string json)
    {
        return JsonConvert.DeserializeObject<Command[]>(json);
    }
}
