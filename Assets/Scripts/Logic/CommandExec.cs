using System.Collections;
using System.Collections.Generic;
using YX;

public partial class Command
{
    public static bool ExecuteEnable { get; private set; } = true;

    public Actor Caller { get; private set; }
    public Actor Other { get; private set; }
    public string SArg;
    public float FArg;

    public CmdType Cmd = CmdType.None;
    public ActionType ActType = ActionType.Self;


    public void SetCaller(Actor caller)
    {
        Caller = caller;
        Other = (GameMgr.Instance.CurrState as BattleState).GetAnother(caller);
    }

    public override string ToString()
    {
        return string.Format("cmd:{0},type:{1},caller:{2}", Cmd.ToString(), ActType.ToString(), Caller.ToString());
    }

    List<Actor> _targets = new List<Actor>(2);
    public List<Actor> GetCmdTargets()
    {
        _targets.Clear();
        switch (ActType)
        {
            case ActionType.None:
                break;
            case ActionType.Self:
                _targets.Add(Caller);
                break;
            case ActionType.Other:
                _targets.Add(Other);
                break;
            case ActionType.Both:
                _targets.Add(Other);
                _targets.Add(Caller);
                break;
            default:
                break;
        }
        return _targets;
    }

    public bool Execute()
    {
        if (!ExecuteEnable && Cmd != CmdType.PushTrue && Cmd!= CmdType.SetExecuteEnable)
            return false;

        var targets = GetCmdTargets();
        EventManager.Instance.QueueEvent(new Evt_CmdExec()
        {
            Targets = targets,
            Cmd = this
        });
        float fArg;
        string sArg;

        switch (Cmd)
        {
            #region 变量操作
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
            #endregion
            //
            #region 变量比较
            case CmdType.CompareActor:
                var a0 = g_OArgs.Pop();
                var a1 = g_OArgs.Pop();
                g_bool.Push(a0 == a1);
                break;
            case CmdType.IsRoundOf:
                var a = g_OArgs.Pop();
                var curr = (GameMgr.Instance.CurrState as BattleState).CurrentActor;
                g_bool.Push(a == curr);
                break;
            #endregion
            // 条件判定
            case CmdType.SetExecuteEnable:
                var e = g_bool.Pop();
                ExecuteEnable = e;
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
                break;
        }

        return true;
    }
}
