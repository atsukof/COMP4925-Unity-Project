using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class Hoge: MonoBehaviour
{
    private void Start()
    {
        Debug.Log($"[TriggerDebug] Start on {gameObject.name} in scene {SceneManager.GetActiveScene().name}");
        var col = GetComponent<Collider2D>();
        Debug.Log($"[TriggerDebug] Collider enabled={col.enabled}, isTrigger={col.isTrigger}");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"[TriggerDebug] OnTriggerEnter2D with {other.name}, tag={other.tag}, layer={LayerMask.LayerToName(other.gameObject.layer)}, time={Time.time}");
    }
}
