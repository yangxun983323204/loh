using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YX;

public class GameMgr : MonoBehaviour
{
    public abstract class GameState
    {
        public abstract string GetName();
        public virtual void OnEnter() { }
        public virtual void OnUpdate() { }
        public virtual void OnExit() { }
    }

    private static GameMgr _inst;
    public static GameMgr Instance { get; private set; }


    public GameState MainMenu { get; private set; }
    public GameState Battle { get; private set; }

    public GameState CurrState { get; set; }

    public IGameCardDB CardDB { get { return _db; } }
    public IActorDB ActorDB { get { return _db; } }
    public IActorDeckDB DeckDB { get { return _db; } }
    public BattleMgr BattleMgr { get; private set; }
    public Pool<GameObject> CardPool { get; private set; }
    public LevelLoader LevelLoader { get; private set; }

    private SqliteDB _db;

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
        _db = new SqliteDB();
        BattleMgr = gameObject.AddComponent<BattleMgr>();
        CardPool = new Pool<GameObject>();
        var tmp = Instantiate(Resources.Load<GameObject>("CardView/TCard"));
        var allocator = new GameObjectAllocator();
        CardPool.SetTemplate(tmp, allocator);
        DontDestroyOnLoad(allocator.CacheRoot);
        LevelLoader = gameObject.AddComponent<LevelLoader>();
        //
        MainMenu = new MainMenuState();
        Battle = new BattleState();
        //
        EnterState(MainMenu);
    }

    public void Shutdown()
    {
        Instance = null;
        _db.Release();
        Application.Quit();
    }

    public void EnterState(GameState state)
    {
        if (CurrState != null)
            CurrState.OnExit();

        CurrState = state;
        CurrState.OnEnter();
    }

    void Update()
    {
        EventManager.Instance.Update(200);
        CurrState.OnUpdate();
    }
}
