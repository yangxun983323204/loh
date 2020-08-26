using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T:SingletonMono<T>
{
    private static T _inst;
    public static T Instance
    {
        get
        {
            if (_inst == null)
            {
                var obj = new GameObject(typeof(T).ToString());
                _inst = obj.AddComponent<T>();
                _inst.OnInit();
            }
            return _inst;
        }
    }

    protected virtual void OnInit()
    {

    }
}
