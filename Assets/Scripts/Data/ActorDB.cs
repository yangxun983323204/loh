using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewActorDB", menuName = "LOH/ActorDB")]
public class ActorDB : ScriptableObject
{
    public Actor[] Actors;
}
