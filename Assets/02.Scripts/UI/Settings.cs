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
        audioMixer.SetFloat("BGMVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(BGMVolumeKey, volume);
        PlayerPrefs.Save();
    }
}
