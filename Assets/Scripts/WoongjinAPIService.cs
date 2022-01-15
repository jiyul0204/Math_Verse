using System.Collections.Generic;
using UnityEngine;

public class WoongjinAPIService : Singleton<WoongjinAPIService>
{
    public List<StudyInfoData> studyInfoDatas { get; set; }
    public List<StudyResultData> studyResultDatas { get; set; }

    private const string API_URL_GET_STUDY_INFO = "https://api.wjtbgame.com/getStudyInfo";
    private const string API_URL_GET_ALL_STUDY_INFO = "https://api.wjtbgame.com/getAllStudyInfo";
    private const string API_URL_GET_STUDY_RESULT = "https://api.wjtbgame.com/getStudyResult";

    private void Start()
    {
        RequestAPI(WoongjinAPIType.getStudyInfo, "MPID0120060011959", "F02_0_1", "mathpd28500398652414182");
    }

    public void RequestAPI(WoongjinAPIType woongjinAPIType, string mbr_id, string stg_cd, string sid)
    {
        var userData = new UserData(mbr_id, stg_cd, sid);
        string jsonValue = JsonUtility.ToJson(userData);

        switch (woongjinAPIType)
        {
            case WoongjinAPIType.getStudyInfo:
                NetworkService.Instance.RequestJSONData<StudyInfoData>(jsonValue, API_URL_GET_STUDY_INFO, HandleOnGetStudyInfo);
                break;
            case WoongjinAPIType.getAllStudyInfo:
                NetworkService.Instance.RequestJSONData<StudyInfoData>(jsonValue, API_URL_GET_ALL_STUDY_INFO, HandleOnGetStudyInfo);
                break;
            case WoongjinAPIType.getStudyResult:
                NetworkService.Instance.RequestJSONData<StudyResultData>(jsonValue, API_URL_GET_STUDY_RESULT, HandleOnGetStudyResult);
                break;
        }
    }

    private void HandleOnGetStudyInfo(List<StudyInfoData> responseData)
    {
        studyInfoDatas = responseData;

        foreach (StudyInfoData resultData in studyInfoDatas)
        {
            Debug.Log($"[KHW] sid : {resultData.sid}");
            Debug.Log($"[KHW] thma_nm : {resultData.thma_nm}");
            Debug.Log($"[KHW] step_nm : {resultData.step_nm}");
            Debug.Log($"[KHW] stg_cd : {resultData.stg_cd}");
            Debug.Log($"[KHW] stg_nm : {resultData.stg_nm}");
            Debug.Log($"[KHW] dtl_cn : {resultData.dtl_cn}");
            Debug.Log($"[KHW] lrn_prgs_sts_cd : {resultData.lrn_prgs_sts_cd}");
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
