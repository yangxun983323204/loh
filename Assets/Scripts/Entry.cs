using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entry : MonoBehaviour
{
    public bool Debug = true;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        GameMgr.Create();
        GameMgr.Instance.Debug = Debug;
        if (!Debug)
        {
            yield return new WaitForSeconds(2);
        }
        GameMgr.Instance.EnterState(GameMgr.StateType.MainMenu);
    }
}
