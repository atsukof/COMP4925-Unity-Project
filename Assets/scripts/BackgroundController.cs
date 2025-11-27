using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{

    // Initial position of the background
    private float startPos, length;
    public GameObject cam;
    public float parallaxEffect; // The speed at which the background should move relative to the camera
    
    void Start()
    {
        startPos = transform.position.x;
        // This gets the width of the sprite
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void LateUpdate()
    {
        // Calculate the distance background move based on camera movement
        // HOW MUCH we have moved relative to the parallax speed
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        
        // WHERE the background should be right now
        float dist = (cam.transform.position.x * parallaxEffect);

        // Move the background
        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        // --- THE CAROUSEL LOGIC ---
        
        // If the camera has moved 'temp' distance to the RIGHT past the image length...
        if (temp > startPos + length)
        {
            // ...Snap the start position forward by one image length.
            startPos += length;
        }
        // If the camera has moved 'temp' distance to the LEFT past the image length...
        else if (temp < startPos - length)
        {
            // ...Snap the start position backward by one image length.
            startPos -= length;
        }
    }
} 