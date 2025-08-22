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
        // �ΰ��� �����̴� �� ����
        sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity", 10f);

        // ���� ��� ��� �� ����
        bool isTilt = SettingsManager.Instance.currentControlType == SettingsManager.ControlType.Phone;
        tiltControlToggle.isOn = isTilt;
        buttonControlToggle.isOn = !isTilt;

        // ������ ����
        sensitivitySlider.onValueChanged.RemoveAllListeners();
        //sensitivitySlider.onValueChanged.AddListener(SettingsManager.Instance.SetSensitivity);

        tiltControlToggle.onValueChanged.RemoveAllListeners();
        buttonControlToggle.onValueChanged.RemoveAllListeners();
        tiltControlToggle.onValueChanged.AddListener((isOn) => { if (isOn) SettingsManager.Instance.SetControlType(0); }); // 0 = Phone
        buttonControlToggle.onValueChanged.AddListener((isOn) => { if (isOn) SettingsManager.Instance.SetControlType(1); }); // 1 = Keyboard
    }
}
