using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CCalculate : MonoBehaviour
{
    ChangeImage CChangeimg;
    public Text PopUp_txt;
    #region Cal_Text
    public Text Txt_FinNum;
    public Text Txt_MidNum;
    public Text[] Txt_AnsNum = new Text[3];
    int nAnsNumCnt = 1;
    public int nRealAns = 0;
    #endregion

    float [] likeability = new float[10];     //호감도 배열
    int HavetoAns;                                //맞춰야할 수
    float nLikestandard = 50.0f;             //기준 호감도

    #region Quiz_Num(static)
    public static int MiddleNum=0;                          //문제의 수
    public static int FinalNum=0;
    public static int AnsNum=0;                               //맞춰야 할 수
    #endregion

    int[] nArrnum = new int[3];

    private void Awake()
    {
        CChangeimg = GetComponent<ChangeImage>();
        GenerateQuiz();
        
        // 아래 Code는 최상위 Object가 아니면 작동을 안 해서 주석 처리함. - Hyeonwoo, 2021.11.26.
        // DontDestroyOnLoad(this);
        try
        {
            // Debugging 끝난 Code는 삭제하거나 주석 처리 해주기! - Hyeonwoo, 2021.11.26.
            // Debug.Log(nLikestandard.ToString());
        }
        catch (NullReferenceException ex)
        {
            Debug.Log("변수 할당 안됨.");
        }
    }
    enum Operation
    {
        MULTIPLY,
        DIVISION
    }
    enum Type
    {
        EASY,
        NORMAL,
        HARD,
        HARDEST
    }
    Type DecideType(int num)
    {
        if (likeability[num] < 25) return Type.EASY;
        else if (likeability[num] < 50) return Type.NORMAL;
        else if (likeability[num] < 75) return Type.HARD;
        else if (likeability[num] < 95) return Type.HARDEST;
        return Type.EASY;
    }

    int Range(int nMin, int nMax)
    {
        int Num = UnityEngine.Random.Range(nMin, nMax);
        return Num;
    }

    int Calculate(Operation oper, int a, int b)
    {
        int Num = 0;
        switch (oper)
        {
            case Operation.MULTIPLY:
                Num = a * b;
                break;
            case Operation.DIVISION:
                Num = b/ a;
                break;
        }
        return Num;
    }

    void SelectCurSel(int num)         //호감도에 따른 문제 출제 유형 결정
    {
        MiddleNum = num;

        int fin = Range(2, 9);
        FinalNum = fin;

        int ans = 0;
        switch (DecideType(num))
        {
            case Type.EASY:
                ans = Calculate(Operation.MULTIPLY, MiddleNum, FinalNum);
                break;
            case Type.NORMAL:
                FinalNum = FinalNum * MiddleNum;
                ans = Calculate(Operation.DIVISION, MiddleNum, FinalNum);
                break;
            case Type.HARD:
                FinalNum = FinalNum * MiddleNum;
                ans = Calculate(Operation.DIVISION, MiddleNum, FinalNum);
                break;
            case Type.HARDEST:
                FinalNum = FinalNum * MiddleNum;
                ans = Calculate(Operation.DIVISION, MiddleNum, FinalNum);
                break;
        }
        nRealAns = AnsNum = ans;
        Txt_FinNum.text = FinalNum.ToString();
        nAnsNumCnt = Range(0, 2);
        Txt_AnsNum[nAnsNumCnt].text = ans.ToString();

        int min = 2;
        int max = 9;
        if (ans == 2) min = 3;
        else if (ans == 9) max = 8;

        int Spare1 = 0;
        int Spare2 = 0;

        do
        {
            Spare1 = Range(min, max);
            Spare2 = Range(min, max);
        } while ((Spare1 == Spare2) | (Spare1 == ans) | (Spare2 == ans));

        if (nAnsNumCnt > 1)
        {
            Txt_AnsNum[0].text = Spare1.ToString();
            Txt_AnsNum[1].text = Spare2.ToString();
        }
        else
        {
            Txt_AnsNum[2].text = Spare2.ToString();
            int a = (nAnsNumCnt == 1) ? (0) : (1);
            Txt_AnsNum[a].text = Spare1.ToString();
        }
    }

    public void GenerateQuiz()
    {
        CChangeimg.ChangeImg();
        PopUp_txt.text = "일시정지";
        for (int n = 0; n < 3; n++)
            nArrnum[n] = 1;//정규화

        int nMid;
        int Multiplication_tableCnt = 0;

        int[] Multiplication_table = new int[10];  // 출제할 구구단 배열의 동적 할당

        // 호감도 정해주는 코드가 없어서 임시 할당
        for (int i = 2; i < 10; i++)
        {
            likeability[i] = 55.0f;    //전체 구구단 호감도 55로 지정
        }
        //

        for (int i = 2; i < 10; i++)
        {
            if (likeability[i] < nLikestandard)
            {
                Multiplication_table[i] = i;
                Multiplication_tableCnt++;
            }
            else continue;
        }
        // Debugging 끝난 Code는 삭제하거나 주석 처리 해주기! - Hyeonwoo, 2021.11.26.
        /*Debug.Log(Multiplication_tableCnt.ToString());*/
        if (Multiplication_tableCnt > 6)              // 호감도 기준치 이하 로봇 4개 이하일경우,
        {
            nMid = Range(2, 9);               // 2~9단까지 모두 출력
        }
        else if (Multiplication_tableCnt == 0)     //모두 기준치 이상 호감도를 넘겼을 경우,
        {
            nLikestandard += 10.0f;                //기준 호감도 +10
            nMid = Range(2, 9);
            if (nLikestandard == 110.0f)
            {
                CreateNewRobot();               //새로운 로봇 생성
            }//호감도가 모두 100퍼센트일경우,
            GenerateQuiz();
        }
        else
        {
            nMid = Range(0, Multiplication_tableCnt - 1);
            nMid = Multiplication_table[nMid];      //기준 호감도 이하인 문제 출제
        }
        SelectCurSel(nMid);
        Txt_MidNum.text = nMid.ToString();
    }

    void CreateNewRobot()
    {

    }
}
