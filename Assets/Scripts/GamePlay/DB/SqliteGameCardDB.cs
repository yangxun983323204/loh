using SQLite4Unity3d;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SqliteGameCardDB : IGameCardDB
{
    private SQLiteConnection _connection;

    public SqliteGameCardDB()
    {
        var ta = Resources.Load<TextAsset>("DB/GameCardDB.bytes");
        var outPath = Path.Combine(Application.persistentDataPath, "DB", "GameCardDB.db");
        File.WriteAllBytes(outPath, ta.bytes);
        _connection = new SQLiteConnection(outPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
    }

    public GameCard GetCard(int id)
    {
        throw new System.NotImplementedException();
    }

    public List<GameCard> GetCards(GameCard.CardType type)
    {
        throw new System.NotImplementedException();
    }

    public List<GameCard> GetCardsWithCost(int cost, GameCard.CardType type)
    {
        throw new System.NotImplementedException();
    }

    public List<GameCard> GetCardsWithCostMax(int cost, GameCard.CardType type)
    {
        throw new System.NotImplementedException();
    }

    public List<GameCard> GetCardsWithCostMin(int cost, GameCard.CardType type)
    {
        throw new System.NotImplementedException();
    }
}
