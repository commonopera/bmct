using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AchievementID
{
    ACT_I = 0,
    ACT_II = 1,
    ACT_III = 3,
    ONE_MINUTE_FLIGHT = 2,
}

public enum AchievementStatID
{
    FLIGHT_TIME = 0
}

public class AchievementsAndStats : MonoBehaviour
{
    public static Achievement[] Achievements = new Achievement[]
{
        new Achievement(AchievementID.ACT_I, "Act I", ""),
        new Achievement(AchievementID.ACT_II, "Act II", ""),
        new Achievement(AchievementID.ONE_MINUTE_FLIGHT, "One Minute Flight", "")
};

    public static AchievementStat[] AchievementStats = new AchievementStat[]
    {
        new AchievementStat(AchievementStatID.FLIGHT_TIME, AchievementStatType.FLOAT, AchievementID.ONE_MINUTE_FLIGHT)
    };

    private void LateUpdate()
    {
        foreach (Achievement achievement in Achievements)
        {
            if (achievement.Achieved)
                continue;

            switch (achievement.AchievementID)
            {
                case AchievementID.ONE_MINUTE_FLIGHT:
                    float time;
                    if (AchievementManager.TryGetStat(AchievementStatID.FLIGHT_TIME, out time))
                    {
                        if (time >= 60f)
                        {
                            AchievementManager.TryUnlockAchievement(AchievementID.ONE_MINUTE_FLIGHT);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
