using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBattleMgr : MonoBehaviour
{
    public float TimeScale = 1;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        // 0,1,-4
        Camera.main.transform.position = new Vector3(0, 1, -4);
        Time.timeScale = TimeScale;
        yield return AssetsMgr.Instance.Prepare<GameObject>("Prefab/UI/ActorCanvas", 5);
        var iter = AssetsMgr.Instance.Get<DataBase>("DB/ActorDB");
        yield return iter;
        var actorDB = iter.Asset<DataBase>();
        yield return BattleMgr.Instance.InitBattle(
            new Actor[] {
                actorDB.At<Actor>(0).Clone()
            }, 
            new Actor[] {
                actorDB.At<Actor>(1).Clone(),
                actorDB.At<Actor>(1).Clone()
            });
        BattleMgr.Instance.ForeachPlayer(a => { a.Renderer.gameObject.AddComponent<TestTurnHandler>(); });
        BattleMgr.Instance.ForeachEnemy(a => { a.Renderer.gameObject.AddComponent<TestTurnHandler>(); });
        BattleMgr.Instance.BeginTurnLoop();
        yield return new WaitForSeconds(100);
        yield return BattleMgr.Instance.EndBattle();
    }
}
