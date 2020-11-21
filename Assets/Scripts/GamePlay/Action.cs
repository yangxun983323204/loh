using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    public enum ActionType
    {
        HpChange,
        MpChange,
        ApChange,

        HpSet,
        MpSet,
        ApSet,
    }

    public class Arg
    {
        float _f;
        string _s;
        ushort _type;

        public void SetFormatValue(string val)
        {
            var a = val.Split(':');
            if (a[1] == "f")
            {
                _f = float.Parse(a[0]);
                _s = null;
                _type = 1;
            }
            else if (a[1] == "s")
            {
                _s = a[0];
                _type = 2;
            }
        }

        public float GetF()
        {
            if (_type != 1)
                throw new System.InvalidCastException();

            return _f;
        }

        public string GetS()
        {
            if (_type != 2)
                throw new System.InvalidCastException();

            return _s;
        }
    }

    public Actor Caller;
    public Actor Target;
    public ActionType Type { get; private set;}

    public Lazy<List<Arg>> Args { get; private set; }

    public Action()
    {
        Args = new Lazy<List<Arg>>(() => { return new List<Arg>(2); });
    }

    public void Init(int type,params string[] args)
    {
        Type = (ActionType)type;
        if (args!=null)
        {
            for (int i = 0; i < args.Length; i++)
            {
                var arg = new Arg();
                arg.SetFormatValue(args[i]);
                Args.Value.Add(arg);
            }
        }
    }

    public void Execute()
    {
        switch (Type)
        {
            case ActionType.HpChange:
                Target.SetHp(Target.Hp + Args.Value[0].GetF());
                break;
            case ActionType.MpChange:
                Target.SetMp(Target.Mp + Args.Value[0].GetF());
                break;
            case ActionType.ApChange:
                Target.SetAp(Target.Ap + Args.Value[0].GetF());
                break;
            case ActionType.HpSet:
                Target.SetHp(Args.Value[0].GetF());
                break;
            case ActionType.MpSet:
                Target.SetMp(Args.Value[0].GetF());
                break;
            case ActionType.ApSet:
                Target.SetAp(Args.Value[0].GetF());
                break;
            default:
                break;
        }
    }
}
