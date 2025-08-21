using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundKey
{
    eCoin,
    eInGameBGM,
    eJump,
    eMainTitle,
    eShopGBM,
    eCarCrashA,
    eCarCrashB,
    eCarCrashC,
    eCount,
    SoundErr
}

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private AudioClip[] carCrashSound;
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip[] sounds;
    [SerializeField]
    private SoundKey[] soundsKey;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            this.gameObject.AddComponent<AudioSource>();
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void PlaySoundEffect(SoundKey key)
    {
        if (sounds == null)
        {
            Debug.LogWarning("No Sounds List");

            return;
        }
        int index = (int)key;
        if (index <= sounds?.Length)
        {
            if (sounds[index] == null)
            {
                Debug.LogWarning("Coin sound not assigned in AudioManager.");
            }
            else
            {
                audioSource.PlayOneShot(sounds[index]);
            }
        }
        else
        {
            Debug.LogWarning($"{key}is out of range to  sounds {sounds?.Length}.");
        }
    }

    public void PlayLoopSound(SoundKey key)
    {
        int index = (int)key;
        if (sounds == null)
        {
            Debug.LogWarning("No Sounds List");
            return;
        }
        Debug.Log("fda");
        if (index <= sounds?.Length)
        {
            Debug.Log("fda");

            if (sounds[index] == null)
            {
                Debug.LogWarning("Coin sound not assigned in AudioManager.");
            }
            else
            {
                audioSource.clip = sounds[index];
                audioSource.Play();
            }
        }
        else
        {
            Debug.LogWarning($"{key}is out of range to  sounds {sounds.Length}.");
        }
    }
    public void PlayCoinSound()
    {
        PlaySoundEffect(SoundKey.eCoin);
    }
    public void PlayCarCrashSound()
    {
        PlaySoundEffect(RandomToSoundKey(SoundKey.eCarCrashA, SoundKey.eCarCrashC));
    }


    private SoundKey RandomToSoundKey(SoundKey minNumber, SoundKey maxNumber)
    {
        int val = (int)maxNumber - (int)minNumber;
        if (val < 0)
        {
            Debug.LogError("maxNumber < minNumber");
            return SoundKey.SoundErr;
        }

        return (SoundKey)((int)minNumber + UnityEngine.Random.Range(0, val));
    }
}
