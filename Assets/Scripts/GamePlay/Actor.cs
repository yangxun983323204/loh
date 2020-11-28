using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor:MonoBehaviour
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

    public List<Buff> Buffs { get; set; }
    public ActionChain<Command> onActionExecute { get; private set; } = new ActionChain<Command>();

    private CardPlayer _player;

    private void Awake()
    {
        _player = gameObject.GetComponent<CardPlayer>();
        _player.onWillPlay.Add(PlayCheck);
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

    public void SetHp(float val) { }

    public void SetAp(float val) { }

    public void SetMp(float val) { }

    public bool PlayCheck(CardPlayer player, Card card)
    {
        return true;
    }
}
