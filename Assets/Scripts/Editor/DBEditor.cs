using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class DBEditor
{
    [MenuItem("Assets/Game/DB/EnsureTable")]
    public static void EnsureTable()
    {
        var res = Resources.Load<TextAsset>(SqliteDB.GetResPath());
        Debug.Assert(res!=null);
        var path = AssetDatabase.GetAssetPath(res);
        Debug.Assert(File.Exists(path));
        var db = new SqliteDB(path);
        db.EnsureTable();
        db.Release();
    }
}
