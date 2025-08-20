using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int _score;
    private int _topScore;

    public void AddScore(int val) //���� �߰�
    {
        _score += val;
        TopScore();
    }

    public void TopScore() //�ְ����� ����
    {
        if (_score > _topScore)
        {
          _topScore = _score;  
        }
    }
}
