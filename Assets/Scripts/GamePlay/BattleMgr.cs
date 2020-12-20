using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleMgr : MonoBehaviour
{
    public Actor Player;
    public Actor Enemy;

    public void Create(ActorRecord playerRec,Deck playerDeck,ActorRecord enemyRec,Deck enemyDeck)
    {
        StartCoroutine(CreateImpl(playerRec, playerDeck, enemyRec, enemyDeck));
    }

    private IEnumerator CreateImpl(ActorRecord playerRec, Deck playerDeck, ActorRecord enemyRec, Deck enemyDeck)
    {
        SceneManager.LoadScene("Battle", LoadSceneMode.Single);
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
