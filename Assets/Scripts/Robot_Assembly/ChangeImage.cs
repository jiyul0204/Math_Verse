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
        AddPath = "Robot\\RobotA\\AddBody\\";
        AnsPath = "Robot\\RobotA\\RobotBody\\";
    }
    private void Start()
    {
        ChangeImg();

    }
    public void ChangeImg()
    {
        int nStage = CDragnDrop.nStage;
        switch (CDragnDrop.nStage)
        {
            case 0:
                MidSpt = Resources.Load(AddPath + "AddStart", typeof(Sprite)) as Sprite;
                FinSpt = Resources.Load(AddPath + "AddEyes1", typeof(Sprite)) as Sprite;
                AnsSpt = Resources.Load(AnsPath + "Eyes", typeof(Sprite)) as Sprite;
                break;
            case 1:
                MidSpt = Resources.Load(AddPath + "AddEyes1", typeof(Sprite)) as Sprite;
                FinSpt = Resources.Load(AddPath + "AddEars2", typeof(Sprite)) as Sprite;
                AnsSpt = Resources.Load(AnsPath + "Ears", typeof(Sprite)) as Sprite;
                break;
            case 2:
                MidSpt = Resources.Load(AddPath + "AddEars2", typeof(Sprite)) as Sprite;
                FinSpt = Resources.Load(AddPath + "AddUpperBody3", typeof(Sprite)) as Sprite;
                AnsSpt = Resources.Load(AnsPath + "UpperBody", typeof(Sprite)) as Sprite;
                break;
            case 3:
                MidSpt = Resources.Load(AddPath + "AddUpperBody3", typeof(Sprite)) as Sprite;
                FinSpt = Resources.Load(AddPath + "AddAccesary4", typeof(Sprite)) as Sprite;
                AnsSpt = Resources.Load(AnsPath + "Accessary", typeof(Sprite)) as Sprite;
                break;
            case 4:
                MidSpt = Resources.Load(AddPath + "AddAccesary4", typeof(Sprite)) as Sprite;
                FinSpt = Resources.Load(AddPath + "AddArms5", typeof(Sprite)) as Sprite;
                AnsSpt = Resources.Load(AnsPath + "Arms", typeof(Sprite)) as Sprite;
                break;
            case 5:
                MidSpt = Resources.Load(AddPath + "AddArms5", typeof(Sprite)) as Sprite;
                FinSpt = Resources.Load(AddPath + "AddLowerBody6", typeof(Sprite)) as Sprite;
                AnsSpt = Resources.Load(AnsPath + "LowerBody", typeof(Sprite)) as Sprite;
                break;
            case 6:
                MidSpt = Resources.Load(AddPath + "AddLowerBody6", typeof(Sprite)) as Sprite;
                FinSpt = Resources.Load(AddPath + "AddLegs7", typeof(Sprite)) as Sprite;
                AnsSpt = Resources.Load(AnsPath + "Leg", typeof(Sprite)) as Sprite;
                break;
        }

        MidImg.GetComponent<Image>().sprite = MidSpt;
        FinImg.GetComponent<Image>().sprite = FinSpt;
        AnsImg1.GetComponent<Image>().sprite = AnsSpt;
        AnsImg2.GetComponent<Image>().sprite = AnsSpt;
        AnsImg3.GetComponent<Image>().sprite = AnsSpt;
    }


}
