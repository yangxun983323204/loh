using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ActorRecord: IdObj
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Lv { get; set; }
    public int MaxHp { get; set; }
    public int MaxAp { get; set; }
    public int Hp { get; set; }
    public int Ap { get; set; }
    public string View { get; set; }
    public Dictionary<string,string> Sounds { get; set; }
}
