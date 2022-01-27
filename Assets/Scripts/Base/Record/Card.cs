using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YX;

public struct Card:IdObj
{
    public int Id { get; set; }
    public CardType Type { get; set; }
    public int Cost { get; set; }
    public Command[] Commands { get; set; }

    public string Name { get; set; }
    public string Desc { get; set; }
    public string View { get; set; }
    public string Sound { get; set; }
    public string Effect { get; set; }


    public override string ToString()
    {
        return "[Card]".Dye(Color.yellow) + $"id:{Id},name:{Name},type:{Type},cost:{Cost}";
    }
}
