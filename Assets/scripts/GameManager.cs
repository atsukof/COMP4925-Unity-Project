using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Debug = UnityEngine.Debug;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public AudioClip gameOverSound;
    private AudioSource audioSource;

    public int life = 3;
    public int score = 0;

    private void Awake()
    {
        // Singleton pattern
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

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        life = 3;
        score = 0;
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score);
    }

    public void TakeDamage(int amount)
    {
        life -= amount;
        Debug.Log("Life: " + life);

        if (life <= 0)
        {
            GoToGameOver();
        }
    }

    private void GoToGameOver()
    {
        // Play GameOver SE
        if (gameOverSound != null)
        {
            audioSource.PlayOneShot(gameOverSound);
        }
        // Delay a little before loading the scene to make sure the sound plays
        StartCoroutine(LoadGameOverScene());
    }

    private IEnumerator LoadGameOverScene()
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("GameOver");
    }

    public void ResetGame()
    {
        life = 3;
        score = 0;
    }

}
