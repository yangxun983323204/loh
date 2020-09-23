using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewActorRenderData", menuName = "LOH/ActorRenderData")]
public class ActorRenderData : ScriptableObject
{
    public string AssetId;

    public string CenterPath = "center";

    public AnimInfo CreateAnim = new AnimInfo() { Name = "create", Time = 0.5f };

    public AnimInfo IdleAnim = new AnimInfo() { Name = "idle", Time = -1 };

    public AnimInfo DamageAnim = new AnimInfo() { Name = "damage", Time = 0.2f };

    public AnimInfo HitAnim = new AnimInfo() { Name = "hit", Time = 0.2f };

    public AnimInfo DeathAnim = new AnimInfo() { Name = "death", Time = 1 };

    public AnimInfo SkillAnim1 = new AnimInfo() { Name = "skill1", Time = 1 };

    public AnimInfo SkillAnim2 = new AnimInfo() { Name = "skill2", Time = 1 };
}
