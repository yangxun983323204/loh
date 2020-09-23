using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDB", menuName = "LOH/DataBase")]
public class DataBase : ScriptableObject
{
    public ScriptableObject[] Datas;
    private Dictionary<string, int> _indices;

    private void Awake()
    {
        if (Application.isPlaying)
        {
            _indices = new Dictionary<string, int>(Datas.Length);
            for (int i = 0; i < Datas.Length; i++)
            {
                _indices.Add(Datas[i].name, i);
            }
        }
    }

    public T At<T>(int idx) where T : ScriptableObject
    {
        return Datas[idx] as T;
    }

    public T Get<T>(string name) where T: ScriptableObject
    {
        return Datas[_indices[name]] as T;
    }
}
