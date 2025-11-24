using UnityEngine;

public class Item : MonoBehaviour
{
    private bool isScheduledToDestroy = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Hit Player
        if (collision.collider.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(1);
            Destroy(gameObject);
            return;
        }

        // Hit Ground
        if (collision.collider.CompareTag("Floor"))
        {
            if (!isScheduledToDestroy)
            {
                isScheduledToDestroy = true;
                Destroy(gameObject, 1f);
            }
        }
    }
}
