using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YX;

public class Actor
{
    public int Id { get;private set; }
    public int Lv { get;private set; }

    public string Name { get; set; }

    public int HpMax { get; private set; }
    public int ApMax { get; private set; }
    public int MpMax { get; private set; }

    public int Hp { get; private set; }
    public int Ap { get; private set; }
    public int Mp { get; private set; }

    public string View { get;private set; }

    public LinkedListEx<Buff> Buffs { get;private set; }

    public CardPlay Play { get; private set; }

    public CommandEnv Env { get; private set; } = new CommandEnv();

    public Actor()
    {
        Play = new CardPlay();
        Play.Owner = this;
        Buffs = new LinkedListEx<Buff>();
    }

    public void SetData(ActorRecord record)
    {
        Id = record.Id;
        Lv = record.Lv;
        Name = record.Name;
        Hp = record.Hp;
        HpMax = record.Hp;
        Ap = record.Ap;
        ApMax = record.Ap;
        Mp = record.Mp;
        MpMax = record.Mp;
        View = record.View;
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
                canPlay = card.Cost <= Ap;
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
        EventManager.Instance.TriggerEvent(willChange.GenDynamicEvt());
        val = willChange.Value;
        //
        Hp += (int)val;
        if (Hp<=0)
            Hp = 0;

        EventManager.Instance.QueueEvent(
            new Evt_ActorPropChange() { Target = this, PropName = "Hp" });

        if (Hp<=0)
        {
            EventManager.Instance.QueueEvent(
            new Evt_ActorDie() { Target = this });
        }
    }

    public void ChangeAp(float val)
    {
        Ap += (int)val;
        if (Ap <= 0)
            Ap = 0;
        EventManager.Instance.QueueEvent(
            new Evt_ActorPropChange() { Target = this, PropName = "Ap" });
    }

    public void ChangeMp(float val)
    {
        Mp += (int)val;
        if (Mp <= 0)
            Mp = 0;
        EventManager.Instance.QueueEvent(
            new Evt_ActorPropChange() { Target = this, PropName = "Mp" });
    }

    public void AddBuff(int id)
    {
        var buff = Buff.Create(id);
        var node = Buffs.Find(i => { return i.Id == id; });
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
        var node = Buffs.Find(i => { return i.Id == id; });
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
                    ChangeAp(Ap-card.Cost);
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
        return Name;
    }
}