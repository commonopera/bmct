using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AchievementID
{
    ACT_I,
    ONE_MINUTE_FLIGHT
}

public enum AchievementStatID
{
    FLIGHT_TIME
}

public class AchievementsAndStats : MonoBehaviour
{
    public static Achievement[] Achievements = new Achievement[]
{
        new Achievement(AchievementID.ACT_I, "Act I", ""),
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
                    if(AchievementManager.TryGetStat(AchievementStatID.FLIGHT_TIME, out time))
                    {
                        if(time >= 60f)
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
