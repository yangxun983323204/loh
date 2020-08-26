using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewActorRenderData", menuName = "LOH/ActorRenderData")]
public class ActorRenderData : ScriptableObject
{
    public string AssetId;

    public string CenterPath = "center";

    public string CreateAnim = "create";
    public float CreateAnimTime = 0.5f;

    public string IdleAnim = "idle";

    public string DamageAnim = "damage";
    public float DamageAnimTime = 0.2f;

    public string HitAnim = "hit";
    public float HitAnimTime = 0.2f;

    public string DeathAnim = "death";
    public float DeathAnimTime = 1f;

    public string SkillAnim1 = "skill1";
    public float SkillAnim1Time = 1f;

    public string SkillAnim2 = "skill2";
    public float SkillAnim2Time = 1f;
}
