using UnityEngine;

public class PlayerDeathByBarrier : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If barrier has a Tag, check tag here
        if (other.CompareTag("Death"))
        {
            // Kill player by reducing all life
            GameManager.Instance.TakeDamage(GameManager.Instance.life);
        }
    }
}
