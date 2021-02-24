using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using YX;

public class CardView : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Canvas SelfCanvas;
    public RectTransform CanvasRect;
    public RawImage ActorImg;
    public Text Name;
    public Text Desc;
    public Transform CardTypeRoot;

    public Actor Owner { get; set; }
    public GameCard Data { get; set; }
    public bool Draggable { get; set; } = true;

    private bool _isDraging = false;
    private Vector3 _dragOffset = Vector2.zero;

    void Start()
    {
        SelfCanvas.overrideSorting = false;
    }

    public void Init(GameCard card)
    {
        _isDraging = false;
        Data = card;
        // todo
        var r = Resources.Load<Texture2D>(Path.Combine("CardView",card.View));
        ActorImg.texture = r;
        Name.text = card.Name;
        Desc.text = card.Desc;

        var t = (int)card.Type;
        int i = 0;
        foreach (Transform tt in CardTypeRoot)
        {
            if (i != t)
                tt.gameObject.SetActive(false);
            else
                tt.gameObject.SetActive(true);

            i++;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!Draggable)
            return;

        _isDraging = true;
        Vector3 wpos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(CanvasRect,(Vector3)eventData.pointerCurrentRaycast.screenPosition,Camera.main,out wpos);
        _dragOffset = transform.position - wpos;
        SelfCanvas.overrideSorting = true;
        SelfCanvas.sortingOrder = 10;
        transform.localScale = Vector3.one * 2f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!Draggable)
            return;

        Vector3 wpos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(CanvasRect, (Vector3)eventData.pointerCurrentRaycast.screenPosition, Camera.main, out wpos);
        transform.position = wpos + _dragOffset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isDraging = false;
        if (!Draggable)
            return;

        SelfCanvas.overrideSorting = false;
        transform.localScale = Vector3.one;
        if (Mathf.Abs(transform.localPosition.y)< (transform as RectTransform).rect.height)
        {
            transform.localPosition = Vector3.zero;
            return;
        }

        if (GameMgr.Instance.CurrState is BattleState)
        {
            var battleState = GameMgr.Instance.CurrState as BattleState;
            if (battleState.CurrentActor == Owner && battleState.CanPlay(Owner,Data))
            {
                EventManager.Instance.TriggerEvent(
                    new Evt_TryPlayCard()
                    {
                        Owner = Owner,
                        Target = battleState.GetAnother(Owner),
                        Card = Data
                    });
            }
            else
            {
                transform.localPosition = Vector3.zero;
            }
        }
        else
        {
            transform.localPosition = Vector3.zero;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SelfCanvas.overrideSorting = true;
        SelfCanvas.sortingOrder = 10;
        transform.localScale = Vector3.one * 2f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_isDraging)
            return;

        SelfCanvas.overrideSorting = false;
        transform.localScale = Vector3.one;
        transform.localPosition = Vector3.zero;
    }
}
