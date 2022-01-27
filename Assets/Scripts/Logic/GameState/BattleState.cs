using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YX;

public class BattleState : GameMgr.GameState
{
    public Actor CurrentActor { get; private set; }
    public Actor Player { get; private set; }
    public Actor Enemy { get; private set; }

    public override string GetName()
    {
        return "BattleState";
    }

    public override void OnEnter()
    {
        GameMgr.Instance.LevelLoader.FadeTo(LevelLoader.Scene.Battle,OnLoadScene);
        Bind();
    }

    public override void OnExit()
    {
        Unbind();
    }

    public Actor GetAnother(Actor actor)
    {
        if (actor == Player)
            return Enemy;
        else if (actor == Enemy)
            return Player;
        else
            throw new System.NotSupportedException("非战斗对象");
    }

    public bool CanPlay(Actor actor, Card card)
    {
        return actor.CanPlayCard(card);
    }

    void Bind()
    {
        EventManager.Instance.AddListener(Evt_EnterArea.EvtType, OnEnterArea);
        EventManager.Instance.AddListener(Evt_TryPlayCard.EvtType, OnTryPlayCard);
        EventManager.Instance.AddListener(Evt_ActorActionDone.EvtType, OnActorActionDone);
        EventManager.Instance.AddListener(Evt_ActorDie.EvtType, OnActorDie);
    }
    void Unbind()
    {
        EventManager.Instance.RemoveListener(Evt_EnterArea.EvtType, OnEnterArea);
        EventManager.Instance.RemoveListener(Evt_TryPlayCard.EvtType, OnTryPlayCard);
        EventManager.Instance.RemoveListener(Evt_ActorActionDone.EvtType, OnActorActionDone);
        EventManager.Instance.RemoveListener(Evt_ActorDie.EvtType, OnActorDie);
    }

    private static MatchId<ActorRecord> _actorRecChecker = new MatchId<ActorRecord>(0);
    private static MatchId<DeckRecord> _deckRecChecker = new MatchId<DeckRecord>(0);

    private void OnLoadScene(LevelLoader.Scene sc)
    {
        if (sc == LevelLoader.Scene.Battle)
        {
            var gMgr = GameMgr.Create();
            var player = gMgr.DB.Find<ActorRecord>(_actorRecChecker.SetId(1).Check);
            var deck1 = gMgr.DB.Find<DeckRecord>(_deckRecChecker.SetId(1).Check);
            var enemy = gMgr.DB.Find<ActorRecord>(_actorRecChecker.SetId(2).Check);
            var deck2 = gMgr.DB.Find<DeckRecord>(_deckRecChecker.SetId(2).Check);
            GameMgr.Instance.StartCoroutine(InitScene(player, deck1.GetDeck(), enemy, deck2.GetDeck()));
        }
    }

    private IEnumerator InitScene(ActorRecord playerRec, CardSet playerDeck, ActorRecord enemyRec, CardSet enemyDeck)
    {
        yield return null;
        Player = new Actor();
        Enemy = new Actor();

        Player.Data = playerRec;
        Player.Play.Init(playerDeck);

        Enemy.Data = enemyRec;
        Enemy.Play.Init(enemyDeck);

        EventManager.Instance.QueueEvent(new Evt_InitBattle()
        {
            Player = Player,
            Enemy = Enemy
        });

        NextRound();
    }

    void OnEnterArea(YX.EventDataBase evt)
    {
        var e = evt as Evt_EnterArea;
    }

    void OnTryPlayCard(EventDataBase evt)
    {
        var evtTryPlay = evt as Evt_TryPlayCard;
        var owner = evtTryPlay.Owner;
        var target = evtTryPlay.Target;
        var card = evtTryPlay.Card;
        if (owner.PlayCard(card))
        {
            var cmds = card.Commands;
            if (cmds == null) return;
            Debug.Log("执行Card:" + card.ToString());
            owner.Env.SetCommandList(card.Commands);
            owner.Env.Run();
        }
    }

    void OnActorActionDone(EventDataBase e)
    {
        NextRound();
    }

    void OnActorDie(EventDataBase e)
    {
        var evt = e as Evt_ActorDie;
        if (evt.Target == Enemy)// 赢得战斗
        {
            Debug.Log("你赢得了战斗".Dye(Color.red));
            ExitRound();
        }
        else// 输了战斗
        {
            Debug.Log("你死亡了".Dye(Color.red));
            ExitRound();
            GameMgr.Instance.EnterState(GameMgr.StateType.MainMenu);
        }
    }

    private void NextRound()
    {

        if (CurrentActor != null)
            CurrentActor = GetAnother(CurrentActor);
        else
            CurrentActor = Player;

        CurrentActor.Play.Take(2);
        var evt = new Evt_InRound() { Current = CurrentActor };
        EventManager.Instance.QueueEvent(evt);
    }

    private void ExitRound()
    {
        CurrentActor = null;
        var evt = new Evt_InRound() { Current = CurrentActor };
        EventManager.Instance.QueueEvent(evt);
    }
}
