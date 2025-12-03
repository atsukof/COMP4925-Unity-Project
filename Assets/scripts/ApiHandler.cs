using System;
using System.Collections;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class ApiHandler : MonoBehaviour
{
    private string API_BASE_URL = "https://unity-project-backend.onrender.com";

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
        var request = UnityWebRequest.Get(API_BASE_URL + $"/fastest-time?level_id={levelId}");

        yield return SendAuthenticatedRequest(request, (res) =>
        {
            ApiResponse<FastestTimeData> response =
                JsonUtility.FromJson<ApiResponse<FastestTimeData>>(res.downloadHandler.text);

            if (response.ok)
                onResult?.Invoke(response.data);
            else
                onResult?.Invoke(null);
        });
    }

    public IEnumerator getTotalScore(Action<TotalScoreData> onResult)
    {
        UnityWebRequest request = UnityWebRequest.Get(API_BASE_URL + $"/retrieve-total-score");
        // ADD JWT HEADER
        request.SetRequestHeader("Authorization", "Bearer " + AuthManager.AccessToken);
        
        yield return SendAuthenticatedRequest(request, (res) =>
        {
            ApiResponse<TotalScoreData> response =
                JsonUtility.FromJson<ApiResponse<TotalScoreData>>(res.downloadHandler.text);

            if (response.ok)
                onResult?.Invoke(response.data);
            else
                onResult?.Invoke(null);
        });
    }
    
    public IEnumerator getLevelCompleted(Action<LevelCompleteData> onResult)
    {
        var request = UnityWebRequest.Get(API_BASE_URL + "/retrieve-level-completed");

        yield return SendAuthenticatedRequest(request, (res) =>
        {
            ApiResponse<LevelCompleteData> response =
                JsonUtility.FromJson<ApiResponse<LevelCompleteData>>(res.downloadHandler.text);

            if (response.ok)
                onResult?.Invoke(response.data);
            else
                onResult?.Invoke(null);
        });
    }
    
    private IEnumerator SendAuthenticatedRequest(UnityWebRequest request, Action<UnityWebRequest> onDone)
    {
        // 1. Add access token
        request.SetRequestHeader("Authorization", "Bearer " + AuthManager.AccessToken);

        // 2. Send request
        yield return request.SendWebRequest();

        // 3. If unauthorized → attempt refresh
        if (request.responseCode == 401)
        {
            Debug.LogWarning("Access expired → trying refresh token...");

            bool refreshed = false;
            yield return AuthManager.RefreshAccessToken(success => refreshed = success);

            if (!refreshed)
            {
                Debug.LogError("Refresh failed → user logged out");
                onDone?.Invoke(request);
                SceneManager.LoadScene("Scenes/Login");
                yield break;
            }

            // 4. Retry the request with NEW access token
            UnityWebRequest retry = UnityWebRequest.Get(request.url);
            retry.SetRequestHeader("Authorization", "Bearer " + AuthManager.AccessToken);

            yield return retry.SendWebRequest();

            onDone?.Invoke(retry);
            yield break;
        }

        // Normal result
        onDone?.Invoke(request);
    }

    
    public IEnumerator SendRequest(UnityWebRequest req, Action<string> callback)
    {
        req.SetRequestHeader("Authorization", "Bearer " + AuthManager.AccessToken);

        yield return req.SendWebRequest();

        // If Access Token expired → try refresh
        if (req.responseCode == 401)
        {
            bool refreshed = false;
            yield return AuthManager.RefreshAccessToken(result => refreshed = result);

            if (refreshed)
            {
                req.SetRequestHeader("Authorization", "Bearer " + AuthManager.AccessToken);
                yield return req.SendWebRequest();
            }
            else
            {
                callback("ERROR: SESSION EXPIRED");
                SceneManager.LoadScene("Scenes/Login");
                yield break;
            }
        }

        callback(req.downloadHandler.text);
    }
}
