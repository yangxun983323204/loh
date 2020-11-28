using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command
{
    public enum CommandType
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
    public CommandType Type { get; private set;}

    public Lazy<List<Arg>> Args { get; private set; }

    public Command()
    {
        Args = new Lazy<List<Arg>>(() => { return new List<Arg>(2); });
    }

    public void Init(int type,params string[] args)
    {
        Type = (CommandType)type;
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
            case CommandType.HpChange:
                Target.SetHp(Target.Hp + Args.Value[0].GetF());
                break;
            case CommandType.MpChange:
                Target.SetMp(Target.Mp + Args.Value[0].GetF());
                break;
            case CommandType.ApChange:
                Target.SetAp(Target.Ap + Args.Value[0].GetF());
                break;
            case CommandType.HpSet:
                Target.SetHp(Args.Value[0].GetF());
                break;
            case CommandType.MpSet:
                Target.SetMp(Args.Value[0].GetF());
                break;
            case CommandType.ApSet:
                Target.SetAp(Args.Value[0].GetF());
                break;
            default:
                break;
        }
    }
}
