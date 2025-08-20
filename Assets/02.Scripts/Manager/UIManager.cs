using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //public bool gmMode;
    public GameObject menuPannel;
    public GameObject gameOverPannel;
    public Image[] heartImages; // 하트(체력) 변수

    private void Start()
    {
        CharacterManager.Instance.player.uiManager = this;
    }
    public void Stop()
    {
        Time.timeScale = 0; //시간 0으로 해서 멈춤
        menuPannel.SetActive(true); //옵션창 띄우기
    }

    public void ReStart()
    {
        menuPannel.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //현재 씬 다시불러오기
    }

    public void Continue()
    {
        menuPannel.SetActive(false);
        Time.timeScale = 1f;
        
    }

    //public void Exit()
    //{
    //    Debug.Log("나가기 기능");
    //    //UnityEditor.EditorApplication.isPlaying = false;
    //    Application.Quit();
    //}

    public void GameOver()
    {
        Debug.Log("게임오버");
        Time.timeScale = 0f;
        gameOverPannel.SetActive(true);
    }

    public void UpdateHealthUI(int currentHealth) //UI 체력창 보여주는 함수
    {
        //if (gmMode) return;
        for (int i = 0; i < heartImages.Length; i++)
        {
            // 현재 체력보다 인덱스가 크면 (잃어버린 체력 칸이면) 하트를 끈다
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