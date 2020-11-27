using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDB : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var db = new SqliteDB();
        var card = db.GetCard(1);
        Debug.Log(card);
        db.Release();
    }
}
