using UnityEngine;
using UnityEngine.UI;

public class SoundSettingsUI : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider bgmSlider;

    void OnEnable()
    {
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 1f);

        bgmSlider.onValueChanged.RemoveAllListeners();
        bgmSlider.onValueChanged.AddListener(SettingsManager.Instance.SetBGMVolume);
    }
}
