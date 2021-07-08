using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapState : GameMgr.GameState
{
    public override string GetName()
    {
        return "WorldMapState";
    }

    public override void OnEnter()
    {
        Bind();
        GameMgr.Instance.LevelLoader.FadeTo(LevelLoader.Scene.World);
    }

    public override void OnExit()
    {
        Unbind();
    }

    void Bind() {
        YX.EventManager.Instance.AddListener(Evt_Back.EvtType, OnBack);
        YX.EventManager.Instance.AddListener(Evt_EnterArea.EvtType, OnEnterArea);
    }
    void Unbind()
    {
        YX.EventManager.Instance.RemoveListener(Evt_Back.EvtType, OnBack);
        YX.EventManager.Instance.RemoveListener(Evt_EnterArea.EvtType, OnEnterArea);
    }

    void OnBack(YX.EventDataBase evt)
    {
        var e = evt as Evt_Back;
        if (e.Self(this))
        {
            GameMgr.Instance.EnterState(GameMgr.StateType.MainMenu);
        }
    }

    void OnEnterArea(YX.EventDataBase evt)
    {
        var e = evt as Evt_EnterArea;
        GameMgr.Instance.EnterState(GameMgr.StateType.Battle);
        YX.EventManager.Instance.QueueEvent(evt);
    }
}
