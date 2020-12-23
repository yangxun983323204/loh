using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YX;

public class GameMgr : MonoBehaviour
{
    enum GameState
    {
        MainMenu,
        SelectActor,
        LoadLevel,
        Running,
    }

    private static GameMgr _inst;
    public static GameMgr Instance { get; private set; }

    public IGameCardDB CardDB { get { return _db; } }
    public IActorDB ActorDB { get { return _db; } }
    public IActorDeckDB DeckDB { get { return _db; } }
    public BattleMgr BattleMgr { get; private set; }
    public Pool<GameObject> CardPool { get; private set; }

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
        _db = new SqliteDB();
        BattleMgr = gameObject.AddComponent<BattleMgr>();
        CardPool = new Pool<GameObject>();
        var tmp = Instantiate(Resources.Load<GameObject>("CardView/TCard"));
        var allocator = new GameObjectAllocator();
        CardPool.SetTemplate(tmp, allocator);
        DontDestroyOnLoad(allocator.CacheRoot);
    }

    public void Shutdown()
    {
        Instance = null;
        _db.Release();
    }

    void Update()
    {
        
    }
}
