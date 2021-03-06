using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragnDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform root;
    // Start is called before the first frame update
    void Start()
    {
        root = transform.root;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        root.BroadcastMessage("BeginDrag", transform, SendMessageOptions.DontRequireReceiver);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        root.BroadcastMessage("Drag", transform, SendMessageOptions.DontRequireReceiver);
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        root.BroadcastMessage("Ending", transform, SendMessageOptions.DontRequireReceiver);
    }
}
