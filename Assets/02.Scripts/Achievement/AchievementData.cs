public enum AchievementType
{
    Score,
    Distance,
    Time
}

public class AchievementData
{
    public int ID { get; set; }
    public string AchievementName { get; set; }
    public string Description { get; set; }
    public AchievementType Type { get; set; }
    public int GoalValue { get; set; }
    public bool IsCleared { get; set; }
    public int Reward { get; set; }
}