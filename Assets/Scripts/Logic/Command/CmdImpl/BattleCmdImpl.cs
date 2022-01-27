using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Command;

public class BattleCmdImpl:ICommandExec
{
    List<Actor> targets = new List<Actor>(2);

    public bool Execute(CommandEnv env, Command cmd)
    {
        targets.Clear();
        if (cmd.ActType == ActionType.Self)
            targets.Add(env.Self);
        else if (cmd.ActType == ActionType.Enemy)
            targets.Add(env.Enemy);
        else if(cmd.ActType == ActionType.Both)
        {
            targets.Add(env.Self);
            targets.Add(env.Enemy);
        }

        switch (cmd.Type)
        {
            case CmdType.IsRoundOf:
                var a = env.Self;
                var curr = (GameMgr.Instance.CurrState as BattleState).CurrentActor;
                env.PushNum(a == curr ? 0 : 1);
                break;
            // 血、蓝改变
            case CmdType.HpChange:
                var fArg = env.PopNum();
                foreach (var tar in targets)
                    tar.ChangeHp(fArg);
                break;
            case CmdType.EpChange:
                fArg = env.PopNum();
                foreach (var tar in targets)
                    tar.ChangeAp(fArg);
                break;
            // buff增加和移除
            case CmdType.AddBuff:
                fArg = env.PopNum();
                foreach (var tar in targets)
                    tar.AddBuff((int)fArg);
                break;
            case CmdType.RemoveBuff:
                fArg = env.PopNum();
                foreach (var tar in targets)
                    tar.RemoveBuff((int)fArg);
                break;
            //
            case CmdType.PlayFx:
                var sArg = env.PopStr();
                foreach (var tar in targets)
                    GameMgr.Instance.FxMgr.PlayFx(sArg, tar);
                break;
            case CmdType.Say:
                sArg = env.PopStr();
                foreach (var tar in targets)
                    UnityEngine.Debug.LogFormat("{0}:{1}", tar, sArg);
                break;
            default:
                return false;// 没有处理
        }

        return true;// 已经处理
    }
}
