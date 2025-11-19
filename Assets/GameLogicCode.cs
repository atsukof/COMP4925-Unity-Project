using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.Networking;
using System;


public class GameLogicCode : MonoBehaviour
{
    [Header("Input Fields")]
    [SerializeField] TextMeshProUGUI label;

    [Space(10)]
    [Header("Prefabs")]
    [SerializeField] GameObject face_prefab;

    [Space(10)]
    [Header("Variables")]
    [Range(0.1f, 20f)][SerializeField] float period = 1.0f;

    [Space(10)]
    [Header("Audio")]
    [SerializeField] AudioPlayer itemSound;

    [Header("Camera Follow")]
    [SerializeField] private Transform player;

    private Vector3 initPos;

    private void Awake()
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        label.text = "Hi!";
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
