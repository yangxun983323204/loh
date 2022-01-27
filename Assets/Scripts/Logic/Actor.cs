using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YX;

public class Actor
{
    public ActorRecord Data;

    public LinkedListEx<Buff> Buffs { get;private set; }

    public CardPlay Play { get; private set; }

    public CommandEnv Env { get; private set; } = new CommandEnv();

    public Actor()
    {
        Play = new CardPlay();
        Play.Owner = this;
        Buffs = new LinkedListEx<Buff>();
        Env.Self = this;
    }

    public bool CanPlayCard(Card card)
    {
        bool canPlay = false;
        switch (card.Type)
        {
            case CardType.Attack:
                canPlay = true;
                break;
            case CardType.Ability:
                canPlay = card.Cost <= Data.Ap;
                break;
            case CardType.Equip:
                canPlay = true;
                break;
            case CardType.Special:
                canPlay = true;
                break;
            default:
                break;
        }

        return canPlay;
    }

    public void ChangeHp(float val)
    {
        // 先trigger一个事件，如果有其它拦截，就可以关注这个消息以完成
        var willChange = new Evt_ActorPropWillChange() { Target = this, PropName = "Hp", Value = val };
        EventManager.Instance.TriggerEvent(willChange);
        val = willChange.Value;
        //
        Data.Hp += (int)val;
        if (Data.Hp <=0)
            Data.Hp = 0;

        EventManager.Instance.QueueEvent(
            new Evt_ActorPropChange() { Target = this, PropName = "Hp" });

        if (Data.Hp <=0)
        {
            EventManager.Instance.QueueEvent(
            new Evt_ActorDie() { Target = this });
        }
    }

    public void ChangeAp(float val)
    {
        Data.Ap += (int)val;
        if (Data.Ap <= 0)
            Data.Ap = 0;
        EventManager.Instance.QueueEvent(
            new Evt_ActorPropChange() { Target = this, PropName = "Ap" });
    }

    public void AddBuff(int id)
    {
        var buff = Buff.Create(id);
        var node = Buffs.Find(i => { return i.Data.Id == id; });
        if (node!=null)
        {
            node.Overlay(buff);
            var evt = new Evt_UpdateBuff() {Target=this, Data = node };
            EventManager.Instance.QueueEvent(evt);
        }
        else
        {
            buff.SetOwner(this);
            Buffs.AddLast(buff);
            var evt = new Evt_AddBuff() { Target = this, Data = buff };
            EventManager.Instance.QueueEvent(evt);
        }
    }

    public void RemoveBuff(int id)
    {
        var node = Buffs.Find(i => { return i.Data.Id == id; });
        if (node!=null)
        {
            Buffs.Remove(node);
            var evt = new Evt_RemoveBuff() { Target=this, Data = node };
            EventManager.Instance.QueueEvent(evt);
        }
    }

    public bool PlayCard(Card card)
    {
        if (!CanPlayCard(card))
            return false;

        var s =Play.Play(card);
        if (s)
        {
            switch (card.Type)
            {
                case CardType.Attack:
                    break;
                case CardType.Ability:
                    ChangeAp(Data.Ap -card.Cost);
                    break;
                case CardType.Equip:
                    break;
                case CardType.Special:
                    break;
                default:
                    break;
            }
        }
        return s;
    }

    public override string ToString()
    {
        return Data.Name;
    }
}