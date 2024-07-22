using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AchievementID
{
    ACT_I = 0,
    ACT_II = 1,
    ACT_III = 3,
    ONE_MINUTE_FLIGHT = 2,
    ONE_HOUR_FLIGHT = 4,
    TEN_HOUR_FLIGHT = 5,
    TRUE_CARTOGRAPHER = 15,
    WELL_READ = 16,

    LADY_OF_THE_LAKE = 6,
    FORBIDDEN_GEM = 7,
    ODD_CUBE = 8,
    COLD_FATE = 9,
    STONY_GAZE = 10,
    RING_OF_WATER = 11,
    SUN_MENTOR = 12,
    GLASSLANDS = 13,
    SOLITUDE = 14,
}

public enum AchievementStatID
{
    FLIGHT_TIME = 0
}

public class AchievementsAndStats : MonoBehaviour
{
    public static Achievement[] Achievements = new Achievement[]
{
        new Achievement(AchievementID.ACT_I, "Vol I", ""),
        new Achievement(AchievementID.ACT_II, "Vol II", ""),
        new Achievement(AchievementID.ACT_III, "Vol III", ""),
        new Achievement(AchievementID.ONE_MINUTE_FLIGHT, "Feeble Grip", ""),
        new Achievement(AchievementID.ONE_HOUR_FLIGHT, "Steady Grip", ""),
        new Achievement(AchievementID.ONE_HOUR_FLIGHT, "Iron Grip", ""),
        new Achievement(AchievementID.LADY_OF_THE_LAKE, "Lady Of The Lake", ""),
        new Achievement(AchievementID.FORBIDDEN_GEM, "Forbidden Gem", ""),
        new Achievement(AchievementID.ODD_CUBE, "Odd Cube", ""),
        new Achievement(AchievementID.COLD_FATE, "Cold Fate", ""),
        new Achievement(AchievementID.STONY_GAZE, "Stony Gaze", ""),
        new Achievement(AchievementID.RING_OF_WATER, "Ring Of Water", ""),
        new Achievement(AchievementID.SUN_MENTOR, "Sun Mentor", ""),
        new Achievement(AchievementID.GLASSLANDS, "Glasslands", ""),
        new Achievement(AchievementID.SOLITUDE, "Solitude", ""),
        new Achievement(AchievementID.TRUE_CARTOGRAPHER, "True Cartographer", ""),
        new Achievement(AchievementID.WELL_READ, "Well-Read", ""),
};

    public static AchievementStat[] AchievementStats = new AchievementStat[]
    {
        new AchievementStat(AchievementStatID.FLIGHT_TIME, AchievementStatType.FLOAT, AchievementID.ONE_MINUTE_FLIGHT, AchievementID.ONE_HOUR_FLIGHT, AchievementID.TEN_HOUR_FLIGHT)
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

                case AchievementID.ONE_HOUR_FLIGHT:
                    time = 0f;
                    if (AchievementManager.TryGetStat(AchievementStatID.FLIGHT_TIME, out time))
                    {
                        if (time >= 3600f)
                        {
                            AchievementManager.TryUnlockAchievement(AchievementID.ONE_HOUR_FLIGHT);
                        }
                    }
                    break;

                case AchievementID.TEN_HOUR_FLIGHT:
                    time = 0f;
                    if (AchievementManager.TryGetStat(AchievementStatID.FLIGHT_TIME, out time))
                    {
                        if (time >= 36000f)
                        {
                            AchievementManager.TryUnlockAchievement(AchievementID.TEN_HOUR_FLIGHT);
                        }
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
