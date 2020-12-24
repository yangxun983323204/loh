using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private string _currScene = "Splash";

    public void MoveGameObjectToScene(GameObject obj)
    {
        SceneManager.MoveGameObjectToScene(obj, SceneManager.GetSceneByName(_currScene));
    }

    public void FadeTo(string name,System.Action<string> callback = null)
    {
        StartCoroutine(FadeToImpl(name,callback));
    }

    private IEnumerator FadeToImpl(string name, System.Action<string> callback)
    {
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
        
        callback?.Invoke(name);
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
