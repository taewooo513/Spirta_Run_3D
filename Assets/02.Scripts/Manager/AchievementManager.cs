using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class AchievementManager : Singleton<AchievementManager>
{
    private string achievementPath = Path.Combine(Application.streamingAssetsPath, "AchievementDataJson.json");

    public Dictionary<int, AchievementData> AchievementDict = new Dictionary<int, AchievementData>();
    public List<AchievementData> scoreAchievementList = new List<AchievementData>();
    public List<AchievementData> distanceAchievementList = new List<AchievementData>();
    public List<AchievementData> timeAchievementList = new List<AchievementData>();

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
    public int GetAchievementClear(List<AchievementData> achievements) // 도전과제 깼는지 안깼는지를 가져오는 친구, 배열만 가져오는 역할
    {
        int index = 0;
        foreach (var achievement in achievements)
        {
            if(achievement.IsCleared)
            {
                index++;
            }
            else
            {
                break;
            }
        }
        return index;
    }

    public string GetAchievementDescription(AchievementType type)
    {
        int id = FindClearedAchievementIDByType(type);
        if (id == 0)
        {
            return null;
        }
        else
        {
            return AchievementDict[id].Description;
        }
    }
    public int FindClearedAchievementIDByType(AchievementType type)
    {
        int id = 0;
        switch (type)
        {
            case AchievementType.Score:
                foreach (var achievement in scoreAchievementList)
                {
                    if (achievement.IsCleared)
                        id = achievement.ID;
                }
                break;
            case AchievementType.Distance:
                foreach (var achievement in distanceAchievementList)
                {
                    if (achievement.IsCleared)
                        id = achievement.ID;
                }
                break;
            case AchievementType.Time:
                foreach (var achievement in timeAchievementList)
                {
                    if (achievement.IsCleared)
                        id = achievement.ID;
                }
                break;
        }
        return id;
    }
    public void OnScoreAdded(int curScore)
    {
        foreach(var achievement in scoreAchievementList)
        {
            if (achievement.IsCleared)
                continue;
            else
            {
                if (curScore >= achievement.GoalValue)
                {
                    achievement.IsCleared = true;
                    AchievementDict[achievement.ID].IsCleared = true;
                    AchievementReward(AchievementDict[scoreAchievementList[0].ID].Reward);
                }
                else
                {
                    break;
                }
            }
        }
        
    }
    public void OnDistanceUpdated(float playDistance)
    {
        foreach (var achievement in distanceAchievementList)
        {
            if (achievement.IsCleared)
                continue;
            else
            {
                if (playDistance >= achievement.GoalValue)
                {
                    achievement.IsCleared = true;
                    AchievementDict[achievement.ID].IsCleared = true;
                    AchievementReward(AchievementDict[achievement.ID].Reward);
                }
                else
                {
                    break;
                }
            }
        }
    }
    public void OnTimeUpdated(float elapsedTime)
    {
        foreach (var achievement in timeAchievementList)
        {
            if (achievement.IsCleared)
                continue;
            else
            {
                if (elapsedTime >= achievement.GoalValue)
                {
                    achievement.IsCleared = true;
                    AchievementDict[achievement.ID].IsCleared = true;
                    AchievementReward(AchievementDict[achievement.ID].Reward);
                }
                else
                {
                    break;
                }
            }
        }
    }

    private void AchievementReward(int rewardAmount)
    {
        GameManager.Instance.AddJewel(rewardAmount);
    }
}
