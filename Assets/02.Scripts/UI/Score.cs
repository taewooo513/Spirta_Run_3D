using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int _curScore;
    public int CurScore //���� ����
    {
        get { return _curScore; }
    }

    private int _topScore;
    public int TopScore //�ְ� ����
    {
        get { return _topScore; }
    }

    void Start()
    {
        GameManager.Instance.score = this;
    }

    public void AddScore(int val) //���� �߰�
    {
        _curScore += val;
        SetTopScore();
        CharacterManager.Instance.player.uiManager.scoreUI.UpdateUI();
        Debug.Log("���� ����: " + _curScore);
    }

    public void SetTopScore() //�ְ����� ����
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
