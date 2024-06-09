using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor : MonoBehaviour
{
    [SerializeField] private Animator door = null;
    [SerializeField] private bool openTrigger = false;
    [SerializeField] private AudioClip openSound = null;
    [SerializeField] private AudioClip closeSound = null;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (openTrigger)
            {
                PlayDoorAnimation("OpenDoor", openSound);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (openTrigger)
            {
                PlayDoorAnimation("CloseDoor", closeSound);
            }
        }
    }

    private void PlayDoorAnimation(string animationName, AudioClip sound)
    {
        door.Play(animationName, 0, 0.0f);
        if (sound != null && audioSource != null)
        {
            audioSource.clip = sound;
            audioSource.Play();
        }
    }
}
