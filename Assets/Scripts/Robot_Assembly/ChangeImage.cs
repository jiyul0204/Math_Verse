using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
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

    private void Awake()
    {
        //여러개의 로봇이 생겼을 경우 Path를 랜덤으로 돌려준다 .
        AddPath = "Robot\\" + SelRobot() + "\\AddBody\\";
        AnsPath = "Robot\\"+SelRobot() +"\\RobotBody\\";
    }
    private void Start()
    {
        ChangeImg();
    }
    string SelRobot()
    {
        string strRobot;
        strRobot = "RobotA";
        return strRobot;
    }
    public void ChangeImg()
    {
        int nStage = CDragnDrop.nStage;
        switch (CDragnDrop.nStage)
        {
            case 0:
                MidSpt = Resources.Load(AddPath + "mugari", typeof(Sprite)) as Sprite;
                FinSpt = Resources.Load(AddPath + "a", typeof(Sprite)) as Sprite;
                AnsSpt = Resources.Load(AnsPath + "noon", typeof(Sprite)) as Sprite;
                break;
            case 1:
                MidSpt = Resources.Load(AddPath + "a", typeof(Sprite)) as Sprite;
                FinSpt = Resources.Load(AddPath + "b", typeof(Sprite)) as Sprite;
                AnsSpt = Resources.Load(AnsPath + "gye", typeof(Sprite)) as Sprite;
                break;
            case 2:
                MidSpt = Resources.Load(AddPath + "b", typeof(Sprite)) as Sprite;
                FinSpt = Resources.Load(AddPath + "c", typeof(Sprite)) as Sprite;
                AnsSpt = Resources.Load(AnsPath + "mom", typeof(Sprite)) as Sprite;
                break;
            case 3:
                MidSpt = Resources.Load(AddPath + "c", typeof(Sprite)) as Sprite;
                FinSpt = Resources.Load(AddPath + "d", typeof(Sprite)) as Sprite;
                AnsSpt = Resources.Load(AnsPath + "jangsik", typeof(Sprite)) as Sprite;
                break;
            case 4:
                MidSpt = Resources.Load(AddPath + "d", typeof(Sprite)) as Sprite;
                FinSpt = Resources.Load(AddPath + "e", typeof(Sprite)) as Sprite;
                AnsSpt = Resources.Load(AnsPath + "pal", typeof(Sprite)) as Sprite;
                break;
            case 5:
                MidSpt = Resources.Load(AddPath + "e", typeof(Sprite)) as Sprite;
                FinSpt = Resources.Load(AddPath + "f", typeof(Sprite)) as Sprite;
                AnsSpt = Resources.Load(AnsPath + "habansin", typeof(Sprite)) as Sprite;
                break;
            case 6:
                MidSpt = Resources.Load(AddPath + "f", typeof(Sprite)) as Sprite;
                FinSpt = Resources.Load(AddPath + "g", typeof(Sprite)) as Sprite;
                AnsSpt = Resources.Load(AnsPath + "dari", typeof(Sprite)) as Sprite;
                break;
        }

        MidImg.GetComponent<Image>().sprite = MidSpt;
        FinImg.GetComponent<Image>().sprite = FinSpt;
        AnsImg1.GetComponent<Image>().sprite = AnsSpt;
        AnsImg2.GetComponent<Image>().sprite = AnsSpt;
        AnsImg3.GetComponent<Image>().sprite = AnsSpt;
    }


}
