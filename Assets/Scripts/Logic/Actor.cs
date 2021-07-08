using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YX;

public class Actor
{
    public int Id { get;private set; }
    public int Lv { get;private set; }

    public int HpMax { get; private set; }
    public int ApMax { get; private set; }
    public int MpMax { get; private set; }

    public int Hp { get; private set; }
    public int Ap { get; private set; }
    public int Mp { get; private set; }

    public string View { get;private set; }

    public LinkedListEx<Buff> Buffs { get;private set; }

    public CardPlay Play { get; private set; }

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
        Hp = record.Hp;
        HpMax = record.Hp;
        Ap = record.Ap;
        ApMax = record.Ap;
        Mp = record.Mp;
        MpMax = record.Mp;
        View = record.View;
    }

    public bool CanPlayCard(GameCard card)
    {
        bool canPlay = false;
        switch (card.Type)
        {
            case GameCard.CardType.Normal:
                canPlay = true;
                break;
            case GameCard.CardType.Magic:
                canPlay = card.Cost <= Mp;
                break;
            case GameCard.CardType.Action:
                canPlay = card.Cost <= Ap;
                break;
            case GameCard.CardType.Equip:
                canPlay = true;
                break;
            default:
                break;
        }

        return canPlay;
    }

    public void ChangeHp(float val)
    {
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

    public bool PlayCard(GameCard card)
    {
        if (!CanPlayCard(card))
            return false;

        var s =Play.Play(card);
        if (s)
        {
            switch (card.Type)
            {
                case GameCard.CardType.Normal:
                    break;
                case GameCard.CardType.Magic:
                    ChangeMp(-card.Cost);
                    break;
                case GameCard.CardType.Action:
                    ChangeAp(Ap-card.Cost);
                    break;
                case GameCard.CardType.Equip:
                    break;
                default:
                    break;
            }
        }
        return s;
    }
}