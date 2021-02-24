using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorRecord
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Lv { get; set; }
    public int MaxHp { get; set; }
    public int MaxAp { get; set; }
    public int MaxMp { get; set; }
    public int Hp { get; set; }
    public int Ap { get; set; }
    public int Mp { get; set; }
    public string View { get; set; }

    public class MatchId
    {
        private int _id;

        public MatchId(int id)
        {
            _id = id;
        }

        public bool Check(ActorRecord r)
        {
            return r.Id == _id;
        }
    }
}
