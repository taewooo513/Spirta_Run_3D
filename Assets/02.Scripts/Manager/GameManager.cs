using _02.Scripts.map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Map map;
    public float playDistance; // 진행 거리
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
        yield return new WaitForSeconds(maxTime);
    }
    public void GetSpeedUpItem(float val, float maxTime)
    {
        StartCoroutine(GetSpeedItemBuff(val, maxTime));
    }

    IEnumerator GetSpeedItemBuff(float val, float maxTime)
    {
        float speed = map.speed;
        map.speed = val;
        yield return new WaitForSeconds(maxTime);
        map.speed = speed;
    }
}
