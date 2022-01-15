using System;
using System.Collections.Generic;

// 원칙적으로 Class의 멤버 변수는 private 접근자로 은닉화 시키는 게 맞으나,
// Network 송수신용으로 사용할 구조체 형식 Class이므로 예외적으로 public으로 선언. - Hyeonwoo, 2022.01.15.

#region RequestData

[Serializable]
public class UserData
{
    public string gameKey = "dcbb2ad5bc4d3fe7d85fd4cd113984e7";  // 게임키
    public string mbr_id;   // Default 회원 ID (가상의 학습회원 ID)
    public string stg_cd;   // 학습주제코드 (가상의 학습이력 코드)
    public string sid;      // 이력코드 (소주제 코드)

    public UserData(string mbr_id, string stg_cd, string sid)
    {
        this.mbr_id = mbr_id;
        this.stg_cd = stg_cd;
        this.sid = sid;
    }
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