using System.Collections;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

using Quaternion = UnityEngine.Quaternion;

public class GoalLine : MonoBehaviour
{
    private string API_BASE_URL = "http://localhost:3000";
    
    public GameObject clearParticlePrefab;  // set in Inspector
    public float delay = 4f;
    
    [Header("Input Fields")]
    [SerializeField] TextMeshProUGUI label;
    [SerializeField] TextMeshProUGUI timeLabel;
    [SerializeField] private int levelId;

    private float startTime;
    private float levelDuration;

    void Start()
    {
        startTime = Time.time;
        GameManager.Instance.ResetGame();
        GameManager.Instance.setFastestTime( levelId + 1);
    }

    void Update()
    {
        float timePassed = Time.time - startTime;
        string formatted = timePassed.ToString("F2");
        timeLabel.text = $"Time: {formatted} Seconds" ;
    }

    private IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            levelDuration = Time.time - startTime;
            string newFastestTime = "";
            if (GameManager.Instance.fastestTime == 0 || levelDuration < GameManager.Instance.fastestTime)
            {
                newFastestTime = $"New time Record!";
            }
            string formatted  = levelDuration.ToString("F2");
            label.text = $"Congratulations!\nLevel Done!\nTime: {formatted} Seconds" + newFastestTime;

            StartCoroutine(RecordLevelAttempt(formatted));
            
            
            // Spawn particle at player's position
            if (clearParticlePrefab != null)
            {
                Instantiate(clearParticlePrefab, other.transform.position, Quaternion.identity);
            }
            
            yield return new WaitForSeconds(0.4f);

            // Load next scene after a delay
            Invoke(nameof(LoadNextScene), delay);
        }
    }

    void LoadNextScene()
    {
        int next = SceneManager.GetActiveScene().buildIndex + 1;

        // If next scene exists
        if (next < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(next);
        }
        else
        {
            // If no next scene, restart
            SceneManager.LoadScene("Scenes/MainMenu");
        }
    }
    
    private IEnumerator RecordLevelAttempt(string timeTaken)
    {
        WWWForm userLevelInfo = new WWWForm();
        userLevelInfo.AddField("user_id", PlayerPrefs.GetString("userId"));
        userLevelInfo.AddField("level_id", levelId);
        userLevelInfo.AddField("score", GameManager.Instance.score);
        userLevelInfo.AddField("remaining_lives", GameManager.Instance.life);
        userLevelInfo.AddField("timer", timeTaken);
        
        UnityWebRequest request = UnityWebRequest.Post(API_BASE_URL + "/record-level-attempt", userLevelInfo);
        
        // ADD JWT HEADER
        request.SetRequestHeader("Authorization", "Bearer " + AuthManager.AccessToken);
        
        yield return request.SendWebRequest();
        
        Debug.Log("Raw Json:" + request.downloadHandler.text);

        ApiResponse<TokenData> response = JsonUtility.FromJson<ApiResponse<TokenData>>(request.downloadHandler.text);
        Debug.Log(response.msg);

        if (response.ok)
        {
            // TODO Game logic
        }
    }
}
