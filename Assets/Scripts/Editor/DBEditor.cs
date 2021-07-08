using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using SQLite4Unity3d;
using System;

public class DBEditor
{
    public const string RES_NAME = "gamedb";

    [MenuItem("Assets/Game/DB/EnsureSqliteTable")]
    public static void EnsureSqliteTable()
    {
        var res = Resources.Load<TextAsset>(GetResPath());
        Debug.Assert(res!=null);
        var path = AssetDatabase.GetAssetPath(res);
        Debug.Assert(File.Exists(path));
        Ensure(path);
    }

    static string GetResPath()
    {
        return Path.Combine("DB", RES_NAME);
    }

    static void Ensure(string path)
    {
        var connection = new SQLiteConnection(path, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        try
        {
            connection.CreateTable<GameCard>(CreateFlags.ImplicitPK | CreateFlags.AutoIncPK);
            connection.CreateTable<ActorRecord>(CreateFlags.ImplicitPK | CreateFlags.AutoIncPK);
            connection.CreateTable<ActorDeckRecord>(CreateFlags.ImplicitPK | CreateFlags.AutoIncPK);
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.ToString());
        }

        if (connection != null)
        {
            connection.Close();
            connection = null;
        }
    }
}
