using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewSkillDB", menuName = "LOH/SkillDB")]
public class SkillDB : ScriptableObject
{
    public SkillData[] Skills;
}
