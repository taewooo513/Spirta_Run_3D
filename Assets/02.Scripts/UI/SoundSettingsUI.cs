using UnityEngine;
using UnityEngine.UI;

public class SoundSettingsUI : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider bgmSlider;

    void OnEnable()
    {
        bgmSlider.value = AudioManager.Instance.volume;

        bgmSlider.onValueChanged.RemoveAllListeners();
        bgmSlider.onValueChanged.AddListener(AudioManager.Instance.SetVolume);
    }
}
