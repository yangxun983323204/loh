using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public enum Scene
    {
        Splash,
        MainMenu,
        World,
        Battle,
    }

    private string _currScene = "Splash";
    private Dictionary<Scene, string> _scDict;

    private void Awake()
    {
        _scDict = new Dictionary<Scene, string>();
        _scDict.Add(Scene.Splash, "Splash");
        _scDict.Add(Scene.MainMenu, "MainMenu");
        _scDict.Add(Scene.World, "World");
        _scDict.Add(Scene.Battle, "Battle");
    }

    public void MoveGameObjectToScene(GameObject obj)
    {
        SceneManager.MoveGameObjectToScene(obj, SceneManager.GetSceneByName(_currScene));
    }

    public void FadeTo(Scene sc,System.Action<Scene> callback = null)
    {
        StartCoroutine(FadeToImpl(sc, callback));
    }

    private IEnumerator FadeToImpl(Scene sc, System.Action<Scene> callback)
    {
        var name = _scDict[sc];
        var t0 = Time.time;
        SceneManager.LoadScene("Fade",LoadSceneMode.Additive);
        yield return new WaitForSeconds(1f);
        DisableScene(_currScene);
        yield return SceneManager.UnloadSceneAsync(_currScene);
        yield return WaitFrame(3);
        yield return SceneManager.LoadSceneAsync(name,LoadSceneMode.Additive);
        _currScene = name;
        yield return WaitFrame(3);
        MoveDynamicObjects();
        yield return SceneManager.UnloadSceneAsync("Fade", UnloadSceneOptions.None);
        yield return WaitFrame(30);
        var wait = 2 - (Time.time - t0);
        if (wait>0)
        {
            yield return new WaitForSeconds(wait);
        }
        
        callback?.Invoke(sc);
    }

    private IEnumerator WaitFrame(int cnt)
    {
        var frame = new WaitForEndOfFrame();
        for (int i = 0; i < cnt; i++)
        {
            yield return frame;
        }
    }

    private void DisableScene(string name)
    {
        var sc = SceneManager.GetSceneByName(name);
        var roots = sc.GetRootGameObjects();
        foreach (var go in roots)
        {
            go.SetActive(false);
        }
    }

    private void MoveDynamicObjects()
    {
        var sc = SceneManager.GetSceneByName("Fade");
        var roots = sc.GetRootGameObjects();
        var targetSc = SceneManager.GetSceneByName(_currScene);
        
        foreach (var go in roots)
        {
            if (go.name!="YxFade")
            {
                SceneManager.MoveGameObjectToScene(go, targetSc);
            }
        }
    }
}
