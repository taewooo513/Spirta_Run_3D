using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int _curScore;
    public int CurScore
    {
        get { return _curScore; }
    } //현재 점수

    private int _topScore;
    public int TopScore
    {
        get { return _topScore; }
    } //최고 점수

    public void AddScore(int val) //점수 추가
    {
        _curScore += val;
        SetTopScore();
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
