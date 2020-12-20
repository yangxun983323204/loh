using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBattle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var gMgr = GameMgr.Create();
        var btMgr = gMgr.BattleMgr;
        var player = gMgr.ActorDB.GetActor(1);
        var deck1 = gMgr.DeckDB.GetDeck(1);
        var enemy = gMgr.ActorDB.GetActor(2);
        var deck2 = gMgr.DeckDB.GetDeck(2);
        btMgr.Create(player, deck1, enemy, deck2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
