using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeLine : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private AchievementType type;
    [SerializeField] private Image star1;
    [SerializeField] private Image star2;
    [SerializeField] private Image star3;
    [SerializeField] private Sprite starImage;
    private void Start()
    {
        SetAchievement();
    }
    private void SetAchievement()
    {
        List<AchievementData> achievementList = SetAchievementList();
        SetStarsAndDescription(achievementList);
    }
    private List<AchievementData> SetAchievementList()
    {
        switch (type)
        {
            case AchievementType.Score:
                return AchievementManager.Instance.scoreAchievementList;
            case AchievementType.Distance:
                return AchievementManager.Instance.distanceAchievementList;
            case AchievementType.Time:
                return AchievementManager.Instance.timeAchievementList;
            default:
                Debug.LogError("Invalid AchievementType: " + type);
                return new List<AchievementData>();
        }
    }
    private void SetStarsAndDescription(List<AchievementData> achievementList)
    {
        Debug.Log("Achievement List Count: " + achievementList.Count);
        switch (AchievementManager.Instance.GetAchievementClear(achievementList))
        {
            case 0:
                break;
            case 1:
                star1.sprite = starImage;
                text.text = AchievementManager.Instance.GetAchievementDescription(type);
                break;
            case 2:
                star1.sprite = starImage;
                star2.sprite = starImage;
                text.text = AchievementManager.Instance.GetAchievementDescription(type);
                break;
            case 3:
                star1.sprite = starImage;
                star2.sprite = starImage;
                star3.sprite = starImage;
                text.text = AchievementManager.Instance.GetAchievementDescription(type);
                break;
        }
    }
}
