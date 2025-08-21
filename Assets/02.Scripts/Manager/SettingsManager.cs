using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : Singleton<SettingsManager>
{
    public AudioMixer audioMixer;

    private const string BGMVolumeKey = "BGMVolume";
    private const string QualityKey = "QualityLevel";
    private const string SensitivityKey = "Sensitivity";
    private const string ControlTypeKey = "ControlType";

    //현재 설정 값 변수
    public float currentSensitivity;
    public ControlType currentControlType; 

    public enum ControlType { Tilt, Button }

    private void Start()
    {
        LoadSettings();
    }

    private void LoadSettings()
    {
        // 사운드 볼륨 불러오기
        SetBGMVolume(PlayerPrefs.GetFloat(BGMVolumeKey, 1f));

        // 그래픽 품질 불러오기
        SetGraphicsQuality(PlayerPrefs.GetInt(QualityKey, 1));

        // 조작 민감도 불러오기
        currentSensitivity = PlayerPrefs.GetFloat(SensitivityKey, 10f);

        // 조작 방식 불러오기
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

    // 조작 방식을 변경하는 함수 (추가된 부분)
    public void SetControlType(int index)
    {
        currentControlType = (ControlType)index;
        PlayerPrefs.SetInt(ControlTypeKey, index);
    }
}
