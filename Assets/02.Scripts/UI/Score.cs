using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int _score;
    private int _topScore;

    public void AddScore(int val) //점수 추가
    {
        _score += val;
        TopScore();
    }

    public void TopScore() //최고점수 갱신
    {
        if (_score > _topScore)
        {
          _topScore = _score;  
        }
    }
}
