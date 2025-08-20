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
        audioMixer.SetFloat("BGMVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(BGMVolumeKey, volume);
        PlayerPrefs.Save();
    }
}
