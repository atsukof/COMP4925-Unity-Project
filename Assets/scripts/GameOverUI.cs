using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public void Retry()
    {
        // Restart from scene 0 (your first gameplay scene)
        SceneManager.LoadScene("Level 1");

        // reset score/life if needed
        GameManager.Instance.ResetGame();
    }
}
