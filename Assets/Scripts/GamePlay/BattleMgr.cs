using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMgr : MonoBehaviour
{
    public static BattleMgr Instance;

    public Actor Player;
    public Actor Enemy;

    public void Create(ActorRecord playerRec,Deck playerDeck,ActorRecord enemyRec,Deck enemyDeck)
    {
        Player.SetData(playerRec);
        Player.gameObject.GetComponent<CardPlayer>().Init(playerDeck);
        Player.gameObject.GetComponent<PlayerView>().Init(Player);

        Enemy.SetData(enemyRec);
        Enemy.gameObject.GetComponent<CardPlayer>().Init(enemyDeck);
        Enemy.gameObject.GetComponent<EnemyView>().Init(Enemy);

        Player.gameObject.GetComponent<CardPlayer>().Take(2);
        Enemy.gameObject.GetComponent<CardPlayer>().Take(2);
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
