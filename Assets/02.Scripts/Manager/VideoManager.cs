using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;

public class VideoManager : MonoBehaviour
{
    public static VideoManager Instance { get; private set; }

    public GameObject videoScreen; 
    public VideoPlayer videoPlayer;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            if (videoScreen != null) videoScreen.SetActive(false); // 시작할 때 비디오 화면 숨기기
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlayVideo(VideoClip clipToPlay)
    {
        StartCoroutine(PlayVideoCoroutine(clipToPlay));
    }

    private IEnumerator PlayVideoCoroutine(VideoClip clipToPlay)
    {
        Time.timeScale = 0f;
        videoScreen.SetActive(true);

        videoPlayer.clip = clipToPlay;
        videoPlayer.Prepare();

        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }

        videoPlayer.Play();

        while (videoPlayer.isPlaying)
        {
            yield return null;
        }

        videoScreen.SetActive(false);
        Time.timeScale = 1f;
    }
}
