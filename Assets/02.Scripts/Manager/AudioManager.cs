using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum SoundKey
{
    eTestBGM,
    eCarCrashA,
    eCarCrashB,
    eCarCrashC,
    eCoin,
    SoundCount,
    SoundErr
}

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioClip coinSound; // ��� X
    [SerializeField] private AudioClip[] carCrashSound; // �������ڵ� 
    private AudioSource audioSource;  // BGM����� �ҽ�
    private AudioSource audioEFXSource;  // EFX����� �ҽ�
    [SerializeField]
    private AudioClip[] sounds;
    [SerializeField]
    private SoundKey[] soundsKey;


    private void Start()
    {
        var soundSources = transform.GetComponents<AudioSource>();
        if (soundSources.Length != 2)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioEFXSource = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            audioSource = soundSources[0];
            audioEFXSource = soundSources[0];
        }

        audioSource.loop = true;

        PlayLoopSound(SoundKey.eTestBGM);
    }

    public void PlaySoundEffect(SoundKey key)
    {
        int index = (int)key;
        if (index < sounds.Length)
        {
            if (sounds[index] == null)
            {
                Debug.LogWarning("Coin sound not assigned in AudioManager.");
            }
            else
            {
                audioEFXSource.PlayOneShot(sounds[index]);
            }
        }
        else
        {
            Debug.LogWarning($"{key}is out of range to  sounds {sounds.Length}.");
        }
    }

    public void PlayLoopSound(SoundKey key)
    {
        int index = (int)key;
        if (index < sounds.Length)
        {
            if (sounds[index] == null)
            {
                Debug.LogWarning("Coin sound not assigned in AudioManager.");
            }
            else
            {
                audioSource.clip = sounds[index];
                audioSource.Play();
                Debug.Log("fasd");
            }
        }
        else
        {
            Debug.LogWarning($"{key}is out of range to  sounds {sounds.Length}.");
        }
    }
    public void StopLoopSound()
    {
        audioSource.Stop();
    }
    public void StopEFXSound()
    {
        audioEFXSource.Stop();
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
        int val = int.Parse(maxNumber.ToString()) - int.Parse(minNumber.ToString());
        if (val < 0)
        {
            Debug.LogError("maxNumber < minNumber");
            return SoundKey.SoundErr;
        }

        return (SoundKey)UnityEngine.Random.Range(0, val);
    }
}
