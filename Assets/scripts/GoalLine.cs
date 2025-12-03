using System.Collections;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

using Quaternion = UnityEngine.Quaternion;

public class GoalLine : MonoBehaviour
{
    private string API_BASE_URL = "https://unity-project-backend.onrender.com";
    
    public GameObject clearParticlePrefab;  // set in Inspector
    public float delay = 4f;
    private float timer = 0f;
    private bool isPaused = false;
    
    [Header("Input Fields")]
    [SerializeField] TextMeshProUGUI label;
    [SerializeField] TextMeshProUGUI timeLabel;
    [SerializeField] private int levelId;
    [SerializeField] private AudioClip goalSound;
    
    private GameManager gameManager;

    // private float startTime;
    private float levelDuration;

    public void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        // startTime = Time.time;
        gameManager.ResetGame();
        gameManager.setFastestTime( levelId);
    }

    void Update()
    {
        if (!isPaused)
        {
            timer += Time.deltaTime;  // counts only when not paused
        }
        
        timeLabel.text = $"Time: {timer.ToString("F2")} Seconds";
    }

    public void Pause()
    {
        isPaused = true;
    }

    public void Resume()
    {
        isPaused = false;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(HandleGoal(other));
    }
    
    private IEnumerator HandleGoal(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (goalSound != null)
                AudioSource.PlayClipAtPoint(goalSound, Camera.main.transform.position, 0.6f);

            if (clearParticlePrefab != null)
                Instantiate(clearParticlePrefab, other.transform.position, Quaternion.identity);

            levelDuration = timer;
            string formatted = timer.ToString("F2");
            
            string newFastestTime = "";

            if (gameManager.fastestTime == 0 || levelDuration < GameManager.Instance.fastestTime)
                newFastestTime = "\nNew time Record!";

            label.text = $"Congratulations!\nLevel Done!\nTime: {formatted} Seconds" + newFastestTime;

            // Fire API without blocking game flow
            StartCoroutine(RecordLevelAttempt(formatted));

            yield return new WaitForSeconds(0.7f);
            
            LoadNextScene();
        }
    }


    void LoadNextScene()
    {
        int next = SceneManager.GetActiveScene().buildIndex + 1;

        Debug.Log("I get here");
        // If next scene exists
        if (next < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("Next scene:" + SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(next);
        }
        else
        {
            // If no next scene, restart
            Debug.Log("Next scene not found");
            SceneManager.LoadScene("Scenes/Main_menu");
        }
    }
    
    private IEnumerator RecordLevelAttempt(string timeTaken)
    {
        WWWForm form = new WWWForm();
        form.AddField("user_id", PlayerPrefs.GetString("userId"));
        form.AddField("level_id", levelId);
        form.AddField("score", GameManager.Instance.score);
        form.AddField("remaining_lives", GameManager.Instance.life);
        form.AddField("timer", timeTaken);

        UnityWebRequest req = UnityWebRequest.Post(API_BASE_URL + "/record-level-attempt", form);

        yield return ApiHandler.Instance.SendRequest(req, (json) =>
        {
            Debug.Log("Response: " + json);

            ApiResponse<TokenData> res = JsonUtility.FromJson<ApiResponse<TokenData>>(json);
        
            if (!res.ok) Debug.LogWarning("Record failed");
        });
    }
}
