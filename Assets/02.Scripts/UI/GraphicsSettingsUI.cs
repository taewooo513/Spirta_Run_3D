using UnityEngine;

public class GraphicsSettingsUI : MonoBehaviour
{
    public void OnGraphicsLowButton()
    {
        SettingsManager.Instance.SetGraphicsQuality(0);
    }

    public void OnGraphicsMediumButton()
    {
        SettingsManager.Instance.SetGraphicsQuality(1);
    }

    public void OnGraphicsHighButton()
    {
        SettingsManager.Instance.SetGraphicsQuality(2);
    }
}
