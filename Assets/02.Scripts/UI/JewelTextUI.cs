using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JewelTextUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI jewelText;
    private void Start()
    {
        jewelText.text = GameManager.Instance.Jewel.ToString();
    }
}
