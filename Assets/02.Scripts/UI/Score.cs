using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    UIManager uiManager;

    private void Start()
    {
        uiManager = CharacterManager.Instance.player.uiManager;
    }
    private int _curScore;
    public int CurScore //현재 점수
    {
        get { return _curScore; }
    }

    public void AddScore(int val) //점수 추가
    {
        _curScore += val;
        SetTopScore();
        Debug.Log("현재 점수: " + _curScore);
        AchievementManager.Instance.OnScoreAdded(_curScore); //도전과제 점수 추가
    }

    public void SetTopScore() //최고점수 갱신
    {
        if (_curScore > GameManager.Instance.TopScore)
        {
            GameManager.Instance.TopScore = _curScore;
        }
    }

    public void InitializeScore()
    {
        _curScore = 0;
    }
}
