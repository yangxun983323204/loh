using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff
{
    protected static Dictionary<BuffType, System.Func<int,Buff>> _ctorDict;
    public static Buff Create(BuffType type,int arg)
    {
        if (_ctorDict.ContainsKey(type))
        {
            return _ctorDict[type](arg);
        }

        return null;
    }

    public enum BuffType
    {
        None,

        Poison,// 中毒
        IceArmor,// 冰甲
        Burn,// 燃烧
        Electrified,// 感电
    }

    public Actor Owner { get; protected set; }
    public int Count { get; protected set; }
    public abstract BuffType Type { get; }

    public abstract string Id { get; }
    public abstract string Name { get; }
    public abstract string Desc { get; }

    public virtual void SetOwner(Actor owner)
    {
        Owner = owner;
    }

    public virtual void RemoveOwner()
    {
        Owner = null;
    }

    public virtual void RoundStart() { }
    public virtual void RoundEnd() { }
    public abstract void Overlay(Buff buff);
    /// <summary>
    /// 注册buff
    /// </summary>
    public static void Reg()
    {
        _ctorDict = new Dictionary<BuffType, System.Func<int, Buff>>();
        _ctorDict.Add(BuffType.Poison, GameBuff.Poison.New);
        _ctorDict.Add(BuffType.IceArmor, GameBuff.IceArmor.New);
    }
}
