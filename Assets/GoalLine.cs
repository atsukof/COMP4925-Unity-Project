using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;

using Quaternion = UnityEngine.Quaternion;

public class GoalLine : MonoBehaviour
{
    public GameObject clearParticlePrefab;  // set in Inspector
    public float delay = 2f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Spawn particle at player's position
            if (clearParticlePrefab != null)
            {
                Instantiate(clearParticlePrefab, other.transform.position, Quaternion.identity);
            }

            // Load next scene after a delay
            Invoke(nameof(LoadNextScene), delay);
        }
    }

    void LoadNextScene()
    {
        int next = SceneManager.GetActiveScene().buildIndex + 1;

        // If next scene exists
        if (next < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(next);
        }
        else
        {
            // If no next scene, restart
            SceneManager.LoadScene(0);
        }
    }
}
