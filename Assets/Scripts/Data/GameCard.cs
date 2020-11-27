using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YX;

public class GameCard : Card
{
    public enum CardType
    {
        Normal,
        Magic,
        Action,
        Equip,
    }

    public string View { get; set; }
    public CardType Type { get; set; }
    public int Cost { get; set; }
    public string CommandsJson { get; set; }

    public override string ToString()
    {
        return "[Card]".Dye(Color.yellow) + string.Format("id:{0},type:{1},cost:{2},view:{3}\nCmd:{4}", Id, Type, Cost,View,CommandsJson);
    }
}
