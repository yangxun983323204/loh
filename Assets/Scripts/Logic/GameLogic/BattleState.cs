using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YX;

public class BattleState : GameMgr.GameState
{
    public Actor Player;
    public Actor Enemy;

    public override string GetName()
    {
        return "BattleState";
    }

    public override void OnEnter()
    {
        GameMgr.Instance.LevelLoader.FadeTo("Battle",OnLoadScene);
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

    public bool CanPlay(Actor actor,GameCard card)
    {
        return actor.CanPlayCard(card);
    }

    void Bind()
    {
        EventManager.Instance.AddListener(Evt_TryPlayCard.EvtType, OnTryPlayCard);
    }
    void Unbind()
    {
        EventManager.Instance.RemoveListener(Evt_TryPlayCard.EvtType, OnTryPlayCard);
    }

    private void OnLoadScene(string name)
    {
        if (name == "Battle")
        {
            var gMgr = GameMgr.Create();
            var player = gMgr.ActorDB.GetActor(1);
            var deck1 = gMgr.DeckDB.GetDeck(1);
            var enemy = gMgr.ActorDB.GetActor(2);
            var deck2 = gMgr.DeckDB.GetDeck(2);
            GameMgr.Instance.StartCoroutine(InitScene(player, deck1, enemy, deck2));
        }
    }

    private IEnumerator InitScene(ActorRecord playerRec, Deck playerDeck, ActorRecord enemyRec, Deck enemyDeck)
    {
        yield return null;
        Player = new Actor();
        Enemy = new Actor();

        Player.SetData(playerRec);
        Player.Play.Init(playerDeck);

        Enemy.SetData(enemyRec);
        Enemy.Play.Init(enemyDeck);

        EventManager.Instance.QueueEvent(new Evt_InitBattle()
        {
            Player = Player,
            Enemy = Enemy
        });

        Player.Play.Take(5);
        Enemy.Play.Take(2);
    }

    void OnTryPlayCard(EventDataBase evt)
    {
        var evtTryPlay = evt as Evt_TryPlayCard;
        var owner = evtTryPlay.Owner;
        var target = evtTryPlay.Target;
        var card = evtTryPlay.Card;
        if (owner.PlayCard(card))
        {
            var cmds = Command.Load(card.CommandsJson);
            if (cmds == null) return;
            foreach (var c in cmds)
            {
                c.Caller = owner;
                c.Target = target;
                c.Execute();
            }
        }
    }
}
