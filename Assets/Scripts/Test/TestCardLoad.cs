using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCardLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var card = new GameCard();
        card.View = "Prefabs/Card/Test";
        var go = new GameObject("Card");
        var v = go.AddComponent<CardView>();
        v.Init(card);
    }
}
