using System;
using System.Collections.Generic;

// 원칙적으로 Class의 멤버 변수는 private 접근자로 은닉화 시키는 게 맞으나,
// Network 송수신용으로 사용할 구조체 형식 Class이므로 예외적으로 public으로 선언. - Hyeonwoo, 2022.01.15.

#region Woongjin RequestData

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

#region Woongjin ResponseData

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

#region Local DB Data

[Serializable]
public class DBInitData     // 비식별전처리 인입데이터
{
    public string mbr_id;   // 회원 ID (회원 가입시 생성, 일반적으로 회원마다 1개씩 부여)
    public string sid;      // 이력 ID (회원 학습 정보 테이블과 join 하여 정보 가져올 수 있는 key 값)
    public string bgn_dt;   // 학습진입시간 (YYYY-MM-DD (KST 사용), 해당 학습 회차에서 첫 번째 문제가 출제된 시점)
    public string stg_cd;   // 소주제 코드 (학습에서 현재 진행중인 소주제)
    public string thma_nm;  // 주제명 (소주제가 포함된 주제 이름)
    public string step_nm;  // 단계명 (소주제가 포함된 단계 이름)
    public string stg_nm;   // 소주제명 (소주제 이름)
    public string ctl_cn;   // 소주제 세부 사항 (소주제에 대한 세부 설명)
    public int try_no;      // 학습 시도 횟수 (소주제 별로 학습을 시도한 횟수, 종료 여부와 관계없이 진입 횟수에 따라 증가함)
    public string lrn_prgs_sts_cd;  // 학습 성취 (LPSC01: 노력, LPSC02: 기본, LPSC03: 충분, LPSC04: 훌륭)
}

[Serializable]
public class DBInitDataList
{
    public List<DBInitData> initDatas;
}

[Serializable]
public class DBStudyData    // 비식별전처리 학습데이터
{
    public string mbr_id;   // 회원 ID (회원 가입시 생성, 일반적으로 회원마다 1개씩 부여 (= Irnmbrid)
    public string sid;      // 이력 ID (회원 학습 정보 테이블과 join 하여 정보 가져올 수 있는 key 값
    public string row_dt;   // 이력 발생시간 (YYYY-MM-DD (KST 사용), 문제를 출제한 시점)
    public string stg_cd;   // 소주제 코드 (학습에서 현재 진행중인 소주제)
    public int qst_no;      // 제공 문제 번호 (한 회차 내에서 회원이 풀고 있는 문제 번호)
    public string qst_cd;   // 문항 코드 (출제된 문항코드)
    public string text_cn;  // 지시문 (문제에 대한 지시문)
    public string qst_cn;   // 문제 (매쓰피드에서 제공되는 문제, latex 형태로 되어 있음)
    public string qst_cransr;   // 정답 (문제에 대한 정답, 정답이 여러 개일 경우 세미콜론(;)으로 구분)
    public string qst_cransr_yn;    // 정오답여부 (Y : 정답, N : 오답)
    public int qst_expl_scond;  // 문제 풀이 시간 (문제가 처음 노출되었을 때부터 제출될 때까지의 시간 (millisecond 단위))
}

[Serializable]
public class DBStudyDataList
{
    public List<DBStudyData> studyDatas;
}

#endregion

public enum WoongjinAPIType
{
    getStudyInfo,
    getAllStudyInfo,
    getStudyResult
}