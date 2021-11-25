using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CDragnDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 invisdefaultposition;
    public Vector3 defaultposition;

    public GameObject Invisiable;

    bool IsCollision = false;

    void Start()
    {
        invisdefaultposition = Invisiable.transform.position;
        defaultposition =  this.transform.position;
    }

    void OnTriggerEnter2D(Collider2D o)
    {
        //Debug.Log("Tf");
        if (o.tag == "inv")
        {
            IsCollision = true;
        }
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        defaultposition = this.transform.position;  //처음 위치 저장
    }
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector2 currentPos = Input.mousePosition;
        this.transform.position = currentPos;
    }
    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (IsCollision == false)
        {
            this.transform.position = defaultposition;
        }
        else
        {
            this.transform.position = invisdefaultposition;
            Invisiable.transform.position = defaultposition;
        }
    }
}
