using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    Self,
    Other,
    Any
}

[System.Serializable]
public class SkillData
{
    public int Id;
    public string Name;
    public SkillType Type;
    public float ATK;
    public float Heal;
    public int[] Effects;
    public int[] EffectsValue;
    // render
    public string SpellAnim;
    public float SpellAnimTime;

    public string HitAnim;
    public float HitAnimTime1;
    public float HitAnimTime2;
}

[CreateAssetMenu(fileName = "NewSkillDB", menuName = "LOH/SkillDB")]
public class SkillDB : ScriptableObject
{
    public SkillData[] Skills;
}
