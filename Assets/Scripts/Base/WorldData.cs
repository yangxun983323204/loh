using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMap
{
    public string Id { get; set; }
    public string View { get; set; }
    public List<Area> Areas { get; set; }
}

public class Area
{
    public string Id { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public string View { get; set; }
    public List<Domain> Domains { get; set; }
}

public class Domain
{
    public string Id { get; set; }
    public string View { get; set; }
    public List<string> Actors { get; set; }
}
