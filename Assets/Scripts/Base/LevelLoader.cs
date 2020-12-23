using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void FadeTo(string name,System.Action<string> callback = null)
    {
        StartCoroutine(FadeToImpl(name,callback));
    }

    private IEnumerator FadeToImpl(string name, System.Action<string> callback)
    {
        SceneManager.LoadScene("Fade");
        var iter = SceneManager.LoadSceneAsync(name);
        yield return iter;
        yield return null;
        callback?.Invoke(name);
    }
}
