using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DeckRecord:IdObj
{
    public int Id { get; set; }
    public int ActorId { get; set; }
    public string Cards { get; set; }

    private static MatchId<Card> _checker = new MatchId<Card>(0);

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
