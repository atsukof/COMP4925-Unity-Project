using UnityEngine;

public class death_barrier : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Kill the player
            GameManager.Instance.TakeDamage(GameManager.Instance.life);
            return;
        }

        Destroy(collision.gameObject);
    }
}
