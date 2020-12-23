using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    public Button BtnStart;
    public Button BtnQuit;

    // Start is called before the first frame update
    void Start()
    {
        BtnStart.onClick.AddListener(()=> {
            YX.EventManager.Instance.QueueEvent(new Evt_StartGame());
        });

        BtnQuit.onClick.AddListener(()=> {
            YX.EventManager.Instance.QueueEvent(new Evt_Quit());
        });
    }
}
