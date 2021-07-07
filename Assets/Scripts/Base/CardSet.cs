using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSet
{
    public int Count { get { return _cardStack.Count; } }

    public int Capacity { get; set; }

    protected List<Card> _cardStack;

    public CardSet(int capacity)
    {
        Capacity = capacity;
        _cardStack = new List<Card>(Capacity);
    }

    public void Add(Card card)
    {
        _cardStack.Add(card);
    }

    public void InsertSorted(Card card)
    {
        Debug.Assert(!_cardStack.Contains(card));
        for (int i = 0; i < _cardStack.Count; i++)
        {
            if (_cardStack[i].Id>card.Id)
            {
                _cardStack.Insert(i, card);
                return;
            }
        }

        _cardStack.Add(card);
    }

    public void InsertRandom(Card card)
    {
        Debug.Assert(!_cardStack.Contains(card));
        var idx = UnityEngine.Random.Range(0, _cardStack.Count);
        _cardStack.Insert(idx, card);
    }

    public bool Remove(Card card)
    {
        var idx = _cardStack.IndexOf(card);
        if (idx>=0)
        {
            _cardStack.RemoveAt(idx);
            return true;
        }

        return false;
    }

    public Card Take()
    {
        if (_cardStack.Count <= 0)
            return null;

        var card = _cardStack[_cardStack.Count - 1];
        _cardStack.RemoveAt(_cardStack.Count - 1);
        return card;
    }

    public Card Peek(int idx)
    {
        idx = _cardStack.Count - 1 - idx;
        if (idx < 0 || idx >= _cardStack.Count)
            return null;

        return _cardStack[idx];
    }

    public Card PeekRandom()
    {
        if (_cardStack.Count <= 0)
            return null;

        var idx = UnityEngine.Random.Range(0, _cardStack.Count);
        return _cardStack[idx];
    }

    public Card TakeRandom()
    {
        if (_cardStack.Count <= 0)
            return null;

        var idx = UnityEngine.Random.Range(0, _cardStack.Count);
        var card = _cardStack[idx];
        _cardStack.RemoveAt(idx);
        return card;
    }

    public void Random()
    {
        for (int i = 0; i < _cardStack.Count; i++)
        {
            var ni = UnityEngine.Random.Range(0, _cardStack.Count);
            var tmp = _cardStack[i];
            _cardStack[i] = _cardStack[ni];
            _cardStack[ni] = tmp;
        }
    }

    public void Sort()
    {
        _cardStack.Sort((a, b) => { return a.Id.CompareTo(b.Id); });
    }

    public bool Contains(Card card)
    {
        return _cardStack.Contains(card);
    }

    public int IndexOf(Card card)
    {
        return _cardStack.IndexOf(card);
    }

    public Card Left(int idx)
    {
        return _cardStack[idx];
    }

    public Card Right(int idx)
    {
        return _cardStack[_cardStack.Count - 1 - idx];
    }

    public void Clear()
    {
        _cardStack.Clear();
    }

    public void Move(CardSet set)
    {
        for (int i = 0; i < set._cardStack.Count; i++)
        {
            Add(set._cardStack[i]);
        }

        set.Clear();
    }

    public CardSet Clone()
    {
        var newSet = new CardSet(_cardStack.Count);
        newSet.Capacity = Capacity;
        foreach (var item in _cardStack)
        {
            newSet._cardStack.Add(item);
        }

        return newSet;
    }
}
