using UnityEngine;
using UnityEngine.UI;

public class ControlSettingsUI : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider sensitivitySlider;
    public Toggle tiltControlToggle;
    public Toggle buttonControlToggle;

    void OnEnable()
    {
        // 민감도 슬라이더 값 설정
        sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity", 10f);

        // 조작 방식 토글 값 설정
        bool isTilt = SettingsManager.Instance.currentControlType == SettingsManager.ControlType.Phone;
        tiltControlToggle.isOn = isTilt;
        buttonControlToggle.isOn = !isTilt;

        // 리스너 연결
        sensitivitySlider.onValueChanged.RemoveAllListeners();
        //sensitivitySlider.onValueChanged.AddListener(SettingsManager.Instance.SetSensitivity);

        tiltControlToggle.onValueChanged.RemoveAllListeners();
        buttonControlToggle.onValueChanged.RemoveAllListeners();
        tiltControlToggle.onValueChanged.AddListener((isOn) => { if (isOn) SettingsManager.Instance.SetControlType(0); }); // 0 = Phone
        buttonControlToggle.onValueChanged.AddListener((isOn) => { if (isOn) SettingsManager.Instance.SetControlType(1); }); // 1 = Keyboard
    }
}
