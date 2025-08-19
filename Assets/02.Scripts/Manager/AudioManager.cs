using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private AudioClip[] carCrashSound;
    private AudioSource audioSource;

    private void Start()
    { 
        audioSource = GetComponent<AudioSource>();
        if(audioSource == null)
        {
            this.gameObject.AddComponent<AudioSource>();
            audioSource = GetComponent<AudioSource>();
        }
    }
    public void PlayCoinSound()
    {
        if (coinSound != null)
        {
            audioSource.PlayOneShot(coinSound);
        }
        else
        {
            Debug.LogWarning("Coin sound not assigned in AudioManager.");
        }
    }
    public void PlayCarCrashSound()
    {
        if (carCrashSound.Length > 0)
        {
            audioSource.PlayOneShot(GetRandomCarCrashSound());
        }
        else
        {
            Debug.LogWarning("Car crash sounds not assigned in AudioManager.");
        }
    }
    private AudioClip GetRandomCarCrashSound()
    {
        if (carCrashSound.Length == 0)
        {
            Debug.LogWarning("No car crash sounds available.");
            return null;
        }
        int randomIndex = Random.Range(0, carCrashSound.Length);
        return carCrashSound[randomIndex];
    }
}
