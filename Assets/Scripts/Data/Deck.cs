using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : CardSet
{
    public Deck()
    {
        Capacity = 20;
        _cardStack = new List<Card>(Capacity);
    }

    public Deck Clone()
    {
        var newDeck = new Deck();
        foreach (var item in _cardStack)
        {
            newDeck._cardStack.Add(item);
        }

        return newDeck;
    }
}
