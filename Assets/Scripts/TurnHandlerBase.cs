using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurnHandlerBase : MonoBehaviour
{
    public abstract AsyncOperation Handle();
}
