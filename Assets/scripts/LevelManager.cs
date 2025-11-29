using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Scenes/Main_menu");
    }
    
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void LoadStartLevel()
    {
        SceneManager.LoadScene("Scenes/Level 1");
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Scenes/Level 1");
    }

    public void LoadRegister()
    {
        SceneManager.LoadScene("Scenes/Register");    
    }

    public void LoadLogin()
    {
        SceneManager.LoadScene("Scenes/Login");
    }

    public void LoadQuit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        AuthManager.Reset();
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
