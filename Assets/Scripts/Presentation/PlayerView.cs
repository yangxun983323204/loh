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
        GameMgr.Instance.BattleMgr.SpawnCardView(_actor, card);
    }
}
