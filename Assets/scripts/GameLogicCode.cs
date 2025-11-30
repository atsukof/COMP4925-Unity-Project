using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.Networking;
using System;


public class GameLogicCode : MonoBehaviour
{
    [Header("Camera Follow")]
    [SerializeField] private Transform player;

    private Vector3 initPos;

    private void Awake()
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initPos = transform.position;
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
