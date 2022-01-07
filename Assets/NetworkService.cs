using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

[SerializeField]
public class UserData
{
    public string gameKey = "dcbb2ad5bc4d3fe7d85fd4cd113984e7";  // 게임키
    public string mbr_id;   // Default 회원 ID
    public string stg_cd;   // 학습주제 코드
    public string sid;      // 이력코드
}


public class NetworkService : MonoBehaviour
{
    private void Start()
    {
        UserData userData = new UserData();

        userData.mbr_id = "MPID0120060011959";
        userData.stg_cd = "F02_0_1";
        userData.sid = "mathpd28500398652414182";

        string jsonString = JsonUtility.ToJson(userData);

        Debug.Log($"[KHW] jsonString : {jsonString}");

        StartCoroutine(SendRequestJson(jsonString));
    }

    private IEnumerator SendRequestJson(string jsonString)
    {
        var www = UnityWebRequest.Post("https://api.wjtbgame.com/getStudyResult", jsonString);
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            Debug.Log($"* Result : {www.downloadHandler.text}");
        }
    }
}
