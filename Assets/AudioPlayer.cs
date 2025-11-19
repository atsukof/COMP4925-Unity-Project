using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioClip sound;
    [SerializeField][Range(0f, 1f)] float volume = 1f;

    static AudioPlayer instance = null; // singleton

    public void PlaySound()
    {
        if (sound != null)
        {
            AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position, volume);
        }
    }

    private void Awake()
    {
        if (instance != null) // trying to create a 2nd instance
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // this
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
