using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeLine : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Scriptable Scriptable;
    
    void Awake()
    {
        string str = $"{Scriptable.description}";
        text.text = str;
    }
}
