using System.Diagnostics;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //GameOverUI ui;

    private void Start()
    {
        //ui = FindAnyObjectByType<GameOverUI>();
    }

    public void Die()
    {
        UnityEngine.Debug.Log("GAME OVER");

        //if (ui != null)
        //{
        //    ui.ShowGameOver();
        //}

        Time.timeScale = 0f;
    }
}
