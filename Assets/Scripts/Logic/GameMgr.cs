﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YX;

public class GameMgr : MonoBehaviour
{
    public bool Debug;

    public enum StateType
    {
        MainMenu,
        WorldMap,
        Battle,
    }

    public abstract class GameState
    {
        public abstract string GetName();
        public virtual void OnEnter() { }
        public virtual void OnUpdate() { }
        public virtual void OnExit() { }
    }

    private static GameMgr _inst;
    public static GameMgr Instance { get; private set; }

    public GameState CurrState { get;private set; }
    public IDB DB { get; private set; }
    public Pool<GameObject> CardPool { get; private set; }
    public LevelLoader LevelLoader { get; private set; }
    public ProcessManager ProcessMgr { get; private set; }
    public FxManager FxMgr { get; set; }

    private Dictionary<StateType, GameState> _stateDict = new Dictionary<StateType, GameState>();

    public static GameMgr Create()
    {
        if (Instance != null)
            return Instance;

        var obj = new GameObject("mgr");
        var mgr = obj.AddComponent<GameMgr>();
        mgr.Awake();
        return mgr;
    }

    private void Awake()
    {
        if (Instance == this)
            return;

        if (Instance!=null && Instance!=this)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Setup();
        }
    }

    private void OnDestroy()
    {
        Shutdown();
    }

    public void Setup()
    {
        new YX.EventManager();
        DB = new MockDB();
        CardPool = new Pool<GameObject>();
        var tmp = Instantiate(Resources.Load<GameObject>("view/card/TCard"));
        var allocator = new GameObjectAllocator();
        CardPool.SetTemplate(tmp, allocator);
        DontDestroyOnLoad(allocator.CacheRoot);
        LevelLoader = gameObject.AddComponent<LevelLoader>();
        ProcessMgr = new ProcessManager();
        //
        _stateDict.Add(StateType.MainMenu, new MainMenuState());
        _stateDict.Add(StateType.WorldMap, new WorldMapState());
        _stateDict.Add(StateType.Battle, new BattleState());
    }

    public void Shutdown()
    {
        Instance = null;
        DB.Dispose();
        Application.Quit();
    }

    public void EnterState(StateType type)
    {
        if (CurrState != null)
            CurrState.OnExit();

        CurrState = _stateDict[type];
        CurrState.OnEnter();
    }

    void Update()
    {
        var dms = (ulong)(Time.deltaTime * 100000);
        EventManager.Instance.Update(200);
        ProcessMgr.UpdateProcesses((ulong)dms);
        if (CurrState!=null)
            CurrState.OnUpdate();
    }
}
