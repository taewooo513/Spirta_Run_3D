using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI curScoreText;
    public TextMeshProUGUI topScoreText;

    public void UpdateUI()
    {
        scoreText.text = GameManager.Instance.score.CurScore.ToString();
        curScoreText.text = GameManager.Instance.score.CurScore.ToString();
        topScoreText.text = GameManager.Instance.score.TopScore.ToString();
    }
}
