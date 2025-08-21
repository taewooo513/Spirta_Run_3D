using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    public void OnClickSetHairColor(string color)
    {
        CharacterManager.Instance.SetColorHair(color);
    }

    public void OnClothesSetColor(string color)
    {
        CharacterManager.Instance.SetColorClothes(color);
    }

    public void GameStart()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnClickShopButton()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void OnClickTitleButton()
    {
        SceneManager.LoadScene("StandbyScene");
    }
}
