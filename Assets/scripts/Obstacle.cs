using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Obstacle : MonoBehaviour
{
    [Header("Effects")]
    [SerializeField] private bool dealDamageToPlayer = true;
    [SerializeField] private int damageAmount = 1;
    [SerializeField] private AudioClip hitSound;

    [Header("Destroy Settings")]
    [SerializeField] private float destroyDelayOnPlayer = 0f; // 0 = instant
    [SerializeField] private float destroyDelayOnFloor = 0.5f;

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
        if (isDestroyed) return;

        // Player collision
        if (collision.collider.CompareTag("Player"))
        {
            if (hitSound != null)
                AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position);

            if (dealDamageToPlayer)
                GameManager.Instance.TakeDamage(damageAmount);

            Destroy(gameObject, destroyDelayOnPlayer);
            isDestroyed = true;
            return;
        }

        // Floor collision
        if (collision.collider.CompareTag("Floor"))
        {
            Destroy(gameObject, destroyDelayOnFloor);
            isDestroyed = true;
        }
    }
}
