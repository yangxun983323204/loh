using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorDeckRecord
{
    public int Id { get; set; }
    public int ActorId { get; set; }
    public string Cards { get; set; }

    public Deck GetDeck()
    {
        var deck = new Deck();
        string[] ids = Cards.Split(',');
        foreach (var str in ids)
        {
            var id = int.Parse(str);
            var card = GameMgr.Instance.DB.Find<GameCard>(new GameCard.MatchId(id).Check);
            deck.Add(card);
        }

        return deck;
    }

    public class MatchActorId
    {
        private int _id;

        public MatchActorId(int id)
        {
            _id = id;
        }

        public bool Check(ActorDeckRecord r)
        {
            return r.Id == _id;
        }
    }
}
