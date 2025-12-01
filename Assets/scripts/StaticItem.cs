using UnityEngine;

public class StaticItem : MonoBehaviour
{
    [Header("Effects")]
    [SerializeField] private bool addScoreToPlayer = true;
    [SerializeField] private int scoreAmount = 2;
    [SerializeField] private AudioClip hitSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (hitSound != null)
                AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position);

            if (addScoreToPlayer)
                GameManager.Instance.AddScore(scoreAmount);

            Destroy(gameObject);
        }
    }
}
