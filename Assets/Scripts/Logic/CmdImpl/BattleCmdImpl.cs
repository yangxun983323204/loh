using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Command;

public static class BattleCmdImpl
{
    public static bool Execute(ref Command cmd, ref List<Actor> targets)
    {
        var type = cmd.Cmd;
        var Caller = cmd.Caller;
        var FArg = cmd.FArg;
        var SArg = cmd.SArg;
        var Other = cmd.Other;

        float fArg;
        string sArg;
        switch (type)
        {
            case CmdType.IsRoundOf:
                var a = g_OArgs.Pop();
                var curr = (GameMgr.Instance.CurrState as BattleState).CurrentActor;
                g_bool.Push(a == curr);
                break;
            // 血、蓝改变
            case CmdType.HpChange:
                fArg = g_FArgs.Pop();
                foreach (var tar in targets)
                    tar.ChangeHp(fArg);
                break;
            case CmdType.MpChange:
                fArg = g_FArgs.Pop();
                foreach (var tar in targets)
                    tar.ChangeMp(fArg);
                break;
            case CmdType.ApChange:
                fArg = g_FArgs.Pop();
                foreach (var tar in targets)
                    tar.ChangeAp(fArg);
                break;
            // buff增加和移除
            case CmdType.AddBuff:
                fArg = g_FArgs.Pop();
                foreach (var tar in targets)
                    tar.AddBuff((int)fArg);
                break;
            case CmdType.RemoveBuff:
                fArg = g_FArgs.Pop();
                foreach (var tar in targets)
                    tar.RemoveBuff((int)fArg);
                break;
            case CmdType.ChangeBuffTier:
                fArg = g_FArgs.Pop();
                var buff = GameMgr.Instance.Scope.buff;
                if (buff!=null)
                    buff.Count += (int)fArg;
                else
                    Debug.LogWarningFormat("无法执行{0}，因为当前作用域内没有buff对象", cmd);

                break;
            //
            case CmdType.PlayFx:
                sArg = g_SArgs.Pop();
                foreach (var tar in targets)
                    GameMgr.Instance.FxMgr.PlayFx(sArg, tar);
                break;
            case CmdType.Say:
                sArg = g_SArgs.Pop();
                foreach (var tar in targets)
                    UnityEngine.Debug.LogFormat("{0}:{1}", tar, sArg);
                break;
            default:
                return true;// 没有处理
        }

        return false;// 已经处理
    }
}
