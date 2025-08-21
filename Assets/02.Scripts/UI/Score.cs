using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Score : MonoBehaviour
{
    [SerializeField] public UIManager uiManager;

    private void Start()
    {
        GameManager.Instance.score = this;
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
        CharacterManager.Instance.player.uiManager.scoreUI.UpdateUI();
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
    public void SendThisUIManager(UIManager UIManager)
    {
        uiManager = UIManager;
    }
}
