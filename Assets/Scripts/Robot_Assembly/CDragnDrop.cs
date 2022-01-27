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
    static public int nStage=0;
    public bool IsCorrect = true;

    private Vector3 invisdefaultposition;
    private Vector3 defaultposition;

    public GameObject Invisiable;
    Text SCriptTxt;

    CollisionEvent m_CCollsion;

    [SerializeField]
    CCalculate     m_CCal;

    public Transform PopUp;
    public Text PopUp_txt;
    private void Awake()
    {
        m_CCollsion = GetComponent<CollisionEvent>();
        m_CCal = CCalculate.Instance;
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
        Vector2 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = currentPos;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (CollisionEvent.IsCollision == false)
        {
            transform.position = defaultposition;
        }
        else
        {
            CollisionEvent.IsCollision = false;

            transform.position = invisdefaultposition;
            //Invisiable.transform.position = defaultposition;


            int nAnsNum = CCalculate.AnsNum;
            SCriptTxt = GetComponentInChildren<Text>();
            int DragNum = int.Parse(SCriptTxt.text);

            Debug.Log(nAnsNum);
            Debug.Log(DragNum);

            if (nAnsNum == DragNum)
            {
                ++nStage;
                Invoke("waiting3second", 3.0f);
                transform.position = defaultposition;
                m_CCal.GenerateQuiz();
                IsCorrect = true;
            }
            else
            {
                nStage = 0;
                m_CCal.GenerateQuiz();
                PopUp_txt.text = "게임 오버";
                PopUp.gameObject.SetActive(true);
                transform.position = defaultposition;
                IsCorrect = false;
            }
        }
    }

}
