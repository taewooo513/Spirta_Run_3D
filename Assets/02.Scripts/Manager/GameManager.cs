using _02.Scripts.map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public Map[] map;
    public float playDistance; // ���� �Ÿ�
    bool isInv;
    float score;
    public void InitGame()
    {

    }

    public void UpdateDistance()
    {
        playDistance += Time.deltaTime * 100;
    }

    public void AddScore(int val)
    {
        
    }

    public void StopGame()
    {
    }

    public void ResumGame()
    {

    }

    public void GetInvItem(float maxTime)
    {
        StartCoroutine(GetSpeedItemBuff(maxTime));
    }

    IEnumerator GetSpeedItemBuff(float maxTime)
    {
        isInv = true;
        yield return new WaitForSeconds(maxTime);
        isInv = false;
    }
    public void GetSpeedUpItem(float val, float maxTime)
    {
        StartCoroutine(GetSpeedItemBuff(val, maxTime));
    }

    IEnumerator GetSpeedItemBuff(float val, float maxTime)
    {
        for (int i = 0; i < 2; i++)
        {
            float speed = map[i].speed;
            map[i].speed = val;
        }
        yield return new WaitForSeconds(maxTime);
        for (int i = 0; i < 2; i++)
        {
            float speed = map[i].speed;
            map[i].speed = speed;
        }
    }

    public void GameEnd()
    {
        PatternManager.Instance.ClearPatter();
    }

    public void GameStart()
    {
        playDistance = 0;
        isInv = false;
    }
}
