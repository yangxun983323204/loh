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

        PushFloat,
        PushStr,
        PushTrue,
        PushFalse,
        PushCaller,
        PushOther,

        CompareFloat,
        CompareStr,
        CompareActor,
        SetExecuteEnable,

        IsRoundOf,

        HpChange,
        MpChange,
        ApChange,

        HpSet,
        MpSet,
        ApSet,

        AddBuff,
        RemoveBuff,

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

    private static Stack<float> g_FArgs = new Stack<float>(256);
    private static Stack<string> g_SArgs = new Stack<string>(256);
    private static Stack<Actor> g_OArgs = new Stack<Actor>();
    private static Stack<bool> g_bool = new Stack<bool>();
}
