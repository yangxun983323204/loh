using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    Attack,
    Ability,
    Equip,
    Special,
}

public enum Nature
{
    Physics,
    Lightning,
    Fire,
    Water,
    Wind,
    Divine,
    Dark
}

public static class KeyEvent
{
    public const string RoundBegin = "RoundBegin";
    public const string RoundEnd = "RoundEnd";
    public const string Dead = "Dead";
    public const string Hurt = "Hurt";
    public const string Heal = "";
    public const string Buff = "";
}