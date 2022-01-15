using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkService : Singleton<NetworkService>
{
    public void RequestJSONData<T>(string json, string url, Action<List<T>> completeCallback = null)
    {
        StartCoroutine(SendRequestJson<T>(json, url, completeCallback));
    }

    private IEnumerator SendRequestJson<T>(string json, string url, Action<List<T>> completeCallback = null)
    {
        var www = new UnityWebRequest(url, "POST");

        www.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            var responseData = JsonUtility.FromJson<ResponseData<T>>(www.downloadHandler.text);

            completeCallback?.Invoke(responseData.data);
        }
    }
}
