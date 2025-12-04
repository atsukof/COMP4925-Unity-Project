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
    [SerializeField] private TextMeshProUGUI feedbackText;

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

    public void NavigateLogin()
    {
        levelManager.LoadLogin();
    }

    public void registerUser()
    {
        
        string pw = password_input.text;
        
        if (!ValidatePassword(pw))
        {
            feedbackText.text =
                "Weak Password! Min. 10 Char and Add special characters!";
            return;
        }
        
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
            AuthManager.SaveTokens(response.data.userId, response.data.username, response.data.accessToken, response.data.refreshToken);

            levelManager.LoadMainMenu();
        }
        else
        {
            feedbackText.text = response.msg;
        }
    }
    
    private bool ValidatePassword(string pw)
    {
        bool lengthCheck = pw.Length >= 10;
        bool upperCheck = System.Text.RegularExpressions.Regex.IsMatch(pw, "[A-Z]");
        bool lowerCheck = System.Text.RegularExpressions.Regex.IsMatch(pw, "[a-z]");
        bool numberCheck = System.Text.RegularExpressions.Regex.IsMatch(pw, "[0-9]");
        bool symbolCheck = System.Text.RegularExpressions.Regex.IsMatch(pw, "[^A-Za-z0-9]");

        return lengthCheck && upperCheck && lowerCheck && numberCheck && symbolCheck;
    }
}
