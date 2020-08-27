using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestBattleMgr
    {
        [UnityTest]
        public IEnumerator Battle()
        {
            var cam = new GameObject("Camera");
            var camera = cam.AddComponent<Camera>();
            camera.transparencySortMode = TransparencySortMode.Perspective;
            camera.transform.position = new Vector3(0, 0, -5);
            var iter = AssetsMgr.Instance.Get<ActorDB>("DB/ActorDB");
            yield return iter;
            var actorDB = iter.Asset<ActorDB>();
            var player = Object.Instantiate(actorDB.Actors[0]);
            var enemy0 = Object.Instantiate(actorDB.Actors[1]);
            var enemy1 = Object.Instantiate(actorDB.Actors[1]);

            yield return BattleMgr.Instance.BeginBattle(new Actor[] { player }, new Actor[] { enemy0,enemy1 });
            yield return new WaitForSeconds(5);
            yield return BattleMgr.Instance.EndBattle();
            yield return new WaitForSeconds(3);
        }
    }
}
