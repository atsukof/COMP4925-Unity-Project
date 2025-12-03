using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoginLogic : MonoBehaviour
{
    [Header("Register System")]
    [SerializeField] TMP_InputField username_input;
    [SerializeField] TMP_InputField password_input;
    private LevelManager levelManager;
    
    private string API_BASE_URL = "https://unity-project-backend.onrender.com";

    public void Awake()
    {
        AuthManager.LoadTokens();
        levelManager = FindAnyObjectByType<LevelManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NavigateRegister()
    {
        levelManager.LoadRegister();
    }
    
    public void validateUser()
    {
        Debug.Log("Request sent!");
        StartCoroutine(ValidateRequest());
    }
    
    private IEnumerator ValidateRequest()
    {
        WWWForm userLoginForm = new WWWForm();
        userLoginForm.AddField("username", username_input.text);
        userLoginForm.AddField("password", password_input.text);
        
        UnityWebRequest request = UnityWebRequest.Post(API_BASE_URL + "/validate-user", userLoginForm);
        yield return request.SendWebRequest();

        ApiResponse<TokenData> response = JsonUtility.FromJson<ApiResponse<TokenData>>(request.downloadHandler.text);
        
        if (response.ok)
        {
            // Save tokens globally
            AuthManager.SaveTokens(response.data.userId, response.data.username, response.data.accessToken, response.data.refreshToken);
            
            levelManager.LoadMainMenu();
        }

    }
}
