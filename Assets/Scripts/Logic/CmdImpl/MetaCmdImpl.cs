using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Command;

public static class MetaCmdImpl
{
    public static bool Execute(ref Command cmd,ref List<Actor> targets)
    {
        var type = cmd.Cmd;
        var Caller = cmd.Caller;
        var FArg = cmd.FArg;
        var SArg = cmd.SArg;
        var Other = cmd.Other;
        switch (type)
        {
            case CmdType.SetExecuteTrue:
                ExecuteEnable = true;
                break;
            case CmdType.SetExecuteFalse:
                ExecuteEnable = false;
                break;
            case CmdType.SetExecuteEnable:
                var e = g_bool.Pop();
                ExecuteEnable = e;
                break;
            case CmdType.Add:
                var v1 = g_FArgs.Pop();
                var v2 = g_FArgs.Pop();
                g_FArgs.Push(v1 + v2);
                break;
            case CmdType.Muti:
                v1 = g_FArgs.Pop();
                v2 = g_FArgs.Pop();
                g_FArgs.Push(v1 * v2);
                break;
            case CmdType.Less:
                v2 = g_FArgs.Pop();
                v1 = g_FArgs.Pop();
                g_bool.Push(v1 < v2);
                break;
            case CmdType.Equal:
                v2 = g_FArgs.Pop();
                v1 = g_FArgs.Pop();
                g_bool.Push(v1 == v2);
                break;
            case CmdType.Greater:
                v2 = g_FArgs.Pop();
                v1 = g_FArgs.Pop();
                g_bool.Push(v1 > v2);
                break;
            case CmdType.PushFloat:
                g_FArgs.Push(FArg);
                break;
            case CmdType.PushStr:
                g_SArgs.Push(SArg);
                break;
            case CmdType.PushTrue:
                g_bool.Push(true);
                break;
            case CmdType.PushFalse:
                g_bool.Push(false);
                break;
            case CmdType.PushCaller:
                g_OArgs.Push(Caller);
                break;
            case CmdType.PushOther:
                g_OArgs.Push(Other);
                break;
            case CmdType.EqualStr:
                var s0 = g_SArgs.Pop();
                var s1 = g_SArgs.Pop();
                g_bool.Push(s0 == s1);
                break;
            case CmdType.EqualActor:
                var a0 = g_OArgs.Pop();
                var a1 = g_OArgs.Pop();
                g_bool.Push(a0 == a1);
                break;
            default:
                return true;// 没有处理
        }

        return false;// 已经处理
    }
}
