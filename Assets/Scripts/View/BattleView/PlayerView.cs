﻿using System;
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

    public Button BtnNext;

    public RectTransform PlayerBuffList;
    public RectTransform EnemyBuffList;

    private Actor _player;
    private Actor _enemy;
    private HandLayout _handView;

    private Pool<GameObject> _buffPool;

    void Awake()
    {
        _buffPool = new Pool<GameObject>();
        _buffPool.SetTemplate(Instantiate(Resources.Load<GameObject>("Prefab/UI/Buff")), new GameObjectAllocator());
        _handView = gameObject.GetComponentInChildren<HandLayout>();
        Bind();
    }

    private void OnDestroy()
    {
        Unbind();
    }

    void Bind()
    {
        BtnNext.onClick.AddListener(() =>
        {
            var evt = new Evt_ActorActionDone() { Caller = _player };
            EventManager.Instance.QueueEvent(evt);
        });
        EventManager.Instance.AddListener(Evt_InitBattle.EvtType, OnInitBattle);
        EventManager.Instance.AddListener(Evt_InRound.EvtType, OnInRound);
        EventManager.Instance.AddListener(Evt_TakedCard.EvtType, OnTakeCard);
        EventManager.Instance.AddListener(Evt_ActorPropChange.EvtType, OnActorPropsChange);
        EventManager.Instance.AddListener(Evt_PlayedCard.EvtType, OnPlayCard);

        EventManager.Instance.AddListener(Evt_AddBuff.EvtType, OnAddBuff);
        EventManager.Instance.AddListener(Evt_UpdateBuff.EvtType, OnUpdateBuff);
        EventManager.Instance.AddListener(Evt_RemoveBuff.EvtType, OnRemoveBuff);
    }

    void Unbind()
    {
        EventManager.Instance.RemoveListener(Evt_InitBattle.EvtType, OnInitBattle);
        EventManager.Instance.RemoveListener(Evt_InRound.EvtType, OnInRound);
        EventManager.Instance.RemoveListener(Evt_TakedCard.EvtType, OnTakeCard);
        EventManager.Instance.RemoveListener(Evt_ActorPropChange.EvtType, OnActorPropsChange);
        EventManager.Instance.RemoveListener(Evt_PlayedCard.EvtType, OnPlayCard);

        EventManager.Instance.RemoveListener(Evt_AddBuff.EvtType, OnAddBuff);
        EventManager.Instance.RemoveListener(Evt_UpdateBuff.EvtType, OnUpdateBuff);
        EventManager.Instance.RemoveListener(Evt_RemoveBuff.EvtType, OnRemoveBuff);
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
        var e = evt as Evt_ActorPropChange;
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
        var res = Resources.Load<GameObject>(enemy.Data.View);
        var obj = Instantiate(res);
        obj.transform.SetParent(EnemyObj.transform);
        obj.transform.SetAsFirstSibling();
        UpdateEnemyInfo();
    }

    private void UpdateEnemyInfo()
    {
        EnemyAPCount.text = _enemy.Data.Ap.ToString();
        EnemyHP.value = (float)_enemy.Data.Hp / _enemy.Data.MaxHp;
    }

    private void UpdatePlayerInfo()
    {
        PlayerAPCount.text = _player.Data.Ap.ToString();
        PlayerHP.value = (float)_player.Data.Hp / _player.Data.MaxHp;
    }

    private void SpawnCardView(Actor actor,Card card)
    {
        var self = actor;
        var hand = PlayerObj.GetComponent<HandLayout>();

        var obj = GameMgr.Instance.CardPool.Spawn();
        obj.SetActive(true);
        var view = obj.GetComponentInChildren<CardView>();
        view.Owner = actor;
        view.Init(card);
        hand.Add(obj);
    }

    private void OnPlayCard(EventDataBase e)
    {
        var evt = e as Evt_PlayedCard;
        if (evt.Owner == _player)
        {
            var cardsView = _handView.GetComponentsInChildren<CardView>();
            var cv = Array.Find(cardsView, n => { return n.Data.Value.Id == evt.Card.Id; });
            cv.Data = null;
            var img = cv.gameObject.GetComponentInChildren<RawImage>();
            img.texture = null;
            _handView.Remove(cv.gameObject);
            GameMgr.Instance.CardPool.Recycle(cv.gameObject);
        }
    }

    private void OnAddBuff(EventDataBase e)
    {
        var evt = e as Evt_AddBuff;
        Debug.Assert(evt.Target != null);
        var buff = _buffPool.Spawn().GetComponent<BuffView>();
        buff.SetData(evt.Data.Data);

        if (evt.Target == _player)
            buff.transform.SetParent(PlayerBuffList,false);
        else
            buff.transform.SetParent(EnemyBuffList,false);
    }

    private void OnRemoveBuff(EventDataBase e)
    {
        var evt = e as Evt_RemoveBuff;
        Debug.Assert(evt.Target != null);
        Transform tran;
        if (evt.Target == _player)
            tran = PlayerBuffList;
        else
            tran = EnemyBuffList;

        int idx = -1;
        foreach (Transform item in tran)
        {
            if(item.GetComponent<BuffView>().Data.Id == evt.Data.Data.Id)
            {
                idx = item.GetSiblingIndex();
                break;
            }
        }

        if (idx>=0)
        {
            var t = tran.GetChild(idx);
            t.SetParent(null);
            _buffPool.Recycle(t.gameObject);
        }
    }

    private void OnUpdateBuff(EventDataBase e)
    {
        var evt = e as Evt_UpdateBuff;
        Debug.Assert(evt.Target != null);
        Transform tran;
        if (evt.Target == _player)
            tran = PlayerBuffList;
        else
            tran = EnemyBuffList;

        foreach (Transform item in tran)
        {
            var view = item.GetComponent<BuffView>();
            if (view.Data.Id == evt.Data.Data.Id)
            {
                view.Data = evt.Data.Data;
                view.UpdateProps();
                break;
            }
        }
    }

    private void OnInRound(EventDataBase e)
    {
        var evt = e as Evt_InRound;
        if (evt.Current == _enemy)
        {
            BtnNext.gameObject.SetActive(false);
            var cardviews = _handView.GetComponentsInChildren<CardView>();
            foreach (var c in cardviews)
            {
                c.Draggable = false;
            }
        }
        else if(evt.Current == _player)
        {
            BtnNext.gameObject.SetActive(true);
            var cardviews = _handView.GetComponentsInChildren<CardView>();
            foreach (var c in cardviews)
            {
                c.Draggable = true;
            }
        }
        else
        {
            BtnNext.gameObject.SetActive(false);
            var cardviews = _handView.GetComponentsInChildren<CardView>();
            foreach (var c in cardviews)
            {
                c.Draggable = false;
            }
        }
    }
}
