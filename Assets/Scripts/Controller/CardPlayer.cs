using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlayer : MonoBehaviour
{
    public int HandCount { get { return _hand.Count; }}
    public int HandCapacity { get { return _hand.Capacity; } set { _hand.Capacity = value; } }

    public int DeckCount { get { return _deck.Count; }}

    public event System.Action<Card> onTakeCard;
    public event System.Action onTakeCardFail;
    public event System.Action<Card> onPlayCard;
    public event System.Action<Card> onDiscardCard;

    private Deck _deckOrigin;
    private Deck _deck;
    private Hand _hand = new Hand();
    private Grave _grave = new Grave();

    public void Init(Deck deck)
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
                onTakeCard?.Invoke(card);
            }
            else
            {
                onTakeCardFail?.Invoke();
            }
        }
    }

    public void Play(Card card)
    {
        if (_hand.Contains(card))
        {
            _hand.Remove(card);
            _grave.Add(card);
            onPlayCard?.Invoke(card);
        }
    }

    public void DiscardRight(int cnt)
    {
        if (cnt <= 0)
            return;

        while (_hand.Count > 0 && cnt > 0)
        {
            cnt--;
            var card = _hand.Right();
            Debug.Assert(card != null);
            _hand.Remove(card);
            _grave.Add(card);
            onDiscardCard?.Invoke(card);
        }
    }

    public Card HandLeft(int idx)
    {
        return _hand.Left(idx);
    }
}
