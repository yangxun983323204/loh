using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entry : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        GameMgr.Create();
        yield return new WaitForSeconds(2);
        GameMgr.Instance.EnterState(GameMgr.Instance.MainMenu);
    }
}
