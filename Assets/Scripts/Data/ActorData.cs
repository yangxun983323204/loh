using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewActorData",menuName = "LOH/ActorData")]
public class ActorData : ScriptableObject
{
    public int MaxHp;
    public int MaxMp;
    public int CurrHp;
    public int CurrMp;
    public int Level;
    public float HpGrowthA = 0;
    public float HpGrowthB = 2;
    public float MpGrowthA = 0;
    public float MpGrowthB = 1;

    public void LevelUp()
    {
        Level++;
        MaxHp = Mathf.FloorToInt(HpGrowthA * MaxHp + HpGrowthB);
        CurrHp = MaxHp;

        MaxMp = Mathf.FloorToInt(MpGrowthA * MaxMp + MpGrowthB);
        CurrMp = MaxMp;
    }
}
