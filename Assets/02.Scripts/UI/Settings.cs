using UnityEngine;
using UnityEngine.Audio; // AudioMixer�� ����ϱ� ���� �ʿ��մϴ�.

public class Settings : Singleton<Settings>
{
    // ����� �ͼ� (BGM, SFX ���� ������)
    public AudioMixer audioMixer;

    // ���� ��� ������ ���� Ű
    private const string ControlTypeKey = "ControlType";
    // ���� ������ ���� Ű
    private const string BGMVolumeKey = "BGMVolume";
    // �׷��� ������ ���� Ű
    private const string GraphicsQualityKey = "GraphicsQuality";
    // ���� ������ ���� Ű
    private const string VibrationKey = "Vibration";

    // ���� ���� ���
    public enum ControlType { Tilt, Button }
    public ControlType currentControlType;

    private void LoadSettings()
    {
        // ���� ��� �ҷ����� (����� ���� ������ �⺻���� Tilt)
        currentControlType = (ControlType)PlayerPrefs.GetInt(ControlTypeKey, 0);

        // ���� ���� �ҷ����� (����� ���� ������ �⺻���� 0)
        float bgmVolume = PlayerPrefs.GetFloat(BGMVolumeKey, 0f);
        SetBGMVolume(bgmVolume);

        // �׷��� ǰ�� �ҷ����� (����� ���� ������ �⺻���� 1: Medium)
        int qualityIndex = PlayerPrefs.GetInt(GraphicsQualityKey, 1);
        SetGraphicsQuality(qualityIndex);

        // ���� ������ PlayerPrefs���� ���� �о ����մϴ�.
    }

    // --- ���� ��� ���� ---
    public void SetControlType(int controlIndex)
    {
        currentControlType = (ControlType)controlIndex;
        PlayerPrefs.SetInt(ControlTypeKey, controlIndex);
        PlayerPrefs.Save();
    }

    // --- ���� ���� ---
    public void SetBGMVolume(float volume)
    {
        // AudioMixer�� "BGMVolume" �Ķ���� ���� �����մϴ�.
        // �����̴� ��(0~1)�� ���ú�(-80~0)�� ��ȯ�մϴ�.
        audioMixer.SetFloat("BGMVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(BGMVolumeKey, volume);
        PlayerPrefs.Save();
    }

    // --- �׷��� ǰ�� ���� ---
    public void SetGraphicsQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt(GraphicsQualityKey, qualityIndex);
        PlayerPrefs.Save();
    }

    // --- ���� ���� ---
    public void SetVibration(bool isOn)
    {
        // ���� ������ 1(����) �Ǵ� 0(����)���� �����մϴ�.
        PlayerPrefs.SetInt(VibrationKey, isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public bool IsVibrationEnabled()
    {
        // ����� ���� 1�̸� true, �ƴϸ� false�� ��ȯ�մϴ�. (�⺻���� ����)
        return PlayerPrefs.GetInt(VibrationKey, 1) == 1;
    }
}
