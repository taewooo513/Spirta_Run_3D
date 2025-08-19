using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
}
