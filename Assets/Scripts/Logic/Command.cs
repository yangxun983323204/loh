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
        Nature,
    }

    public enum CommandKey
    {
        None,

        HpChange,
        MpChange,
        ApChange,

        AddBuff,
        RemoveBuff,
    }

    public Actor Caller;
    public Actor Target;

    public string Key;
    public string Type;
    public float NumArg;
    public int IntArg { get { return (int)NumArg; } }
    public string StrArg;
    public T GetStrEnum<T>() where T:Enum { return (T)Enum.Parse(typeof(T), StrArg); }

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
            // 血、蓝改变
            case CommandKey.HpChange:
                Target.ChangeHp(NumArg, GetCmdType());
                break;
            case CommandKey.MpChange:
                Target.ChangeMp(NumArg, GetCmdType());
                break;
            case CommandKey.ApChange:
                Target.ChangeAp(NumArg, GetCmdType());
                break;
            // buff增加和移除
            case CommandKey.AddBuff:
                Target.AddBuff(GetStrEnum<Buff.BuffType>(),IntArg);
                break;
            case CommandKey.RemoveBuff:
                Target.RemoveBuff(GetStrEnum<Buff.BuffType>());
                break;
            //
            default:
                break;
        }
    }

    public static Command[] Load(string json)
    {
        return JsonConvert.DeserializeObject<Command[]>(json);
    }
}
