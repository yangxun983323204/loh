using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayProcess : YX.Process
{
    private float _delayTime;
    private Action _action;

    private float _curr;

    public DelayProcess Set(float delayTime,Action action)
    {
        _delayTime = delayTime;
        _action = action;
        _curr = 0;
        return this;
    }

    public override void OnUpdate(ulong deltaMs)
    {
        _curr += deltaMs / 100000;
        if (_curr>=_delayTime)
        {
            Succeed();
            _action.Invoke();
        }
    }
}
