using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHand : MonoBehaviour
{
    public HandLayout Layout;

    private List<GameObject> _cardObjList = new List<GameObject>(10);

    // Start is called before the first frame update
    IEnumerator Start()
    {
        Layout.SetRect(280,1080 - 320);
        for (int i = 0; i < 10; i++)
        {
            var cardObj = CreateCard();
            _cardObjList.Add(cardObj);
            Layout.Add(cardObj);
            yield return new WaitForSeconds(0.5f);
        }

        for (int i = 0; i < 10; i++)
        {
            Layout.Remove(_cardObjList[i]);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private GameObject CreateCard()
    {
        var card = new GameCard();
        card.View = "test";

        var obj = new GameObject("Card");
        var view = obj.AddComponent<CardView>();
        view.Init(card);

        return obj;
    }
}
