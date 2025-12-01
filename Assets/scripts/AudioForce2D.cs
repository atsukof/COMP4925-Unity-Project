using System.Collections.Generic;
using UnityEngine;

public class AudioForce2D : MonoBehaviour
{
    void Start()
    {
        // all AudioSource in the scene
        AudioSource[] sources = FindObjectsOfType<AudioSource>();
        foreach (var s in sources)
        {
            s.spatialBlend = 0f;
        }
    }

    private HashSet<AudioSource> patched = new HashSet<AudioSource>();

    void Update()
    {
        foreach (var s in FindObjectsOfType<AudioSource>())
        {
            if (!patched.Contains(s))
            {
                s.spatialBlend = 0f;
                patched.Add(s);
            }
        }
    }
}
