using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FontSizeChanger : MonoBehaviour
{
    private TextMeshProUGUI text;
    [SerializeField] private float minFontSize;
    [SerializeField] private float maxFontSize;
    [SerializeField] private float startTime;
    private bool isIncreasing = true;
    private float currentTime;
    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if(currentTime < startTime)
        {
            currentTime += Time.deltaTime;
            return; // Wait until the start time has passed
        }
        if (text.fontSize >= maxFontSize)
        {
            isIncreasing = false;
        }
        else if (text.fontSize <= minFontSize)
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
