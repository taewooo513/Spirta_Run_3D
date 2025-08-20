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
    public int CurScore //���� ����
    {
        get { return _curScore; }
    }

    public void AddScore(int val) //���� �߰�
    {
        _curScore += val;
        SetTopScore();
        Debug.Log("���� ����: " + _curScore);
        AchievementManager.Instance.OnScoreAdded(_curScore); //�������� ���� �߰�
    }

    public void SetTopScore() //�ְ����� ����
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
