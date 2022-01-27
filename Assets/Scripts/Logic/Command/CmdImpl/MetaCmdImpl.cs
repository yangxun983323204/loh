using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Command;

public class MetaCmdImpl: ICommandExec
{
    public bool Execute(CommandEnv env, Command cmd)
    {
        switch (cmd.Type)
        {
            // 算术
            case CmdType.Add:
                var v1 = env.PopNum();
                var v2 = env.PopNum();
                env.PushNum(v1 + v2);
                break;
            case CmdType.Muti:
                v1 = env.PopNum();
                v2 = env.PopNum();
                env.PushNum(v1 * v2);
                break;
            // 逻辑
            case CmdType.Neg:
                v1 = env.PopNum();
                env.PushNum(-v1);
                break;
            case CmdType.Less:
                v2 = env.PopNum();
                v1 = env.PopNum();
                env.PushNum(v1 < v2 ? 0 : 1);
                break;
            case CmdType.Equal:
                v2 = env.PopNum();
                v1 = env.PopNum();
                env.PushNum(v1 == v2 ? 0 : 1);
                break;
            case CmdType.Greater:
                v2 = env.PopNum();
                v1 = env.PopNum();
                env.PushNum(v1 > v2 ? 0 : 1);
                break;
            case CmdType.EqualStr:
                var s2 = env.PopStr();
                var s1 = env.PopStr();
                env.PushNum(s1 == s2 ? 0 : 1);
                break;
            // 栈操作
            case CmdType.PushNum:
                env.PushNum(cmd.GetArgNum(0));
                break;
            case CmdType.PushStr:
                env.PushStr(cmd.GetArgStr(0));
                break;
            // 指令控制
            case CmdType.JMP:
                env.SetPointer((int)cmd.GetArgNum(0));
                break;
            case CmdType.JGT:
                v1 = env.PopNum();
                if (v1>0)
                    env.SetPointer((int)cmd.GetArgNum(0));
                break;
            case CmdType.JEQ:
                v1 = env.PopNum();
                if (v1 == 0)
                    env.SetPointer((int)cmd.GetArgNum(0));
                break;
            case CmdType.JLE:
                v1 = env.PopNum();
                if (v1 < 0)
                    env.SetPointer((int)cmd.GetArgNum(0));
                break;
            case CmdType.JNE:
                v1 = env.PopNum();
                if (v1 != 0)
                    env.SetPointer((int)cmd.GetArgNum(0));
                break;
            case CmdType.JNG:
                v1 = env.PopNum();
                if (v1 <= 0)
                    env.SetPointer((int)cmd.GetArgNum(0));
                break;
            case CmdType.JNL:
                v1 = env.PopNum();
                if (v1 >= 0)
                    env.SetPointer((int)cmd.GetArgNum(0));
                break;
            //
            default:
                return false;// 没有处理
        }

        return true;// 已经处理
    }
}
