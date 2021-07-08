using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IdObj
{
    public int Id { get; set; }

    public class MatchId
    {
        private int _id;
        private Type _t;

        public MatchId(Type type,int id)
        {
            _id = id;
            _t = type;
        }

        public MatchId SetId(int id)
        {
            _id = id;
            return this;
        }

        public bool Check(IdObj r)
        {
            return r.Id == _id && r.GetType()==_t;
        }
    }
}
