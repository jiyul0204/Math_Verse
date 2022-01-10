using System;
using System.Collections.Generic;
using UnityEngine;

#region RequestData

[Serializable]
public class UserData
{
    public string gameKey = "dcbb2ad5bc4d3fe7d85fd4cd113984e7";  // 게임키
    public string mbr_id;   // Default 회원 ID (가상의 학습회원 ID)
    public string stg_cd;   // 학습주제코드 (가상의 학습이력 코드)
    public string sid;      // 이력코드 (소주제 코드)
}

#endregion

#region ResponseData

[Serializable]
public class ResponseData<T>
{
    public int status;
    public string message;
    public List<T> data;
}

[Serializable]
public class StudyInfoData
{
    public string sid;      // 학습이력코드
    public string thma_nm;  // 대주제명 (소주제가 포함된 주제 이름)
    public string step_nm;  // 단계명 (소주제가 포함된 단계 이름)
    public string stg_cd;   // 학습주제코드
    public string stg_nm;   // 학습주제명 (소주제 이름)
    public string dtl_cn;   // 세부학습명 (소주제에 대한 세부 설명)
    public string lrn_prgs_sts_cd;  // 학습성취도정보 (학습성취도)
}

[Serializable]
public class StudyResultData
{
    public string qst_no;   // 문제번호 (문항번호)
    public string qst_cd;   // 문제코드 (문항코드)
    public string qst_cransr_yn;    // 문제에 대한 정오답 여부
    public string qst_expl_scond;   // 문제 푼 시간 (밀리세컨드)
    public string row_dt;   // 학습이력시간 (학습완료일자)
}

#endregion

public enum WoongjinAPIType
{
    getStudyInfo,
    getAllStudyInfo,
    getStudyResult
}

public class WoongjinAPIService : Singleton<WoongjinAPIService>
{
    public List<StudyInfoData> studyInfoDatas { get; set; }
    public List<StudyResultData> studyResultDatas { get; set; }

    private const string API_URL_GET_STUDY_INFO = "https://api.wjtbgame.com/getStudyInfo";
    private const string API_URL_GET_ALL_STUDY_INFO = "https://api.wjtbgame.com/getAllStudyInfo";
    private const string API_URL_GET_STUDY_RESULT = "https://api.wjtbgame.com/getStudyResult";

    private void Start()
    {
        RequestAPI(WoongjinAPIType.getStudyResult);
    }

    public void RequestAPI(WoongjinAPIType woongjinAPIType)
    {
        var userData = new UserData();

        userData.mbr_id = "MPID0120060011959";
        userData.stg_cd = "F02_0_1";
        userData.sid = "mathpd28500398652414182";

        string jsonValue = JsonUtility.ToJson(userData);

        switch (woongjinAPIType)
        {
            case WoongjinAPIType.getStudyInfo:
                NetworkService.Instance.RequestJSONData<StudyInfoData>(jsonValue, API_URL_GET_STUDY_INFO, null);
                break;
            case WoongjinAPIType.getAllStudyInfo:
                NetworkService.Instance.RequestJSONData<StudyInfoData>(jsonValue, API_URL_GET_ALL_STUDY_INFO, null);
                break;
            case WoongjinAPIType.getStudyResult:
                NetworkService.Instance.RequestJSONData<StudyResultData>(jsonValue, API_URL_GET_STUDY_RESULT, HandleOnGetStudyResult);
                break;
        }
    }

    private void HandleOnGetStudyResult(List<StudyResultData> responseData)
    {
        studyResultDatas = responseData;

        // TODO: <이지율> 아래 Code는 Debugging용 Code이므로 사용 방법 확인 후 지울 것. - Hyeonwoo, 2022.01.11.
        foreach (StudyResultData resultData in studyResultDatas)
        {
            Debug.Log($"[KHW] qst_no : {resultData.qst_no}");
            Debug.Log($"[KHW] qst_cd : {resultData.qst_cd}");
            Debug.Log($"[KHW] qst_cransr_yn : {resultData.qst_cransr_yn}");
            Debug.Log($"[KHW] qst_expl_scond : {resultData.qst_expl_scond}");
            Debug.Log($"[KHW] row_dt : {resultData.row_dt}");
        }
    }
}
