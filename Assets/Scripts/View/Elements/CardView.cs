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

    private RectTransform _canvasRect;
    private Vector3 _dragOffset = Vector2.zero;
    private Canvas _selfCanvas;

    void Start()
    {
        _canvasRect = GetComponentInParent<Canvas>().transform as RectTransform;
        _selfCanvas = gameObject.GetComponent<Canvas>();
        _selfCanvas.overrideSorting = false;
    }

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

        Vector3 wpos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(_canvasRect,(Vector3)eventData.pointerCurrentRaycast.screenPosition,Camera.main,out wpos);
        _dragOffset = transform.position - wpos;
        _selfCanvas.overrideSorting = true;
        _selfCanvas.sortingOrder = 10;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!Draggable)
            return;

        Vector3 wpos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(_canvasRect, (Vector3)eventData.pointerCurrentRaycast.screenPosition, Camera.main, out wpos);
        transform.position = wpos + _dragOffset;
        transform.localScale = Vector3.one * 1.5f;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!Draggable)
            return;

        _selfCanvas.overrideSorting = false;
        transform.localScale = Vector3.one;
        if (onEndDrag.Invoke(this))
        {
            transform.localPosition = Vector3.zero;
        }
    }

    public void Clear()
    {
        Data = null;
        onEndDrag.Clear();
        var img = gameObject.GetComponentInChildren<RawImage>();
        img.texture = null;
    }
}
