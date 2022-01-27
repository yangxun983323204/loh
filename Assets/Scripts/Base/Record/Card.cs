using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YX;

public class Card : IdObj
{
    public CardType Type { get; set; }
    public int Cost { get; set; }
    public Command[] Commands { get; set; }

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
}
