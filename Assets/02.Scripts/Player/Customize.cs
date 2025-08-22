using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CustomizeType
{
    Hair,
    Clothes
}
public class Customize
{
    public string colorString;
    public CustomizeType type;
    public int price;
    public bool isUnlocked;

    public Customize(string colorString, CustomizeType type, int price, bool isUnlocked)
    {
        this.colorString = colorString;
        this.type = type;
        this.price = price;
        this.isUnlocked = isUnlocked;
    }
}
