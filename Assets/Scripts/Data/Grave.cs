using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave : CardSet
{
    public Grave()
    {
        Capacity = int.MaxValue;
        _cardStack = new List<Card>(20);
    }
}
