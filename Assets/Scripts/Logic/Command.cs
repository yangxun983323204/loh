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

        SetIdx,
        SetFloat,
        SetStr,

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

    private static int g_idx = 0;
    private static float[] g_FArgs = new float[256];
    private static string[] g_SArgs = new string[256];
}
