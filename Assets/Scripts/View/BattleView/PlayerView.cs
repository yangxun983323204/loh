using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using YX;

public class PlayerView : MonoBehaviour
{
    public GameObject PlayerObj;
    public GameObject EnemyObj;

    public Text EnemyAPCount;
    public Slider EnemyHP;
    public Slider EnemyMP;

    public Text PlayerAPCount;
    public Slider PlayerHP;
    public Slider PlayerMP;

    private Actor _player;
    private Actor _enemy;
    private HandLayout _handView;

    void Awake()
    {
        _handView = gameObject.GetComponentInChildren<HandLayout>();
        Bind();
    }

    private void OnDestroy()
    {
        Unbind();
    }

    void Bind()
    {
        EventManager.Instance.AddListener(Evt_InitBattle.EvtType, OnInitBattle);
        EventManager.Instance.AddListener(Evt_TakedCard.EvtType, OnTakeCard);
        EventManager.Instance.AddListener(Evt_ActorPropsChange.EvtType, OnActorPropsChange);
        EventManager.Instance.AddListener(Evt_PlayedCard.EvtType, OnPlayCard);
    }

    void Unbind()
    {
        EventManager.Instance.RemoveListener(Evt_InitBattle.EvtType, OnInitBattle);
        EventManager.Instance.RemoveListener(Evt_TakedCard.EvtType, OnTakeCard);
        EventManager.Instance.RemoveListener(Evt_ActorPropsChange.EvtType, OnActorPropsChange);
        EventManager.Instance.RemoveListener(Evt_PlayedCard.EvtType, OnPlayCard);
    }

    void OnInitBattle(EventDataBase evt)
    {
        var initEvt = evt as Evt_InitBattle;
        _player = initEvt.Player;
        _enemy = initEvt.Enemy;
        InitEnemy(_enemy);
        UpdateEnemyInfo();
        UpdatePlayerInfo();
    }

    void OnTakeCard(EventDataBase evt)
    {
        var takeEvt = evt as Evt_TakedCard;
        if (takeEvt.Owner == _player)
        {
            SpawnCardView(takeEvt.Owner, takeEvt.Card);
        }
    }

    void OnActorPropsChange(EventDataBase evt)
    {
        var e = evt as Evt_ActorPropsChange;
        var target = e.Target;
        if (target == _player)
        {
            UpdatePlayerInfo();
        }
        else
        {
            UpdateEnemyInfo();
        }
    }

    public void InitEnemy(Actor enemy)
    {
        var res = Resources.Load<GameObject>(Path.Combine("ActorView", enemy.View));
        var obj = Instantiate(res);
        obj.transform.SetParent(EnemyObj.transform);
        obj.transform.SetAsFirstSibling();
        UpdateEnemyInfo();
    }

    private void UpdateEnemyInfo()
    {
        EnemyAPCount.text = _enemy.Ap.ToString();
        EnemyHP.value = (float)_enemy.Hp / _enemy.HpMax;
        EnemyMP.value = (float)_enemy.Mp / _enemy.MpMax;
    }

    private void UpdatePlayerInfo()
    {
        PlayerAPCount.text = _player.Ap.ToString();
        PlayerHP.value = (float)_player.Hp / _player.HpMax;
        PlayerMP.value = (float)_player.Mp / _player.MpMax;
    }

    private void SpawnCardView(Actor actor,Card card)
    {
        var self = actor;
        var hand = PlayerObj.GetComponent<HandLayout>();

        var obj = GameMgr.Instance.CardPool.Spawn();
        obj.SetActive(true);
        var view = obj.GetComponentInChildren<CardView>();
        view.Owner = actor;
        view.Init(card as GameCard);
        hand.Add(obj);
    }

    private void OnPlayCard(EventDataBase e)
    {
        var evt = e as Evt_PlayedCard;
        if (evt.Owner == _player)
        {
            var cardsView = _handView.GetComponentsInChildren<CardView>();
            var cv = Array.Find(cardsView, n => { return n.Data == evt.Card; });
            cv.Data = null;
            var img = cv.gameObject.GetComponentInChildren<RawImage>();
            img.texture = null;
            _handView.Remove(cv.gameObject);
            GameMgr.Instance.CardPool.Recycle(cv.gameObject);
        }
    }
}
