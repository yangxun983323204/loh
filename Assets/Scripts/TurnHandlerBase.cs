using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurnHandlerBase : MonoBehaviour
{
    public bool IsDone { get { return _isDone; } }
    protected bool _isDone = true;

    public abstract void StartTurn();
    public abstract void ForceStop();
}
