using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : Singleton<SettingsManager>
{
    public AudioMixer audioMixer;

    private const string BGMVolumeKey = "BGMVolume";
    private const string QualityKey = "QualityLevel";
    private const string SensitivityKey = "Sensitivity";
    private const string ControlTypeKey = "ControlType";

    //���� ���� �� ����
    public float currentSensitivity;
    public ControlType currentControlType; 

    public enum ControlType { Tilt, Button }

    private void Start()
    {
        LoadSettings();
    }

    private void LoadSettings()
    {
        // ���� ���� �ҷ�����
        SetBGMVolume(PlayerPrefs.GetFloat(BGMVolumeKey, 1f));

        // �׷��� ǰ�� �ҷ�����
        SetGraphicsQuality(PlayerPrefs.GetInt(QualityKey, 1));

        // ���� �ΰ��� �ҷ�����
        currentSensitivity = PlayerPrefs.GetFloat(SensitivityKey, 10f);

        // ���� ��� �ҷ�����
        currentControlType = (ControlType)PlayerPrefs.GetInt(ControlTypeKey, 0); // 0 = Tilt
    }

    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGMVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(BGMVolumeKey, volume);
    }

    public void SetGraphicsQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
        PlayerPrefs.SetInt(QualityKey, index);
    }

    public void SetSensitivity(float sensitivity)
    {
        currentSensitivity = sensitivity;
        PlayerPrefs.SetFloat(SensitivityKey, sensitivity);
    }

    // ���� ����� �����ϴ� �Լ� (�߰��� �κ�)
    public void SetControlType(int index)
    {
        currentControlType = (ControlType)index;
        PlayerPrefs.SetInt(ControlTypeKey, index);
    }
}
