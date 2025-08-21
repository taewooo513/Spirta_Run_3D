using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [Header("UI 설정")]
    public Slider bgmSlider;
    public Slider sfxSlider;
    public Toggle tiltControlToggle;
    public Toggle buttonControlToggle;

    // 이 스크립트가 활성화될 때(설정 창이 켜질 때) 호출됩니다.
    void OnEnable()
    {
        // 현재 저장된 설정값으로 UI 상태를 초기화합니다.
        LoadAndSetUIValues();
    }

    // UI 요소들에 리스너(Listener)를 추가하고 초기값을 설정하는 함수
    private void LoadAndSetUIValues()
    {
        // 사운드 슬라이더
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        // 리스너를 추가하기 전에 혹시 모를 중복을 막기 위해 초기화합니다.
        bgmSlider.onValueChanged.RemoveAllListeners();
        sfxSlider.onValueChanged.RemoveAllListeners();
        bgmSlider.onValueChanged.AddListener(SettingsManager.Instance.SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SettingsManager.Instance.SetSFXVolume);

        // 조작 방식 토글
        bool isTilt = SettingsManager.Instance.currentControlType == SettingsManager.ControlType.Tilt;
        tiltControlToggle.isOn = isTilt;
        buttonControlToggle.isOn = !isTilt;

        tiltControlToggle.onValueChanged.RemoveAllListeners();
        buttonControlToggle.onValueChanged.RemoveAllListeners();
        tiltControlToggle.onValueChanged.AddListener((isOn) => { if (isOn) SettingsManager.Instance.SetControlType(0); });
        buttonControlToggle.onValueChanged.AddListener((isOn) => { if (isOn) SettingsManager.Instance.SetControlType(1); });
    }
}
