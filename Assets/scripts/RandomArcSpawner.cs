using UnityEngine;

public class SpawnerArcRandom : MonoBehaviour
{
    public Transform player;
    public float xOffset = 10f;

    public GameObject obstaclePrefab;
    public GameObject itemPrefab;

    public float spawnInterval = 1.5f;
    public float spawnRangeX = 3f;

    [Header("Arc Settings")]
    public float minLaunchSpeed = 5f;   // minimum initial speed of the arc
    public float maxLaunchSpeed = 12f;  // maximum initial speed of the arc

    public float minAngle = 20f;  // minimum launch angle in degrees
    public float maxAngle = 60f;  // maximum launch angle in degrees

    private float timer = 0f;

    void Update()
    {
        // Move the spawner ahead of the player
        if (player != null)
        {
            Vector3 pos = transform.position;
            pos.x = player.position.x + xOffset;
            transform.position = pos;
        }

        timer += Time.deltaTime;

        // Spawn at fixed intervals
        if (timer >= spawnInterval)
        {
            SpawnArcObject();
            timer = 0f;
        }
    }

    void SpawnArcObject()
    {
        // Randomly choose between obstacle or item
        bool spawnObstacle = (Random.value > 0.5f);
        GameObject prefab = spawnObstacle ? obstaclePrefab : itemPrefab;

        // Slightly randomize the spawn position
        Vector3 spawnPos = new Vector3(
            transform.position.x + Random.Range(-spawnRangeX, spawnRangeX),
            transform.position.y,
            0
        );

        // Instantiate the object
        GameObject obj = Instantiate(prefab, spawnPos, Quaternion.identity);

        // Make sure it has a Rigidbody2D
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = obj.AddComponent<Rigidbody2D>(); // add if missing
        }

        // ===== Arc calculation =====
        // 1. Random launch angle
        float angle = Random.Range(minAngle, maxAngle);
        float rad = angle * Mathf.Deg2Rad;  // convert degree to radian

        // Make the object fly from right to left
        Vector2 direction = new Vector2(-Mathf.Cos(rad), Mathf.Sin(rad));

        // 2. Random launch speed
        float launchSpeed = Random.Range(minLaunchSpeed, maxLaunchSpeed);

        // 3. Apply initial velocity
        rb.linearVelocity = direction * launchSpeed;

        // 4. Add random spin for more natural motion
        rb.angularVelocity = Random.Range(-180f, 180f);
    }
}
