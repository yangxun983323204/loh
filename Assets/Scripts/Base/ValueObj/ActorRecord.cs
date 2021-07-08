using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorRecord: IdObj
{
    public string Name { get; set; }
    public int Lv { get; set; }
    public int MaxHp { get; set; }
    public int MaxAp { get; set; }
    public int MaxMp { get; set; }
    public int Hp { get; set; }
    public int Ap { get; set; }
    public int Mp { get; set; }
    public string View { get; set; }
}
