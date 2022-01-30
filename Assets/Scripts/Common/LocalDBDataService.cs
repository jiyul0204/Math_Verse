using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public enum GameType
{
    Assemble_Robot,
    Satellite
}

public enum SoundType
{
    main_bgm,   // 메인 화면, 카드 컬렉션 기본 BGM
    main_button_touch,  // 시작, 컬렉션 버튼, 설정, 부모님 계정 확인 버튼 터치 시 출력되는 효과음
    collection_card_touch,  // 획득한 카드 터치 시 출력 사운드
    collection_check_box,   // 게임 종류별 수집한 카드를 확인하는 체크 박스 터치 시 출력되는 효과음
    store_bgm,  // 장난감 가게의 메인 BGM
    store_door_touch,   // 가게 문 버튼 터치 시 출력되는 효과음
    store_enter_guest,  // 손님 진입 시 출력되는 효과음
    multiplication_new_question,    // 게임 시작 후 새로운 문제 생성 시 출력되는 효과음
    multiplication_parts_selection, // 부품 선택 시 출력 효과음
    multiplication_correct, // 정답을 맞춘 경우 출력되는 효과음
    multiplication_incorrect,   // 정답을 맞추지 못한 경우 출력되는 효과음
    multiplication_doll_complete,   // 모든 문제를 풀고 인형 완성 시 출력 효과음
    multiplication_get_doll,    // 난이도가 상승해 인형 획득 시 출력되는 효과음
    division_bgm,   // 인공위성 게임 메인 BGM
    division_enter, // 인공위성 게임 진입 시 출력되는 효과음
    division_correct,   // 정답인 위성을 드래그한 경우 출력되는 효과음
    division_incorrect  // 오답인 위성을 드래그한 경우 출력되는 효과음
}

public class StoreQuestData : IEquatable<StoreQuestData>
{
    public string stg_cd;   // 소주제 코드 (학습에서 현재 진행중인 소주제)
    public string stg_nm;   // 소주제명 (소주제 이름)
    public string dtl_cn;   // 소주제 세부 사항 (소주제에 대한 세부 설명)

    public StoreQuestData(string stg_cd, string stg_nm, string dtl_cn)
    {
        this.stg_cd = stg_cd;
        this.stg_nm = stg_nm;
        this.dtl_cn = dtl_cn;
    }

    // 참고 : https://docs.microsoft.com/ko-kr/dotnet/api/system.linq.enumerable.distinct?view=net-6.0
    public bool Equals(StoreQuestData other)
    {
        // Check whether the compared object is null.
        if (ReferenceEquals(other, null))
        {
            return false;
        }

        // Check whether the compared object references the same data.
        if (ReferenceEquals(this, other))
        {
            return true;
        }

        // Check whether the StoreQuestData's properties are equal.
        return stg_cd.Equals(other.stg_cd);
    }

    // If Equals() returns true for a pair of objects
    // then GetHashCode() must return the same value for these objects.
    public override int GetHashCode()
    {
        // Get hash code for the stg_cd field if it is not null.
        int hashProductName = stg_cd == null ? 0 : stg_cd.GetHashCode();

        // Calculate the hash stg_cd for the StoreQuestData.
        return hashProductName;
    }
}

public class LocalDBDataService : Singleton<LocalDBDataService>
{
    #region Common

    public GameType PlayGameType { get; set; }

    #endregion

    #region Store

    private const string FILE_NAME_STUDY_INFO_DATA = "DBInitDataList";

    private int currentMultiplicationNameIndex;
    private List<StoreQuestData> storeQuestMultiplicationNameList;

    private int currentDivisionNameIndex;
    private List<StoreQuestData> storeQuestDivisionNameList;

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
        string questMultiplicationDataListCSV = ((TextAsset)Resources.Load($"CSV/{FILE_NAME_MULTIPLICATION_TEST}", typeof(TextAsset))).text;

        GameQuestMultiplicationList = new List<DBQuestMultiplicationData>(CSVParser.Deserialize<DBQuestMultiplicationData>(questMultiplicationDataListCSV));

        // 나눗셈 문제 Parsing 후 Caching
        string questDivisionDataListCSV = ((TextAsset)Resources.Load($"CSV/{FILE_NAME_DIVISION_TEST}", typeof(TextAsset))).text;

        GameQuestDivisionList = new List<DBQuestDivisionData>(CSVParser.Deserialize<DBQuestDivisionData>(questDivisionDataListCSV));

        string initDataListCSV = ((TextAsset)Resources.Load($"CSV/{FILE_NAME_STUDY_INFO_DATA}", typeof(TextAsset))).text;
        DBInitData[] initDatas = CSVParser.Deserialize<DBInitData>(initDataListCSV);

        // Store 곱셈 문제 유형 Parsing 후 Caching
        string[] multiplicationFilter = { "E" };
        storeQuestMultiplicationNameList = new List<StoreQuestData>((from initData in initDatas
                          where multiplicationFilter.Any(filter => initData.stg_cd.Contains(filter))
                          select new StoreQuestData(initData.stg_cd, initData.stg_nm, initData.dtl_cn)).Distinct());

        // Store 나눗셈 문제 유형 Parsing 후 Caching
        string[] divisionFilter = { "F" };
        storeQuestDivisionNameList = new List<StoreQuestData>((from initData in initDatas
                          where divisionFilter.Any(filter => initData.stg_cd.Contains(filter))
                          select new StoreQuestData(initData.stg_cd, initData.stg_nm, initData.dtl_cn)).Distinct());
    }

    public string GetCurrentQuestName()
    {
        switch (PlayGameType)
        {
            case GameType.Assemble_Robot:
                return $"{storeQuestMultiplicationNameList[currentMultiplicationNameIndex].stg_nm}\n({storeQuestMultiplicationNameList[currentMultiplicationNameIndex].dtl_cn})";
            case GameType.Satellite:
                return $"{storeQuestDivisionNameList[currentDivisionNameIndex].stg_nm}\n({storeQuestDivisionNameList[currentDivisionNameIndex].dtl_cn})";
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

    public DBQuestDivisionData GetRandomQuestDivisionData()
    {
        var query = new List<DBQuestDivisionData>(from divisionData in GameQuestDivisionList
                    where string.Equals(divisionData.stg_cd, storeQuestDivisionNameList[currentDivisionNameIndex].stg_cd)
                    select divisionData);

        return query[UnityEngine.Random.Range(0, query.Count)];
    }
}