using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YX;

public class HandLayout : MonoBehaviour
{
    public GameObject Template;

    private Pool<GameObject> _pool;
    private int _scaleCnt = 0;
    private float _perWidth = 0;
    private int _maxWidth = 500;
    // Start is called before the first frame update
    void Start()
    {
        _pool = new Pool<GameObject>();
        var allocator = new GameObjectAllocator();
        _pool.SetTemplate(Instantiate(Template), allocator);
    }

    public void SetRect(int cardWidth,int maxWidth)
    {
        _scaleCnt = Mathf.FloorToInt(maxWidth / (cardWidth /2f));
        _perWidth = cardWidth / 2f;
        _maxWidth = maxWidth;
    }

    public void Insert(int idx,GameObject go)
    {
        var item = _pool.Spawn();
        item.transform.SetParent(transform);
        item.transform.SetSiblingIndex(idx);
        go.transform.SetParent(item.transform);
        Scale();
    }

    public void Add(GameObject go)
    {
        var item = _pool.Spawn();
        item.transform.SetParent(transform);
        item.transform.localPosition = Vector3.zero;
        item.transform.localScale = Vector3.one;
        item.transform.SetAsLastSibling();
        go.transform.SetParent(item.transform);
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = Vector3.one;
        Scale();
    }

    public int Remove(GameObject go)
    {
        var idx = IndexOf(go);
        if (idx>0)
        {
            go.transform.SetParent(null, true);
            _pool.Recycle(Item(idx));
        }
        Scale();

        return idx;
    }

    public int IndexOf(GameObject go)
    {
        if (go.transform.parent == null || go.transform.parent.parent == null)
            return -1;

        if (go.transform.parent.parent != transform)
            return -1;

        return go.transform.parent.GetSiblingIndex();
    }

    private GameObject Item(int idx)
    {
        if (idx < 0 || idx >= transform.childCount)
            return null;

        return transform.GetChild(idx).gameObject;
    }

    private void Scale()
    {
        var rt = (transform as RectTransform);
        if (rt.childCount <= _scaleCnt)
        {
            rt.sizeDelta = new Vector2((int)(_perWidth * rt.childCount), rt.sizeDelta.y);
        }
        else
        {
            rt.sizeDelta = new Vector2(_maxWidth, rt.sizeDelta.y);
        }
    }
}
