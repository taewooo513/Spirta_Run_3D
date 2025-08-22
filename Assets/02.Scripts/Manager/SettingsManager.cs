using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    public AudioMixer audioMixer;

    private const string ControlTypeKey = "ControlType";
    private const string BGMVolumeKey = "BGMVolume";

    public enum ControlType { Keyboard, Phone }
    public ControlType currentControlType;

    // 게임이 시작될 때 가장 먼저 호출되는 함수
    void Awake()
    {
        // 싱글턴 패턴 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadSettings();
    }

    private void LoadSettings()
    {
        currentControlType = (ControlType)PlayerPrefs.GetInt(ControlTypeKey, 0);

        SetBGMVolume(PlayerPrefs.GetFloat(BGMVolumeKey, 1f));
    }
    public void SetControlType(int controlIndex)
    {
        currentControlType = (ControlType)controlIndex;
        PlayerPrefs.SetInt(ControlTypeKey, controlIndex);
    }

    // BGM 볼륨을 변경하고 저장하는 함수
    public void SetBGMVolume(float volume)
    {
        //audioMixer.SetFloat("BGMVolume", Mathf.Log10(volume) * 20);
        //PlayerPrefs.SetFloat(BGMVolumeKey, volume);
    }

    //public void SetSFXVolume(float volume)
    //{
    //    audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    //    PlayerPrefs.SetFloat(SFXVolumeKey, volume);
    //}
}
