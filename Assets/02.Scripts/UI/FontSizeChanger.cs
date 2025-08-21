using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FontSizeChanger : MonoBehaviour
{
    private TextMeshProUGUI text;
    private bool isIncreasing = true;
    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if(text.fontSize >= 100)
        {
            isIncreasing = false;
        }
        else if (text.fontSize <= 50)
        {
            isIncreasing = true;
        }
        if (isIncreasing)
        {
            text.fontSize += Time.deltaTime * 80; // Increase font size
        }
        else
        {
            text.fontSize -= Time.deltaTime * 80; // Decrease font size
        }
    }
}
