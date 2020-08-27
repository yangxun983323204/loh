﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EffectUI : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public Image Icon;
    public Text Desc;

    private void Start()
    {
        Desc.enabled = false;
        Desc.text = null;
    }

    public void SetData(Effect effect)
    {
        StopCoroutine("SetDataImpl");
        StartCoroutine(SetDataImpl(effect));
    }

    private IEnumerator SetDataImpl(Effect effect)
    {
        Desc.text = effect.Desc;
        var iter = AssetsMgr.Instance.Get<Sprite>(effect.Icon);
        yield return iter;
        Icon.sprite = iter.Asset<Sprite>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Desc.enabled = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Desc.enabled = false;
    }
}
