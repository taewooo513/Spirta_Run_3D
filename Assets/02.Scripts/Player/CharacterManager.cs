using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{
    public Player player;
    Material hairMaterial;
    Material clothesMaterial;
    public void SetColorHair(string colorCode)
    {
        if (ColorUtility.TryParseHtmlString(colorCode, out Color color))
        {
            player.hair.material.SetColor("_BaseColor", color);
            player.hair1.material.SetColor("_BaseColor", color);
            player.hair2.material.SetColor("_BaseColor", color);
        }
        hairMaterial = player.hair.material;
    }
    public void SetColorClothes(string colorCode)
    {
        if (ColorUtility.TryParseHtmlString(colorCode, out Color color))
        {
            player.clothes.material.SetColor("_BaseColor", color);
        }
        clothesMaterial = player.clothes.material;
    }

    public void SetGamePlayerMaterial()
    {
        if (clothesMaterial != null)
        {
            player.clothes.material = clothesMaterial;
        }
        if (hairMaterial != null)
        {
            Debug.Log(hairMaterial.color);
            player.hair.material = hairMaterial;
            player.hair1.material = hairMaterial;
            player.hair2.material = hairMaterial;
        }
    }
}

