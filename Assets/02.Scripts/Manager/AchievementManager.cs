using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class AchievementManager : Singleton<AchievementManager>
{
    private string achievementPath = Path.Combine(Application.streamingAssetsPath, "AchievementDataJson.json");

    public Dictionary<int, AchievementData> AchievementDict = new Dictionary<int, AchievementData>();
    private List<AchievementData> scoreAchievementList = new List<AchievementData>();
    private List<AchievementData> distanceAchievementList = new List<AchievementData>();
    private List<AchievementData> timeAchievementList = new List<AchievementData>();

    protected override void Awake()
    {
        base.Awake();
        LoadAchievement();
        AddAchievementList();
    }

    private void LoadAchievement()
    {
        if (!File.Exists(achievementPath))
        {
            Debug.LogError("Achievement data file not found at:" + achievementPath);
        }
        else
        {
            List<AchievementData> achievementDataList = LoadJsonData<AchievementData>(achievementPath);
            foreach (AchievementData data in achievementDataList)
            {
                if (!AchievementDict.ContainsKey(data.ID))
                {
                    AchievementDict.Add(data.ID, data);
                }
            }
        }
    }
    private void AddAchievementList()
    {
        foreach (var achievement in AchievementDict)
        {
            switch (achievement.Value.Type)
            {
                case AchievementType.Score:
                    scoreAchievementList.Add(achievement.Value);
                    break;
                case AchievementType.Distance:
                    distanceAchievementList.Add(achievement.Value);
                    break;
                case AchievementType.Time:
                    timeAchievementList.Add(achievement.Value);
                    break;
            }
        }
    }

    public List<T> LoadJsonData<T>(string path)
    {
        string json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<List<T>>(json);
    }

    //AchievementManager.Instance.GetAchievementClear()
    public bool[] GetAchievementClear() // 도전과제 깼는지 안깼는지를 가져오는 친구, 배열만 가져오는 역할
    {
        bool[] achievementClear = new bool[AchievementDict.Count];
        int index = 0;
        foreach (var achievement in AchievementDict)
        {
            achievementClear[index] = achievement.Value.IsCleared;
            index++;
        }
        return achievementClear;
    }
    public void OnScoreAdded(int curScore)
    {
        if (scoreAchievementList.Count == 0)
            return;

        if (curScore >= scoreAchievementList[0].GoalValue)
        {
            AchievementDict[scoreAchievementList[0].ID].IsCleared = true;
            AchievementReward(AchievementDict[scoreAchievementList[0].ID].Reward);
            //여기에 도전과제 달성 UI 띄우는 것 연결하셔야합니다.
            scoreAchievementList.Remove(scoreAchievementList[0]);
        }
    }
    public void OnDistanceUpdated(float playDistance)
    {
        if (distanceAchievementList.Count == 0)
            return;
        if (playDistance >= distanceAchievementList[0].GoalValue)
        {
            AchievementDict[distanceAchievementList[0].ID].IsCleared = true;
            AchievementReward(AchievementDict[distanceAchievementList[0].ID].Reward);
            //여기에 도전과제 달성 UI 띄우는 것 연결하셔야합니다.
            distanceAchievementList.Remove(distanceAchievementList[0]);
        }
    }
    public void OnTimeUpdated(float elapsedTime)
    {
        if (timeAchievementList.Count == 0)
            return;
        if (elapsedTime >= timeAchievementList[0].GoalValue)
        {
            AchievementDict[timeAchievementList[0].ID].IsCleared = true;
            AchievementReward(AchievementDict[timeAchievementList[0].ID].Reward);
            //여기에 도전과제 달성 UI 띄우는 것 연결하셔야합니다.
            timeAchievementList.Remove(timeAchievementList[0]);
        }
    }

    private void AchievementReward(int rewardAmount)
    {
        //보상 증정?
    }
}
