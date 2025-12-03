using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserInfo : MonoBehaviour
{
    
    public int totalScore = 0;
    public int levelCompleted = 0;
    
    [SerializeField] private TextMeshProUGUI totalScoreText;
    [SerializeField] private TextMeshProUGUI levelCompletedText;
    
    public GameObject modalWindow;
    public GameObject player;
    
    public GameObject instructionWindow;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        setTotalScore();
        setLevelCompleted();
        modalWindow.SetActive(false);
        instructionWindow.SetActive(false);
        player.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        totalScoreText.text = "Total Score: " + totalScore;
        levelCompletedText.text = "Level: " + levelCompleted + " / 2";
    }

    public void OpenModalWindow()
    {
        modalWindow.SetActive(true);
        instructionWindow.SetActive(false);
        player.SetActive(false);
    }

    public void CloseModalWindow()
    {
        modalWindow.SetActive(false);
        player.SetActive(true);
    }

    public void OpenInstructionWindow()
    {
        instructionWindow.SetActive(true);
        player.SetActive(false);
        modalWindow.SetActive(false);
    }

    public void CloseInstructionWindow()
    {
        instructionWindow.SetActive(false);
        player.SetActive(true);
    }

    public void LoadStartGame()
    {
        if (levelCompleted == 0)
        {
            SceneManager.LoadScene("Scenes/Level 1");
        }
        else if (levelCompleted == 1)
        {
            SceneManager.LoadScene("Scenes/Level 2");
        }
        else
        {
            SceneManager.LoadScene("Scenes/Level 1");
        }
        

    }
    
    public void setTotalScore()
    {
        StartCoroutine(ApiHandler.Instance.getTotalScore(result =>
        {
            if (result == null)
            {
                Debug.Log("Could not load fastest time");
                totalScore = 0;
                return;
            }

            totalScore = result.totalScore;
            Debug.Log("Total Score = " + result.totalScore);
        }));
    }

    public void setLevelCompleted()
    {
        StartCoroutine(ApiHandler.Instance.getLevelCompleted(result =>
        {
            if (result == null)
            {
                Debug.Log("Could not load fastest time");
                levelCompleted = 0;
                return;
            }

            levelCompleted = result.levelCompleted;
            Debug.Log("Total Score = " + result.levelCompleted);
        }));
    }
}
