using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

public class CardView : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public GameCard Data { get; private set; }
    public bool Draggable { get; set; } = true;
    public ActionChain<CardView> onEndDrag { get; private set; } = new ActionChain<CardView>();

    private Vector3 _dragOffset = Vector2.zero;

    public void Init(GameCard card)
    {
        Data = card;
        // todo
        var r = Resources.Load<Texture2D>(Path.Combine("CardView",card.View));
        var img = gameObject.GetComponentInChildren<RawImage>();
        img.texture = r;
    }

    public void ShowFront() { }

    public void ShowBack() { }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!Draggable)
            return;

        _dragOffset = transform.position - (Vector3)eventData.pointerCurrentRaycast.screenPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!Draggable)
            return;

        transform.position = (Vector3)eventData.pointerCurrentRaycast.screenPosition + _dragOffset;
        transform.localScale = Vector3.one * 1.5f;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!Draggable)
            return;

        transform.localScale = Vector3.one;
        if (onEndDrag.Invoke(this))
        {
            transform.localPosition = Vector3.zero;
        }
    }
}
