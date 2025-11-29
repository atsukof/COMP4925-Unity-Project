using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RegisterLogic : MonoBehaviour
{
    [Header("Register System")]
    [SerializeField] TMP_InputField username_input;
    [SerializeField] TMP_InputField password_input;

    private LevelManager levelManager;
    private string API_BASE_URL = "http://localhost:3000";
    
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

    public void NavigateLogin()
    {
        levelManager.LoadLogin();
    }

    public void registerUser()
    {
        Debug.Log("Request sent!");
        StartCoroutine(Register());
    }

    private IEnumerator Register()
    {
        WWWForm userRegistrationForm = new WWWForm();
        userRegistrationForm.AddField("username", username_input.text);
        userRegistrationForm.AddField("password", password_input.text);
        UnityWebRequest request = UnityWebRequest.Post(API_BASE_URL + "/register", userRegistrationForm);
        yield return request.SendWebRequest();
        Debug.Log("Raw Json:" + request.downloadHandler.text);

        ApiResponse<TokenData> response = JsonUtility.FromJson<ApiResponse<TokenData>>(request.downloadHandler.text);
        Debug.Log(response.msg);

        if (response.ok)
        {
            // Save tokens globally
            AuthManager.SaveTokens(response.data.accessToken, response.data.refreshToken);
            
            levelManager.LoadMainMenu();
        }
    }
}
