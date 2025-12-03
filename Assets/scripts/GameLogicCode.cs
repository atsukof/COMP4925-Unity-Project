using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.Networking;
using System;
using UnityEngine.SceneManagement;


public class GameLogicCode : MonoBehaviour
{
    [Header("Camera Follow")]
    [SerializeField] private Transform player;

    private Vector3 initPos;
    
    [Space(10)]
    [Header("Pause Elements")]
    public GameObject pauseWindow;
    public GameObject playerObj;

    public bool paused = false;
    public GoalLine goalLine;
    public Spawner spawner;
    public SpawnerArcRandom arcRandom;
    
    private void Awake()
    {

    }
    
    public void pause()
    {
        pauseWindow.SetActive(true);
        playerObj.SetActive(false);
        paused = true;
        goalLine.Pause();
        if (spawner != null) spawner.Pause();
        if (arcRandom != null) arcRandom.Pause();
    }

    public void resume()
    {
        pauseWindow.SetActive(false);
        playerObj.SetActive(true);
        paused = false;
        goalLine.Resume();
        if (spawner != null) spawner.Resume();
        if (arcRandom != null) arcRandom.Resume();
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Scenes/Main_menu");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initPos = transform.position;
        pauseWindow.SetActive(false);
        playerObj.SetActive(true);
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();

    }

    public void PressedButton()
    {

    }

    private void FollowPlayer()
    {
        if (player == null) return;

        // follow player's x
        float x = Mathf.Clamp(player.position.x, initPos.x, Mathf.Infinity);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

}
