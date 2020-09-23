using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBuffData", menuName = "LOH/BuffData")]
public class BuffData : ScriptableObject
{
    public int Code;
    public string Icon;
    public string Desc;
    public Pair<int, int> Immediates;
    public Pair<int, int> Cycles;
}
