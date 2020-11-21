using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCard : Card
{
    public enum CardType
    {
        Normal,
        Magic,
        Action,
        Equip,
    }

    public string View;
    public CardType Type;
    public int Cost;
    public SortedDictionary<string, string> Commands;
}
