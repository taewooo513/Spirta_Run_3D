using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [Header("UI ����")]
    public Slider bgmSlider;
    public Slider sfxSlider;
    public Toggle tiltControlToggle;
    public Toggle buttonControlToggle;

    // �� ��ũ��Ʈ�� Ȱ��ȭ�� ��(���� â�� ���� ��) ȣ��˴ϴ�.
    void OnEnable()
    {
        // ���� ����� ���������� UI ���¸� �ʱ�ȭ�մϴ�.
        LoadAndSetUIValues();
    }

    // UI ��ҵ鿡 ������(Listener)�� �߰��ϰ� �ʱⰪ�� �����ϴ� �Լ�
    private void LoadAndSetUIValues()
    {
        // ���� �����̴�
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        // �����ʸ� �߰��ϱ� ���� Ȥ�� �� �ߺ��� ���� ���� �ʱ�ȭ�մϴ�.
        bgmSlider.onValueChanged.RemoveAllListeners();
        sfxSlider.onValueChanged.RemoveAllListeners();
        bgmSlider.onValueChanged.AddListener(SettingsManager.Instance.SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SettingsManager.Instance.SetSFXVolume);

        // ���� ��� ���
        bool isTilt = SettingsManager.Instance.currentControlType == SettingsManager.ControlType.Tilt;
        tiltControlToggle.isOn = isTilt;
        buttonControlToggle.isOn = !isTilt;

        tiltControlToggle.onValueChanged.RemoveAllListeners();
        buttonControlToggle.onValueChanged.RemoveAllListeners();
        tiltControlToggle.onValueChanged.AddListener((isOn) => { if (isOn) SettingsManager.Instance.SetControlType(0); });
        buttonControlToggle.onValueChanged.AddListener((isOn) => { if (isOn) SettingsManager.Instance.SetControlType(1); });
    }
}
