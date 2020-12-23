using SQLite4Unity3d;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using YX;

public class SqliteDB : IGameCardDB, IActorDB,IActorDeckDB, IDisposable
{
    public const string RES_NAME = "gamedb";

    private SQLiteConnection _connection;

    public SqliteDB()
    {
        var resPath = GetResPath();
        var res = Resources.Load<TextAsset>(resPath);
        var outPath = GetOutPath();
        FileUtils.WriteAllBytes(outPath, res.bytes);
        _connection = new SQLiteConnection(outPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
    }

    public SqliteDB(string path)
    {
        _connection = new SQLiteConnection(path, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
    }

    public void Release()
    {
        if (_connection != null)
        {
            _connection.Close();
            _connection = null;
        }
    }

    public void EnsureTable()
    {
        try
        {
            _connection.CreateTable<GameCard>(CreateFlags.ImplicitPK | CreateFlags.AutoIncPK);
            _connection.CreateTable<ActorRecord>(CreateFlags.ImplicitPK | CreateFlags.AutoIncPK);
            _connection.CreateTable<ActorDeckRecord>(CreateFlags.ImplicitPK | CreateFlags.AutoIncPK);
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.ToString());
        }
    }

    public static string GetResPath()
    {
        return Path.Combine("DB", RES_NAME);
    }

    public static string GetOutPath()
    {
        return Path.Combine(Application.persistentDataPath, "DB", RES_NAME + ".db"); ;
    }


    #region IGameCardDB
    public GameCard GetCard(int id)
    {
        return _connection.Table<GameCard>().Where(n => n.Id == id).First();
    }

    public List<GameCard> GetCards(GameCard.CardType type)
    {
        return _connection.Table<GameCard>().Where(n => n.Type == type).ToList();
    }

    public List<GameCard> GetCardsWithCost(int cost, GameCard.CardType type)
    {
        return _connection.Table<GameCard>().Where(n => n.Type == type && n.Cost == cost).ToList();
    }

    public List<GameCard> GetCardsWithCostMax(int cost, GameCard.CardType type)
    {
        return _connection.Table<GameCard>().Where(n => n.Type == type && n.Cost <= cost).ToList();
    }

    public List<GameCard> GetCardsWithCostMin(int cost, GameCard.CardType type)
    {
        return _connection.Table<GameCard>().Where(n => n.Type == type && n.Cost >= cost).ToList();
    }
    #endregion

    #region IActorDB
    public ActorRecord GetActor(int id)
    {
        return _connection.Table<ActorRecord>().Where(n => n.Id == id).First();
    }
    public List<ActorRecord> GetActorWithLv(int lv)
    {
        return _connection.Table<ActorRecord>().Where(n => n.Lv == lv).ToList();
    }
    #endregion

    #region IActorDeckDB
    public Deck GetDeck(int actorId)
    {
        var record = _connection.Table<ActorDeckRecord>().Where(n => n.ActorId == actorId).First();
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
        Release();
    }
}