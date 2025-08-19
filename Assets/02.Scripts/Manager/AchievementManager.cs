using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class AchievementManager : Singleton<AchievementManager>
{
    private string achievementPath = Path.Combine(Application.streamingAssetsPath, "AchievementDataJson.json");

    public Dictionary<int, AchievementData> AchievementDict = new Dictionary<int, AchievementData>();

    protected override void Awake()
    {
        base.Awake();
        LoadAchievement();
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

    public List<T> LoadJsonData<T>(string path)
    {
        string json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<List<T>>(json);
    }

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


}
