using _02.Scripts.map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public Map[] map;
    public float playDistance; // 진행 거리
    bool isInv;

    [HideInInspector] public Score score;

    private float _playTime;

    [HideInInspector] public int TopScore;
    [HideInInspector] public int Coin;
    [HideInInspector] public int Jewel;
    [HideInInspector] public Dictionary<int, Customize> hairDict = new();
    [HideInInspector] public Dictionary<int, Customize> clothesDict = new();
    
    public void HairInit()
    {
        hairDict.Add(0, new Customize("#FF0000", CustomizeType.Hair, 50, false));
        hairDict.Add(1, new Customize("#FF8100", CustomizeType.Hair, 50, false));
        hairDict.Add(2, new Customize("#FFFD00", CustomizeType.Hair, 50, false));
        hairDict.Add(3, new Customize("#15FF00", CustomizeType.Hair, 50, false));
        hairDict.Add(4, new Customize("#00BDFF", CustomizeType.Hair, 50, false));
        hairDict.Add(5, new Customize("#0046FF", CustomizeType.Hair, 50, false));
        hairDict.Add(6, new Customize("#C600FF", CustomizeType.Hair, 50, false));
    }
    public void ClothesInit()
    {
        clothesDict.Add(0, new Customize("#FF0000", CustomizeType.Clothes, 50, false));
        clothesDict.Add(1, new Customize("#FF8100", CustomizeType.Clothes, 50, false));
        clothesDict.Add(2, new Customize("#FFFD00", CustomizeType.Clothes, 50, false));
        clothesDict.Add(3, new Customize("#15FF00", CustomizeType.Clothes, 50, false));
        clothesDict.Add(4, new Customize("#00BDFF", CustomizeType.Clothes, 50, false));
        clothesDict.Add(5, new Customize("#0046FF", CustomizeType.Clothes, 50, false));
        clothesDict.Add(6, new Customize("#C600FF", CustomizeType.Clothes, 50, false));
    }

    public void InitGame()
    {

    }
    public void AddJewel(int val)
    {
        Jewel += val;
    }
    public void UpdateDistance()
    {
        playDistance += map[0].speed * Time.deltaTime;
        AchievementManager.Instance.OnDistanceUpdated(playDistance);
    }
    public void UpdateTime()
    {
        _playTime += Time.deltaTime;
        AchievementManager.Instance.OnTimeUpdated(_playTime);
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
        _playTime = 0;
        isInv = false;
        Time.timeScale = 1f;
    }
}
