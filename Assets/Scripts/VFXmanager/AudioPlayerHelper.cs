using UnityEngine;

public class AudioPlayerHelper : MonoBehaviour
{
    public KeyCode keyCode = KeyCode.P;
    public AudioSource audioSource;

    void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            Play();
        }
    }

    public void Play()
    {
        audioSource.Play();
    }
}
