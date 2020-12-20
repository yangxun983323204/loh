using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    private Actor _actor;

    public void Init(Actor actor)
    {
        _actor = actor;
        var res = Resources.Load<GameObject>(Path.Combine("ActorView", _actor.View));
        var obj = Instantiate(res);
        obj.transform.SetParent(transform);
        obj.transform.SetAsFirstSibling();
    }
}
