using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActorDB
{
    ActorRecord GetActor(int id);
    List<ActorRecord> GetActorWithLv(int lv);
}
