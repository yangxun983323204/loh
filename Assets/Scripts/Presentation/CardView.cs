using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardView : MonoBehaviour
{
    public void Init(GameCard card)
    {
        // todo
        var r = Resources.Load<GameObject>(card.View);
        var go = Instantiate(r);
        go.transform.SetParent(transform);
    }
}
