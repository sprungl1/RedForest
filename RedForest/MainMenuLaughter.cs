using System.Collections;
using UnityEngine;

public class LaughterManager : MonoBehaviour
{
    public AudioClip laughterClip;
    public float minDelay = 10f;
    public float maxDelay = 30f;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Start the coroutine to play laughter clips periodically
        StartCoroutine(PlayLaughter());
    }

    private IEnumerator PlayLaughter()
    {
        while (true)
        {
            // Wait for a random amount of time between minDelay and maxDelay
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);

            // Play the laughter audio clip
            if (audioSource && laughterClip)
            {
                audioSource.PlayOneShot(laughterClip);
            }
        }
    }
}
