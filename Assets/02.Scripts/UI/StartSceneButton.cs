using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneButton : MonoBehaviour
{
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("StandbyScene");
    }
}
