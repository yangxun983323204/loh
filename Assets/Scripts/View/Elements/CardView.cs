using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using YX;

public class CardView : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public Actor Owner { get; set; }
    public GameCard Data { get; set; }
    public bool Draggable { get; set; } = true;

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

        var desc = gameObject.GetComponentInChildren<Text>();
        desc.text = card.Desc;
    }

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
        transform.localScale = Vector3.one * 2f;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!Draggable)
            return;

        _selfCanvas.overrideSorting = false;
        transform.localScale = Vector3.one;
        if (Mathf.Abs(transform.localPosition.y)< (transform as RectTransform).rect.height)
        {
            transform.localPosition = Vector3.zero;
            return;
        }

        var battleState = GameMgr.Instance.Battle;
        if (GameMgr.Instance.CurrState == battleState)
        {
            if (battleState.CanPlay(Owner,Data))
            {
                EventManager.Instance.TriggerEvent(
                    new Evt_TryPlayCard()
                    {
                        Owner = Owner,
                        Target = GameMgr.Instance.Battle.GetAnother(Owner),
                        Card = Data
                    });
            }
        }
    }
}
