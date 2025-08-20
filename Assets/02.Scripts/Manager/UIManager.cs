using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject menuPannel;
    public GameObject gameOverPannel;
    public Image[] heartImages;

    [Header("Settings UI Elements")] // 인스펙터에서 보기 좋게 그룹화
    public Slider bgmSlider;
    public Slider sfxSlider;
    public Toggle tiltControlToggle;
    public Toggle buttonControlToggle;
    public Toggle vibrationToggle;

    void Start()
    {
        // 게임 시작 시, 저장된 설정값으로 UI 상태를 초기화합니다.
        InitializeSettingsUI();
    }

    // UI 요소들에 리스너(Listener)를 추가하고 초기값을 설정하는 함수
    private void InitializeSettingsUI()
    {
        // 사운드 슬라이더
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 1f);
        bgmSlider.onValueChanged.AddListener(Settings.Instance.SetBGMVolume);

        // 조작 방식 토글
        bool isTilt = Settings.Instance.currentControlType == Settings.ControlType.Tilt;
        tiltControlToggle.isOn = isTilt;
        buttonControlToggle.isOn = !isTilt;
        tiltControlToggle.onValueChanged.AddListener((isOn) => { if (isOn) Settings.Instance.SetControlType(0); });
        buttonControlToggle.onValueChanged.AddListener((isOn) => { if (isOn) Settings.Instance.SetControlType(1); });

        // 진동 토글
        vibrationToggle.isOn = Settings.Instance.IsVibrationEnabled();
        vibrationToggle.onValueChanged.AddListener(Settings.Instance.SetVibration);
    }

    // --- 그래픽 품질 버튼 함수 (버튼의 OnClick 이벤트에 연결) ---
    public void OnGraphicsLowButton() => Settings.Instance.SetGraphicsQuality(0);
    public void OnGraphicsMediumButton() => Settings.Instance.SetGraphicsQuality(1);
    public void OnGraphicsHighButton() => Settings.Instance.SetGraphicsQuality(2);

    // --- 기존 UIManager 함수들은 그대로 유지 ---
    public void Stop()
    {
        Time.timeScale = 0;
        menuPannel.SetActive(true);
    }

    public void ReStart()
    {
        menuPannel.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Continue()
    {
        menuPannel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        Debug.Log("게임오버");
        Time.timeScale = 0f;
        gameOverPannel.SetActive(true);
    }

    public void UpdateHealthUI(int currentHealth)
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < currentHealth)
            {
                heartImages[i].color = Color.white;
            }
            else
            {
                heartImages[i].color = Color.black;
            }
        }
    }
}
