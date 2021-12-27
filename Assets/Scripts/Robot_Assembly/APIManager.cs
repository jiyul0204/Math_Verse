using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;

public class EducationInform
{
    public string ID;
    public string Date;
    public bool Correct;
    public int timeSec;
}

public class APIManager : MonoBehaviour
{
    public static string GetCurrentDate()
    {
        return DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss tt"));
    }

    EducationInform EduInform = new EducationInform();
    string strFilename = "Robot_GameData";

    string strDate;
    bool IsCorrect;
    int nTimeSec;

    void SaveData()
    {
        string json = JsonUtility.ToJson(EduInform);
        string strPath = Application.dataPath + "/" + strFilename + ".Json";
        File.WriteAllText(strPath, json);
    }

    public void GetData( bool IsCorrect, int nSec)
    {
        EduInform.ID = "ID";
        EduInform.Date = GetCurrentDate();
        EduInform.Correct = IsCorrect;
        EduInform.timeSec = nSec;
    }

    public void LoadData()
    {
        string strPath = Application.dataPath + "/" + strFilename + ".Json";
        string json = File.ReadAllText(strPath);
        EducationInform EduInform = JsonUtility.FromJson<EducationInform>(json);

        strDate = EduInform.Date;
        IsCorrect = EduInform.Correct;
        nTimeSec = EduInform.timeSec;
    }

    public string Date()
    {
        return strDate;
    }
    public bool Correct()
    {
        return IsCorrect;
    }

    public int TimeSec()
    {
        return nTimeSec;
    }
}
