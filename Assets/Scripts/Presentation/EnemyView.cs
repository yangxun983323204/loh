using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    private Actor _actor;

    public void Init(Actor actor)
    {
        _actor = actor;
        var res = Resources.Load<GameObject>(_actor.View);
        var obj = Instantiate(res);
        obj.transform.SetParent(transform);
        obj.transform.SetAsFirstSibling();
    }
}
