using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinTextUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    void Start()
    {
        coinText.text = GameManager.Instance.Coin.ToString();
    }
}
