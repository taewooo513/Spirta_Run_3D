using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int _curScore;
    public int CurScore //현재 점수
    {
        get { return _curScore; }
    }

    private int _topScore;
    public int TopScore //최고 점수
    {
        get { return _topScore; }
    }

    public void AddScore(int val) //점수 추가
    {
        _curScore += val;
        SetTopScore();
        Debug.Log(_curScore);
    }

    public void SetTopScore() //최고점수 갱신
    {
        if (_curScore > _topScore)
        {
            _topScore = _curScore;
        }
    }

    public void InitializeScore()
    {
        _curScore = 0;
    }
}
