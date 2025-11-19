using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [SerializeField] private AudioClip hitSound;
    private bool isDestroyed = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        HandleCollision(collision);
    }

    private void HandleCollision(Collision2D collision)
    {
        // Player
        if (collision.collider.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position);
            Destroy(gameObject, 1f);
            return;
        }

        // Floor
        if (collision.collider.CompareTag("Floor"))
        {
            Destroy(gameObject, 1f);
        }
    }

}
