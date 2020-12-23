using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    private void OnLoadScene(string name)
    {
        if (name == "Battle")
        {
            var gMgr = GameMgr.Create();
            var btMgr = gMgr.BattleMgr;
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
        yield return null;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Actor>();
        Enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Actor>();

        Player.SetData(playerRec);
        Player.gameObject.GetComponent<CardPlayer>().Init(playerDeck);
        Player.gameObject.GetComponent<PlayerView>().Init(Player);

        Enemy.SetData(enemyRec);
        Enemy.gameObject.GetComponent<CardPlayer>().Init(enemyDeck);
        Enemy.gameObject.GetComponent<EnemyView>().Init(Enemy);

        Player.gameObject.GetComponent<CardPlayer>().Take(2);
        Enemy.gameObject.GetComponent<CardPlayer>().Take(2);
    }
}
