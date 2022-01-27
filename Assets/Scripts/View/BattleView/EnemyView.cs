using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YX;

public class EnemyView : MonoBehaviour
{
    Actor _self = null;
    bool _inRound = false;
    float _thinkSpan = 3;
    float _currTime = 0;

    CommandEnv _cmdEnv = new CommandEnv();

    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.AddListener(Evt_InitBattle.EvtType, OnInitBattle);
        EventManager.Instance.AddListener(Evt_InRound.EvtType, OnInRound);
    }
    private void OnDestroy()
    {
        EventManager.Instance.RemoveListener(Evt_InitBattle.EvtType, OnInitBattle);
        EventManager.Instance.RemoveListener(Evt_InRound.EvtType, OnInRound);
    }
    // Update is called once per frame
    void Update()
    {
        if (!_inRound)
            return;

        _currTime += Time.deltaTime;
        if (_currTime>_thinkSpan)
        {
            _currTime = 0;
            Think();
        }
    }

    void OnInitBattle(EventDataBase e)
    {
        var evt = e as Evt_InitBattle;
        _self = evt.Enemy;
    }
    void OnInRound(EventDataBase e)
    {
        var evt = e as Evt_InRound;
        _inRound = evt.Current == _self;
        _currTime = 0;
    }

    void Think()
    {
        _cmdEnv.Clear();
        var sArg = new Command(CmdType.PushStr, ActionType.None, "我打!".Dye(Color.yellow));
        var say = new Command(CmdType.Say,ActionType.Self);
        _cmdEnv.AddCommand(sArg);
        _cmdEnv.AddCommand(say);
        _cmdEnv.Run();

        if (_self.Play.HandCount > 0)
        {
            var battleState = GameMgr.Instance.CurrState as BattleState;
            var card = _self.Play.HandLeft(0);
            EventManager.Instance.TriggerEvent(
                new Evt_TryPlayCard()
                {
                    Owner = _self,
                    Target = battleState.GetAnother(_self),
                    Card = card
                });
        }

        var evt = new Evt_ActorActionDone() { Caller = _self };
        EventManager.Instance.QueueEvent(evt);
    }
}
