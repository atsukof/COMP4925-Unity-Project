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
    private const string USER_ID = "userId";
    private const string USERNAME = "username";
    
    private static string API_BASE = "http://localhost:3000";

    // Save tokens to PlayerPrefs
    public static void SaveTokens(string userId, string username, string access, string refresh)
    {
        AccessToken = access;
        RefreshToken = refresh;

        PlayerPrefs.SetString(USER_ID, userId);
        PlayerPrefs.SetString(USERNAME, username);
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
    
    public static IEnumerator RefreshAccessToken(Action<bool> callback)
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
                // ⬅ Save NEW access + refresh tokens
                SaveTokens(
                    PlayerPrefs.GetString(USER_ID),
                    PlayerPrefs.GetString(USERNAME),
                    res.data.accessToken,
                    res.data.refreshToken
                );

                callback(true);
            }
        }

        Reset(); // force logout
        callback(false);
    }
}