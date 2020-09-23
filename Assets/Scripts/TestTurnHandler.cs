using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTurnHandler : TurnHandlerBase
{
    private DataBase _skillDB = null;

    private bool _inited = false;
    private IEnumerator Start()
    {
        var iter = AssetsMgr.Instance.Get<DataBase>("DB/SkillDB");
        yield return iter;
        _skillDB = iter.Asset<DataBase>();
        _inited = true;
    }

    public override void StartTurn()
    {
        StartCoroutine(TurnOperate());
    }

    private IEnumerator TurnOperate()
    {
        _isDone = false;
        while (!_inited)
            yield return 0;
        var actor = gameObject.GetComponent<ActorRenderer>().Self;
        var target = BattleMgr.Instance.GetEnemy(actor, 0);
        var skill = _skillDB.At<Skill>(0);
        yield return actor.Cast(target, skill);
        _isDone = true;
    }

    public override void ForceStop()
    {
        _isDone = true;
    }
}
