using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CDragnDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //Transform ClickAnsObj = null;
    //Transform OldAnsObj = null;

    private Vector3 invisdefaultposition;
    private Vector3 defaultposition;

    public GameObject Invisiable;
    Text SCriptTxt;

    CollisionEvent m_CCollsion;
    CCalculate     m_CCal;

    private void Awake()
    {
    }

    void Start()
    {
        //m_CCal.GenerateQuiz();

        invisdefaultposition = Invisiable.transform.position;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        //Transform ClickAnsObj = this.transform;
        //if (OldAnsObj != null)
        //{ }
        //OldAnsObj = ClickAnsObj;
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
        if (m_CCollsion.IsCollision == false)
        {
            this.transform.position = defaultposition;

            SCriptTxt = this.gameObject.GetComponent<Text>();
            int nAnsNum = int.Parse(SCriptTxt.ToString());
            m_CCal.GenerateQuiz();

            var c = new CCalculate();
            int nMid = CCalculate.MiddleNum;
            SCriptTxt.text = CCalculate.MiddleNum.ToString();
        }
        else
        {
            this.transform.position = invisdefaultposition;
            Invisiable.transform.position = defaultposition;

            m_CCollsion.IsCollision = false;
        }
    }
}
