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
        m_CCollsion = GetComponent<CollisionEvent>();
        m_CCal = GetComponent<CCalculate>();
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
        defaultposition = transform.position;  //처음 위치 저장
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector2 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); ;
        transform.position = currentPos;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (m_CCollsion.IsCollision == false)
        {
            transform.position = defaultposition;
            SCriptTxt = GetComponentInChildren<Text>();

            int nAnsNum = int.Parse(SCriptTxt.text);
            m_CCal.GenerateQuiz();

            // 아래 Code가 Warning 발생해서 임시 주석 처리해둠. 필요 없으면 삭제하기! - Hyeonwoo, 2021.11.26.
            // var c = new CCalculate();
            int nMid = CCalculate.MiddleNum;
            SCriptTxt.text = CCalculate.MiddleNum.ToString();
        }
        else
        {
            transform.position = invisdefaultposition;
            Invisiable.transform.position = defaultposition;

            m_CCollsion.IsCollision = false;
        }
    }
}
