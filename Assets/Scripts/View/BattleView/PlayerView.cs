using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using YX;

public class PlayerView : MonoBehaviour
{
    public GameObject PlayerObj;
    public GameObject EnemyObj;

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
    }

    void Unbind()
    {
        EventManager.Instance.RemoveListener(Evt_InitBattle.EvtType, OnInitBattle);
        EventManager.Instance.RemoveListener(Evt_TakedCard.EvtType, OnTakeCard);
    }

    void OnInitBattle(EventDataBase evt)
    {
        var initEvt = evt as Evt_InitBattle;
        _player = initEvt.Player;
        _enemy = initEvt.Enemy;
        InitEnemy(_enemy);
    }

    void OnTakeCard(EventDataBase evt)
    {
        var takeEvt = evt as Evt_TakedCard;
        if (takeEvt.Owner == _player)
        {
            SpawnCardView(takeEvt.Owner, takeEvt.Card);
        }
    }

    public void InitEnemy(Actor enemy)
    {
        var res = Resources.Load<GameObject>(Path.Combine("ActorView", enemy.View));
        var obj = Instantiate(res);
        obj.transform.SetParent(EnemyObj.transform);
        obj.transform.SetAsFirstSibling();
    }

    private void SpawnCardView(Actor actor,Card card)
    {
        var self = actor;
        var hand = PlayerObj.GetComponent<HandLayout>();

        var obj = GameMgr.Instance.CardPool.Spawn();
        obj.SetActive(true);
        var view = obj.GetComponentInChildren<CardView>();
        view.Init(card as GameCard);
        (obj.transform as RectTransform).sizeDelta = new Vector2(280, 390);
        view.onEndDrag.Add(c => {
            if (self.CanPlayCard(c.Data))
            {
                hand.Remove(c.gameObject);
                self.Play.Play(c.Data);
                return false;
            }
            else
            {
                return true;
            }
        });
        hand.Add(obj);
    }
}
