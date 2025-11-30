using System;
using System.Collections;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class ApiHandler : MonoBehaviour
{
    private string API_BASE_URL = "http://localhost:3000";

    public static ApiHandler Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    void Update()
    {
        
    }
    
    public IEnumerator getFastestTime(int levelId, Action<FastestTimeData> onResult)
    {
        WWWForm userFastestTime = new WWWForm();
        userFastestTime.AddField("level_id", levelId);
        
        UnityWebRequest request = UnityWebRequest.Get(API_BASE_URL + $"/fastest-time?level_id={levelId}");
        
        // ADD JWT HEADER
        request.SetRequestHeader("Authorization", "Bearer " + AuthManager.AccessToken);
        
        yield return request.SendWebRequest();
        
        Debug.Log("Raw Json:" + request.downloadHandler.text);

        ApiResponse<FastestTimeData> response = JsonUtility.FromJson<ApiResponse<FastestTimeData>>(request.downloadHandler.text);
        Debug.Log(response.msg);

        if (response.ok)
        {
            onResult?.Invoke(response.data);  // <-- return value here
        }
        else
        {
            onResult?.Invoke(null); // or some error indicator
        }
    }
}
