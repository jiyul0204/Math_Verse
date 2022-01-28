using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum GameType
{
    Assemble_Robot,
    Satellite
}

public class LocalDBDataService : Singleton<LocalDBDataService>
{
    #region Common

    public GameType PlayGameType { get; set; }

    #endregion

    #region Store

    private const string FILE_NAME_STUDY_INFO_DATA = "DBInitDataList";

    private int currentMultiplicationNameIndex;
    private List<(string, string)> storeQuestMultiplicationNameList; // stg_nm, dtl_cn

    private int currentDivisionNameIndex;
    private List<(string, string)> storeQuestDivisionNameList; // stg_nm, dtl_cn

    #endregion

    #region Game

    private const string FILE_NAME_MULTIPLICATION_TEST = "DBQuestMultiplicationDataList";
    private const string FILE_NAME_DIVISION_TEST = "DBQuestDivisionDataList";

    public List<DBQuestMultiplicationData> GameQuestMultiplicationList { get; private set; }
    public List<DBQuestDivisionData> GameQuestDivisionList { get; private set; }

    #endregion

    private void Start()
    {
        // 참고 : https://www.overflowarchives.com/unity/how-to-convert-excel-to-json-in-unity-c-sharpe-parse-excel-file-to-json/
        // 참고2 : https://blog.goldface.kr/83

        // 곱셈 문제 Parsing 후 Caching
        /*string questMultiplicationDataListCSV = File.ReadAllText($"{Application.persistentDataPath}/{FILE_NAME_MULTIPLICATION_TEST}");*/
        string questMultiplicationDataListCSV = ((TextAsset)Resources.Load($"CSV/{FILE_NAME_MULTIPLICATION_TEST}", typeof(TextAsset))).text;

        GameQuestMultiplicationList = new List<DBQuestMultiplicationData>(CSVParser.Deserialize<DBQuestMultiplicationData>(questMultiplicationDataListCSV));

        // 나눗셈 문제 Parsing 후 Caching
        /*string questDivisionDataListCSV = File.ReadAllText($"{Application.persistentDataPath}/{FILE_NAME_DIVISION_TEST}");*/
        string questDivisionDataListCSV = ((TextAsset)Resources.Load($"CSV/{FILE_NAME_DIVISION_TEST}", typeof(TextAsset))).text;

        GameQuestDivisionList = new List<DBQuestDivisionData>(CSVParser.Deserialize<DBQuestDivisionData>(questDivisionDataListCSV));

        /*string initDataListCSV = File.ReadAllText($"{Application.persistentDataPath}/{FILE_NAME_STUDY_INFO_DATA}");*/
        string initDataListCSV = ((TextAsset)Resources.Load($"CSV/{FILE_NAME_STUDY_INFO_DATA}", typeof(TextAsset))).text;
        DBInitData[] initDatas = CSVParser.Deserialize<DBInitData>(initDataListCSV);

        // Store 곱셈 문제 유형 Parsing 후 Caching
        string[] multiplicationFilter = { "E" };
        storeQuestMultiplicationNameList = new List<(string, string)>((from initData in initDatas
                          where multiplicationFilter.Any(filter => initData.stg_cd.Contains(filter))
                          select (initData.stg_nm, initData.dtl_cn)).Distinct());

        // Store 나눗셈 문제 유형 Parsing 후 Caching
        string[] divisionFilter = { "F" };
        storeQuestDivisionNameList = new List<(string, string)>((from initData in initDatas
                          where divisionFilter.Any(filter => initData.stg_cd.Contains(filter))
                          select (initData.stg_nm, initData.dtl_cn)).Distinct());
    }

    public string GetCurrentQuestName()
    {
        switch (PlayGameType)
        {
            case GameType.Assemble_Robot:
                return $"{storeQuestMultiplicationNameList[currentMultiplicationNameIndex].Item1}\n({storeQuestMultiplicationNameList[currentMultiplicationNameIndex].Item2})";
            case GameType.Satellite:
                return $"{storeQuestDivisionNameList[currentDivisionNameIndex].Item1}\n({storeQuestDivisionNameList[currentDivisionNameIndex].Item2})";
            default:
                return string.Empty;
        }
    }

    public string GetPreviousQuestName()
    {
        switch (PlayGameType)
        {
            case GameType.Assemble_Robot:
                currentMultiplicationNameIndex = --currentMultiplicationNameIndex < 0
                    ? storeQuestMultiplicationNameList.Count - 1
                    : currentMultiplicationNameIndex;
                break;
            case GameType.Satellite:
                currentDivisionNameIndex = --currentDivisionNameIndex < 0
                    ? storeQuestDivisionNameList.Count - 1
                    : currentDivisionNameIndex;
                break;
        }

        return GetCurrentQuestName();
    }

    public string GetNextQuestName()
    {
        switch (PlayGameType)
        {
            case GameType.Assemble_Robot:
                currentMultiplicationNameIndex = ++currentMultiplicationNameIndex % storeQuestMultiplicationNameList.Count;
                break;
            case GameType.Satellite:
                currentDivisionNameIndex = ++currentDivisionNameIndex % storeQuestDivisionNameList.Count;
                break;
        }

        return GetCurrentQuestName();
    }
}