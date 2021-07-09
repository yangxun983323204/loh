using System.Collections;
using System.Collections.Generic;
using YX;

public partial class Command
{
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
        if (Cmd == CmdType.None)
            return true;

        EventManager.Instance.QueueEvent(new Evt_CmdExec()
        {
            Target = Other,
            Cmd = this
        });

        var targets = GetCmdTargets();

        switch (Cmd)
        {
            case CmdType.SetIdx:
                g_idx = (int)FArg;
                break;
            case CmdType.SetFloat:
                g_FArgs[g_idx] = FArg;
                break;
            case CmdType.SetStr:
                g_SArgs[g_idx] = SArg;
                break;
            // 血、蓝改变
            case CmdType.HpChange:
                foreach (var tar in targets)
                    tar.ChangeHp(FArg);
                break;
            case CmdType.MpChange:
                foreach (var tar in targets)
                    tar.ChangeMp(FArg);
                break;
            case CmdType.ApChange:
                foreach (var tar in targets)
                    tar.ChangeAp(FArg);
                break;
            // buff增加和移除
            case CmdType.AddBuff:
                foreach (var tar in targets)
                    tar.AddBuff((int)FArg);
                break;
            case CmdType.RemoveBuff:
                foreach (var tar in targets)
                    tar.RemoveBuff((int)FArg);
                break;
            //
            case CmdType.PlayFx:
                foreach (var tar in targets)
                    GameMgr.Instance.FxMgr.PlayFx(SArg, tar);
                break;
            case CmdType.Say:
                foreach (var tar in targets)
                    UnityEngine.Debug.LogFormat("{0}:{1}", tar, SArg);
                break;
            default:
                break;
        }

        return true;
    }
}
