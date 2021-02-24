using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldView : MonoBehaviour
{
    public ToggleGroup AreaGroup;
    public Button BtnEsc;
    public Button BtnEnter;

    // Start is called before the first frame update
    void Start()
    {
        BtnEnter.interactable = false;
        Bind();
    }

    void Bind()
    {
        BtnEsc.onClick.AddListener(()=> {
            YX.EventManager.Instance.QueueEvent(new Evt_Back());
        });

        BtnEnter.onClick.AddListener(()=> {
            YX.EventManager.Instance.QueueEvent(new Evt_EnterArea(1));
        });

        var areas = AreaGroup.GetComponentsInChildren<AreaView>();
        foreach (var tog in areas)
        {
            tog.onValueChanged.AddListener(v=> {
                if (AreaGroup.AnyTogglesOn())
                    BtnEnter.interactable = true;
                else
                    BtnEnter.interactable = false;
            });
        }
    }
}
