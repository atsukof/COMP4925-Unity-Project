using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Obstacle : MonoBehaviour
{
    private bool isScheduledToDestroy = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Hit Player
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Hit: " + collision.collider.name);

            GameManager.Instance.TakeDamage(1);
            Destroy(gameObject);   // immediately
            return;
        }

        // Hit Ground
        if (collision.collider.CompareTag("Floor"))
        {
            Debug.Log("Hit: " + collision.collider.name);

            if (!isScheduledToDestroy)
            {
                isScheduledToDestroy = true;
                Destroy(gameObject, 1f);  // destroy after 1 second
            }
        }
    }
}