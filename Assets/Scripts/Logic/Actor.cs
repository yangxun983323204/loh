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
    public ActionChain<float, Command.CommandType> onChangeHp { get; private set; } = new ActionChain<float, Command.CommandType>();
    public ActionChain<float, Command.CommandType> onChangeMp { get; private set; } = new ActionChain<float, Command.CommandType>();
    public ActionChain<float, Command.CommandType> onChangeAp { get; private set; } = new ActionChain<float, Command.CommandType>();

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

    public void ChangeHp(float val,Command.CommandType type)
    {
        if (!onChangeHp.Invoke(ref val,ref type))
            return;

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

    public void ChangeAp(float val, Command.CommandType type)
    {
        if (!onChangeAp.Invoke(ref val,ref type))
            return;

        Ap += (int)val;
        EventManager.Instance.QueueEvent(
            new Evt_ActorPropChange() { Target = this, PropName = "Ap" });
    }

    public void ChangeMp(float val, Command.CommandType type)
    {
        if (!onChangeMp.Invoke(ref val,ref type))
            return;

        Mp += (int)val;
        EventManager.Instance.QueueEvent(
            new Evt_ActorPropChange() { Target = this, PropName = "Mp" });
    }

    public void AddBuff(Buff.BuffType type,int arg)
    {
        var buff = Buff.Create(type, arg);
        var node = Buffs.Find(i => { return i.Type == type; });
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

    public void RemoveBuff(Buff.BuffType type)
    {
        var node = Buffs.Find(i => { return i.Type == type; });
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
                    ChangeMp(-card.Cost, Command.CommandType.None);
                    break;
                case GameCard.CardType.Action:
                    ChangeAp(Ap-card.Cost, Command.CommandType.None);
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