﻿using System.Collections;
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

    public CardType Type { get; set; }
    public int Cost { get; set; }
    public string CommandsJson { get; set; }

    public string Name { get; set; }
    public string Desc { get; set; }
    public string View { get; set; }

    public string EnemyFx { get; set; } = null;
    public float EnemyFxTime { get; set; } = 0;

    public string SelfFx { get; set; } = null;
    public float SelfFxTime { get; set; } = 0;

    public override string ToString()
    {
        return "[Card]".Dye(Color.yellow) + $"id:{Id},name:{Name},type:{Type},cost:{Cost}";
    }

    public class MatchId
    {
        private int _id;

        public MatchId(int id)
        {
            _id = id;
        }

        public bool Check(GameCard r)
        {
            return r.Id == _id;
        }
    }
}
