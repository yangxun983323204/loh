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

    public Actor GetSelf()
    {
        throw new System.NotImplementedException();
    }

    public Actor GetOpposite()
    {
        throw new System.NotImplementedException();
    }

    public CardView SpawnCardView(Actor actor,Card card)
    {
        var self = actor;
        var hand = self.gameObject.GetComponent<HandLayout>();

        var obj = GameMgr.Instance.CardPool.Spawn();
        obj.SetActive(true);
        var view = obj.GetComponentInChildren<CardView>();
        view.Init(card as GameCard);
        (obj.transform as RectTransform).sizeDelta = new Vector2(280, 390);
        view.onEndDrag.Add(c => {
            if (self.CanPlayCard(c.Data))
            {
                hand.Remove(c.gameObject);
                self.Player.Play(c.Data);
                return false;
            }
            else
            {
                return true;
            }
        });
        hand.Add(obj);
        return view;
    }

    public void RecycleCardView(CardView view)
    {
        view.Clear();
        GameMgr.Instance.CardPool.Recycle(view.gameObject);
    }
}
