using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardView : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public bool Draggable { get; set; } = true;
    public ActionChain<CardView> onEndDrag { get; private set; } = new ActionChain<CardView>();

    private Vector3 _dragOffset = Vector2.zero;

    public void Init(GameCard card)
    {
        // todo
        var r = Resources.Load<Texture2D>(card.View);
        var img = gameObject.AddComponent<RawImage>();
        img.texture = r;
        (transform as RectTransform).sizeDelta = new Vector2(r.width, r.height);
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
