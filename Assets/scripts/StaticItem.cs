using UnityEngine;

public class StaticItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(2);
            Destroy(gameObject);
        }
    }
}
