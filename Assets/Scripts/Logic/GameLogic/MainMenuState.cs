using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuState : GameMgr.GameState
{
    public override string GetName()
    {
        return "MainMenuState";
    }

    public override void OnEnter()
    {
        Bind();
        GameMgr.Instance.LevelLoader.FadeTo(LevelLoader.Scene.MainMenu);
    }

    public override void OnExit()
    {
        Unbind();
    }

    private void Bind()
    {
        YX.EventManager.Instance.AddListener(Evt_StartGame.EvtType,OnStartGame);
        YX.EventManager.Instance.AddListener(Evt_Quit.EvtType, OnQuit);
    }

    private void Unbind()
    {
        YX.EventManager.Instance.RemoveListener(Evt_StartGame.EvtType, OnStartGame);
        YX.EventManager.Instance.RemoveListener(Evt_Quit.EvtType, OnQuit);
    }

    private void OnStartGame(YX.EventDataBase evt)
    {
        GameMgr.Instance.EnterState(GameMgr.StateType.WorldMap);
    }

    private void OnQuit(YX.EventDataBase evt)
    {
        GameMgr.Instance.Shutdown();
    }
}
