using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Command;

public class MetaExCmdImpl:ICommandExec
{
    public bool Execute(CommandEnv env, Command cmd)
    {
        switch (cmd.Type)
        {
            case CmdType.AddWith:
                var v1 = env.PopNum();
                env.PushNum(v1 + cmd.GetArgNum(0));
                break;
            case CmdType.MutiWith:
                v1 = env.PopNum();
                env.PushNum(v1 * cmd.GetArgNum(0));
                break;
            case CmdType.LessWith:
                v1 = env.PopNum();
                env.PushNum(v1 < cmd.GetArgNum(0) ? 0 : 1);
                break;
            case CmdType.EqualWith:
                v1 = env.PopNum();
                env.PushNum(v1 == cmd.GetArgNum(0) ? 0 : 1);
                break;
            case CmdType.GreaterWith:
                v1 = env.PopNum();
                env.PushNum(v1 > cmd.GetArgNum(0) ? 0 : 1);
                break;
            default:
                return true;// 没有处理
        }

        return false;// 已经处理
    }
}
