using System;
using System.Numerics;
using UnityEngine;

using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;


public class Spawner : MonoBehaviour
{
    public Transform player;
    public float xOffset = 4f;   // ahead of player

    public GameObject obstaclePrefab;
    public GameObject itemPrefab;

    public float spawnInterval = 1.5f;
    public float spawnRangeX = 4f;

    private float timer = 0f;
    
    public bool paused = false;

    public void Start()
    {
       paused = false;
    }

    public void Awake()
    {
        paused = false;
    }

    public void Pause()
    {
        paused = true;
    }

    public void Resume()
    {
        paused = false;
    }

    void Update()
    {
        if (!paused)
        {
            if (player != null)
            {
                Vector3 pos = transform.position;
                pos.x = player.position.x + xOffset;
                transform.position = pos;
            }

            timer += Time.deltaTime;

            if (timer >= spawnInterval)
            {
                SpawnRandomObject();
                timer = 0f;
            }
        }
        
    }

    void SpawnRandomObject()
    {
        
        bool spawnObstacle = (Random.value > 0.3f); // 70% obstacle, 30% item

        GameObject prefabToSpawn =
            spawnObstacle ? obstaclePrefab : itemPrefab;

        // Randomize spawn position
        Vector3 pos = new Vector3(
            transform.position.x + Random.Range(-spawnRangeX, spawnRangeX),
            transform.position.y,
            0
        );

        Instantiate(prefabToSpawn, pos, Quaternion.identity);
    }
}
