using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BuffRecord : IdObj
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
    public string View { get; set; }

    public bool CanMerge { get; set; }
    public int Count { get; set; }

    public Dictionary<string, List<Command>> Triggers;

    public static MatchId<BuffRecord> Checker = new MatchId<BuffRecord>(0);
}
