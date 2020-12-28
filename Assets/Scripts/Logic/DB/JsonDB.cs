using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
/// <summary>
/// 开发时使用，方便调整数据结构，稳定后用SqliteDB
/// </summary>
public class JsonDB : IGameCardDB, IActorDB, IActorDeckDB, IDisposable
{
    private const string DIR = "json";
    private const string DB_GAMECARD = "carddb";
    private const string DB_ACTOR = "actordb";
    private const string DB_DECK = "deckdb";

    private List<GameCard> _cards;
    private List<ActorRecord> _actors;
    private List<ActorDeckRecord> _decks;

    public JsonDB()
    {
        LoadJson();
    }

    private void LoadJson()
    {
        _cards = JsonConvert.DeserializeObject<List<GameCard>>(Resources.Load<TextAsset>($"DB/{DIR}/{DB_GAMECARD}").text);
        _actors = JsonConvert.DeserializeObject<List<ActorRecord>>(Resources.Load<TextAsset>($"DB/{DIR}/{DB_ACTOR}").text);
        _decks = JsonConvert.DeserializeObject<List<ActorDeckRecord>>(Resources.Load<TextAsset>($"DB/{DIR}/{DB_DECK}").text);
    }

    #region IGameCardDB
    public GameCard GetCard(int id)
    {
        return _cards.Find(n => n.Id == id);
    }

    public List<GameCard> GetCards(GameCard.CardType type)
    {
        return _cards.FindAll(n => n.Type == type);
    }

    public List<GameCard> GetCardsWithCost(int cost, GameCard.CardType type)
    {
        return _cards.FindAll(n => n.Type == type && n.Cost == cost);
    }

    public List<GameCard> GetCardsWithCostMax(int cost, GameCard.CardType type)
    {
        return _cards.FindAll(n => n.Type == type && n.Cost <= cost);
    }

    public List<GameCard> GetCardsWithCostMin(int cost, GameCard.CardType type)
    {
        return _cards.FindAll(n => n.Type == type && n.Cost >= cost);
    }
    #endregion

    #region IActorDB
    public ActorRecord GetActor(int id)
    {
        return _actors.Find(n => n.Id == id);
    }

    public List<ActorRecord> GetActorWithLv(int lv)
    {
        return _actors.FindAll(n => n.Lv == lv);
    }
    #endregion

    #region IActorDeckDB
    public Deck GetDeck(int actorId)
    {
        var record = _decks.Find(n => n.ActorId == actorId);
        if (record == null)
            return null;

        var deck = new Deck();
        string[] ids = record.Cards.Split(',');
        foreach (var str in ids)
        {
            var id = int.Parse(str);
            var card = GetCard(id);
            deck.Add(card);
        }

        return deck;
    }
    #endregion

    public void Dispose()
    {
    }

    public void Release() { }
}
