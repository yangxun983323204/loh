using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleMgr : SingletonMono<BattleMgr>
{
    private List<Actor> _players = new List<Actor>(2);
    private List<Actor> _enemies = new List<Actor>(2);
    private Coroutine _turnHandler = null;

    public IEnumerator InitBattle(Actor[] players,Actor[] enemies)
    {
        for (int i = 0; i < players.Length; i++)
            yield return AssetsMgr.Instance.Prepare<GameObject>(players[i].RenderData.AssetId, 1);
        for (int i = 0; i < enemies.Length; i++)
            yield return AssetsMgr.Instance.Prepare<GameObject>(enemies[i].RenderData.AssetId, 1);

        for (int i = 0; i < players.Length; i++)
        {
            var player = players[i];
            var iter = AssetsMgr.Instance.Get<GameObject>(player.RenderData.AssetId);
            yield return iter;
            var obj = iter.Asset<GameObject>();
            player.SetRenderer(obj.AddComponent<ActorRenderer>());
            Place(_players,player);
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            var enemy = enemies[i];
            var iter = AssetsMgr.Instance.Get<GameObject>(enemy.RenderData.AssetId);
            yield return iter;
            var obj = iter.Asset<GameObject>();
            enemy.SetRenderer(obj.AddComponent<ActorRenderer>());
            Place(_enemies,enemy);
        }

        var animList = new List<Actor>(players.Length + enemies.Length);
        animList.AddRange(players);
        animList.AddRange(enemies);
        yield return animList.SelcetParallel(actor => StartCoroutine(actor.Renderer.Appear())).GetEnumerator();
    }

    public IEnumerator EndBattle()
    {
        EndTurnLoop();
        var actors = new List<Actor>(_players.Count + _enemies.Count);
        actors.AddRange(_players);
        actors.AddRange(_enemies);
        yield return actors.Select(actor => StartCoroutine(actor.Renderer.Dispose())).GetEnumerator();
        for (int i = 0; i < actors.Count; i++)
        {
            var actor = actors[i];
            AssetsMgr.Instance.Recycle(actor.RenderData.AssetId, actor.Renderer.gameObject);// 回收资源
            Destroy(actor.Renderer);// 移除组件
        }
        _players.Clear();
        _enemies.Clear();
    }

    public int GetMateCount(Actor actor)
    {
        if (_players.Contains(actor))
        {
            return _players.Count - 1;
        }
        else if (_enemies.Contains(actor))
        {
            return _enemies.Count - 1;
        }
        return 0;
    }

    public int GetEnemyCount(Actor actor)
    {
        if (_players.Contains(actor))
        {
            return _enemies.Count;
        }
        else if (_enemies.Contains(actor))
        {
            return _players.Count;
        }
        return 0;
    }
    /// <summary>
    /// 获取同伴
    /// </summary>
    public Actor GetMate(Actor actor,int idx)
    {
        if (_players.Contains(actor))
        {
            var a = _players[idx];
            if (a == actor)
                return _players[idx + 1];
        }
        else if (_enemies.Contains(actor))
        {
            var a = _enemies[idx];
            if (a == actor)
                return _enemies[idx + 1];
        }

        return null;
    }
    /// <summary>
    /// 获取敌人
    /// </summary>
    public Actor GetEnemy(Actor actor,int idx)
    {
        if (_players.Contains(actor))
        {
            return _enemies[idx];
        }
        else if (_enemies.Contains(actor))
        {
            return _players[idx];
        }

        return null;
    }

    public void ForeachPlayer(System.Action<Actor> func)
    {
        foreach (var a in _players)
        {
            func?.Invoke(a);
        }
    }

    public void ForeachEnemy(System.Action<Actor> func)
    {
        foreach (var a in _enemies)
        {
            func?.Invoke(a);
        }
    }

    private IEnumerator BeginTurnLoopImpl()
    {
        int perMaxTime = 20;//
        var span = new WaitForSeconds(1);
        while (true)
        {
            Debug.Log("回合开始");
            var actors = new List<Actor>(_players.Count + _enemies.Count);
            actors.AddRange(_players);
            actors.AddRange(_enemies);
            foreach (var p in actors)
            {
                var handler = p.Renderer.gameObject.GetComponent<TurnHandlerBase>();
                if (handler!=null)
                {
                    handler.StartTurn();
                    var currTime = Time.realtimeSinceStartup;
                    while (!handler.IsDone)
                    {
                        yield return span;
                        if (Time.realtimeSinceStartup - currTime > perMaxTime)
                        {
                            handler.ForceStop();
                            goto tag_next;
                        }
                    }
                }
                tag_next:
                yield return span;
            }

            Debug.Log("回合结束");
        }
    }

    public void BeginTurnLoop()
    {
        if (_turnHandler==null)
        {
            _turnHandler = StartCoroutine(BeginTurnLoopImpl());
        }
    }

    private void EndTurnLoop()
    {
        if (_turnHandler != null)
        {
            StopCoroutine(_turnHandler);
            Debug.Log("回合中止");
            _turnHandler = null;
        }
    }

    private void Place(List<Actor> list,Actor actor)
    {
        bool isPlayer = list == _players;
        Vector3 basePos,offset;
        Debug.Log("这里决定放置位置");
        if (isPlayer)
        {
            basePos = new Vector3(-2, 0, 0);
            offset = new Vector3(-0.2f, -0.2f, -0.2f);
        }
        else
        {
            basePos = new Vector3(2, 0, 0);
            offset = new Vector3(0.2f, -0.2f, -0.2f);
        }
        int idx = list.Count;
        if (idx == 0)
            actor.Renderer.transform.position = basePos;
        else if (idx % 2 == 1)
            actor.Renderer.transform.position = basePos + (idx + 1) / 2 * offset;
        else
            actor.Renderer.transform.position = basePos - idx / 2 * offset;

        list.Add(actor);
    }
}
