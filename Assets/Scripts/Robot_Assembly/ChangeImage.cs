using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ChangeImage : MonoBehaviour
{
    System.Random random = new System.Random();
    public Image MidImg;
    public Image FinImg;
    public Image AnsImg1;
    public Image AnsImg2;
    public Image AnsImg3;

    Sprite MidSpt;
    Sprite FinSpt;
    Sprite AnsSpt;

    string Path;
    string AddPath;
    string AnsPath;

    string SetVersion()  //구구단 수 입력
    {
        return Convert.ToChar(random.Next(65, 69)).ToString();
    }

    private void Awake()
    {
        //여러개의 로봇이 생겼을 경우 Path를 랜덤으로 돌려준다 .
        string ForcePath = "Robot\\Robot" + SetVersion();
        AddPath = ForcePath + "\\AddBody\\Mid";
        AnsPath = ForcePath + "\\RobotBody\\Ans";
        Debug.Log(AddPath);
    }
    private void Start()
    {
        ChangeImg();
    }
    public void ChangeImg()
    {
        int nStage = CDragnDrop.nStage;
        int nAns = nStage + 1;
        MidSpt = Resources.Load(AddPath + nStage, typeof(Sprite)) as Sprite;
        AnsSpt = Resources.Load(AnsPath + nStage, typeof(Sprite)) as Sprite;
        FinSpt = Resources.Load(AddPath + nAns, typeof(Sprite)) as Sprite;
        //switch (CDragnDrop.nStage)
        //{
        //    case 0:
        //        MidSpt = Resources.Load(AddPath + "Mid0", typeof(Sprite)) as Sprite;
        //        FinSpt = Resources.Load(AddPath + "Mid1", typeof(Sprite)) as Sprite;
        //        break;
        //    case 1:
        //        MidSpt = Resources.Load(AddPath + "Mid1", typeof(Sprite)) as Sprite;
        //        FinSpt = Resources.Load(AddPath + "b", typeof(Sprite)) as Sprite;
        //        break;
        //    case 2:
        //        MidSpt = Resources.Load(AddPath + "b", typeof(Sprite)) as Sprite;
        //        FinSpt = Resources.Load(AddPath + "c", typeof(Sprite)) as Sprite;
        //        break;
        //    case 3:
        //        MidSpt = Resources.Load(AddPath + "c", typeof(Sprite)) as Sprite;
        //        FinSpt = Resources.Load(AddPath + "d", typeof(Sprite)) as Sprite;
        //        break;
        //    case 4:
        //        MidSpt = Resources.Load(AddPath + "d", typeof(Sprite)) as Sprite;
        //        FinSpt = Resources.Load(AddPath + "e", typeof(Sprite)) as Sprite;
        //        break;
        //    case 5:
        //        MidSpt = Resources.Load(AddPath + "e", typeof(Sprite)) as Sprite;
        //        FinSpt = Resources.Load(AddPath + "f", typeof(Sprite)) as Sprite;
        //        break;
        //    case 6:
        //        MidSpt = Resources.Load(AddPath + "f", typeof(Sprite)) as Sprite;
        //        FinSpt = Resources.Load(AddPath + "g", typeof(Sprite)) as Sprite;
        //        break;
        //    case 7:
        //        MidSpt = Resources.Load(AddPath + "f", typeof(Sprite)) as Sprite;
        //        FinSpt = Resources.Load(AddPath + "g", typeof(Sprite)) as Sprite;
        //        break;
        //}

        MidImg.GetComponent<Image>().sprite = MidSpt;
        FinImg.GetComponent<Image>().sprite = FinSpt;
        AnsImg1.GetComponent<Image>().sprite = AnsSpt;
        AnsImg2.GetComponent<Image>().sprite = AnsSpt;
        AnsImg3.GetComponent<Image>().sprite = AnsSpt;
    }


}
