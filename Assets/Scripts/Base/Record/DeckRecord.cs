using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckRecord: IdObj
{
    public int ActorId { get; set; }
    public string Cards { get; set; }

    private static MatchId _checker = new MatchId(typeof(Card), 0);

    public CardSet GetDeck()
    {
        string[] ids = Cards.Split(',');
        var deck = new CardSet(ids.Length);
        foreach (var str in ids)
        {
            var id = int.Parse(str);
            var card = GameMgr.Instance.DB.Find<Card>(_checker.SetId(id).Check);
            deck.Add(card);
        }

        return deck;
    }
}
