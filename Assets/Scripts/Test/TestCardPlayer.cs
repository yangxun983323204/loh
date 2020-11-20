using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCardPlayer : MonoBehaviour
{
    CardPlayer _player;
    // Start is called before the first frame update
    void Start()
    {
        Deck deck = new Deck();
        for (int i = 0; i < 20; i++)
        {
            deck.Add(new Card() { Id = i });
        }

        var go = new GameObject();
        _player = go.AddComponent<CardPlayer>();
        _player.Init(deck);
        _player.onTakeCard += (c) => {
            Debug.Log("摸到卡牌" + c.ToString());
        };
        _player.onPlayCard += (c) => {
            Debug.Log("打出卡牌" + c.ToString());
        };
        _player.onDiscardCard += (c) => {
            Debug.Log("丢弃卡牌" + c.ToString());
        };
        _player.onTakeCardFail += () => {
            Debug.Log("疲劳");
        };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (_player.HandCount > 0)
                _player.Play(_player.HandLeft(0));
            else
                Debug.Log("没有手牌了！");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _player.Take(2);
        }
    }
}
