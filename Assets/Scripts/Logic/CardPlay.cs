using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YX;

public class CardPlay
{
    public Actor Owner { get; set; }
    public int HandCount { get { return _hand.Count; }}
    public int HandCapacity { get { return _hand.Capacity; } set { _hand.Capacity = value; } }
    public int DeckCount { get { return _deck.Count; }}

    private CardSet _deckOrigin;
    private CardSet _deck;
    private CardSet _hand = new CardSet(5);
    private CardSet _grave = new CardSet(20);

    public void Init(CardSet deck)
    {
        _deckOrigin = deck;
        _deck = deck.Clone();
        _hand.Clear();
        _grave.Clear();
        _grave.Capacity = 1000;

        _deck.Random();
    }

    public void Take(int cnt)
    {
        for (int i = 0; i < cnt; i++)
        {
            var card = _deck.Take();
            if (card!=null)
            {
                _hand.Add(card);
                EventManager.Instance.QueueEvent(new Evt_TakedCard() { Owner = Owner, Card = card});
            }
            else
            {
                EventManager.Instance.QueueEvent(new Evt_TakeCardFailed() { Owner = Owner});
            }
        }
    }

    public bool Play(Card card)
    {
        if (_hand.Contains(card))
        {
            _hand.Remove(card);
            _grave.Add(card);
            EventManager.Instance.QueueEvent(new Evt_PlayedCard() { Owner = Owner, Card = card});
            return true;
        }

        return false;
    }

    public void DiscardRight(int cnt)
    {
        if (cnt <= 0)
            return;

        while (_hand.Count > 0 && cnt > 0)
        {
            cnt--;
            var card = _hand.Right(0);
            Debug.Assert(card != null);
            _hand.Remove(card);
            _grave.Add(card);
            EventManager.Instance.QueueEvent(new Evt_DiscardCard() { Owner = Owner, Card = card});
        }
    }

    public Card HandLeft(int idx)
    {
        return _hand.Left(idx);
    }

    public int HandIdx(Card card)
    {
        return _hand.IndexOf(card);
    }
}
