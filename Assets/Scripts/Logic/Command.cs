using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using YX;

public partial class Command
{
    public enum CmdType
    {
        None = 0,

        SetExecuteEnable,
        SetExecuteTrue,
        SetExecuteFalse,

        Add,
        AddWith,
        Muti,
        MutiWith,

        Less,
        LessWith,
        Equal,
        EqualWith,
        Greater,
        GreaterWith,

        PushFloat,
        PushStr,
        PushTrue,
        PushFalse,
        PushCaller,
        PushOther,

        EqualStr,
        EqualActor,
        

        IsRoundOf,

        HpChange,
        MpChange,
        ApChange,

        HpSet,
        MpSet,
        ApSet,

        AddBuff,
        RemoveBuff,
        ChangeBuffTier,

        PlayFx,
        Say,
    }

    public enum ActionType
    {
        None,
        Self,
        Other,
        Both,
    }

    internal static Stack<float> g_FArgs = new Stack<float>(256);
    internal static Stack<string> g_SArgs = new Stack<string>(256);
    internal static Stack<Actor> g_OArgs = new Stack<Actor>();
    internal static Stack<bool> g_bool = new Stack<bool>();
}
