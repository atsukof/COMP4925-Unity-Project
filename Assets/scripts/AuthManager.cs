using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Networking;

public static class AuthManager
{
    public static string AccessToken { get; private set; }
    public static string RefreshToken { get; private set; }

    private const string ACCESS_KEY = "accessToken";
    private const string REFRESH_KEY = "refreshToken";
    
    private static string API_BASE = "http://localhost:3000";

    // Save tokens to PlayerPrefs
    public static void SaveTokens(string access, string refresh)
    {
        AccessToken = access;
        RefreshToken = refresh;

        PlayerPrefs.SetString(ACCESS_KEY, access);
        PlayerPrefs.SetString(REFRESH_KEY, refresh);
        PlayerPrefs.Save();
    }

    // Load tokens from PlayerPrefs on app start
    public static void LoadTokens()
    {
        AccessToken = PlayerPrefs.GetString(ACCESS_KEY, "");
        RefreshToken = PlayerPrefs.GetString(REFRESH_KEY, "");
    }

    // Clear tokens (logout)
    public static void Reset()
    {
        AccessToken = "";
        RefreshToken = "";
        PlayerPrefs.DeleteKey(ACCESS_KEY);
        PlayerPrefs.DeleteKey(REFRESH_KEY);
    }

    // Helper: Is user authenticated?
    public static bool HasSession()
    {
        return !string.IsNullOrEmpty(AccessToken);
    }
    
    // MAIN FUNCTION: Refreshes the token when expired
    public static IEnumerator RefreshAccessToken(System.Action<bool> callback)
    {
        if (string.IsNullOrEmpty(RefreshToken))
        {
            callback(false);
            yield break;
        }

        WWWForm form = new WWWForm();
        form.AddField("refreshToken", RefreshToken);

        UnityWebRequest req = UnityWebRequest.Post(API_BASE + "/refresh-token", form);
        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            ApiResponse<TokenData> res =
                JsonUtility.FromJson<ApiResponse<TokenData>>(req.downloadHandler.text);

            if (res.ok)
            {
                SaveTokens(res.data.accessToken, res.data.refreshToken);
                callback(true);
                yield break;
            }
        }

        // If refresh fails → force logout
        Reset();
        callback(false);
    }
}