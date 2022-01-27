using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCardLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var gMgr = GameMgr.Create();
        var card = new Card();
        card.View = "Prefabs/Card/Test";
        var obj = GameMgr.Instance.CardPool.Spawn();
        var v = obj.GetComponent<CardView>();
        v.Init(card);
    }
}
