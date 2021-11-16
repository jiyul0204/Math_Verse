using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using UniRx;
using UniRx.Triggers;

public class Drag : MonoBehaviour
{
    public RectTransform target;
    public bool horizontal = true;
    public bool vertical = true;

    public bool Interactable
    {
        get
        {
            return graphic.raycastTarget;
        }
        set
        {
            graphic.raycastTarget = value;
        }
    }

    private Canvas _canvas;
    public Canvas Canvas
    {
        get
        {
            if (_canvas == null)
            {
                _canvas = GetComponentInParent<Canvas>();
            }

            return _canvas;
        }
        set
        {
            _canvas = value;
        }
    }

    private Graphic _graphic;
    private Graphic graphic
    {
        get
        {
            return _graphic ?? (_graphic = GetComponent<Graphic>());
        }
    }

    private ObservableEventTrigger _trigger;
    private ObservableEventTrigger trigger
    {
        get
        {
            if (_trigger != null)
            {
                return _trigger;
            }

            _trigger = gameObject.GetComponent<ObservableEventTrigger>();

            if (_trigger != null)
            {
                return _trigger;
            }

            _trigger = gameObject.AddComponent<ObservableEventTrigger>();

            return _trigger;
        }
    }

    private void Awake()
    {
        if (graphic == null)
        {
            Debug.LogWarning("Graphic component is required.");

            gameObject.AddComponent<Image>().color = Color.clear;
        }

        if (target == null)
        {
            target = GetComponent<RectTransform>();
        }

        trigger.OnBeginDragAsObservable()
            .Where(e => Interactable && target)
            .SelectMany(trigger.OnDragAsObservable())
            .TakeUntil(trigger.OnEndDragAsObservable())
            .TakeWhile(e => Interactable && target)
            .Select(e => GetPosition(e))
            .Pairwise() //NOTE: Buffer (2,1) it is not the last to pair value will come 
            .RepeatUntilDestroy(this)
            .Subscribe(OnDrag)
            .AddTo(this);
    }

    private Vector3 GetPosition(PointerEventData eventData)
    {
        var screenPosition = eventData.position;
        var result = Vector3.zero;

        switch (Canvas.renderMode)
        {
            case RenderMode.ScreenSpaceOverlay:
            case RenderMode.ScreenSpaceCamera:
                RectTransformUtility.ScreenPointToWorldPointInRectangle(
                    target,
                    screenPosition,
                    eventData.pressEventCamera,
                    out result);
                break;
            case RenderMode.WorldSpace:
                Debug.LogWarning("not supported RenderMode.WorldSpace.");
                break;
        }

        return result;
    }

    private void OnDrag(Pair<Vector3> positions)
    {
        var deltaX = horizontal ? positions.Current.x - positions.Previous.x : 0;
        var deltaY = vertical ? positions.Current.y - positions.Previous.y : 0;

        target.position += new Vector3(deltaX, deltaY, 0);
    }
}