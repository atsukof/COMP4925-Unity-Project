using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smooth = 0.1f;

    private float fixedY;
    private float fixedZ;

    private void Start()
    {
        fixedY = transform.position.y;
        fixedZ = transform.position.z;
    }

    private void LateUpdate()
    {
        if (player == null) return;

        float targetX = Mathf.Lerp(transform.position.x, player.position.x, smooth);

        transform.position = new Vector3(targetX, fixedY, fixedZ);
    }
}
