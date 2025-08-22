using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    public AudioMixer audioMixer;

    private const string ControlTypeKey = "ControlType";
    private const string BGMVolumeKey = "BGMVolume";
    private const string SFXVolumeKey = "SFXVolume";

    public enum ControlType { Tilt, Button }
    public ControlType currentControlType;

    // ������ ���۵� �� ���� ���� ȣ��Ǵ� �Լ�
    void Awake()
    {
        // �̱��� ���� ����
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
        SetSFXVolume(PlayerPrefs.GetFloat(SFXVolumeKey, 1f));
    }
    public void SetControlType(int controlIndex)
    {
        currentControlType = (ControlType)controlIndex;
        PlayerPrefs.SetInt(ControlTypeKey, controlIndex);
    }

    // BGM ������ �����ϰ� �����ϴ� �Լ�
    public void SetBGMVolume(float volume)
    {
        //audioMixer.SetFloat("BGMVolume", Mathf.Log10(volume) * 20);
        //PlayerPrefs.SetFloat(BGMVolumeKey, volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(SFXVolumeKey, volume);
    }
}
