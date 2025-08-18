using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
    {
        public GameObject menuPannel;
        public GameObject gameOverPannel;

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

        public void Exit()
        {
            Debug.Log("나가기 기능");
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }

        public void GameOver()
        {
            Debug.Log("게임오버");
            gameOverPannel.SetActive(true);
        }
    }