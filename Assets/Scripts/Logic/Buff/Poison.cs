using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YX;

namespace GameBuff
{
    public class Poison : Buff
    {
        public override string Id => "Poison";
        public override string Name => "中毒";
        public override string Desc => $"在回合结束时造成{Count}点自然伤害";
        public override BuffType Type => Buff.BuffType.Poison;

        public override void Overlay(Buff buff)
        {
            if (buff.Type == BuffType.Poison)
            {
                Count += buff.Count;
            }
        }

        public override void RoundEnd()
        {
            var cmd = new Command();
            cmd.Key = Command.CommandKey.HpChange.ToString();
            cmd.Type = Command.CommandType.Nature.ToString();
            cmd.NumArg = -Count;
            cmd.Caller = (GameMgr.Instance.CurrState as BattleState).GetAnother(Owner);
            cmd.Target = Owner;
            cmd.Execute();

            Count -= 1;
            if (Count <= 0)
                Owner.RemoveBuff(Type);
            else
            {
                var evt = new Evt_UpdateBuff() { Target = Owner, Data = this };
                EventManager.Instance.QueueEvent(evt);
            }
        }

        public static Poison New(int arg)
        {
            var p = new Poison();
            p.Count = arg;
            return p;
        }
    }
}
