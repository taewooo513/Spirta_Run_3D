using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI curScoreText;
    public TextMeshProUGUI topScoreText;
    public TextMeshProUGUI MainCoinScoreText;

    public void UpdateUI()
    {
        scoreText.text = GameManager.Instance.score.CurScore.ToString();
        curScoreText.text = GameManager.Instance.score.CurScore.ToString();
        topScoreText.text = GameManager.Instance.TopScore.ToString();
        MainCoinScoreText.text = GameManager.Instance.Coin.ToString();
    }
}
