using UnityEngine;
using UnityEngine.Audio; // AudioMixer를 사용하기 위해 필요합니다.

public class Settings : Singleton<Settings>
{
    // 오디오 믹서 (BGM, SFX 볼륨 조절용)
    public AudioMixer audioMixer;

    // 조작 방식 저장을 위한 키
    private const string ControlTypeKey = "ControlType";
    // 사운드 저장을 위한 키
    private const string BGMVolumeKey = "BGMVolume";
    // 그래픽 저장을 위한 키
    private const string GraphicsQualityKey = "GraphicsQuality";
    // 진동 저장을 위한 키
    private const string VibrationKey = "Vibration";

    // 현재 조작 방식
    public enum ControlType { Tilt, Button }
    public ControlType currentControlType;

    private void LoadSettings()
    {
        // 조작 방식 불러오기 (저장된 값이 없으면 기본값은 Tilt)
        currentControlType = (ControlType)PlayerPrefs.GetInt(ControlTypeKey, 0);

        // 사운드 볼륨 불러오기 (저장된 값이 없으면 기본값은 0)
        float bgmVolume = PlayerPrefs.GetFloat(BGMVolumeKey, 0f);
        SetBGMVolume(bgmVolume);

        // 그래픽 품질 불러오기 (저장된 값이 없으면 기본값은 1: Medium)
        int qualityIndex = PlayerPrefs.GetInt(GraphicsQualityKey, 1);
        SetGraphicsQuality(qualityIndex);

        // 진동 설정은 PlayerPrefs에서 직접 읽어서 사용합니다.
    }

    // --- 조작 방식 설정 ---
    public void SetControlType(int controlIndex)
    {
        currentControlType = (ControlType)controlIndex;
        PlayerPrefs.SetInt(ControlTypeKey, controlIndex);
        PlayerPrefs.Save();
    }

    // --- 사운드 설정 ---
    public void SetBGMVolume(float volume)
    {
        // AudioMixer의 "BGMVolume" 파라미터 값을 조절합니다.
        // 슬라이더 값(0~1)을 데시벨(-80~0)로 변환합니다.
        audioMixer.SetFloat("BGMVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(BGMVolumeKey, volume);
        PlayerPrefs.Save();
    }

    // --- 그래픽 품질 설정 ---
    public void SetGraphicsQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt(GraphicsQualityKey, qualityIndex);
        PlayerPrefs.Save();
    }

    // --- 진동 설정 ---
    public void SetVibration(bool isOn)
    {
        // 진동 설정은 1(켜짐) 또는 0(꺼짐)으로 저장합니다.
        PlayerPrefs.SetInt(VibrationKey, isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public bool IsVibrationEnabled()
    {
        // 저장된 값이 1이면 true, 아니면 false를 반환합니다. (기본값은 켜짐)
        return PlayerPrefs.GetInt(VibrationKey, 1) == 1;
    }
}
