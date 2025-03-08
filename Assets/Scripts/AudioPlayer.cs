using System.Collections;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource audioSource;  
    public float delayInSeconds = 3f; 

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        StartCoroutine(PlayAudioWithDelay());
    }

    IEnumerator PlayAudioWithDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        audioSource.Play();
    }
}
