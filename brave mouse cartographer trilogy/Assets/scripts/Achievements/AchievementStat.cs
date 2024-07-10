using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AchievementStatType
{
    INT,
    FLOAT,
    AVG_RATE
}

public class AchievementStat
{
    public AchievementStatID StatID { get; private set; }
    public AchievementStatType StatType { get; private set; }
    public float FloatValue { get; private set; }
    public int IntValue { get; private set; }
    public float SessionCount { get; private set; }
    public double SessionTime { get; private set; }
    public float AveragedValue { get; private set; }
    public AchievementID[] Achievements { get; private set; }

    public AchievementStat(AchievementStatID statID, AchievementStatType statType, params AchievementID[] achievements)
    {
        StatID = statID;
        StatType = statType;
        Achievements = achievements;
    }

    public void IncrementStat(int value)
    {
        switch(StatType)
        {
            case AchievementStatType.INT:
                SetStat(IntValue + value);
                break;

            case AchievementStatType.FLOAT:
                SetStat(FloatValue + value);
                break;
        }
    }

    public void IncrementStat(float value)
    {
        switch (StatType)
        {
            case AchievementStatType.INT:
                SetStat(IntValue + value);
                break;

            case AchievementStatType.FLOAT:
                SetStat(FloatValue + value);
                break;
        }
    }

    public void SetStat(float value)
    {
        switch (StatType)
        {
            case AchievementStatType.INT:
                IntValue = (int)value;
                break;

            case AchievementStatType.FLOAT:
                FloatValue = value;
                break;
        }
    }

    public void SetStat(int value)
    {
        switch (StatType)
        {
            case AchievementStatType.INT:
                IntValue = value;
                break;

            case AchievementStatType.FLOAT:
                FloatValue = value;
                break;
        }
    }
}
