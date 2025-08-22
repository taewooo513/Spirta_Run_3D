using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private GameObject buyUI;
    [SerializeField] private TextMeshProUGUI priceText;
    private int _curIndex;
    private CustomizeType _type;
    private void Start()
    {
        AudioManager.Instance.PlayLoopSound(SoundKey.eShopGBM);

        if (GameManager.Instance.hairDict.Count == 0)
        {
            GameManager.Instance.HairInit();
        }
        if (GameManager.Instance.clothesDict.Count == 0)
        {
            GameManager.Instance.ClothesInit();
        }
        if(buyUI != null)
        {
            buyUI.SetActive(false);
        }
        if(priceText != null)
        {
            priceText.color = UnityEngine.Color.white;
        }
    }
    public void OnClickSetHairColor(int index)
    {
        if (GameManager.Instance.hairDict[index].isUnlocked)
        {
            CharacterManager.Instance.SetColorHair(GameManager.Instance.hairDict[index].colorString);
        }
        else
        {
            _curIndex = index;
            _type = CustomizeType.Hair;
            buyUI.SetActive(true);
            priceText.text = $"보석 {GameManager.Instance.hairDict[index].price}개";
        }
    }

    public void OnClothesSetColor(int index)
    {
        if (GameManager.Instance.clothesDict[index].isUnlocked)
        {
            CharacterManager.Instance.SetColorClothes(GameManager.Instance.hairDict[index].colorString);
        }
        else
        {
            _curIndex = index;
            _type = CustomizeType.Clothes;
            buyUI.SetActive(true);
            priceText.text = $"동전 {GameManager.Instance.hairDict[index].price}개";
        }
    }
    public void OnBuyButton()
    {
        switch (_type)
        {
            case CustomizeType.Hair:
                if (GameManager.Instance.Jewel >= GameManager.Instance.hairDict[_curIndex].price)
                {
                    GameManager.Instance.Jewel -= GameManager.Instance.hairDict[_curIndex].price;
                    GameManager.Instance.hairDict[_curIndex].isUnlocked = true;
                    CharacterManager.Instance.SetColorHair(GameManager.Instance.hairDict[_curIndex].colorString);
                    buyUI.SetActive(false);
                }
                else
                {
                    priceText.color = UnityEngine.Color.red;
                }
                    break;
            case CustomizeType.Clothes:
                if (GameManager.Instance.Coin >= GameManager.Instance.clothesDict[_curIndex].price)
                {
                    GameManager.Instance.Coin -= GameManager.Instance.clothesDict[_curIndex].price;
                    GameManager.Instance.clothesDict[_curIndex].isUnlocked = true;
                    CharacterManager.Instance.SetColorClothes(GameManager.Instance.clothesDict[_curIndex].colorString);
                    buyUI.SetActive(false);
                }
                else
                {
                    priceText.color = UnityEngine.Color.red;
                }
                    break;
        }
    }
    public void OnCancelButton()
    {
        priceText.color = UnityEngine.Color.white;
        buyUI.SetActive(false);
    }
    public void GameStart()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnClickShopButton()
    {
        SceneManager.LoadScene("ShopScene");
    }

    public void OnClickTitleButton()
    {
        SceneManager.LoadScene("StandbyScene");
    }
}
