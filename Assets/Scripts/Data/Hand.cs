using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : CardSet
{
    public Hand()
    {
        Capacity = 5;
        _cardStack = new List<Card>(Capacity);
    }

    public Card Left()
    {
        return Left(0);
    }

    public Card Left(int idx)
    {
        return _cardStack[0];
    }

    public Card Right()
    {
        return Right(0);
    }

    public Card Right(int idx)
    {
        return _cardStack[_cardStack.Count - 1 - idx];
    }
}
