using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockDB : IDB
{
    private Dictionary<Type, IList> _caches = new Dictionary<Type, IList>();

    public MockDB()
    {
        _caches.Add(typeof(Card), GenCards());
        _caches.Add(typeof(ActorRecord), GenActors());
        _caches.Add(typeof(DeckRecord), GenDecks());
        _caches.Add(typeof(BuffRecord), GenBuffs());
    }

    public override T Find<T>(Predicate<T> func)
    {
        return GetCache<T>().Find(func);
    }

    public override List<T> Finds<T>(Predicate<T> func)
    {
        return GetCache<T>().FindAll(func);
    }

    IList GenActors()
    {
        var list = new List<ActorRecord>();

        var a0 = new ActorRecord()
        {
            Id = 1,
            Name = "循",
            Lv = 999,
            MaxHp = 999,
            MaxAp = 999,
            Hp = 999,
            Ap = 999,
            View = "view/actor/axun",
            Sounds = new Dictionary<string, string>() {
            }
        };

        list.Add(a0);

        var a1  = new ActorRecord()
        {
            Id = 2,
            Name = "草人",
            Lv = 1,
            MaxHp = 10,
            MaxAp = 10,
            Hp = 10,
            Ap = 10,
            View = "view/actor/straw",
            Sounds = new Dictionary<string, string>()
            {
            }
        };

        list.Add(a1);

        return list;
    }

    IList GenCards()
    {
        var list = new List<Card>();

        var c0 = new Card()
        {
            Id = 1,
            Name = "测试卡-攻击卡",
            Desc = "我~是~创世神！",
            View = "view/card/god",
            Type = CardType.Attack,
            Cost = 0,
            Commands = new Command[] {
                new Command(CmdType.PushNum, ActionType.None,"-1"),
                new Command(CmdType.HpChange, ActionType.Enemy),
            },
            Sound = "sound/attack",
            Effect = null
        };
        list.Add(c0);

        var c1 = new Card()
        {
            Id = 2,
            Name = "测试卡-技能卡",
            Desc = "我~是~创世神！",
            View = "view/card/god",
            Type = CardType.Ability,
            Cost = 1,
            Commands = new Command[] {
                new Command(CmdType.PushNum, ActionType.None,"1"),
                new Command(CmdType.HpChange, ActionType.Enemy),
            },
            Sound = "sound/skill",
            Effect = null
        };
        list.Add(c1);

        var c2 = new Card()
        {
            Id = 3,
            Name = "测试卡-技能卡-施毒",
            Desc = "我~是~创世神！",
            View = "view/card/god",
            Type = CardType.Ability,
            Cost = 1,
            Commands = new Command[] {
                new Command(CmdType.PushNum, ActionType.None,"1"),
                new Command(CmdType.AddBuff, ActionType.Enemy),
            },
            Sound = "sound/skill",
            Effect = null
        };
        list.Add(c2);

        var c3 = new Card()
        {
            Id = 4,
            Name = "测试卡-技能卡-冰铠",
            Desc = "我~是~创世神！",
            View = "view/card/god",
            Type = CardType.Ability,
            Cost = 1,
            Commands = new Command[] {
                new Command(CmdType.PushStr, ActionType.None,"冰霜，守护我！"),
                new Command(CmdType.Say, ActionType.Self),
                new Command(CmdType.PushNum, ActionType.None,"2"),
                new Command(CmdType.AddBuff, ActionType.Self),
            },
            Sound = "sound/skill",
            Effect = null
        };
        list.Add(c3);

        return list;
    }

    IList GenBuffs()
    {
        var list = new List<BuffRecord>();

        var b0 = new BuffRecord()
        {
            Id = 1,
            Name = "绿毒",
            Desc = "蚀骨之毒",
            View = "view/buff/poison",
            CanMerge = true,
            Count = 5,
            Triggers = new Dictionary<string, List<Command>>()
            {
                { KeyEvent.RoundBegin,new List<Command>()
                    {
                        new Command(CmdType.PushNum, ActionType.None,"-4"),
                        new Command(CmdType.HpChange, ActionType.Self),
                    }
                }
            }
        };
        list.Add(b0);

        var b1 = new BuffRecord()
        {
            Id = 2,
            Name = "冰甲",
            Desc = "凝水为冰",
            View = "view/buff/ice_armor",
            CanMerge = true,
            Count = 5,
            Triggers = new Dictionary<string, List<Command>>() {
                {KeyEvent.Hurt,new List<Command>()
                    {
                        new Command(CmdType.PushStr, ActionType.None, "砰！"),
                        new Command(CmdType.Say, ActionType.Self),
                    }
                }
            }
        };
        list.Add(b1);

        return list;
    }

    IList GenDecks()
    {
        var list = new List<DeckRecord>();

        var d0 = new DeckRecord()
        {
            Id = 1,
            ActorId = 1,
            Cards = "1,2,3,4"
        };
        list.Add(d0);

        var d1 = new DeckRecord()
        {
            Id = 2,
            ActorId = 2,
            Cards = "1,1,1"
        };
        list.Add(d1);

        return list;
    }

    private List<T> GetCache<T>()
    {
        var t = typeof(T);
        if (!_caches.ContainsKey(t))
            throw new NotSupportedException(t.ToString());
        else
            return _caches[t] as List<T>;
    }
}
