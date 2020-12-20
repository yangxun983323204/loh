using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private Actor _actor;
    private CardPlayer _player;
    private HandLayout _handView;

    private void Awake()
    {
        _player = gameObject.GetComponent<CardPlayer>();
        _handView = gameObject.GetComponentInChildren<HandLayout>();
        _player.onTakeCard += CreateCard;
    }

    public void Init(Actor actor)
    {
        _actor = actor;
    }

    private void CreateCard(Card card)
    {
        var obj = GameMgr.Instance.CardPool.Spawn();
        obj.SetActive(true);
        var view = obj.GetComponentInChildren<CardView>();
        view.Init(card as GameCard);
        _handView.Add(obj);
        (obj.transform as RectTransform).sizeDelta = new Vector2(280, 390);
        view.onEndDrag.Add(c => {
            if (_actor.CanPlayCard(c.Data))
            {
                _handView.Remove(c.gameObject);
                _player.Play(c.Data);
                return false;
            }
            else
            {
                return true;
            }
        });
    }
}
