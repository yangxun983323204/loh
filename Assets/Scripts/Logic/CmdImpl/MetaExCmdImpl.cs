using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Command;

public static class MetaExCmdImpl
{
    public static bool Execute(ref Command cmd, ref List<Actor> targets)
    {
        var type = cmd.Cmd;
        var Caller = cmd.Caller;
        var FArg = cmd.FArg;
        var SArg = cmd.SArg;
        var Other = cmd.Other;
        switch (type)
        {
            case CmdType.AddWith:
                var v1 = g_FArgs.Pop();
                g_FArgs.Push(v1 + FArg);
                break;
            case CmdType.MutiWith:
                v1 = g_FArgs.Pop();
                g_FArgs.Push(v1 * FArg);
                break;
            case CmdType.LessWith:
                v1 = g_FArgs.Pop();
                g_bool.Push(v1 < FArg);
                break;
            case CmdType.EqualWith:
                v1 = g_FArgs.Pop();
                g_bool.Push(v1 == FArg);
                break;
            case CmdType.GreaterWith:
                v1 = g_FArgs.Pop();
                g_bool.Push(v1 > FArg);
                break;
            default:
                return true;// 没有处理
        }

        return false;// 已经处理
    }
}
