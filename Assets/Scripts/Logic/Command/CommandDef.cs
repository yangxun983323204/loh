using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command
{
    const int ARG_OFFSET = 2;

    string[] Data;

    public Command() { }

    public Command(params string[] data)
    {
        Data = data;
    }

    public Command(CmdType type, ActionType actType,params string[] data)
    {
        Data = new string[data.Length + 2];
        Data[0] = type.ToString();
        Data[1] = actType.ToString();
        Array.Copy(data, 0, Data, 2, data.Length);
    }

    public CmdType Type
    {
        get
        {
            return (CmdType)Enum.Parse(typeof(CmdType), GetStr(0));
        }
    }

    public ActionType ActType
    {
        get
        {
            return (ActionType)Enum.Parse(typeof(ActionType), GetStr(1));
        }
    }

    public string GetStr(int i)
    {
        return Data[i];
    }

    public float GetNum(int i)
    {
        return float.Parse(Data[i]);
    }

    public string GetArgStr(int i)
    {
        return Data[i + ARG_OFFSET];
    }

    public float GetArgNum(int i)
    {
        return float.Parse(Data[i + ARG_OFFSET]);
    }

    public override string ToString()
    {
        return string.Format("{0},{1}", Type, ActType);
    }
}

public enum ActionType
{
    None,
    Self,
    Enemy,
    Both,
}

public enum CmdType
{
    None = 0,
    // 算术
    Add,
    Muti,

    AddWith,
    MutiWith,
    // 逻辑，成立时入栈值为0，不成立时入栈值为1
    Neg,
    Less,
    Equal,
    Greater,

    EqualStr,

    LessWith,
    EqualWith,
    GreaterWith,
    // 栈操作
    PushNum,
    PushStr,
    // 指令控制
    JMP, // 无条件
    JGT, // 大于0
    JEQ, // 等于0
    JLE, // 小于0
    JNE, // 不等于0
    JNG, // 不大于0
    JNL, // 不小于0

    IsRoundOf,

    HpChange,
    EpChange,

    HpSet,
    EpSet,

    AddBuff,
    RemoveBuff,

    Say,
    PlayFx,
}
