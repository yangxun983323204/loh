using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuffView : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public Image Icon;
    public Text Count;

    public Transform TextRoot;
    public Text Name;
    public Text Desc;

    public BuffRecord Data;

    public void SetData(BuffRecord buff)
    {
        Data = buff;
        Icon.sprite = Resources.Load<Sprite>(buff.View);
        if (Icon.sprite == null)
        {
            Debug.LogWarning($"加载{0}失败");
        }
        TextRoot.gameObject.SetActive(false);
        UpdateProps();
    }

    public void UpdateProps()
    {
        Count.text = Data.Count.ToString();
        Name.text = Data.Name;
        Desc.text = Data.Desc;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        TextRoot.gameObject.SetActive(true);
        Vector3 wpos = transform.position + Vector3.up * 20 * transform.parent.lossyScale.y;
        TextRoot.position = wpos;
        Desc.text = Data.Desc;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        TextRoot.gameObject.SetActive(false);
    }
}
