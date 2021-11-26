using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CCalculate : MonoBehaviour
{
    #region Cal_Text
    public Text Txt_FinNum;
    public Text Txt_MidNum;
    public Text Txt_AnsNum;
    #endregion

    float [] likeability = new float[10];     //호감도 배열
    int HavetoAns;                          //맞춰야할 수
    float nLikestandard = 50.0f;        //기준 호감도

    #region Quiz_Num(static)
    public static int MiddleNum=0;                          //문제의 수
    public static int FinalNum=0;
    public static int AnsNum=0;                               //맞춰야 할 수
    #endregion

  
    private void Awake()
    {
        GenerateQuiz();
        DontDestroyOnLoad(this);
        try
        {
            Debug.Log(nLikestandard.ToString());
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
                fin = FinalNum * MiddleNum;
                ans = Calculate(Operation.MULTIPLY, MiddleNum, FinalNum);
                break;
            case Type.NORMAL:
                ans = Calculate(Operation.DIVISION, MiddleNum, FinalNum);
                break;
            case Type.HARD:
                ans = Calculate(Operation.DIVISION, MiddleNum, FinalNum);
                break;
            case Type.HARDEST:
                ans = Calculate(Operation.DIVISION, MiddleNum, FinalNum);
                break;
        }
        AnsNum = ans;
        Txt_FinNum.text = fin.ToString();
        Txt_AnsNum.text = ans.ToString();
    }

    public void GenerateQuiz()
    {
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
        Debug.Log(Multiplication_tableCnt.ToString());
        if (Multiplication_tableCnt > 6)              // 호감도 기준치 이하 로봇 4개 이하일경우,
        {
            nMid = Range(2, 9);               // 2~9단까지 모두 출력
        }
        else if (Multiplication_tableCnt == 0)     //모두 기준치 이상 호감도를 넘겼을 경우,
        {
            nLikestandard += 10.0f;                //기준 호감도 +10
            if (nLikestandard == 110.0f)          //호감도가 모두 100퍼센트일경우,
                CreateNewRobot();               //새로운 로봇 생성
            nMid = Range(2, 9);
            GenerateQuiz();
        }
        else
        {
            nMid = Range(0, Multiplication_tableCnt - 1);
            nMid = Multiplication_table[nMid];      //기준 호감도 이하인 문제 출제
        }
        SelectCurSel(nMid);
        Debug.Log(nMid+"단출력");
        Txt_MidNum.text = nMid.ToString();
    }

    void CreateNewRobot()
    {

    }
}
