using Steamworks;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
public class AchievementManager : MonoBehaviour
{
    private static AchievementManager m_Instance;
    private static AchievementManager Instance
    {
        get
        {
            if(m_Instance == null)
                m_Instance = FindObjectOfType<AchievementManager>();
            return m_Instance;
        }
        set => m_Instance = value;
    }

    private Dictionary<AchievementID, Achievement> m_AchievementDictionary;
    private Dictionary<AchievementID, Achievement> AchievementDictionary
    {
        get
        {
            if (m_AchievementDictionary == null)
            {
                m_AchievementDictionary = new Dictionary<AchievementID, Achievement>();
                foreach(Achievement achievement in AchievementsAndStats.Achievements)
                {
                    if(!m_AchievementDictionary.ContainsKey(achievement.AchievementID))
                        m_AchievementDictionary.Add(achievement.AchievementID, achievement);
                }
            }
            return m_AchievementDictionary;
        }

        set => m_AchievementDictionary = value;
    }

    private Dictionary<AchievementStatID, AchievementStat> m_AchievementStatDictionary;
    private Dictionary<AchievementStatID, AchievementStat> AchievementStatDictionary
    {
        get
        {
            if(m_AchievementStatDictionary == null)
            {
                m_AchievementStatDictionary = new Dictionary<AchievementStatID, AchievementStat>();
                foreach(AchievementStat achievementStat in AchievementsAndStats.AchievementStats)
                {
                    if(!m_AchievementStatDictionary.ContainsKey(achievementStat.StatID))
                        m_AchievementStatDictionary.Add(achievementStat.StatID, achievementStat);
                }
            }
            return m_AchievementStatDictionary;
        }
        set => m_AchievementStatDictionary= value;
    }

    private SteamAchievements m_SteamAchievements;
    private SteamAchievements SteamAchievements => m_SteamAchievements ?? (m_SteamAchievements = GetComponent<SteamAchievements>());

    float StorageTimer;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        StorageTimer = 0f;
    }

    private void OnDestroy()
    {
        Instance = null;
        AchievementDictionary = null;
        AchievementStatDictionary = null;
    }

    private void Update()
    {
        StorageTimer += Time.deltaTime;
        if(StorageTimer >= 60f)
        {
            StorageTimer = 0f;
            StoreStats();
        }
    }

    public static bool StoreStats()
    {
        return Instance.SteamAchievements.StoreStats();
    }

    public static void TryUnlockAchievement(AchievementID achievementID)
    {
        Achievement ach;
        if (Instance.AchievementDictionary.TryGetValue(achievementID, out ach))
        {
            if(!ach.Achieved)
                Instance.SteamAchievements.UnlockAchievement(ach);
        }
    }

    public static void TryIncrementStat(AchievementStatID statID, bool storeStats = false)
    {
        AchievementStat stat;
        if (Instance.AchievementStatDictionary.TryGetValue(statID, out stat))
        {
            switch(stat.StatType)
            {
                case AchievementStatType.INT:
                    TryIncrementStat(statID, 1, storeStats);
                    break;

                case AchievementStatType.FLOAT:
                    TryIncrementStat(statID, 1f, storeStats);
                    break;
            }
        }
    }

    public static void TryIncrementStat(AchievementStatID statID, int amount, bool storeStats = false)
    {
        AchievementStat stat;
        if(Instance.AchievementStatDictionary.TryGetValue(statID, out stat))
        {
            stat.IncrementStat(amount);
            Instance.UpdateStat(statID, storeStats);
        }
    }

    public static void TryIncrementStat(AchievementStatID statID, float amount, bool storeStats = false)
    {
        AchievementStat stat;
        if (Instance.AchievementStatDictionary.TryGetValue(statID, out stat))
        {
            stat.IncrementStat(amount);
            Instance.UpdateStat(statID, storeStats);
        }
    }

    public static void TrySetStat(AchievementStatID statID, int amount, bool storeStats = false)
    {
        AchievementStat stat;
        if (Instance.AchievementStatDictionary.TryGetValue(statID, out stat))
        {
            stat.SetStat(amount);
            Instance.UpdateStat(statID, storeStats);
        }
    }

    public static void TrySetStat(AchievementStatID statID, float amount, bool storeStats = false)
    {
        AchievementStat stat;
        if (Instance.AchievementStatDictionary.TryGetValue(statID, out stat))
        {
            stat.SetStat(amount);
            Instance.UpdateStat(statID, storeStats);
        }
    }

    void UpdateStat(AchievementStatID statID, bool storeStats = false)
    {
        AchievementStat stat;
        if (Instance.AchievementStatDictionary.TryGetValue(statID, out stat))
        {
            switch(stat.StatType)
            {
                case AchievementStatType.INT:
                    SteamAchievements.SetStat(statID, stat.IntValue);
                    break;

                case AchievementStatType.FLOAT:
                    SteamAchievements.SetStat(statID, stat.FloatValue);
                    break;
            }

            if (storeStats)
            {
                if(StoreStats())
                {
                    UpdateAchievementProgress(statID);
                }
            }
        }
    }

    void UpdateAchievementProgress(AchievementStatID statID)
    {
        AchievementStat stat;
        if (Instance.AchievementStatDictionary.TryGetValue(statID, out stat))
        {
            foreach(AchievementID achievementID in stat.Achievements)
            {
                switch(stat.StatType)
                {
                    case AchievementStatType.INT:
                        int minInt, maxInt;
                        if(GetAchievementProgressLimits(achievementID, out minInt, out maxInt))
                        {
                            SteamUserStats.IndicateAchievementProgress(achievementID.ToString(), (uint)minInt, (uint)maxInt);
                        }
                        break;

                    case AchievementStatType.FLOAT:
                        int minFloat, maxFloat;
                        if (GetAchievementProgressLimits(achievementID, out minFloat, out maxFloat))
                        {
                            SteamUserStats.IndicateAchievementProgress(achievementID.ToString(), (uint)minFloat, (uint)maxFloat);
                        }
                        break;
                }
            }
        }
    }

    public static bool GetAchievementProgressLimits(AchievementID achievementID, out int minValue, out int maxValue)
    {
        return Instance.SteamAchievements.GetAchievementProgressLimits(achievementID.ToString(), out minValue, out maxValue);
    }

    public static bool GetAchievementProgressLimits(AchievementID achievementID, out float minValue, out float maxValue)
    {
        return Instance.SteamAchievements.GetAchievementProgressLimits(achievementID.ToString(), out minValue, out maxValue);
    }

    public static bool TryGetStat(AchievementStatID statID, out float value)
    {
        value = 0f;
        AchievementStat stat;
        if(Instance.AchievementStatDictionary.TryGetValue(statID, out stat))
        {
            switch(stat.StatType)
            {
                case AchievementStatType.INT:
                    value = stat.IntValue;
                    break;

                case AchievementStatType.FLOAT:
                    value = stat.FloatValue;
                    break;
            }
            return true;
        }
        return false;
    }

    public static bool TryGetStat(AchievementStatID statID, out int value)
    {
        value = 0;
        AchievementStat stat;
        if (Instance.AchievementStatDictionary.TryGetValue(statID, out stat))
        {
            switch (stat.StatType)
            {
                case AchievementStatType.INT:
                    value = stat.IntValue;
                    break;

                case AchievementStatType.FLOAT:
                    value = (int)stat.FloatValue;
                    break;
            }
            return true;
        }
        return false;
    }
}
