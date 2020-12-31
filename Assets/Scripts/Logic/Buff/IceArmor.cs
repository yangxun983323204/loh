using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YX;

namespace GameBuff
{
    public class IceArmor : Buff
    {
        const int MAX_DEFENSE = 4;

        public override string Id => "IceArmor";
        public override BuffType Type => BuffType.IceArmor;
        public override string Name => "冰铠";
        public override string Desc => $"每次受伤最多抵挡{Mathf.Min(MAX_DEFENSE, Count)}点伤害并削减相应层数,受到冰属性攻击时防御效果翻倍，但是受到火属性伤害时防御效果减半。";

        public override void Overlay(Buff buff)
        {
            if (buff.Type == BuffType.IceArmor)
            {
                Count += buff.Count;
            }
        }

        public override void SetOwner(Actor owner)
        {
            base.SetOwner(owner);
            Owner.onChangeHp.Add(Fx);
        }

        public override void RemoveOwner()
        {
            Owner.onChangeHp.Remove(Fx);
            base.RemoveOwner();
        }

        private bool Fx(ref float val,ref Command.CommandType type)
        {
            if (val<0)
            {
                float abs = Mathf.Abs(val);
                float needDef = 0;
                float scale = 1;

                if (type== Command.CommandType.Ice)
                    scale = 2;
                else if (type == Command.CommandType.Fire)
                    scale = 0.5f;
                else
                    scale = 1;

                var canDef = Mathf.Min(MAX_DEFENSE, Count * scale);
                if (canDef >= abs)
                    needDef = abs;
                else
                    needDef = canDef;

                Count -= (int)(needDef/scale);
                val += needDef;
                if (val > 0) val = 0;

                if (Count <= 0)
                    Owner.RemoveBuff(Type);
                else
                {
                    var evt = new Evt_UpdateBuff() { Target=Owner, Data = this };
                    EventManager.Instance.QueueEvent(evt);
                }
            }
            return true;
        }

        public static IceArmor New(int arg)
        {
            var p = new IceArmor();
            p.Count = arg;
            return p;
        }
    }
}
