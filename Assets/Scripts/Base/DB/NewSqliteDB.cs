using SQLite4Unity3d;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using YX;

public class NewSqliteDB : IDB
{
    private SQLiteConnection _connection = null;

    public NewSqliteDB()
    {
        var RES_NAME = "gamedb";
        var resPath = Path.Combine("DB", RES_NAME); ;
        var res = Resources.Load<TextAsset>(resPath);
        var outPath = Path.Combine(Application.persistentDataPath, "DB", Path.GetFileNameWithoutExtension(RES_NAME) + ".db");
        FileUtils.WriteAllBytes(outPath, res.bytes);
        _connection = new SQLiteConnection(outPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
    }

    public override void Dispose()
    {
        if (_connection!=null)
        {
            _connection.Dispose();
            _connection = null;
        }
    }

    public override T Find<T>(Predicate<T> func)
    {
        return _connection.Table<T>().Where(n=>func(n)).First();
    }

    public override List<T> Finds<T>(Predicate<T> func)
    {
        return _connection.Table<T>().Where(n =>func(n)).ToList();
    }
}
